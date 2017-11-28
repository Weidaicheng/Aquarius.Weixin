using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 消息/事件处理-未实现
    /// </summary>
    public sealed class NorEvtMessageHandlerNotImp : INorEvtMessageHandler
    {
        public string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }

        public string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }

        public string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }

        public string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }

        public string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }

        public string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }

        public string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }

        public string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }

        public string TextMessageHandler(TextMessage message)
        {
            return "success";
        }

        public string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }

        public string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }

        public string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
