using System.ComponentModel.DataAnnotations;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 关闭订单
    /// </summary>
    public class CloseOrder
    {
        /// <summary>
        /// 公众账号ID
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
        /// 商户订单号
        /// </summary>
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
        /// 签名类型
        /// </summary>
        [MaxLength(32)]
        public string sign_type { get; set; }
    }
}
