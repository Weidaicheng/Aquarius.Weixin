namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 接收消息
    /// </summary>
    public interface IMessageReceive
    {
        /// <summary>
        /// 转化实体
        /// </summary>
        /// <param name="xml"></param>
        void ConvertEntity(string xml);
    }

    /// <summary>
    /// 接收消息-泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageReceive<T>
    {
        /// <summary>
        /// 转化实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        T ConvertEntity(string xml);
    }
}
