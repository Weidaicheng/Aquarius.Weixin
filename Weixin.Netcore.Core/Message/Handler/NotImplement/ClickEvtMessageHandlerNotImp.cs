using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler.NotImplement
{
    /// <summary>
    /// 自定义菜单点击事件处理-未实现
    /// </summary>
    public sealed class ClickEvtMessageHandlerNotImp : IClickEvtMessageHandler
    {
        public string ClickEventHandler(ClickEvtMessage message)
        {
            return "success";
        }
    }
}
