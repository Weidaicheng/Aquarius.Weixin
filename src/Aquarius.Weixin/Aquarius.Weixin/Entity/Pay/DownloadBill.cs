using System.ComponentModel.DataAnnotations;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 下载对账单
    /// </summary>
    public class DownloadBill
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
        /// 对账单日期
        /// </summary>
        [Required]
        [MaxLength(8)]
        public string bill_date { get; set; }

        /// <summary>
        /// 账单类型
        /// </summary>
        [Required]
        [MaxLength(8)]
        public BillType bill_type { get; set; }

        /// <summary>
        /// 压缩账单
        /// </summary>
        [MaxLength(8)]
        public CompressType? tar_type { get; set; }
    }
}
