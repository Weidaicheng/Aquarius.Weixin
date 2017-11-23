using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（链接消息）
    /// </summary>
    public class LinkMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ILinkMessageHandler _linkMessageHandlder;

        public LinkMessageProcesser(IMessageRepetHandler messageRepetHandler,
            ILinkMessageHandler linkMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _linkMessageHandlder = linkMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is LinkMessage)//链接消息
            {
                return _linkMessageHandlder.LinkMessageHandler(message as LinkMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
