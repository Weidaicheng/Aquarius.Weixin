namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 普通消息
    /// </summary>
    public class NormalMessage : MessageBase
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public long MsgId { get; internal set; }
    }
}
