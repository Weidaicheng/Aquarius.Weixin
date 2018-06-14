using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultUnsubscribeEvtMessageHandler : IUnsubscribeEvtMessageHandler
    {
        public string Handle(UnSubscribeEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
