using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultSubscribeEvtMessageHandler : ISubscribeEvtMessageHandler
    {
        public string Handle(SubscribeEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
