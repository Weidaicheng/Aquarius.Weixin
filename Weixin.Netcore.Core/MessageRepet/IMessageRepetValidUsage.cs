namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 重复验证使用
    /// </summary>
    public interface IMessageRepetValidUsage
    {
        /// <summary>
        /// 是否启用消息重复验证
        /// </summary>
        bool IsRepetValidUse { get; }
    }
}
