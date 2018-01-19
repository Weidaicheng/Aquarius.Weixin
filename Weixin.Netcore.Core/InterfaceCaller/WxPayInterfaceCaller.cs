using RestSharp;
using System;
using System.IO;
using System.Xml.Serialization;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Model;
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
        private readonly SignatureGenerater _signatureGenerater;

        #region const
        private const string WeixinUri = "https://api.mch.weixin.qq.com";
        #endregion

        public WxPayInterfaceCaller(IRestClient restClient, BaseSettings baseSettings, SignatureGenerater signatureGenerater)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(WeixinUri);
            _baseSettings = baseSettings;
            _signatureGenerater = signatureGenerater;
        }
        #endregion

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="orderXml">xml</param>
        /// <returns></returns>
        internal WxPayResult UnifiedOrder(string orderXml)
        {
            IRestRequest request = new RestRequest("pay/unifiedorder", Method.POST);
            request.AddHeader("Accept", "application/xml");
            request.Parameters.Clear();
            request.AddParameter("application/xml", orderXml, ParameterType.RequestBody);

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
