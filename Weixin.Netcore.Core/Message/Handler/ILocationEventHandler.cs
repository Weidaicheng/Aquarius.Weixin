using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 上报地理位置事件处理
    /// </summary>
    public interface ILocationEventHandler : IMessageHandler
    {
        /// <summary>
        /// 上报地理位置事件处理
        /// </summary>
        /// <param name="message"></param>
        string LocationEventHandler(LocationEvtMessage message);
    }
}
