using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// Refresh Token过期异常
    /// </summary>
    public class RefreshTokenExpiredException : Exception
    {
        public RefreshTokenExpiredException(string message)
            : base(message)
        { }
    }
}
