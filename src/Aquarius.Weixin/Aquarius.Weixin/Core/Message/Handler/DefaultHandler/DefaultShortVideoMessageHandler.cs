using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultShortVideoMessageHandler : IShortVideoMessageHandler
    {
        public string Handle(ShortVideoMessage message)
        {
            return Consts.Success;
        }
    }
}
