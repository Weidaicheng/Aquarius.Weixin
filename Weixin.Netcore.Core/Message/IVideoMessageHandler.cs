using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 视频消息处理
    /// </summary>
    public interface IVideoMessageHandler
    {
        /// <summary>
        /// 视频消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string VideoMessageHandler(Dictionary<string, string> dictionary);
    }
}
