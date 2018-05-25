using System.ComponentModel.DataAnnotations;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 退款查询
    /// </summary>
    public class RefundQuery
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

        #region 四选一
        /// <summary>
        /// 微信订单号
        /// 微信订单号查询的优先级是： refund_id > out_refund_no > transaction_id > out_trade_no
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
        /// </summary>
        [MaxLength(64)]
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        [MaxLength(32)]
        public string refund_id { get; set; }
        #endregion

        /// <summary>
        /// 偏移量，当部分退款次数超过10次时可使用，表示返回的查询结果从这个偏移量开始取记录 
        /// </summary>
        public int? offset { get; set; }
    }
}
