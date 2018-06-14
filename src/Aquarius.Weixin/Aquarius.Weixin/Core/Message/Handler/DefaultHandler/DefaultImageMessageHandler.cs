using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultImageMessageHandler : IImageMessageHandler
    {
        public string Handle(ImageMessage message)
        {
            return Consts.Success;
        }
    }
}
