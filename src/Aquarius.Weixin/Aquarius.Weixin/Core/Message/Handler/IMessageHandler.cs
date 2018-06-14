using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 消息处理
    /// </summary>
    public interface IMessageHandler
    { }

    /// <summary>
    /// 消息处理
    /// </summary>
    /// <typeparam name="T"><see cref="IMessage"/></typeparam>
    public interface IMessageHandler<T> : IMessageHandler where T : IMessage
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="message">类型：<see cref="IMessage"/></param>
        /// <returns></returns>
        string Handle(T message);
    }
}
