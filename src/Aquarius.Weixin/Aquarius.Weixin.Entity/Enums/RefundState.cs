namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 退款状态
    /// </summary>
    public enum RefundState
    {
        /// <summary>
        /// 退款成功
        /// </summary>
        SUCCESS,

        /// <summary>
        /// 退款关闭
        /// </summary>
        REFUNDCLOSE,

        /// <summary>
        /// 退款处理中
        /// </summary>
        PROCESSING,

        /// <summary>
        /// 退款异常，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，可前往商户平台（pay.weixin.qq.com）-交易中心，手动处理此笔退款
        /// </summary>
        CHANGE
    }
}
