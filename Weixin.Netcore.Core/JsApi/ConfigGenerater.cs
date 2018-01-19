using Weixin.Netcore.Core.Authentication;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Core.MaintainContainer;
using Weixin.Netcore.Model;
using Weixin.Netcore.Model.JsApi;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.JsApi
{
    /// <summary>
    /// 配置生成
    /// </summary>
    public class ConfigGenerater
    {
        #region .ctor
        private readonly SignatureGenerater _signatureGenerator;
        private readonly BaseSettings _baseSettings;
        private readonly TicketInterfaceCaller _ticketInterfaceCaller;
        private readonly AccessTokenContainer _accessTokenContainer;
        private readonly WxPayInterfaceCaller _wxPayInterfaceCaller;

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
    }
}
