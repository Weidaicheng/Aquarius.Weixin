using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（订阅事件）- 无重复验证
    /// </summary>
    public class SubscribeClickEvtMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly ISubscribeEvtMessageHandler _subscribeEventHandler;

        public SubscribeClickEvtMessageProcesserCanRepet(ISubscribeEvtMessageHandler subscribeEventHandler)
        {
            _subscribeEventHandler = subscribeEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ClickEvtMessage)//点击事件消息
            {
                return _subscribeEventHandler.SubscribeEventHandler(message as SubscribeEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
