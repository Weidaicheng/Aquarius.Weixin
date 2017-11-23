using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : NormalMessage, IMessageReceive, IMessageReply
    {
        public VideoMessage()
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

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            MediaId = dic["MediaId"];
            ThumbMediaId = dic["ThumbMediaId"];
        }

        public string CreateXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            sb.Append($"<Video>");
            sb.Append($"<MediaId><![CDATA[{MediaId}]]></MediaId>");
            sb.Append($"<Title><![CDATA[{Title}]]></Title> ");
            sb.Append($"<Description><![CDATA[{Description}]]></Description>");
            sb.Append($"</Video>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
