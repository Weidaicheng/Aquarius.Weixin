using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// Redis未配置
    /// </summary>
    public class RedisNotConfiguredExpection : Exception
    {
        public RedisNotConfiguredExpection(string message)
            : base(message)
        { }
    }
}
