using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 地理位置消息处理
    /// </summary>
    public interface ILocationMessageHandler
    {
        /// <summary>
        /// 地理位置消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string LocationMessageHandler(Dictionary<string, string> dictionary);
    }
}
