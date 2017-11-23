using System.Text;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 音乐消息
    /// </summary>
    public class MusicMessage : MessageBase, IMessageReply
    {
        public MusicMessage()
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

        public string CreateXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            sb.Append($"<music>");
            sb.Append($"<Title><![CDATA[{Title}]]></Title>");
            sb.Append($"<Description><![CDATA[{Description}]]></Description>");
            sb.Append($"<MusicUrl><![CDATA[{MusicURL}]]></MusicUrl>");
            sb.Append($"<HQMusicUrl><![CDATA[{HQMusicUrl}]]></HQMusicUrl>");
            sb.Append($"<ThumbMediaId><![CDATA[{ThumbMediaId}]]></ThumbMediaId>");
            sb.Append($"</music>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
