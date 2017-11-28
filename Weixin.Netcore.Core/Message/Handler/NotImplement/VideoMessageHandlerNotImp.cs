using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 视频消息处理-未实现
    /// </summary>
    public sealed class VideoMessageHandlerNotImp : IVideoMessageHandler
    {
        public string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }
    }
}
