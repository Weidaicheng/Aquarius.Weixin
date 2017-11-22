using System.Collections.Generic;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 语音消息处理
    /// </summary>
    public interface IVoiceMessageHandler
    {
        /// <summary>
        /// 语音消息处理
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="result"></param>
        string VoiceMessageHandler(Dictionary<string, string> dictionary);
    }
}
