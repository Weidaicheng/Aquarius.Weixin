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
        private readonly IMessageRepetValidUsage _messageRepetValidUsage;
        private readonly IUnsubscribeEvtMessageHandler _unsubscribeEventHandler;

        public UnsubscribeEvtMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IMessageRepetValidUsage messageRepetValidUsage, IUnsubscribeEvtMessageHandler unsubscribeEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            _messageRepetValidUsage = messageRepetValidUsage;
            _unsubscribeEventHandler = unsubscribeEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as UnSubscribeEvtMessage).FromUserName + (message as UnSubscribeEvtMessage).CreateTime))
                    return "success";
                return _unsubscribeEventHandler.UnsubscribeEventHandler(message as UnSubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
