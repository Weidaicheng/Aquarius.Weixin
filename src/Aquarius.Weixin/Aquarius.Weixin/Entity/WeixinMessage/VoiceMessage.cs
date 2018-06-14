namespace Aquarius.Weixin.Entity.WeixinMessage
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : NormalMessage, ICanBeUsedToReply
    {
        public VoiceMessage()
        {
            MsgType = "voice";
        }

        public VoiceMessage(MessageBase receivedMsg) : base(receivedMsg)
        {
            MsgType = "voice";
        }

        /// <summary>
		/// 素材Id
		/// </summary>
		public string MediaId { get; set; }

        /// <summary>
        /// 语音格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别
        /// </summary>
        public string Recognition { get; set; }
    }
}
