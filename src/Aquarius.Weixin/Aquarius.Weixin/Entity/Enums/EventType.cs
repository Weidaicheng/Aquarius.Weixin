namespace Aquarius.Weixin.Entity.Enums
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        Subscribe,

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        Unsubscribe,

        /// <summary>
        /// 扫码事件
        /// </summary>
        Scan,

        /// <summary>
        /// 位置上报事件
        /// </summary>
        Location,

        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        Click,

        /// <summary>
        /// 自定义View菜单事件
        /// </summary>
        View
    }
}
