using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : NormalMessage, IMessageReceive, IMessageReply
    {
        public ImageMessage()
        {
            MsgType = "image";
        }

        /// <summary>
		/// 图片消息内容
		/// </summary>
		public string MediaId { get; set; }

        public void ConvertEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            ToUserName = dic["ToUserName"];
            FromUserName = dic["FromUserName"];
            CreateTime = long.Parse(dic["CreateTime"]);
            MsgId = long.Parse(dic["MsgId"]);
            MediaId = dic["MediaId"];
        }

        public string CreateXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{MsgType}]]></MsgType>");
            sb.Append($"<Image>");
            sb.Append($"<MediaId><![CDATA[{MediaId}]]></MediaId>");
            sb.Append($"</Image>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
