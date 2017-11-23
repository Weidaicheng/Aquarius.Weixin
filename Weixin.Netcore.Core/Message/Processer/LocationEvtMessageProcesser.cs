using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（位置上报事件）
    /// </summary>
    public class LocationEvtMessageProcesser : IMessageProcesser
    {
        private readonly IMessageRepetHandler _messageRepetHandler;

        private readonly ILocationEvtMessageHandler _locationEventHandler;

        public LocationEvtMessageProcesser(IMessageRepetHandler messageRepetHandler,
            ILocationEvtMessageHandler locationEventHandler)
        {
            _messageRepetHandler = messageRepetHandler;

            _locationEventHandler = locationEventHandler;
        }

        public void ProcessMessage(IMessage message)
        {
            if (message is LocationEvtMessage)//位置上报事件消息
            {
                _locationEventHandler.LocationEventHandler(message as LocationEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
