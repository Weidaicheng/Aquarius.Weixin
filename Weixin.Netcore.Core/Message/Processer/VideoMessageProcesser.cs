using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（视频消息）
    /// </summary>
    public class VideoMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IVideoMessageHandler _videoMessageHandler;

        public VideoMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IVideoMessageHandler videoMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _videoMessageHandler = videoMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is VideoMessage)//视频消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as VideoMessage).MsgId.ToString()))
                    return "success";
                return _videoMessageHandler.VideoMessageHandler(message as VideoMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
