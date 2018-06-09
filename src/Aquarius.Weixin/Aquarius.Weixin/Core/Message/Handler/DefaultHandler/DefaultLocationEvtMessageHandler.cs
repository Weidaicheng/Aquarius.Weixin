using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultLocationEvtMessageHandler : ILocationEvtMessageHandler
    {
        public string Handle(LocationEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
