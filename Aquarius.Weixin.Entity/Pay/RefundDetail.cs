using System;
using System.Collections.Generic;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 退款详情
    /// </summary>
    public class RefundDetail
    {
        /// <summary>
        /// Id编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 退款渠道
        /// </summary>
        public RefundChannel RefundChannel { get; set; }

        /// <summary>
        /// 申请退款金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款金额
        /// 退款金额=申请退款金额-非充值代金券退款金额，退款金额小于等于申请退款金额
        /// </summary>
        public int? SettlementRefundFee { get; set; }

        /// <summary>
        /// 退款代金券使用量
        /// </summary>
        public int? CouponRefundCount { get; set; }

        /// <summary>
        /// 总代金券退款金额
        /// </summary>
        public int? CouponRefundFee { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public List<Coupon> Coupons { get; set; }

        /// <summary>
        /// 退款状态
        /// </summary>
        public RefundState RefundState { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        public RefundAccount RefundAccount { get; set; }

        /// <summary>
        /// 退款入账账户
        /// 取当前退款单的退款入账方 
        ///1）退回银行卡：
        ///{银行名称}{卡类型}{卡尾号}
        ///2）退回支付用户零钱:
        ///支付用户零钱
        ///3）退还商户:
        ///商户基本账户
        ///商户结算银行账户
        ///4）退回支付用户零钱通:
        ///支付用户零钱通
        /// </summary>
        public string RefundRecvAccount { get; set; }

        /// <summary>
        /// 退款成功时间，当退款状态为退款成功时有返回
        /// </summary>
        public DateTime? RefundSuccessTime { get; set; }
    }
}
