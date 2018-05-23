using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 图片消息处理
    /// </summary>
    public class ImageMessageHandlerBase
    {
        public virtual string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }
    }
}
