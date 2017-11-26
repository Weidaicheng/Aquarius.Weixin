using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（文本消息）- 无重复验证
    /// </summary>
    public class TextMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly ITextMessageHandler _textMessageHandler;

        public TextMessageProcesserCanRepet(ITextMessageHandler textMessageHandler)
        {
            _textMessageHandler = textMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is TextMessage)//文本消息
            {
                return _textMessageHandler.TextMessageHandler(message as TextMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
