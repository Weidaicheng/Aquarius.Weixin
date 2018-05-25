using System.Collections.Generic;
using System.Linq;
using Aquarius.Weixin.Core.Exceptions;
using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Authentication
{
    /// <summary>
    /// 签名生成
    /// </summary>
    public class SignatureGenerater
    {
        #region 签名
        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="strings">生成签名的字符串</param>
        /// <returns></returns>
        public static string GenerateSignature(params string[] strings)
        {
            var arr = strings.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);

            return UtilityHelper.SHA1Encrypt(arrString).ToLower();
        }

        /// <summary>
        /// 创建JS-API签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerateJsApiSignature(string ticket, string noncestr, long timestamp, string url)
        {
            string str = $"jsapi_ticket={ticket}&noncestr={noncestr}&timestamp={timestamp}&url={url}";
            return UtilityHelper.SHA1Encrypt(str).ToLower();
        }

        /// <summary>
        /// 创建微信支付签名
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="apiKey"></param>
        /// <param name="signType"></param>
        /// <returns></returns>
        public static string GenerateWxPaySignature(Dictionary<string, string> dic, string apiKey, WxPaySignType signType)
        {
            var arr = dic.OrderBy(z => z.Key).ToArray();
            string stringSign = string.Empty;

            foreach (var item in arr)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    stringSign += $"{item.Key}={item.Value}&";
                }
            }
            stringSign += $"key={apiKey}";

            string sign = string.Empty;
            switch(signType)
            {
                case WxPaySignType.MD5:
                    sign = UtilityHelper.MD5Encrypt(stringSign).ToUpper();
                    break;
                case WxPaySignType.SHA256:
                    sign = UtilityHelper.SHA256Encrypt(stringSign, apiKey).ToUpper();
                    break;
                default:
                    throw new SignTypeNotSupportException($"{signType.ToString()}不受支持");
            }

            return sign;
        }
        #endregion
    }
}
