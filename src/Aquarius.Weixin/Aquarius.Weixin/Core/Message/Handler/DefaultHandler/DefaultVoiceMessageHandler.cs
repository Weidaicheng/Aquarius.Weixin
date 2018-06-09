using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultVoiceMessageHandler : IVoiceMessageHandler
    {
        public string Handle(VoiceMessage message)
        {
            return Consts.Success;
        }
    }
}
