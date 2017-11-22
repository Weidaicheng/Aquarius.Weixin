namespace Weixin.Netcore.Model.WeixinMessage
{
    /// <summary>
    /// 回复消息
    /// </summary>
    public interface IMessageReply
    {
        /// <summary>
        /// 生成Xml
        /// </summary>
        /// <returns></returns>
        string CreateXml();
    }
}
