using System;

namespace Weixin.Netcore.Cache.Exceptions
{
    /// <summary>
    /// Redis连接null异常
    /// </summary>
    public class RedisConnectionNullException : Exception
    {
        public RedisConnectionNullException(string message)
            : base(message)
        { }
    }
}
