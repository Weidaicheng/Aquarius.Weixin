using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（文本消息）
    /// </summary>
    public class TextMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ITextMessageHandler _textMessageHandler;

        public TextMessageProcesser(IMessageRepetHandler messageRepetHandler,
            ITextMessageHandler textMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _textMessageHandler = textMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is TextMessage)//文本消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as TextMessage).MsgId.ToString()))
                    return "success";
                return _textMessageHandler.TextMessageHandler(message as TextMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
