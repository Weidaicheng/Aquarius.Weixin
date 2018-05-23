namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 退款资金来源
    /// </summary>
    public enum RefundAccount
    {
        /// <summary>
        /// 未结算资金退款（默认使用未结算资金退款）
        /// </summary>
        REFUND_SOURCE_UNSETTLED_FUNDS,

        /// <summary>
        /// 可用余额退款
        /// </summary>
        REFUND_SOURCE_RECHARGE_FUNDS
    }
}
