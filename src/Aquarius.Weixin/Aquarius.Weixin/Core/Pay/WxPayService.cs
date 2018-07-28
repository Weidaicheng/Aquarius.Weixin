using Aquarius.Weixin.Core.Authentication;
using Aquarius.Weixin.Core.InterfaceCaller;
using Aquarius.Weixin.Entity;
using Aquarius.Weixin.Entity.Configuration;
using Aquarius.Weixin.Entity.JsApi;
using Aquarius.Weixin.Entity.Pay;
using Aquarius.Weixin.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if NETCOREAPP1_0 || NETCOREAPP1_1
using System.Net;
#else
using System.Web;
#endif

namespace Aquarius.Weixin.Core.Pay
{
    /// <summary>
    /// 微信支付相关服务
    /// </summary>
    public class WxPayService
    {
        #region .ctor
        private readonly BaseSettings _baseSettings;
        private readonly WxPayInterfaceCaller _wxPayInterfaceCaller;

        public WxPayService(BaseSettings baseSettings, WxPayInterfaceCaller wxPayInterfaceCaller)
        {
            _baseSettings = baseSettings;
            _wxPayInterfaceCaller = wxPayInterfaceCaller;
        }
        #endregion

        /// <summary>
        /// Js-API支付
        /// </summary>
        /// <param name="info"></param>
        /// <param name="signType"></param>
        /// <returns></returns>
        [Obsolete("推荐使用异步方法")]
        public ChooseWxPayConfig JsApiPay(UnifiedOrderInfo info)
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

        /// <summary>
        /// Js-API支付
        /// </summary>
        /// <param name="info"></param>
        /// <param name="signType"></param>
        /// <returns></returns>
        public async Task<ChooseWxPayConfig> JsApiPayAsync(UnifiedOrderInfo info)
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
            var unifiedOrderResult = await _wxPayInterfaceCaller.UnifiedOrderAsync(unifiedOrder);

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

        /// <summary>
        /// H5支付
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Obsolete("推荐使用异步方法")]
        public string H5Pay(UnifiedOrderInfo info)
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

            var mWebUrl = unifiedOrderResult.mweb_url;
            if (!string.IsNullOrEmpty(info.RedirectUrl))
#if NETCOREAPP1_0 || NETCOREAPP1_1
                mWebUrl += $"&redirect_url={WebUtility.UrlEncode(info.RedirectUrl)}";
#else
                mWebUrl += $"&redirect_url={HttpUtility.UrlEncode(info.RedirectUrl)}";
#endif

            return mWebUrl;
        }

        /// <summary>
        /// H5支付
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<string> H5PayAsync(UnifiedOrderInfo info)
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
            var unifiedOrderResult = await _wxPayInterfaceCaller.UnifiedOrderAsync(unifiedOrder);

            var mWebUrl = unifiedOrderResult.mweb_url;
            if (!string.IsNullOrEmpty(info.RedirectUrl))
#if NETCOREAPP1_0 || NETCOREAPP1_1
                mWebUrl += $"&redirect_url={WebUtility.UrlEncode(info.RedirectUrl)}";
#else
                mWebUrl += $"&redirect_url={HttpUtility.UrlEncode(info.RedirectUrl)}";
#endif

            return mWebUrl;
        }
    }
}
