using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Reply
{
    /// <summary>
    /// 回复消息
    /// </summary>
    public interface IMessageReply
    { }

    /// <summary>
    /// 回复消息-泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageReply<T> : IMessageReply where T : IMessage, ICanBeUsedToReply
    {
        /// <summary>
        /// 生成Xml
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CreateXml(T entity);
    }
}
