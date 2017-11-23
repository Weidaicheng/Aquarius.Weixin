using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 小视频消息处理
    /// </summary>
    public interface IShortVideoMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 小视频消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string ShortVideoMessageHandler(Dictionary<string, string> dictionary);
    }
}
