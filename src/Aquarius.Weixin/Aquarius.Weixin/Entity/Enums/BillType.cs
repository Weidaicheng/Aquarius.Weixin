namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 账单类型
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// 返回当日所有订单信息，默认值
        /// </summary>
        ALL,

        /// <summary>
        /// 返回当日成功支付的订单
        /// </summary>
        SUCCESS,

        /// <summary>
        /// 返回当日退款订单
        /// </summary>
        REFUND,

        /// <summary>
        /// 返回当日充值退款订单（相比其他对账单多一栏“返还手续费”）
        /// </summary>
        RECHARGE_REFUND
    }
}
