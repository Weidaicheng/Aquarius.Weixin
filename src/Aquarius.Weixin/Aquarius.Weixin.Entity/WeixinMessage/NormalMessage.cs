namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 普通消息
    /// </summary>
    public abstract class NormalMessage : MessageBase
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public long MsgId { get; set; }
    }
}
