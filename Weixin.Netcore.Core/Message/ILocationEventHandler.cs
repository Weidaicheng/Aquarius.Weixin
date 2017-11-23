using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 上报地理位置事件处理
    /// </summary>
    public interface ILocationEventHandler : IMessageHandler
    {
        /// <summary>
        /// 上报地理位置事件处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string LocationEventHandler(Dictionary<string, string> dictionary);
    }
}
