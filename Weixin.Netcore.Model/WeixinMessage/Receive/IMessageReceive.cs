namespace Weixin.Netcore.Model.WeixinMessage.Receive
{
    /// <summary>
    /// 接收消息
    /// </summary>
    public interface IMessageReceive
    {
    }

    /// <summary>
    /// 接收消息-泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageReceive<T> : IMessageReceive
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        T GetEntity(string xml);
    }
}
