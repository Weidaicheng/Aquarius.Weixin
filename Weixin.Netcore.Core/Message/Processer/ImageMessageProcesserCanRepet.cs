using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（图片消息）- 无重复验证
    /// </summary>
    public class ImageMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly IImageMessageHandler _imageMessageHandler;

        public ImageMessageProcesserCanRepet(IImageMessageHandler imageMessageHandler)
        {
            _imageMessageHandler = imageMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ImageMessage)//图片消息
            {
                return _imageMessageHandler.ImageMessageHandler(message as ImageMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
