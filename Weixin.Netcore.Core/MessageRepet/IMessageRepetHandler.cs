namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 消息重复性处理
    /// </summary>
    public interface IMessageRepetHandler
    {
        /// <summary>
        /// 消息重复性验证
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// true：消息未重复
        /// false：消息重复
        /// </returns>
        bool MessageRepetValid(string key);
    }
}
