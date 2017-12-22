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

            IMessage message = null; ;
            IMessageReceive messageReceive;
            switch (dic["MsgType"].ToLower())
            {
                case "text":
                    messageReceive = new TextMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "image":
                    messageReceive = new ImageMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "voice":
                    messageReceive = new VoiceMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "video":
                    messageReceive = new VideoMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "shortvideo":
                    messageReceive = new ShortVideoMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "location":
                    messageReceive = new LocationMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "link":
                    messageReceive = new LinkMessageReceive();
                    message = messageReceive.GetEntity(dic);
                    break;
                case "event":
                    switch (dic["Event"].ToLower())
                    {
                        case "subscribe":
                            messageReceive = new SubscribeEvtMessageReceive();
                            message = messageReceive.GetEntity(dic);
                            break;
                        case "unsubscribe":
                            messageReceive = new UnSubscribeEvtMessageReceive();
                            message = messageReceive.GetEntity(dic);
                            break;
                        case "scan":
                            messageReceive = new ScanEvtMessageReceive();
                            message = messageReceive.GetEntity(dic);
                            break;
                        case "location":
                            messageReceive = new LocationEvtMessageReceive();
                            message = messageReceive.GetEntity(dic);
                            break;
                        case "click":
                            messageReceive = new ClickEvtMessageReceive();
                            message = messageReceive.GetEntity(dic);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
