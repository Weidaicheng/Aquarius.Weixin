using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（订阅事件）
    /// </summary>
    public class SubscribeClickEvtMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ISubscribeEvtMessageHandler _subscribeEventHandler;

        public SubscribeClickEvtMessageProcesser(IMessageRepetHandler messageRepetHandler, 
            ISubscribeEvtMessageHandler subscribeEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            
            _subscribeEventHandler = subscribeEventHandler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (message is ClickEvtMessage)//点击事件消息
            {
                _subscribeEventHandler.SubscribeEventHandler(message as SubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
