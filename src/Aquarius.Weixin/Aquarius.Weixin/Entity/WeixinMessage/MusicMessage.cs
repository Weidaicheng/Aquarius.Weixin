namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 音乐消息
    /// </summary>
    public class MusicMessage : MessageBase, ICanBeUsedToReply
    {
        public MusicMessage()
        {
            MsgType = "music";
        }

        public MusicMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "music";
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
        /// 链接
        /// </summary>
        public string MusicURL { get; set; }

        /// <summary>
        /// 高质量链接
        /// </summary>
        public string HQMusicUrl { get; set; }

        /// <summary>
        /// 缩略图媒体Id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
