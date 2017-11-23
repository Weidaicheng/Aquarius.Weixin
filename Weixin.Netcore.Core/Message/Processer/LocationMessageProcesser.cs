using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（位置消息）
    /// </summary>
    public class LocationMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ILocationMessageHandler _locationMessageHandler;

        public LocationMessageProcesser(IMessageRepetHandler messageRepetHandler,
            ILocationMessageHandler locationMessageHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _locationMessageHandler = locationMessageHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is LocationMessage)//位置消息
            {
                return _locationMessageHandler.LocationMessageHandler(message as LocationMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
