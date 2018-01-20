using Weixin.Netcore.Entity.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 普通消息处理
    /// </summary>
    public class NormalMessageHandlerBase
    {
        public virtual string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }

        public virtual string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }

        public virtual string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }

        public virtual string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }

        public virtual string TextMessageHandler(TextMessage message)
        {
            return "success";
        }

        public virtual string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }

        public virtual string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
