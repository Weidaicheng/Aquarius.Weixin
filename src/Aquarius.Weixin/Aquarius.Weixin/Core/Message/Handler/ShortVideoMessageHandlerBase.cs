using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 小视频消息处理
    /// </summary>
    public class ShortVideoMessageHandlerBase
    {
        public virtual string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }
    }
}
