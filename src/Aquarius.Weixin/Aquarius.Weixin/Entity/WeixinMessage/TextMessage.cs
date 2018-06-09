namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : NormalMessage, ICanBeUsedToReply
    {
        public TextMessage()
        {
            MsgType = "text";
        }

        public TextMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "text";
        }

        /// <summary>
		/// 文本消息内容
		/// </summary>
		public string Content { get; set; }
    }
}
