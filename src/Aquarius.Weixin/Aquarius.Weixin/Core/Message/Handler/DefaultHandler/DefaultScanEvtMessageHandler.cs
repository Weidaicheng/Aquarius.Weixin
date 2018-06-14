using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultScanEvtMessageHandler : IScanEvtMessageHandler
    {
        public string Handle(ScanEvtMessage message)
        {
            return Consts.Success;
        }
    }
}
