using System;
using System.ComponentModel.DataAnnotations;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 拉取评论
    /// </summary>
    public class CommentQuery
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
        [MaxLength(64)]
        public string sign { get; set; }

        /// <summary>
        /// 签名类型，目前仅支持HMAC-SHA256，默认就是HMAC-SHA256
        /// </summary>
        [MaxLength(32)]
        public string sign_type { get; set; }

        /// <summary>
        /// 开始时间，格式为yyyyMMddHHmmss
        /// </summary>
        [Required]
        public string begin_time { get; set; }

        /// <summary>
        /// 结束时间，格式为yyyyMMddHHmmss
        /// </summary>
        [Required]
        [MaxLength(19)]
        public string end_time { get; set; }

        /// <summary>
        /// 位移
        /// 指定从某条记录的下一条开始返回记录。接口调用成功时，会返回本次查询最后一条数据的offset。
        /// 商户需要翻页时，应该把本次调用返回的offset 作为下次调用的入参。
        /// 注意offset是评论数据在微信支付后台保存的索引，未必是连续的 
        /// </summary>
        [Required]
        [MaxLength(19)]
        public UInt64 offset { get; set; }

        /// <summary>
        /// 条数
        /// 一次拉取的条数, 最大值是200，默认是200
        /// </summary>
        public uint? limit { get; set; }
    }
}
