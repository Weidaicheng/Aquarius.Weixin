using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 自定义菜单点击事件处理
    /// </summary>
    public interface IClickEventHandler
    {
        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string ClickEventHandler(Dictionary<string, string> dictionary);
    }
}
