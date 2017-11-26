using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（取消订阅事件）- 无重复验证
    /// </summary>
    public class UnsubscribeEvtMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IUnsubscribeEvtMessageHandler _unsubscribeEventHandler;

        public UnsubscribeEvtMessageProcesserCanRepet(IUnsubscribeEvtMessageHandler unsubscribeEventHandler)
        {
            _unsubscribeEventHandler = unsubscribeEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is UnSubscribeEvtMessage)//取消订阅事件消息
            {
                return _unsubscribeEventHandler.UnsubscribeEventHandler(message as UnSubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
