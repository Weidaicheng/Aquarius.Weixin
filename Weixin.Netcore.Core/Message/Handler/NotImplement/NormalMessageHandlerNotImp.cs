using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 普通消息处理未实现
    /// </summary>
    public sealed class NormalMessageHandlerNotImp : INormalMessageHandler
    {
        public string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }

        public string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }

        public string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }

        public string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }

        public string TextMessageHandler(TextMessage message)
        {
            return "success";
        }

        public string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }

        public string VoiceMessageHandler(VoiceMessage message)
        {
            return "success";
        }
    }
}
