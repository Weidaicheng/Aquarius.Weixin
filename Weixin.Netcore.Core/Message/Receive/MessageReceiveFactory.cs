namespace Weixin.Netcore.Core.Message.Receive
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
        /// <returns></returns>
        public static IMessageReceive GetMessageReceive(string msgType, string eventType)
        {
            IMessageReceive messageReceive = null;

            switch (msgType.ToLower())
            {
                case "text":
                    messageReceive = new TextMessageReceive();
                    break;
                case "image":
                    messageReceive = new ImageMessageReceive();
                    break;
                case "voice":
                    messageReceive = new VoiceMessageReceive();
                    break;
                case "video":
                    messageReceive = new VideoMessageReceive();
                    break;
                case "shortvideo":
                    messageReceive = new ShortVideoMessageReceive();
                    break;
                case "location":
                    messageReceive = new LocationMessageReceive();
                    break;
                case "link":
                    messageReceive = new LinkMessageReceive();
                    break;
                case "event":
                    switch (eventType.ToLower())
                    {
                        case "subscribe":
                            messageReceive = new SubscribeEvtMessageReceive();
                            break;
                        case "unsubscribe":
                            messageReceive = new UnSubscribeEvtMessageReceive();
                            break;
                        case "scan":
                            messageReceive = new ScanEvtMessageReceive();
                            break;
                        case "location":
                            messageReceive = new LocationEvtMessageReceive();
                            break;
                        case "click":
                            messageReceive = new ClickEvtMessageReceive();
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
