using Weixin.Netcore.Core.Exceptions;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器（位置上报事件）- 无重复验证
    /// </summary>
    public class LocationEvtMessageProcesserCanRepet : IMessageProcesser
    {
        private readonly ILocationEvtMessageHandler _locationEventHandler;

        public LocationEvtMessageProcesserCanRepet(ILocationEvtMessageHandler locationEventHandler)
        {
            _locationEventHandler = locationEventHandler;
        }

        public string ProcessMessage(IMessage message)
        {
            if (message is LocationEvtMessage)//位置上报事件消息
            {
                return _locationEventHandler.LocationEventHandler(message as LocationEvtMessage);
            }
            else
            {
                throw new MessageNotSupportException("不支持的消息类型");
            }
        }
    }
}
