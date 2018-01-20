using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model;
using Weixin.Netcore.Model.Enums;
using Weixin.Netcore.Model.Pay;
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

        #region const
        private const string WeixinUri = "https://api.mch.weixin.qq.com";
        private const string SUCCESS = "SUCCESS";
        private const string Y = "Y";
        #endregion

        public WxPayInterfaceCaller(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
        }
        #endregion

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="orderXml">xml</param>
        /// <returns></returns>
        internal UnifiedOrderResult UnifiedOrder(string orderXml)
        {
            IRestRequest request = new RestRequest("pay/unifiedorder", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", orderXml, ParameterType.RequestBody);

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
            if((string.IsNullOrWhiteSpace(orderQuery.transaction_id) && string.IsNullOrWhiteSpace(orderQuery.out_trade_no)) || 
                (!string.IsNullOrEmpty(orderQuery.transaction_id) && !string.IsNullOrEmpty(orderQuery.out_trade_no)))
            {
                throw new ArgumentException("微信订单号和商户订单号必须二选一");
            }

            //转换xml
            string xml = UtilityHelper.Obj2Xml(orderQuery);

            IRestRequest request = new RestRequest("pay/unifiedorder", Method.POST);
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
                    var couponTypes = dic.Where(x => x.Key.Contains("coupon_type"));
                    var couponIds = dic.Where(x => x.Key.Contains("coupon_id"));
                    var couponFees = dic.Where(x => x.Key.Contains("coupon_fee"));
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
    }
}
