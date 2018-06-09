namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : NormalMessage, ICanBeUsedToReply
    {
        public ImageMessage()
        {
            MsgType = "image";
        }

        public ImageMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "image";
        }

        /// <summary>
		/// 图片消息内容
		/// </summary>
		public string MediaId { get; set; }
    }
}
