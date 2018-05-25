using System.ComponentModel.DataAnnotations;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 订单查询
    /// 微信订单号和商户订单号二选一
    /// </summary>
    public class OrderQuery
    {
        /// <summary>
        /// 公众账号ID （企业号corpid即为此appId） 
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string mch_id { get; set; }

        /// <summary>
        /// 微信的订单号，建议优先使用 
        /// </summary>
        [MaxLength(32)]
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [MaxLength(32)]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名 
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string sign { get; set; }

        /// <summary>
        /// 签名类型，目前支持HMAC-SHA256和MD5，默认为MD5
        /// </summary>
        [MaxLength(32)]
        public string sign_type { get; set; }
    }
}
