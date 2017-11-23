namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : NormalMessage
    {
        public TextMessage()
        {
            MsgType = "text";
        }

        /// <summary>
		/// 文本消息内容
		/// </summary>
		public string Content { get; set; }
    }
}
