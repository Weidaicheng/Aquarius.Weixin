using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 语音消息处理-未实现
    /// </summary>
    public sealed class VoiceMessageHandlerNotImp : IVoiceMessageHandler
    {
        public string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
