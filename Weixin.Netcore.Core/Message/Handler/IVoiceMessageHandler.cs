using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 语音消息处理
    /// </summary>
    public interface IVoiceMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 语音消息处理
        /// </summary>
        /// <param name="message"></param>
        string VoiceMessageHandler(VoiceMessage message);
    }
}
