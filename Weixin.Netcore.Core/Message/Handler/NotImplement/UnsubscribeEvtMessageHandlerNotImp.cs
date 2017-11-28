using System;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 取消订阅事件处理-未实现
    /// </summary>
    public sealed class UnsubscribeEvtMessageHandlerNotImp : IUnsubscribeEvtMessageHandler
    {
        public string UnsubscribeEventHandler(UnSubscribeEvtMessage message)
        {
            return "success";
        }
    }
}
