using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 小视频消息处理-未实现
    /// </summary>
    public sealed class ShortVideoMessageHandlerNotImp : IShortVideoMessageHandler
    {
        public string ShortVideoMessageHandler(ShortVideoMessage message)
        {
            return "success";
        }
    }
}
