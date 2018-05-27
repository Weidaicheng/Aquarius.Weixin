using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
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
