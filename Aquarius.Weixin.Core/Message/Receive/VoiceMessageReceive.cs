using System.Collections.Generic;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 语音消息接收
    /// </summary>
    public class VoiceMessageReceive : IMessageReceive//<VoiceMessage>
    {
        public IMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            return new VoiceMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                Format = dic["Format"],
                Recognition = dic["Recognition"]
            };
        }

        public IMessage GetEntity(Dictionary<string, string> dic)
        {
            return new VoiceMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                Format = dic["Format"],
                Recognition = dic["Recognition"]
            };
        }
    }
}
