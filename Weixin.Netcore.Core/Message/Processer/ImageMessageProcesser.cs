using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（图片消息）
    /// </summary>
    public class ImageMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;
        private readonly IMessageRepetValidUsage _messageRepetValidUsage;
        private readonly IImageMessageHandler _imageMessageHandler;

        public ImageMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IMessageRepetValidUsage messageRepetValidUsage, IImageMessageHandler imageMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;
            _messageRepetValidUsage = messageRepetValidUsage;
            _imageMessageHandler = imageMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ImageMessage)//图片消息
            {
                if (_messageRepetValidUsage.IsRepetValidUse && !_messageRepetHandler.MessageRepetValid((message as ImageMessage).MsgId.ToString()))
                    return "success";
                return _imageMessageHandler.ImageMessageHandler(message as ImageMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
