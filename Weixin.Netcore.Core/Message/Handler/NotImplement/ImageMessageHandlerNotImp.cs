using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 图片消息处理-未实现
    /// </summary>
    public sealed class ImageMessageHandlerNotImp : IImageMessageHandler
    {
        public string ImageMessageHandler(ImageMessage message)
        {
            return "success";
        }
    }
}
