using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 订阅事件处理
    /// </summary>
    public interface ISubscribeEventHandler : IMessageHandler
    {
        /// <summary>
		/// 订阅事件处理
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="result"></param>
		string SubscribeEventHandler(Dictionary<string, string> dictionary);
    }
}
