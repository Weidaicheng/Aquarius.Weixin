using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 链接消息处理
    /// </summary>
    public interface ILinkMessageHandler
    {
        /// <summary>
        /// 链接消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string LinkMessageHandler(Dictionary<string, string> dictionary);
    }
}
