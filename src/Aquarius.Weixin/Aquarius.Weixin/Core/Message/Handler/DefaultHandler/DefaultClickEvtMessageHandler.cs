using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultClickEvtMessageHandler : IClickEvtMessageHandler
    {
        public string Handle(ClickEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
