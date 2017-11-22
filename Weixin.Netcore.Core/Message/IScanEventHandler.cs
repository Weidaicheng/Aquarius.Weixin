using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 扫描二维码事件处理
    /// </summary>
    public interface IScanEventHandler
    {
        /// <summary>
        /// 扫描二维码事件处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string ScanEventHandler(Dictionary<string, string> dictionary);
    }
}
