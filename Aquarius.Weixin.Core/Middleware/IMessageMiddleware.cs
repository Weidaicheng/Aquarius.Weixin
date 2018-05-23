namespace Aquarius.Weixin.Core.Middleware
{
    /// <summary>
    /// 消息中间件
    /// </summary>
    public interface IMessageMiddleware
    {
        /// <summary>
        /// 接收消息中间件
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="msgSignature">消息签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机串</param>
        /// <param name="data"></param>
        /// <returns></returns>
        string ReceiveMessageMiddle(string signature, string msgSignature, string timestamp, string nonce, string data);

        /// <summary>
        /// 回复消息中间件
        /// </summary>
        /// <param name="replyMsg"></param>
        /// <returns></returns>
        string ReplyMessageMiddle(string replyMsg);
    }
}
