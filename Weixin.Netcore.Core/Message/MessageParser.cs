using System;
using Weixin.Netcore.Core.Message.Receive;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message
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

            IMessage message = MessageReceiveFactory.GetMessageReceive(dic["MsgType"], dic["Event"]).GetEntity(dic);

            return message;
        }
    }
}
