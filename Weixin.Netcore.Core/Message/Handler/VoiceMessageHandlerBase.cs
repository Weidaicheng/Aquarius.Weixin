using Weixin.Netcore.Entity.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 语音消息处理
    /// </summary>
    public class VoiceMessageHandlerBase
    {
        public virtual string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
