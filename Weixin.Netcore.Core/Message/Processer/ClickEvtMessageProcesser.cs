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
        private readonly IMessageRepetValidUsage _messageRepetValidUsage;

        public ClickEvtMessageProcesser(IMessageRepetHandler messageRepetHandler, 
            IClickEvtMessageHandler clickEventHandler, IMessageRepetValidUsage messageRepetValidUsage)
        {
            _messageRepetHandler = messageRepetHandler;
            _clickEventHandler = clickEventHandler;
            _messageRepetValidUsage = messageRepetValidUsage;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ClickEvtMessage)//点击事件消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as ClickEvtMessage).FromUserName + (message as ClickEvtMessage).CreateTime))
                    return "success";
                return _clickEventHandler.ClickEventHandler(message as ClickEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
