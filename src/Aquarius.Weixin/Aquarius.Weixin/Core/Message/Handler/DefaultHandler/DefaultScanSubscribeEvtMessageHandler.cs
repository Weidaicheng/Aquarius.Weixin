using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultScanSubscribeEvtMessageHandler : IScanSubscribeEvtMessageHandler
    {
        public string Handle(ScanSubscribeEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
