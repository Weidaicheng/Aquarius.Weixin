using System.Collections.Generic;
using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Core.MaintainContainer;
using Weixin.Netcore.Entity;
using Weixin.Netcore.Entity.JsApi;
using Weixin.Netcore.Entity.Pay;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.JsApi
{
    /// <summary>
    /// 配置生成
    /// </summary>
    public class ConfigGenerater
    {
        #region .ctor
        private readonly WxPayInterfaceCaller _wxPayInterfaceCaller;
        private readonly AccessTokenContainer _accessTokenContainer;
        private readonly TicketContainer _ticketContainer;
        private readonly SignatureGenerater _signatureGenerator;
        private readonly BaseSettings _baseSettings;

        public ConfigGenerater(SignatureGenerater signatureGenerater, BaseSettings baseSettings, 
            TicketContainer ticketContainer, AccessTokenContainer accessTokenContainer,
            WxPayInterfaceCaller wxPayInterfaceCaller)
        {
            _signatureGenerator = signatureGenerater;
            _baseSettings = baseSettings;
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
            var ticket = _ticketContainer.GetJsApiTicket(accessToken);
            var signature = _signatureGenerator.GenerateJsApiSignature(ticket, nonceStr, timeStamp, url);

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
            //转换字典
            var dic = UtilityHelper.Obj2Dictionary(order);
            //生成签名
            order.sign = _signatureGenerator.GenerateWxPaySignature(dic, _baseSettings.ApiKey);
            //xml
            string xml = UtilityHelper.Obj2Xml(order);

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
