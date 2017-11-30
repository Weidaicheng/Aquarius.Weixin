namespace Weixin.Netcore.Core.Middleware
{
    /// <summary>
    /// 消息中间件
    /// </summary>
    public interface IMessageMiddleware
    {
        /// <summary>
        /// 接收消息中间件
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        string ReceiveMessageMiddle(string signature, string timestamp, string nonce, string data);

        /// <summary>
        /// 回复消息中间件
        /// </summary>
        /// <param name="replyMsg"></param>
        /// <returns></returns>
        string ReplyMessageMiddle(string replyMsg);
    }
}
