using System.ComponentModel;

namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 微信支付签名类型
    /// </summary>
    public enum WxPaySignType
    {
        /// <summary>
        /// MD5
        /// </summary>
        [Description("MD5")]
        MD5,

        /// <summary>
        /// SHA256
        /// </summary>
        [Description("HMAC-SHA256")]
        SHA256,
    }
}
