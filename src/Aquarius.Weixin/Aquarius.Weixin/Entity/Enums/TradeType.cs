namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 交易类型
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// 公众号支付
        /// </summary>
        JSAPI,

        /// <summary>
        /// 扫码支付
        /// </summary>
        NATIVE,

        /// <summary>
        /// App支付
        /// </summary>
        APP,

        /// <summary>
        /// H5支付
        /// </summary>
        MWEB
    }
}
