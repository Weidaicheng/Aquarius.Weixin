using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 消息处理
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string Handle(IMessage message);
    }
}
