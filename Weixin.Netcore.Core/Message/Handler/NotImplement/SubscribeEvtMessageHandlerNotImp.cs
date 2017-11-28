using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 订阅事件处理-未实现
    /// </summary>
    public sealed class SubscribeEvtMessageHandlerNotImp : ISubscribeEvtMessageHandler
    {
        public string SubscribeEventHandler(SubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
