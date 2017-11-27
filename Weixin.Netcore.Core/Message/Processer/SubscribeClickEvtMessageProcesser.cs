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
        private readonly IMessageRepetValidUsage _messageRepetValidUsage;
        private readonly ISubscribeEvtMessageHandler _subscribeEventHandler;

        public SubscribeClickEvtMessageProcesser(IMessageRepetHandler messageRepetHandler, 
            IMessageRepetValidUsage messageRepetValidUsage, ISubscribeEvtMessageHandler subscribeEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            _messageRepetValidUsage = messageRepetValidUsage;
            _subscribeEventHandler = subscribeEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ClickEvtMessage)//点击事件消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as SubscribeEvtMessage).FromUserName + (message as SubscribeEvtMessage).CreateTime))
                    return "success";
                return _subscribeEventHandler.SubscribeEventHandler(message as SubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
