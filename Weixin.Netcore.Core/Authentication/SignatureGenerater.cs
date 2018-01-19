using System.Collections.Generic;
using System.Linq;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Authentication
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
        public string GenerateSignature(params string[] strings)
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
        public string GenerateJsApiSignature(string ticket, string noncestr, long timestamp, string url)
        {
            string str = $"jsapi_ticket={ticket}&noncestr={noncestr}&timestamp={timestamp}&url={url}";
            return UtilityHelper.SHA1Encrypt(str).ToLower();
        }

        /// <summary>
        /// 创建微信支付签名
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public string GenerateWxPaySignature(Dictionary<string, string> dic, string apiKey)
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

            return UtilityHelper.MD5Encrypt(stringSign).ToUpper();
        }
        #endregion
    }
}
