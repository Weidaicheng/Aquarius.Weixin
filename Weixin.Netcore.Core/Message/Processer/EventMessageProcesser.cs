using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（事件消息）
    /// </summary>
    public class EventMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ISubscribeEvtMessageHandler _subscribeEventHandler;
        private readonly IUnsubscribeEvtMessageHandler _unsubscribeEventHandler;
        private readonly IScanEvtMessageHandler _scanEventHandler;
        private readonly ILocationEvtMessageHandler _locationEventHandler;
        private readonly IClickEvtMessageHandler _clickEventHandler;

        public EventMessageProcesser(IMessageRepetHandler messageRepetHandler,
            ISubscribeEvtMessageHandler subscribeEventHandler,
            IUnsubscribeEvtMessageHandler unsubscribeEventHandler, IScanEvtMessageHandler scanEventHandler,
            ILocationEvtMessageHandler locationEventHandler, IClickEvtMessageHandler clickEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _subscribeEventHandler = subscribeEventHandler;
            _unsubscribeEventHandler = unsubscribeEventHandler;
            _scanEventHandler = scanEventHandler;
            _locationEventHandler = locationEventHandler;
            _clickEventHandler = clickEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is SubscribeEvtMessage)//订阅事件消息
            {
                return _subscribeEventHandler.SubscribeEventHandler(message as SubscribeEvtMessage);
            }
            else if (message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                return _unsubscribeEventHandler.UnsubscribeEventHandler(message as UnSubscribeEvtMessage);
            }
            else if (message is ScanEvtMessage)//扫码事件消息
            {
                return _scanEventHandler.ScanEventHandler(message as ScanEvtMessage);
            }
            else if (message is LocationEvtMessage)//位置上报事件消息
            {
                return _locationEventHandler.LocationEventHandler(message as LocationEvtMessage);
            }
            else if (message is ClickEvtMessage)//点击事件消息
            {
                return _clickEventHandler.ClickEventHandler(message as ClickEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
