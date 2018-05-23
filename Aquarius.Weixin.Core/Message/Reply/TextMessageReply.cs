using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 文本消息回复
    /// </summary>
    public class TextMessageReply : IMessageReply<TextMessage>
    {
        public string CreateXml(TextMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<Content><![CDATA[{entity.Content}]]></Content>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
