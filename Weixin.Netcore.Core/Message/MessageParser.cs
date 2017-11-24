using System;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Receive;
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
                    message = (messageReceive as IMessageReceive<TextMessage>).GetEntity(dic);
                    break;
                case "image":
                    messageReceive = new ImageMessageReceive();
                    message = (messageReceive as IMessageReceive<ImageMessage>).GetEntity(dic);
                    break;
                case "voice":
                    messageReceive = new VoiceMessageReceive();
                    message = (messageReceive as IMessageReceive<VoiceMessage>).GetEntity(dic);
                    break;
                case "video":
                    messageReceive = new VideoMessageReceive();
                    message = (messageReceive as IMessageReceive<VideoMessage>).GetEntity(dic);
                    break;
                case "shortvideo":
                    messageReceive = new ShortVideoMessageReceive();
                    message = (messageReceive as IMessageReceive<ShortVideoMessage>).GetEntity(dic);
                    break;
                case "location":
                    messageReceive = new LocationMessageReceive();
                    message = (messageReceive as IMessageReceive<LocationMessage>).GetEntity(dic);
                    break;
                case "link":
                    messageReceive = new LinkMessageReceive();
                    message = (messageReceive as IMessageReceive<LinkMessage>).GetEntity(dic);
                    break;
                case "event":
                    switch (dic["Event"].ToLower())
                    {
                        case "subscribe":
                            messageReceive = new SubscribeEvtMessageReceive();
                            message = (messageReceive as IMessageReceive<SubscribeEvtMessage>).GetEntity(dic);
                            break;
                        case "unsubscribe":
                            messageReceive = new UnSubscribeEvtMessageReceive();
                            message = (messageReceive as IMessageReceive<UnSubscribeEvtMessage>).GetEntity(dic);
                            break;
                        case "scan":
                            messageReceive = new ScanEvtMessageReceive();
                            message = (messageReceive as IMessageReceive<ScanEvtMessage>).GetEntity(dic);
                            break;
                        case "location":
                            messageReceive = new LocationEvtMessageReceive();
                            message = (messageReceive as IMessageReceive<LocationEvtMessage>).GetEntity(dic);
                            break;
                        case "click":
                            messageReceive = new ClickEvtMessageReceive();
                            message = (messageReceive as IMessageReceive<ClickEvtMessage>).GetEntity(dic);
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
