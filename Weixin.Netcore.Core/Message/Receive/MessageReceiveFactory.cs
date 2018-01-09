using Weixin.Netcore.Model.Enums;

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
        /// <param name="eventKey">事件KEY值</param>
        /// <returns></returns>
        public static IMessageReceive GetMessageReceive(string msgType, string eventType, string eventKey)
        {
            IMessageReceive messageReceive;

            if (msgType.ToLower() == MessageType.Text.ToString().ToLower())
            {
                messageReceive = new TextMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Image.ToString().ToLower())
            {
                messageReceive = new ImageMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Voice.ToString().ToLower())
            {
                messageReceive = new VoiceMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Video.ToString().ToLower())
            {
                messageReceive = new VideoMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Shortvideo.ToString().ToLower())
            {
                messageReceive = new ShortVideoMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Location.ToString().ToLower())
            {
                messageReceive = new LocationMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Link.ToString().ToLower())
            {
                messageReceive = new LinkMessageReceive();
            }
            else if (msgType.ToLower() == MessageType.Event.ToString().ToLower())
            {
                if (eventType.ToLower() == EventType.Subscribe.ToString().ToLower())
                {
                    if (string.IsNullOrEmpty(eventKey))
                    {
                        messageReceive = new SubscribeEvtMessageReceive(); 
                    }
                    else
                    {
                        messageReceive = new ScanSubscribeEvtMessageReceive();
                    }
                }
                else if (eventType.ToLower() == EventType.Unsubscribe.ToString().ToLower())
                {
                    messageReceive = new UnSubscribeEvtMessageReceive();
                }
                if (eventType.ToLower() == EventType.Scan.ToString().ToLower())
                {
                    messageReceive = new ScanEvtMessageReceive();
                }
                if (eventType.ToLower() == EventType.Location.ToString().ToLower())
                {
                    messageReceive = new LocationEvtMessageReceive();
                }
                if (eventType.ToLower() == EventType.Click.ToString().ToLower())
                {
                    messageReceive = new ClickEvtMessageReceive();
                }
                else
                {
                    messageReceive = null;
                }
            }
            else
            {
                messageReceive = null;
            }

            return messageReceive;
        }
    }
}
