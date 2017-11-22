using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 图片消息处理
    /// </summary>
    public interface IImageMessageHandler
    {
        /// <summary>
        /// 图片消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string ImageMessageHandler(Dictionary<string, string> dictionary);
    }
}
