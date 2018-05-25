using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// 签名非法异常
    /// </summary>
    public class SignatureInValidException : Exception
    {
        public SignatureInValidException(string message)
            : base(message)
        { }
    }
}
