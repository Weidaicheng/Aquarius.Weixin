using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Core.MaintainContainer;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.JsApi;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.JsApi
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
        private readonly BaseSettings _baseSettings;

        public ConfigGenerater(BaseSettings baseSettings, 
            TicketContainer ticketContainer, AccessTokenContainer accessTokenContainer,
            WxPayInterfaceCaller wxPayInterfaceCaller)
        {
            _baseSettings = baseSettings;
            _accessTokenContainer = accessTokenContainer;
            _ticketContainer = ticketContainer;
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
            var signature = SignatureGenerater.GenerateJsApiSignature(ticket, nonceStr, timeStamp, url);

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
