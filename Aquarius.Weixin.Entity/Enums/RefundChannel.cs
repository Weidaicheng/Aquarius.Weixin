namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 退款渠道
    /// </summary>
    public enum RefundChannel
    {
        /// <summary>
        /// 原路退款
        /// </summary>
        ORIGINAL,

        /// <summary>
        /// 退回到余额
        /// </summary>
        BALANCE,

        /// <summary>
        /// 原账户异常退到其他余额账户
        /// </summary>
        OTHER_BALANCE,

        /// <summary>
        /// 原银行卡异常退到其他银行卡
        /// </summary>
        OTHER_BANKCARD
    }
}
