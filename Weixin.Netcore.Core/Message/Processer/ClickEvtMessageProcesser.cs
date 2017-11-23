using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（点击事件）
    /// </summary>
    public class ClickEvtMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IClickEvtMessageHandler _clickEventHandler;

        public ClickEvtMessageProcesser(IMessageRepetHandler messageRepetHandler, 
            IClickEvtMessageHandler clickEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            
            _clickEventHandler = clickEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ClickEvtMessage)//点击事件消息
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
