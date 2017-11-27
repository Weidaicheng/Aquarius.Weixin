namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 重复验证使用
    /// </summary>
    public class MessageRepetValidUsage : IMessageRepetValidUsage
    {
        public MessageRepetValidUsage(bool isRepetValidUsa)
        {
            IsRepetValidUse = isRepetValidUsa;
        }

        public bool IsRepetValidUse { get; }
    }
}