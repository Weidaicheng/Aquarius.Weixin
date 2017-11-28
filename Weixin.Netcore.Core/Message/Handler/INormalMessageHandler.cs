using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 普通消息处理
    /// </summary>
    public interface INormalMessageHandler : ITextMessageHandler, IImageMessageHandler, 
        IVoiceMessageHandler, IVideoMessageHandler, IShortVideoMessageHandler, 
        ILocationMessageHandler, ILinkMessageHandler
    { }
}
