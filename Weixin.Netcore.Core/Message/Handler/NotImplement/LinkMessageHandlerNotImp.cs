using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 链接消息处理-未实现
    /// </summary>
    public sealed class LinkMessageHandlerNotImp : ILinkMessageHandler
    {
        public string LinkMessageHandler(LinkMessage message)
        {
            return "success";
        }
    }
}
