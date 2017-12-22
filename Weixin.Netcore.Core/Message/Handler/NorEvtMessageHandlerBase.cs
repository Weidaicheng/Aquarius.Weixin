using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 消息/事件处理
    /// </summary>
    public class NorEvtMessageHandlerBase
    {
        public virtual string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }

        public virtual string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }

        public virtual string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }

        public virtual string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }

        public virtual string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }

        public virtual string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }

        public virtual string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }

        public virtual string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }

        public virtual string TextMessageHandler(TextMessage message)
        {
            return "success";
        }

        public virtual string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }

        public virtual string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }

        public virtual string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
