using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    public interface ITextMessageHandler
    {
        /// <summary>
		/// 文本消息处理
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="result"></param>
		string TextMessageHandler(Dictionary<string, string> dictionary);
    }
}
