using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（点击事件）- 无重复验证
    /// </summary>
    public class ClickEvtMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IClickEvtMessageHandler _clickEventHandler;

        public ClickEvtMessageProcesserCanRepet(IClickEvtMessageHandler clickEventHandler)
        {
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
