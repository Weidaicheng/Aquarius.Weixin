using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（取消订阅事件）
    /// </summary>
    public class UnsubscribeEvtMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IUnsubscribeEvtMessageHandler _unsubscribeEventHandler;

        public UnsubscribeEvtMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IUnsubscribeEvtMessageHandler unsubscribeEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _unsubscribeEventHandler = unsubscribeEventHandler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                _unsubscribeEventHandler.UnsubscribeEventHandler(message as UnSubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
