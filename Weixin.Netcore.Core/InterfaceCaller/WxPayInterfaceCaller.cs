using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model;
using Weixin.Netcore.Model.Enums;
using Weixin.Netcore.Model.Pay;

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
        private readonly SignatureGenerater _generater;

        #region const
        private const string WeixinUri = "https://api.mch.weixin.qq.com";
        #endregion

        public WxPayInterfaceCaller(IRestClient restClient, BaseSettings baseSettings, SignatureGenerater generater)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
            _baseSettings = baseSettings;
            _generater = generater;
        }
        #endregion

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="nonce">随机字符串</param>
        /// <param name="body">商品描述</param>
        /// <param name="tradeNo">订单号</param>
        /// <param name="totalFee">金额（单位：分）</param>
        /// <param name="clientIp">客户Ip</param>
        /// <param name="notifyUrl">通知Url</param>
        /// <param name="openId">OpenId</param>
        /// todo:实现另一个加密方式
        /// <param name="signType">签名方式（目前只实现MD5）</param>
        /// <param name="feeType">币种（目前只有人民币）</param>
        /// <returns></returns>
        internal WxPayResult UnifiedOrder(string nonce, string body, string tradeNo, int totalFee, string clientIp, string notifyUrl, string openId, string signType = "MD5", string feeType = "CNY")
        {
            #region 拼接xml
            #region 获取签名
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"appid", _baseSettings.AppId },
                {"mch_id", _baseSettings.MchId },
                {"nonce_str", nonce },
                {"body", body },
                {"out_trade_no", tradeNo },
                {"total_fee", totalFee.ToString() },
                {"spbill_create_ip", clientIp },
                {"notify_url", notifyUrl },
                {"openid", openId },
                {"sign_type", signType },
                {"fee_type", feeType },
                {"trade_type", TradeType.JSAPI.ToString() },
            };
            var sign = _generater.GenerateWxPaySignature(dic, _baseSettings.ApiKey);
            #endregion

            string xml = $"<xml>";
            xml += $"<appid>{_baseSettings.AppId}</appid>";
            xml += $"<mch_id>{_baseSettings.MchId}</mch_id>";
            xml += $"<nonce_str>{nonce}</nonce_str>";
            xml += $"<body>{body}</body>";
            xml += $"<out_trade_no>{tradeNo}</out_trade_no>";
            xml += $"<total_fee>{totalFee}</total_fee>";
            xml += $"<spbill_create_ip>{clientIp}</spbill_create_ip>";
            xml += $"<notify_url>{notifyUrl}</notify_url>";
            xml += $"<openid>{openId}</openid>";
            xml += $"<sign_type>MD5</sign_type>";
            xml += $"<fee_type>CNY</fee_type>";
            xml += $"<trade_type>{TradeType.JSAPI.ToString()}</trade_type>";
            xml += $"<sign>{sign}</sign>";
            xml += $"</xml>";
            #endregion

            IRestRequest request = new RestRequest("pay/unifiedorder", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            var content = response.Content.Replace("<xml>", $"<{typeof(WxPayResult).Name}>").Replace("</xml>", $"</{typeof(WxPayResult).Name}>");
            using (StringReader r = new StringReader(content))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WxPayResult));
                var wxpayResult = serializer.Deserialize(r) as WxPayResult;

                if (wxpayResult.return_code.ToUpper() == "SUCCESS" && wxpayResult.result_code.ToUpper() == "SUCCESS")
                {
                    return wxpayResult;
                }
                else
                {
                    if (wxpayResult.return_code.ToUpper() != "SUCCESS")
                    {
                        throw new WeixinInterfaceException(wxpayResult.return_msg);
                    }
                    else
                    {
                        throw new WeixinInterfaceException(wxpayResult.err_code_des);
                    }
                }
            }
        }
    }
}
