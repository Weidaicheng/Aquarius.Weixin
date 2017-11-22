using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 扫描二维码订阅事件处理
    /// </summary>
    public interface IScanSubscribeEventHandler
    {
        /// <summary>
        /// 扫描二维码订阅事件处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string ScanSubscribeEventHandler(Dictionary<string, string> dictionary);
    }
}
