using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 地理位置消息处理-未实现
    /// </summary>
    public sealed class LocationMessageHandlerNotImp : ILocationMessageHandler
    {
        public string LocationMessageHandler(LocationMessage message)
        {
            return "success";
        }
    }
}
