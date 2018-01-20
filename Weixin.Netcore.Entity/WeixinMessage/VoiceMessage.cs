namespace Weixin.Netcore.Entity.WeixinMessage
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : NormalMessage
    {
        public VoiceMessage()
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
