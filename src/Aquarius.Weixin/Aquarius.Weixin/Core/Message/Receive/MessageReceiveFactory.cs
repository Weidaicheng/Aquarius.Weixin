using Aquarius.Weixin.Entity.Enums;
using System;

namespace Aquarius.Weixin.Core.Message.Receive
{
    /// <summary>
    /// 消息接收工厂
    /// </summary>
    public static class MessageReceiveFactory
    {
        /// <summary>
        /// 获取消息接收
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventKey">事件KEY值</param>
        /// <returns></returns>
        public static IMessageReceive GetMessageReceive(string msgType, string eventType, string eventKey)
        {
            //更改为首字母大写模式
            msgType = string.IsNullOrEmpty(msgType) ? msgType : $"{msgType.Substring(0, 1).ToUpper()}{msgType.Substring(1)}";
            eventType = string.IsNullOrEmpty(eventType) ? eventType : $"{eventType.Substring(0, 1).ToUpper()}{eventType.Substring(1).ToLower()}";

            IMessageReceive messageReceive;

            switch(Enum.Parse(typeof(MessageType), msgType))
            {
                case MessageType.Text:
                    messageReceive = new TextMessageReceive();
                    break;
                case MessageType.Image:
                    messageReceive = new ImageMessageReceive();
                    break;
                case MessageType.Voice:
                    messageReceive = new VoiceMessageReceive();
                    break;
                case MessageType.Video:
                    messageReceive = new VideoMessageReceive();
                    break;
                case MessageType.Shortvideo:
                    messageReceive = new ShortVideoMessageReceive();
                    break;
                case MessageType.Location:
                    messageReceive = new LocationMessageReceive();
                    break;
                case MessageType.Link:
                    messageReceive = new LinkMessageReceive();
                    break;
                case MessageType.Event:
                    switch(Enum.Parse(typeof(EventType), eventType))
                    {
                        case EventType.Subscribe:
                            if (string.IsNullOrEmpty(eventKey))
                            {
                                messageReceive = new SubscribeEvtMessageReceive();
                            }
                            else
                            {
                                messageReceive = new ScanSubscribeEvtMessageReceive();
                            }
                            break;
                        case EventType.Unsubscribe:
                            messageReceive = new UnSubscribeEvtMessageReceive();
                            break;
                        case EventType.Scan:
                            messageReceive = new ScanEvtMessageReceive();
                            break;
                        case EventType.Location:
                            messageReceive = new LocationEvtMessageReceive();
                            break;
                        case EventType.Click:
                            messageReceive = new ClickEvtMessageReceive();
                            break;
                        case EventType.View:
                            messageReceive = new ViewEvtMessageReceive();
                            break;
                        default:
                            messageReceive = null;
                            break;
                    }
                    break;
                default:
                    messageReceive = null;
                    break;
            }

            return messageReceive;
        }
    }
}
