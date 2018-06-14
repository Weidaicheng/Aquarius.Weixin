using System.Collections.Generic;
using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Core.MaintainContainer;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.JsApi;
using Aquarius.Weixin.Entity.Pay;
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

        /// <summary>
        /// 创建chooseWxPay配置
        /// </summary>
        /// <param name="info"></param>
        /// <param name="signType"></param>
        /// <returns></returns>
        public ChooseWxPayConfig GenerateChooseWxPayConfig(UnifiedOrderInfo info)
        {
            var unifiedOrder = new UnifiedOrder()
            {
                nonce_str = info.NonceStr,
                appid = _baseSettings.AppId,
                mch_id = _baseSettings.MchId,
                out_trade_no = info.TradeNo,
                sign_type = info.SignType.GetDescription(),
                total_fee = (int)(info.TotalFee * 100),
                openid = info.OpenId,
                notify_url = info.NotifyUrl,
                fee_type = info.FeeType,
                spbill_create_ip = info.DeviceIp,
                time_start = info.StartTime.ToString("yyyyMMddHHmmss"),
                time_expire = info.EndTime.ToString("yyyyMMddHHmmss"),
                attach = info.Attach,
                body = info.Body,
                detail = info.Detail,
                goods_tag = info.GoodTags,
                limit_pay = info.UseLimitPay ? "no_credit" : null,
                trade_type = info.TradeType,
                scene_info = info.SceneInfo
            };
            //转换字典
            var dic = UtilityHelper.Obj2Dictionary(unifiedOrder);
            //生成签名
            unifiedOrder.sign = SignatureGenerater.GenerateWxPaySignature(dic, _baseSettings.ApiKey, info.SignType);
            //统一下单
            var unifiedOrderResult = _wxPayInterfaceCaller.UnifiedOrder(unifiedOrder);

            var chooseWxPayConfig = new ChooseWxPayConfig()
            {
                nonceStr = info.NonceStr,
                timestamp = info.TimeStamp,
                package = unifiedOrderResult.prepay_id,
                signType = info.SignType.GetDescription()
            };
            var paySign = SignatureGenerater.GenerateWxPaySignature(new Dictionary<string, string>()
            {
                {"appId", _baseSettings.AppId },
                {"timeStamp", chooseWxPayConfig.timestamp.ToString() },
                {"nonceStr", chooseWxPayConfig.nonceStr },
                {"package", chooseWxPayConfig.package },
                {"signType", chooseWxPayConfig.signType }
            }, _baseSettings.ApiKey, info.SignType);

            chooseWxPayConfig.paySign = paySign;

            return chooseWxPayConfig;
        }
    }
}
