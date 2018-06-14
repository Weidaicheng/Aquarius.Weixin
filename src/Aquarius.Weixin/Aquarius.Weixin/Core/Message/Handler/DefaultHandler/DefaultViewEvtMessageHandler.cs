using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultViewEvtMessageHandler : IViewEvtMessageHandler
    {
        public string Handle(ViewEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
