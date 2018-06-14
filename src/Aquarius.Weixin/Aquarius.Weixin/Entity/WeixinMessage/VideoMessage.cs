namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : NormalMessage, ICanBeUsedToReply
    {
        public VideoMessage()
        {
            MsgType = "video";
        }

        public VideoMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "video";
        }

        /// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 素材Id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图素材Id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
