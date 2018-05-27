namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        public MessageBase()
        { }

        public MessageBase(MessageBase receivedMsg)
        {
            ToUserName = receivedMsg.FromUserName;
            FromUserName = receivedMsg.ToUserName;
        }

        /// <summary>
		/// 接收到的消息：AppId
        /// 发送出的消息：OpenId
		/// </summary>
		public string ToUserName { get; set; }

        /// <summary>
        /// 接收到的消息：OpenId
        /// 发送出的消息：AppId
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
    }
}
