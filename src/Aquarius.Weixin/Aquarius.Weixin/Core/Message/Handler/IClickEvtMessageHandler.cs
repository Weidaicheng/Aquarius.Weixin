using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public interface IClickEvtMessageHandler : IMessageHandler<ClickEvtMessage>
    { }
}
