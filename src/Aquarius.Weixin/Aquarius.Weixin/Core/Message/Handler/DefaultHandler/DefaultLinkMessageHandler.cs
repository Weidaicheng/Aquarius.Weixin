using Aquarius.Weixin.Entity.Enums;
using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler.DefaultHandler
{
    /// <summary>
    /// 链接消息处理
    /// </summary>
    public class DefaultLinkMessageHandler : ILinkMessageHandler
    {
        public string Handle(LinkMessage message)
        {
            return Consts.Success;
        }
    }
}
