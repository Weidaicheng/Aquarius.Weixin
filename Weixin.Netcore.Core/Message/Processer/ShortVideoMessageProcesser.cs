using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（小视频消息）
    /// </summary>
    public class ShortVideoMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly IShortVideoMessageHandler _shortVideoMeessageHandler;

        public ShortVideoMessageProcesser(IMessageRepetHandler messageRepetHandler,
            IShortVideoMessageHandler shortVideoMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _shortVideoMeessageHandler = shortVideoMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is ShortVideoMessage)//小视频消息
            {
                if (!_messageRepetHandler.MessageRepetValid((message as ShortVideoMessage).MsgId.ToString()))
                    return "success";
                return _shortVideoMeessageHandler.ShortVideoMessageHandler(message as ShortVideoMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
