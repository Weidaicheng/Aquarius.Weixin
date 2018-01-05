namespace Weixin.Netcore.Core.DebugSetting
{
    /// <summary>
    /// 调试模式
    /// </summary>
    public interface IDebugMode
    {
        /// <summary>
        /// 是否调试
        /// </summary>
        bool IsDebug { get; }
    }
}
