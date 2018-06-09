using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    public class DefaultVideoMessageHandler : IVideoMessageHandler
    {
        public string Handle(VideoMessage message)
        {
            return Consts.Success;
        }
    }
}
