using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Entity;
using Weixin.Netcore.Entity.Enums;
using Weixin.Netcore.Entity.Pay;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.InterfaceCaller
{
    /// <summary>
    /// 微信支付接口调用
    /// </summary>
    public class WxPayInterfaceCaller
    {
        #region .ctor
        private readonly IRestClient _restClient;
        private readonly BaseSettings _baseSettings;

        #region const
        private const string WeixinUri = "https://api.mch.weixin.qq.com";
        private const string SUCCESS = "SUCCESS";
        private const string Y = "Y";
        #endregion

        public WxPayInterfaceCaller(IRestClient restClient, BaseSettings baseSettings)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
            _baseSettings = baseSettings;
        }
        #endregion

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="unifiedOrder"></param>
        /// <returns></returns>
        internal UnifiedOrderResult UnifiedOrder(UnifiedOrder unifiedOrder)
        {
            //转换xml
            string xml = UtilityHelper.Obj2Xml(unifiedOrder);

            IRestRequest request = new RestRequest("pay/unifiedorder", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content.Replace("<xml>", $"<{typeof(UnifiedOrderResult).Name}>").Replace("</xml>", $"</{typeof(UnifiedOrderResult).Name}>");
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UnifiedOrderResult));
                var result = serializer.Deserialize(r) as UnifiedOrderResult;

                if (result.return_code.ToUpper() == SUCCESS && result.result_code.ToUpper() == SUCCESS)
                {
                    return result;
                }
                else
                {
                    if (result.return_code.ToUpper() != SUCCESS)
                    {
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderQuery"></param>
        /// <returns></returns>
        public OrderQueryResult QueryOrder(OrderQuery orderQuery)
        {
            if((string.IsNullOrEmpty(orderQuery.transaction_id) && string.IsNullOrEmpty(orderQuery.out_trade_no)) || 
                (!string.IsNullOrEmpty(orderQuery.transaction_id) && !string.IsNullOrEmpty(orderQuery.out_trade_no)))
            {
                throw new ArgumentException("微信订单号和商户订单号必须二选一");
            }

            //转换xml
            string xml = UtilityHelper.Obj2Xml(orderQuery);

            IRestRequest request = new RestRequest("pay/orderquery", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content
                .Replace("<xml>", $"<{typeof(OrderQueryResult).Name}>").Replace("</xml>", $"</{typeof(OrderQueryResult).Name}>")
                .Replace('$', '_');
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(OrderQueryResult));
                var result = serializer.Deserialize(r) as OrderQueryResult;

                if (result.return_code.ToUpper() == SUCCESS && result.result_code.ToUpper() == SUCCESS)
                {
                    //属性转换
                    if (!string.IsNullOrEmpty(result.is_subscribe))
                        result.IsSubscribe = result.is_subscribe == Y ? true : false;
                    else
                        result.IsSubscribe = null;

                    result.TradeType = (TradeType)Enum.Parse(typeof(TradeType), result.trade_type);
                    result.TradeState = (TradeState)Enum.Parse(typeof(TradeState), result.trade_state);
                    result.BankType = (BankType)Enum.Parse(typeof(BankType), result.bank_type);
                    result.FeeType = string.IsNullOrEmpty(result.fee_type) ? FeeType.CNY : (FeeType)Enum.Parse(typeof(FeeType), result.fee_type);
                    result.CashFeeType = string.IsNullOrEmpty(result.cash_fee_type) ? FeeType.CNY : (FeeType)Enum.Parse(typeof(FeeType), result.cash_fee_type);
                    result.TimeEnd = DateTime.Parse($"{result.time_end.Substring(0, 4)}-{result.time_end.Substring(4, 2)}-{result.time_end.Substring(6, 2)} {result.time_end.Substring(8, 2)}:{result.time_end.Substring(10, 2)}:{result.time_end.Substring(12, 2)}");

                    var dic = UtilityHelper.Xml2Dictionary(content);
                    var couponTypes = dic.Where(x => x.Key.StartsWith("coupon_type"));
                    var couponIds = dic.Where(x => x.Key.StartsWith("coupon_id"));
                    var couponFees = dic.Where(x => x.Key.StartsWith("coupon_fee"));
                    if(couponTypes != null && couponTypes.Count() > 0)
                    {
                        var coupons = new List<Coupon>();
                        foreach(var item in couponTypes)
                        {
                            coupons.Add(new Coupon()
                            {
                                Id = int.Parse(item.Key.Replace("coupon_type__", string.Empty)),
                                CouponId = couponIds.FirstOrDefault(x => x.Key == $"coupon_id__{item.Key.Replace("coupon_type__", string.Empty)}").Value,
                                CouponType = (CouponType)Enum.Parse(typeof(CouponType), item.Value),
                                CouponFee = int.Parse(couponFees.FirstOrDefault(x => x.Key == $"coupon_fee__{item.Key.Replace("coupon_type__", string.Empty)}").Value)
                            });
                        }

                        result.Coupons = coupons;
                    }

                    return result;
                }
                else
                {
                    if (result.return_code.ToUpper() != SUCCESS)
                    {
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="closeOrder"></param>
        /// <returns></returns>
        public CloseOrderResult CloseOrder(CloseOrder closeOrder)
        {
            //转换xml
            string xml = UtilityHelper.Obj2Xml(closeOrder);

            IRestRequest request = new RestRequest("pay/closeorder ", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content.Replace("<xml>", $"<{typeof(CloseOrderResult).Name}>").Replace("</xml>", $"</{typeof(CloseOrderResult).Name}>");
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CloseOrderResult));
                var result = serializer.Deserialize(r) as CloseOrderResult;

                if (result.return_code.ToUpper() == SUCCESS && result.result_code.ToUpper() == SUCCESS)
                {
                    return result;
                }
                else
                {
                    if (result.return_code.ToUpper() != SUCCESS)
                    {
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="refund"></param>
        /// <returns></returns>
        public RefundResult Refund(Refund refund)
        {
            if ((string.IsNullOrEmpty(refund.transaction_id) && string.IsNullOrEmpty(refund.out_trade_no)) ||
                (!string.IsNullOrEmpty(refund.transaction_id) && !string.IsNullOrEmpty(refund.out_trade_no)))
            {
                throw new ArgumentException("微信订单号和商户订单号必须二选一");
            }

            //转换xml
            string xml = UtilityHelper.Obj2Xml(refund);

            #region 证书
            _restClient.RemoteCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                if (errors == SslPolicyErrors.None)
                    return true;
                return false;
            };
            X509Certificate cert = string.IsNullOrEmpty(_baseSettings.CertPass) ? 
                new X509Certificate(_baseSettings.CertRoot) : 
                new X509Certificate(_baseSettings.CertRoot, _baseSettings.CertPass);
            _restClient.ClientCertificates.Add(cert);
            #endregion

            IRestRequest request = new RestRequest("secapi/pay/refund", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content
                .Replace("<xml>", $"<{typeof(RefundResult).Name}>").Replace("</xml>", $"</{typeof(RefundResult).Name}>")
                .Replace('$', '_');
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RefundResult));
                var result = serializer.Deserialize(r) as RefundResult;

                if (result.return_code.ToUpper() == SUCCESS && result.result_code.ToUpper() == SUCCESS)
                {
                    //属性转换
                    result.FeeType = string.IsNullOrEmpty(result.fee_type) ? FeeType.CNY : (FeeType)Enum.Parse(typeof(FeeType), result.fee_type);
                    result.CashFeeType = string.IsNullOrEmpty(result.cash_fee_type) ? FeeType.CNY : (FeeType)Enum.Parse(typeof(FeeType), result.cash_fee_type);

                    var dic = UtilityHelper.Xml2Dictionary(content);
                    var couponTypes = dic.Where(x => x.Key.StartsWith("coupon_type"));
                    var couponIds = dic.Where(x => x.Key.StartsWith("coupon_refund_id"));
                    var couponFees = dic.Where(x => x.Key.StartsWith("coupon_refund_fee"));
                    if (couponTypes != null && couponTypes.Count() > 0)
                    {
                        var coupons = new List<Coupon>();
                        foreach (var item in couponTypes)
                        {
                            coupons.Add(new Coupon()
                            {
                                Id = int.Parse(item.Key.Replace("coupon_type__", string.Empty)),
                                CouponId = couponIds.FirstOrDefault(x => x.Key == $"coupon_refund_id__{item.Key.Replace("coupon_type__", string.Empty)}").Value,
                                CouponType = (CouponType)Enum.Parse(typeof(CouponType), item.Value),
                                CouponFee = int.Parse(couponFees.FirstOrDefault(x => x.Key == $"coupon_refund_fee__{item.Key.Replace("coupon_type__", string.Empty)}").Value)
                            });
                        }

                        result.Coupons = coupons;
                    }

                    return result;
                }
                else
                {
                    if (result.return_code.ToUpper() != SUCCESS)
                    {
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
        }

        /// <summary>
        /// 查询退款
        /// </summary>
        /// <param name="orderQuery"></param>
        /// <returns></returns>
        public RefundQueryResult QueryRefund(RefundQuery orderQuery)
        {
            if ((string.IsNullOrEmpty(orderQuery.transaction_id) && string.IsNullOrEmpty(orderQuery.out_trade_no) && string.IsNullOrEmpty(orderQuery.refund_id) && string.IsNullOrEmpty(orderQuery.out_refund_no)) ||
                (!string.IsNullOrEmpty(orderQuery.transaction_id) && !string.IsNullOrEmpty(orderQuery.out_trade_no) && !string.IsNullOrEmpty(orderQuery.refund_id) && !string.IsNullOrEmpty(orderQuery.out_refund_no)))
            {
                throw new ArgumentException("微信订单号、商户订单号、微信退款单号、商户退款单号必须四选一");
            }

            //转换xml
            string xml = UtilityHelper.Obj2Xml(orderQuery);

            IRestRequest request = new RestRequest("pay/orderquery", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content
                .Replace("<xml>", $"<{typeof(RefundQueryResult).Name}>").Replace("</xml>", $"</{typeof(RefundQueryResult).Name}>")
                .Replace('$', '_');
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RefundQueryResult));
                var result = serializer.Deserialize(r) as RefundQueryResult;

                if (result.return_code.ToUpper() == SUCCESS && result.result_code.ToUpper() == SUCCESS)
                {
                    //属性转换
                    result.FeeType = string.IsNullOrEmpty(result.fee_type) ? FeeType.CNY : (FeeType)Enum.Parse(typeof(FeeType), result.fee_type);

                    var dic = UtilityHelper.Xml2Dictionary(content);
                    //微信退款单号
                    var refunIds = dic.Where(x => x.Key.StartsWith("refund_id"));
                    //商户退款单号
                    var outRefundNos = dic.Where(x => x.Key.StartsWith("out_refund_no"));
                    //退款渠道
                    var refundChannels = dic.Where(x => x.Key.StartsWith("refund_channel"));
                    //申请退款金额
                    var refundFees = dic.Where(x => x.Key.StartsWith("refund_fee"));
                    //退款金额
                    var settlementRefundFees = dic.Where(x => x.Key.StartsWith("settlement_refund_fee"));
                    //退款状态
                    var refundStates = dic.Where(x => x.Key.StartsWith("refund_status"));
                    //退款资金来源
                    var refundAccounts = dic.Where(x => x.Key.StartsWith("refund_account"));
                    //退款入账账户
                    var refundRecvAccounts = dic.Where(x => x.Key.StartsWith("refund_recv_accout"));
                    //退款成功时间
                    var refundSuccessTimes = dic.Where(x => x.Key.StartsWith("refund_success_time"));
                    if (refunIds != null && refunIds.Count() > 0)
                    {
                        var refundDetails = new List<RefundDetail>();
                        foreach (var refund in refunIds)
                        {
                            var refundDetail = new RefundDetail();
                            //Id编号
                            var id_n = int.Parse(refund.Key.Replace("refund_id__", string.Empty));
                            //微信退款单号
                            var refundId = refund.Value;
                            //退款代金券使用量
                            var couponRefundCount = int.Parse(dic.FirstOrDefault(x => x.Key.StartsWith($"coupon_refund_count__{id_n}")).Value ?? "0");
                            //总代金券退款金额
                            var couponRefundFee = int.Parse(dic.FirstOrDefault(x => x.Key.StartsWith($"coupon_refund_fee__{id_n}")).Value ?? "0");
                            //商户退单单号
                            var outRefundNo = outRefundNos.FirstOrDefault(x => x.Key == $"out_refund_no__{id_n}").Value;
                            //退款渠道
                            var refundChannel = (RefundChannel)Enum.Parse(typeof(RefundChannel), refundChannels.FirstOrDefault(x => x.Key == $"refund_channel__{id_n}").Value);
                            //申请退款金额
                            var refundFee = int.Parse(refundFees.FirstOrDefault(x => x.Key == $"refund_fee__{id_n}").Value);
                            //退款金额
                            int? settlementRefundFee = null;
                            if(settlementRefundFees.Count(x => x.Key == $"settlement_refund_fee__{id_n}") > 0)
                            {
                                settlementRefundFee = int.Parse(settlementRefundFees.FirstOrDefault(x => x.Key == $"settlement_refund_fee__{id_n}").Value);
                            }
                            //退款状态
                            var refundState = (RefundState)Enum.Parse(typeof(RefundState), refundStates.FirstOrDefault(x => x.Key == $"refund_status__{id_n}").Value);
                            //退款资金来源
                            var refundAccount = (RefundAccount)Enum.Parse(typeof(RefundAccount), refundStates.FirstOrDefault(x => x.Key == $"refund_account__{id_n}").Value);
                            //退款入账账户
                            var refundRecvAccount = refundRecvAccounts.FirstOrDefault(x => x.Key == $"refund_recv_accout__{id_n}").Value;
                            //退款成功时间
                            DateTime? refundSuccessTime = null;
                            if(refundSuccessTimes.Count(x => x.Key == $"refund_success_time__{id_n}") > 0)
                            {
                                refundSuccessTime = DateTime.Parse(refundSuccessTimes.FirstOrDefault(x => x.Key == $"refund_success_time__{id_n}").Value);
                            }
                            //退款代金券Id
                            var couponRefundIds = dic.Where(x => x.Key.StartsWith($"coupon_refund_id__{id_n}"));
                            //退款代金券类型
                            var couponRefundTypes = dic.Where(x => x.Key.StartsWith($"coupon_type__{id_n}"));
                            //单个代金券退款金额
                            var couponRefundFees = dic.Where(x => x.Key.StartsWith($"coupon_refund_fee__{id_n}"));
                            if(couponRefundIds != null && couponRefundIds.Count() > 0)
                            {
                                var coupons = new List<Coupon>();
                                foreach(var coupon in couponRefundIds)
                                {
                                    //Id编号
                                    var id_m = int.Parse(coupon.Key.Replace($"coupon_refund_id__{id_n}__", string.Empty));
                                    coupons.Add(new Coupon()
                                    {
                                        Id = id_m,
                                        CouponId = coupon.Value,
                                        CouponType = (CouponType)Enum.Parse(typeof(CouponType), couponRefundTypes.FirstOrDefault(x => x.Key == $"coupon_type__{id_n}__{id_m} ").Value),
                                        CouponFee = int.Parse(couponRefundFees.FirstOrDefault(x => x.Key == $"coupon_refund_fee__{id_n}__{id_m}").Value)
                                    });
                                }

                                refundDetail.Coupons = coupons;
                            }

                            refundDetail.Id = id_n;
                            refundDetail.OutRefundNo = outRefundNo;
                            refundDetail.RefundAccount = refundAccount;
                            refundDetail.RefundChannel = refundChannel;
                            refundDetail.RefundFee = refundFee;
                            refundDetail.RefundId = refundId;
                            refundDetail.RefundRecvAccount = refundRecvAccount;
                            refundDetail.RefundState = refundState;
                            refundDetail.RefundSuccessTime = refundSuccessTime;
                            refundDetail.SettlementRefundFee = settlementRefundFee;

                            refundDetails.Add(refundDetail);
                        }

                        result.RefundDetails = refundDetails;
                    }

                    return result;
                }
                else
                {
                    if (result.return_code.ToUpper() != SUCCESS)
                    {
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
        }
    }
}
