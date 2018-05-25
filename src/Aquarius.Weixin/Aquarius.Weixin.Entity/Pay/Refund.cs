using System.ComponentModel.DataAnnotations;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 退款
    /// </summary>
    public class Refund
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

        /// <summary>
        /// 微信订单号
        /// </summary>
        [MaxLength(32)]
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [MaxLength(32)]
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户退款单号
        /// 商户系统内部的退款单号，商户系统内部唯一，只能是数字、大小写字母_-|*@ ，同一退款单号多次请求只退一笔
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string out_refund_no { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        [Required]
        public int total_fee { get; set; }

        /// <summary>
        /// 退款总金额，订单总金额，单位为分
        /// </summary>
        [Required]
        public int refund_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        [MaxLength(8)]
        public FeeType refund_fee_type { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        [MaxLength(80)]
        public string refund_desc { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        [MaxLength(30)]
        public RefundAccount? refund_account { get; set; }
    }
}
