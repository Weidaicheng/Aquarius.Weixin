using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 取消订阅事件处理
    /// </summary>
    public interface IUnsubscribeEventHandler : IMessageHandler
    {
        /// <summary>
        /// 取消订阅事件处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string UnsubscribeEventHandler(Dictionary<string, string> dictionary);
    }
}
