namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 消息重复性验证
    /// </summary>
    public interface IRepetValid
    {
        /// <summary>
        /// 消息重复性验证
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns>
        /// true：消息未重复
        /// false：消息重复
        /// </returns>
        bool MessageRepetValid(string key);
    }
}
