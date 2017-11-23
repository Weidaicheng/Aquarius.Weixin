using System;
using Weixin.Netcore.Cache;

namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 消息重复性处理
    /// </summary>
    public class MessageRepetHandler : IMessageRepetHandler
    {
        private readonly ICache _cache;

        public MessageRepetHandler(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 消息重复性验证
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// true：消息未重复
        /// false：消息重复
        /// </returns>
        public bool MessageRepetValid(string key)
        {
            var value = _cache.Get(key);

            if (string.IsNullOrEmpty(value))
            {
                _cache.Set(key, key, TimeSpan.FromSeconds(5));
                return true;
            }

            return false;
        }
    }
}
