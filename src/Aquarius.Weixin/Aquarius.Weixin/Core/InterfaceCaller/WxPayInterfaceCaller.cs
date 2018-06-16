using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.Pay;
using Aquarius.Weixin.Utility;
using WxPayError = Aquarius.Weixin.Entity.Pay.Error;

namespace Aquarius.Weixin.Core.InterfaceCaller
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

        public WxPayInterfaceCaller(BaseSettings baseSettings)
        {
            _restClient = new RestClient(WeixinUri);
            _baseSettings = baseSettings;
        }
        #endregion

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="unifiedOrder"></param>
        /// <returns></returns>
        public UnifiedOrderResult UnifiedOrder(UnifiedOrder unifiedOrder)
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

        /// <summary>
        /// 下载对账单
        /// </summary>
        /// <param name="downloadBill"></param>
        /// <returns></returns>
        public DownloadBillResult DownloadBill(DownloadBill downloadBill)
        {
            //转换xml
            string xml = UtilityHelper.Obj2Xml(downloadBill);

            IRestRequest request = new RestRequest("pay/downloadbill ", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("<return_code>"))
            {
                //失败
                var content = response.Content.Replace("<xml>", $"<{typeof(WxPayError).Name}>").Replace("</xml>", $"</{typeof(WxPayError).Name}>");
                using (StreamReader r = new StreamReader(content))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(WxPayError));
                    var result = serializer.Deserialize(r) as WxPayError;

                    throw new WeixinInterfaceException(result.return_msg); 
                }
            }
            else
            {
                //成功
                DownloadBillResult downloadBillResult = new DownloadBillResult()
                {
                    DownloadBillResultDetails = new List<DownloadBillResultDetail>(),
                    DownloadBillResultStatistics = new DownloadBillResultStatistics()
                };

                using (StringReader r = new StringReader(response.Content))
                {
                    var lines = new List<string>();
                    string line;
                    while((line = r.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }

                    //第0行为标题，第1~(Count-3)行为订单数据，第(Count-2)行为统计标题，第(Count-1)行为统计数据
                    //订单数据
                    switch (downloadBill.bill_type)
                    {
                        #region 当日所有订单 
                        case BillType.ALL:
                            for (int i = 1; i < lines.Count - 2; i++)
                            {
                                var details = lines[i].Split(',').Select(x => x.TrimStart('`')).ToArray();
                                downloadBillResult.DownloadBillResultDetails.Add(new DownloadBillResultDetail()
                                {
                                    TradeTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[0]))
                                            return null;
                                        return DateTime.Parse(details[0]);
                                    }).Invoke(),
                                    AppId = details[1],
                                    MchId = details[2],
                                    SubMchId = details[3],
                                    DeviceInfo = details[4],
                                    TransactionId = details[5],
                                    OutTradeNo = details[6],
                                    OpenId = details[7],
                                    TradeType = new Func<TradeType?>(() => 
                                    {
                                        if (string.IsNullOrEmpty(details[8]))
                                            return null;
                                        return (TradeType)Enum.Parse(typeof(TradeType), details[8]);
                                    }).Invoke(),
                                    TradeState = new Func<TradeState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[9]))
                                            return null;
                                        return (TradeState)Enum.Parse(typeof(TradeState), details[9]);
                                    }).Invoke(),
                                    BankType = new Func<BankType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[10]))
                                            return null;
                                        return (BankType)Enum.Parse(typeof(BankType), details[10]);
                                    }).Invoke(),
                                    FeeType = new Func<FeeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[11]))
                                            return FeeType.CNY;
                                        return (FeeType)Enum.Parse(typeof(FeeType), details[11]);
                                    }).Invoke(),
                                    TotalFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[12]))
                                            return null;
                                        return decimal.Parse(details[12]);
                                    }).Invoke(),
                                    CouponFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[13]))
                                            return null;
                                        return decimal.Parse(details[13]);
                                    }).Invoke(),
                                    RefundId = details[14],
                                    OutRefundNo = details[15],
                                    RefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[16]))
                                            return null;
                                        return decimal.Parse(details[16]);
                                    }).Invoke(),
                                    CouponRefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[17]))
                                            return null;
                                        return decimal.Parse(details[17]);
                                    }).Invoke(),
                                    RefundType = details[18],
                                    RefundState = new Func<RefundState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[19]))
                                            return null;
                                        return (RefundState)Enum.Parse(typeof(RefundState), details[19]);
                                    }).Invoke(),
                                    Body = details[20],
                                    Attach = details[21],
                                    ServiceCharge = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[22]))
                                            return null;
                                        return decimal.Parse(details[22]);
                                    }).Invoke(),
                                    Tariff = details[23]
                                });
                            }
                            break;
                        #endregion
                        #region 当日成功支付的订单 
                        case BillType.SUCCESS:
                            for (int i = 1; i < lines.Count - 2; i++)
                            {
                                var details = lines[i].Split(',').Select(x => x.TrimStart('`')).ToArray();
                                downloadBillResult.DownloadBillResultDetails.Add(new DownloadBillResultDetail()
                                {
                                    TradeTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[0]))
                                            return null;
                                        return DateTime.Parse(details[0]);
                                    }).Invoke(),
                                    AppId = details[1],
                                    MchId = details[2],
                                    SubMchId = details[3],
                                    DeviceInfo = details[4],
                                    TransactionId = details[5],
                                    OutTradeNo = details[6],
                                    OpenId = details[7],
                                    TradeType = new Func<TradeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[8]))
                                            return null;
                                        return (TradeType)Enum.Parse(typeof(TradeType), details[8]);
                                    }).Invoke(),
                                    TradeState = new Func<TradeState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[9]))
                                            return null;
                                        return (TradeState)Enum.Parse(typeof(TradeState), details[9]);
                                    }).Invoke(),
                                    BankType = new Func<BankType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[10]))
                                            return null;
                                        return (BankType)Enum.Parse(typeof(BankType), details[10]);
                                    }).Invoke(),
                                    FeeType = new Func<FeeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[11]))
                                            return FeeType.CNY;
                                        return (FeeType)Enum.Parse(typeof(FeeType), details[11]);
                                    }).Invoke(),
                                    TotalFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[12]))
                                            return null;
                                        return decimal.Parse(details[12]);
                                    }).Invoke(),
                                    CouponFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[13]))
                                            return null;
                                        return decimal.Parse(details[13]);
                                    }).Invoke(),
                                    Body = details[14],
                                    Attach = details[15],
                                    ServiceCharge = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[16]))
                                            return null;
                                        return decimal.Parse(details[16]);
                                    }).Invoke(),
                                    Tariff = details[17]
                                });
                            }
                            break;
                        #endregion
                        #region 当日退款的订单 
                        case BillType.REFUND:
                            for (int i = 1; i < lines.Count - 2; i++)
                            {
                                var details = lines[i].Split(',').Select(x => x.TrimStart('`')).ToArray();
                                downloadBillResult.DownloadBillResultDetails.Add(new DownloadBillResultDetail()
                                {
                                    TradeTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[0]))
                                            return null;
                                        return DateTime.Parse(details[0]);
                                    }).Invoke(),
                                    AppId = details[1],
                                    MchId = details[2],
                                    SubMchId = details[3],
                                    DeviceInfo = details[4],
                                    TransactionId = details[5],
                                    OutTradeNo = details[6],
                                    OpenId = details[7],
                                    TradeType = new Func<TradeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[8]))
                                            return null;
                                        return (TradeType)Enum.Parse(typeof(TradeType), details[8]);
                                    }).Invoke(),
                                    TradeState = new Func<TradeState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[9]))
                                            return null;
                                        return (TradeState)Enum.Parse(typeof(TradeState), details[9]);
                                    }).Invoke(),
                                    BankType = new Func<BankType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[10]))
                                            return null;
                                        return (BankType)Enum.Parse(typeof(BankType), details[10]);
                                    }).Invoke(),
                                    FeeType = new Func<FeeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[11]))
                                            return FeeType.CNY;
                                        return (FeeType)Enum.Parse(typeof(FeeType), details[11]);
                                    }).Invoke(),
                                    TotalFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[12]))
                                            return null;
                                        return decimal.Parse(details[12]);
                                    }).Invoke(),
                                    CouponFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[13]))
                                            return null;
                                        return decimal.Parse(details[13]);
                                    }).Invoke(),
                                    RefundTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[14]))
                                            return null;
                                        return DateTime.Parse(details[14]);
                                    }).Invoke(),
                                    RefundSuccessTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[15]))
                                            return null;
                                        return DateTime.Parse(details[15]);
                                    }).Invoke(),
                                    RefundId = details[16],
                                    OutRefundNo = details[17],
                                    RefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[18]))
                                            return null;
                                        return decimal.Parse(details[18]);
                                    }).Invoke(),
                                    CouponRefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[19]))
                                            return null;
                                        return decimal.Parse(details[19]);
                                    }).Invoke(),
                                    RefundType = details[20],
                                    RefundState = new Func<RefundState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[21]))
                                            return null;
                                        return (RefundState)Enum.Parse(typeof(RefundState), details[21]);
                                    }).Invoke(),
                                    Body = details[22],
                                    Attach = details[23],
                                    ServiceCharge = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[24]))
                                            return null;
                                        return decimal.Parse(details[24]);
                                    }).Invoke(),
                                    Tariff = details[25]
                                });
                            }
                            break;
                        #endregion
                        #region 当日充值退款订单
                        case BillType.RECHARGE_REFUND:
                            for (int i = 1; i < lines.Count - 2; i++)
                            {
                                var details = lines[i].Split(',').Select(x => x.TrimStart('`')).ToArray();
                                downloadBillResult.DownloadBillResultDetails.Add(new DownloadBillResultDetail()
                                {
                                    TradeTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[0]))
                                            return null;
                                        return DateTime.Parse(details[0]);
                                    }).Invoke(),
                                    AppId = details[1],
                                    MchId = details[2],
                                    SubMchId = details[3],
                                    DeviceInfo = details[4],
                                    TransactionId = details[5],
                                    OutTradeNo = details[6],
                                    OpenId = details[7],
                                    TradeType = new Func<TradeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[8]))
                                            return null;
                                        return (TradeType)Enum.Parse(typeof(TradeType), details[8]);
                                    }).Invoke(),
                                    TradeState = new Func<TradeState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[9]))
                                            return null;
                                        return (TradeState)Enum.Parse(typeof(TradeState), details[9]);
                                    }).Invoke(),
                                    BankType = new Func<BankType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[10]))
                                            return null;
                                        return (BankType)Enum.Parse(typeof(BankType), details[10]);
                                    }).Invoke(),
                                    FeeType = new Func<FeeType?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[11]))
                                            return FeeType.CNY;
                                        return (FeeType)Enum.Parse(typeof(FeeType), details[11]);
                                    }).Invoke(),
                                    TotalFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[12]))
                                            return null;
                                        return decimal.Parse(details[12]);
                                    }).Invoke(),
                                    CouponFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[13]))
                                            return null;
                                        return decimal.Parse(details[13]);
                                    }).Invoke(),
                                    RefundTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[14]))
                                            return null;
                                        return DateTime.Parse(details[14]);
                                    }).Invoke(),
                                    RefundSuccessTime = new Func<DateTime?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[15]))
                                            return null;
                                        return DateTime.Parse(details[15]);
                                    }).Invoke(),
                                    RefundId = details[16],
                                    OutRefundNo = details[17],
                                    RefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[18]))
                                            return null;
                                        return decimal.Parse(details[18]);
                                    }).Invoke(),
                                    CouponRefundFee = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[19]))
                                            return null;
                                        return decimal.Parse(details[19]);
                                    }).Invoke(),
                                    RefundType = details[20],
                                    RefundState = new Func<RefundState?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[21]))
                                            return null;
                                        return (RefundState)Enum.Parse(typeof(RefundState), details[21]);
                                    }).Invoke(),
                                    Body = details[22],
                                    Attach = details[23],
                                    ServiceCharge = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[24]))
                                            return null;
                                        return decimal.Parse(details[24]);
                                    }).Invoke(),
                                    Tariff = details[25],
                                    RefundServiceCharge = new Func<decimal?>(() =>
                                    {
                                        if (string.IsNullOrEmpty(details[26]))
                                            return null;
                                        return decimal.Parse(details[26]);
                                    }).Invoke()
                                });
                            }
                            break;
                        #endregion
                        default:
                            break;
                    }
                    
                    //统计数据
                    var statistics = lines[lines.Count - 1].Split(',').Select(x => x.TrimStart('`')).ToArray();
                    downloadBillResult.DownloadBillResultStatistics = new DownloadBillResultStatistics()
                    {
                        TotalTradeCount = int.Parse(statistics[0]),
                        TotalTradeFee = decimal.Parse(statistics[1]),
                        TotalRefundFee = decimal.Parse(statistics[2]),
                        TotalCouponRefundFee = decimal.Parse(statistics[3]),
                        TotalTariffFee = decimal.Parse(statistics[4])
                    };
                }

                return downloadBillResult;
            }
        }

        /// <summary>
        /// 拉取评论
        /// </summary>
        /// <param name="commentQuery"></param>
        /// <returns></returns>
        public CommentQueryResult QueryComment(CommentQuery commentQuery)
        {
            //转换xml
            string xml = UtilityHelper.Obj2Xml(commentQuery);

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

            IRestRequest request = new RestRequest("billcommentsp/batchquerycomment", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            if (response.Content.Contains("<return_code>"))
            {
                //失败
                var content = response.Content.Replace("<xml>", $"<{typeof(WxPayError).Name}>").Replace("</xml>", $"</{typeof(WxPayError).Name}>");
                using (StreamReader r = new StreamReader(content))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(WxPayError));
                    var result = serializer.Deserialize(r) as WxPayError;

                    if(result.return_code != SUCCESS)
                    {
                        //接口失败
                        throw new WeixinInterfaceException(result.return_msg);
                    }
                    else
                    {
                        //业务失败
                        throw new WeixinInterfaceException(result.err_code_des);
                    }
                }
            }
            else
            {
                //成功
                CommentQueryResult commentQueryResult = new CommentQueryResult()
                {
                    CommentDetails = new List<CommentDetail>()
                };

                using (StringReader r = new StringReader(response.Content))
                {
                    var lines = new List<string>();
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }

                    //第0行为offset，第1~(Count-1)行为评论数据
                    //offset
                    commentQueryResult.Offset = int.Parse(lines[0]);

                    //评论详情
                    for (int i = 1; i < lines.Count; i++)
                    {
                        var details = lines[i].Split(',').Select(x => x.TrimStart('`')).ToArray();
                        commentQueryResult.CommentDetails.Add(new CommentDetail()
                        {
                            CommentTime = new Func<DateTime?>(() =>
                            {
                                if (string.IsNullOrEmpty(details[0]))
                                    return null;
                                return DateTime.Parse(details[0]);
                            }).Invoke(),
                            OrderId = details[1],
                            Star = new Func<int?>(() =>
                            {
                                if (string.IsNullOrEmpty(details[2]))
                                    return null;
                                return int.Parse(details[2]);
                            }).Invoke(),
                            CommentDesc = details[3]
                        });
                    }
                }

                return commentQueryResult;
            }
        }
    }
}
