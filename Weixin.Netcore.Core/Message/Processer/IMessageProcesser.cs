using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Processer
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    public interface IMessageProcesser
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        void ProcessMessage(IMessage message);
    }
}
