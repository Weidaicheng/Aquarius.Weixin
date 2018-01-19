using System.Collections.Generic;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Core.MaintainContainer;
using Weixin.Netcore.Model;
using Weixin.Netcore.Model.JsApi;
using Weixin.Netcore.Model.Pay;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.JsApi
{
    /// <summary>
    /// 配置生成
    /// </summary>
    public class ConfigGenerater
    {
        #region .ctor
        private readonly TicketInterfaceCaller _ticketInterfaceCaller;
        private readonly WxPayInterfaceCaller _wxPayInterfaceCaller;
        private readonly AccessTokenContainer _accessTokenContainer;
        private readonly SignatureGenerater _signatureGenerator;
        private readonly BaseSettings _baseSettings;

        public ConfigGenerater(SignatureGenerater signatureGenerater, BaseSettings baseSettings, 
            TicketInterfaceCaller ticketInterfaceCaller, AccessTokenContainer accessTokenContainer,
            WxPayInterfaceCaller wxPayInterfaceCaller)
        {
            _signatureGenerator = signatureGenerater;
            _baseSettings = baseSettings;
            _ticketInterfaceCaller = ticketInterfaceCaller;
            _accessTokenContainer = accessTokenContainer;
            _wxPayInterfaceCaller = wxPayInterfaceCaller;
        }
        #endregion

        /// <summary>
        /// 创建JS-API配置
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsApis"></param>
        /// <returns></returns>
        public JsApiConfig GenerateJsApiConfig(string url, params string[] jsApis)
        {
            var timeStamp = UtilityHelper.GetTimeStamp();
            var nonceStr = UtilityHelper.GenerateNonce();
            var accessToken = _accessTokenContainer.GetAccessToken();
            var ticket = _ticketInterfaceCaller.GetJsApiTicket(accessToken);
            var signature = _signatureGenerator.GenerateJsApiSignature(ticket.ticket, nonceStr, timeStamp, url);

            return new JsApiConfig()
            {
                debug = _baseSettings.Debug,
                appId = _baseSettings.AppId,
                timestamp = timeStamp,
                nonceStr = nonceStr,
                signature = signature,
                jsApiList = jsApis
            };
        }

        /// <summary>
        /// 创建chooseWxPay配置
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ChooseWxPayConfig GenerateChooseWxPayConfig(UnifiedOrder order)
        {
            #region 拼接xml
            //类型
            var type = typeof(UnifiedOrder);
            //所有属性
            var properties = order.GetType().GetProperties();

            //xml
            string xml = $"<xml>";
            //字典
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //读取所有属性值
            foreach(var item in properties)
            {
                var name = item.Name;
                var value = (type.GetProperty(name).GetValue(order) ?? "").ToString();

                if (!string.IsNullOrEmpty(value))
                {
                    dic.Add(name, value);
                    xml += $"<{name}>{value}</{name}>";
                }
            }

            //获取签名
            var sign = _signatureGenerator.GenerateWxPaySignature(dic, _baseSettings.ApiKey);

            //拼接签名
            xml += $"<sign>{sign}</sign>";
            xml += $"</xml>";
            #endregion

            var wxPayResult = _wxPayInterfaceCaller.UnifiedOrder(xml);

            var nonceStr = UtilityHelper.GenerateNonce();
            var timeStamp = UtilityHelper.GetTimeStamp();
            var chooseWxPayConfig = new ChooseWxPayConfig()
            {
                nonceStr = nonceStr,
                timestamp = timeStamp,
                package = wxPayResult.prepay_id,
                signType = "MD5"
            };
            var paySign = _signatureGenerator.GenerateWxPaySignature(new Dictionary<string, string>()
            {
                {"appId", _baseSettings.AppId },
                {"timeStamp", chooseWxPayConfig.timestamp.ToString() },
                {"nonceStr", chooseWxPayConfig.nonceStr },
                {"package", chooseWxPayConfig.package },
                {"signType", chooseWxPayConfig.signType }
            }, _baseSettings.ApiKey);

            chooseWxPayConfig.paySign = paySign;

            return chooseWxPayConfig;
        }
    }
}
