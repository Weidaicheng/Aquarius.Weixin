using System.Text;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 语音消息回复
    /// </summary>
    public class VoiceMessageReply : IMessageReply<VoiceMessage>
    {
        public string CreateXml(VoiceMessage entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<xml>");
            sb.Append($"<ToUserName><![CDATA[{entity.ToUserName}]]></ToUserName>");
            sb.Append($"<FromUserName><![CDATA[{entity.FromUserName}]]></FromUserName>");
            sb.Append($"<CreateTime>{entity.CreateTime}</CreateTime>");
            sb.Append($"<MsgType><![CDATA[{entity.MsgType}]]></MsgType>");
            sb.Append($"<Voice>");
            sb.Append($"<MediaId><![CDATA[{entity.MediaId}]]></MediaId>");
            sb.Append($"</Voice>");
            sb.Append($"</xml>");

            return sb.ToString();
        }
    }
}
