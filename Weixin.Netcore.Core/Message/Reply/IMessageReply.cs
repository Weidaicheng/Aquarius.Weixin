namespace Weixin.Netcore.Core.Message.Reply
{
    /// <summary>
    /// 回复消息
    /// </summary>
    public interface IMessageReply
    {
    }

    /// <summary>
    /// 回复消息-泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageReply<T> : IMessageReply
    {
        /// <summary>
        /// 生成Xml
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CreateXml(T entity);
    }
}
