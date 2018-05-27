using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 事件消息处理
    /// </summary>
    public class EventMessageHandlerBase
    {
        public virtual string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }

        public virtual string LocationEventHandler(LocationEvtMessage message)
        {
            return "success";
        }

        public virtual string ScanEventHandler(ScanEvtMessage message)
        {
            return "success";
        }

        public virtual string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }

        public virtual string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }

        public virtual string ScanSubscribeEventHandler(ScanSubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
