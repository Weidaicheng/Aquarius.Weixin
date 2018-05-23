using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// AppId非法（验证失败）
    /// </summary>
    public class AppIdInValidException : Exception
    {
        public AppIdInValidException(string message) 
            : base(message)
        { }
    }
}
