namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 普通消息
    /// </summary>
    public abstract class NormalMessage : MessageBase, IMessage
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public long MsgId { get; internal set; }
    }
}
