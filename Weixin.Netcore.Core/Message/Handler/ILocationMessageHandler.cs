using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 地理位置消息处理
    /// </summary>
    public interface ILocationMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 地理位置消息处理
        /// </summary>
        /// <param name="message"></param>
        string LocationMessageHandler(LocationMessage message);
    }
}
