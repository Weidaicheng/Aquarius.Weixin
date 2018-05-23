using System;

namespace Aquarius.Weixin.Core.Exceptions
{
    /// <summary>
    /// 消息不支持异常
    /// </summary>
    public class MessageNotSupportException : Exception
    {
        public MessageNotSupportException(string message)
            : base(message)
        { }
    }
}
