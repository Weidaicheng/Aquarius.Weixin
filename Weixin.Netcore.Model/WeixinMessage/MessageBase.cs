namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public class MessageBase
    {
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
        public string MsgType { get; protected set; }
    }
}
