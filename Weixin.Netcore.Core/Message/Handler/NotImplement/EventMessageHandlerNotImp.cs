using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 事件消息处理-未实现
    /// </summary>
    public sealed class EventMessageHandlerNotImp : IEventMessageHandler
    {
        public string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }

        public string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }

        public string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }

        public string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }

        public string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
