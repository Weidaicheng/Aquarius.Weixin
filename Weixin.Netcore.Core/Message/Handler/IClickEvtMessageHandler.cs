using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message.Handler
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public interface IClickEvtMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string ClickEventHandler(ClickEvtMessage message);
    }
}
