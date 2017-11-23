using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public interface IClickEventHandler : IMessageHandler
    {
        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        string ClickEventHandler(string xml);
    }
}
