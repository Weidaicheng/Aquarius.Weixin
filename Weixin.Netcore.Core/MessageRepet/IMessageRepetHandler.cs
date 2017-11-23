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
        /// <param name="isDebug">调试模式（默认为false）</param>
        /// <returns>
        /// true：消息未重复
        /// false：消息重复
        /// </returns>
        bool MessageRepetValid(string key, bool isDebug = false);
    }
}
