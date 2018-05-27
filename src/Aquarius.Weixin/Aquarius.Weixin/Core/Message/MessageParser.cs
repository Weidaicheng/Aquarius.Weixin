using System;
using Aquarius.Weixin.Core.Message.Receive;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Utility;

namespace Aquarius.Weixin.Core.Message
{
    /// <summary>
    /// 消息解析
    /// </summary>
    public class MessageParser
    {
        /// <summary>
        /// 解析消息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IMessage ParseMessage(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("xml为空");
            }

            var dic = UtilityHelper.Xml2Dictionary(xml);

            IMessage message = MessageReceiveFactory.GetMessageReceive(dic["MsgType"], (dic.ContainsKey("Event") ? dic["Event"] : null), (dic.ContainsKey("EventKey") ? dic["EventKey"] : null)).GetEntity(dic);

            return message;
        }
    }
}
