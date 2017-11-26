using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（链接消息）- 无重复验证
    /// </summary>
    public class LinkMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly ILinkMessageHandler _linkMessageHandlder;

        public LinkMessageProcesserCanRepet(ILinkMessageHandler linkMessageHandler)
        {
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
