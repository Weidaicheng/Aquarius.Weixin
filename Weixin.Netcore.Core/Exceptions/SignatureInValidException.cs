using System;

namespace Weixin.Netcore.Core.Exceptions
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
