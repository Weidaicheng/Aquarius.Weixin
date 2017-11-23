using System;

namespace Weixin.Netcore.Cache.Exceptions
{
    /// <summary>
    /// Redis未连接异常
    /// </summary>
    public class RedisNotConnectException : Exception
    {
        public RedisNotConnectException(string message)
            : base(message)
        { }
    }
}
