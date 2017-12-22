using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 图片消息处理
    /// </summary>
    public class ImageMessageHandler : IMessageHandler
    {
        public virtual string Handle(IMessage message)
        {
            return "success";
        }
    }
}
