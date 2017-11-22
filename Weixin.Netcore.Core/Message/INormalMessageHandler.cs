namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 普通消息处理
    /// </summary>
    public interface INormalMessageHandler : ITextMessageHandler, IImageMessageHandler, 
        IVoiceMessageHandler, IVideoMessageHandler, IShortVideoMessageHandler, 
        ILocationMessageHandler, ILinkMessageHandler
    { }
}
