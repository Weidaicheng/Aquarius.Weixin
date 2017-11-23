﻿using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 语音消息接收
    /// </summary>
    public class VoiceMessageReceive : IMessageReceive<VoiceMessage>
    {
        public VoiceMessage GetEntity(string xml)
        {
            var dic = UtilityHelper.Xml2Dictionary(xml);
            var message = new VoiceMessage()
            {
                ToUserName = dic["ToUserName"],
                FromUserName = dic["FromUserName"],
                CreateTime = long.Parse(dic["CreateTime"]),
                MsgId = long.Parse(dic["MsgId"]),
                MediaId = dic["MediaId"],
                Format = dic["Format"],
                Recognition = dic["Recognition"]
            };

            return message;
        }
    }
}