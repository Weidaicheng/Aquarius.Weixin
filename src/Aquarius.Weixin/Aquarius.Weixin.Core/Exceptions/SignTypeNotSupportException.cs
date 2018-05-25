using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// 签名类型不受支持异常
    /// </summary>
    public class SignTypeNotSupportException : Exception
    {
        public SignTypeNotSupportException(string message)
            : base(message)
        { }
    }
}
