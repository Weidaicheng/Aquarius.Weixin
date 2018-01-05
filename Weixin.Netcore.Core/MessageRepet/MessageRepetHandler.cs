using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.DebugSetting;

namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 消息重复性处理
    /// </summary>
    public class MessageRepetHandler : IMessageRepetHandler
    {
        private readonly ICache _cache;
        private readonly IDebugMode _debug;

        public MessageRepetHandler(ICache cache, IDebugMode debug)
        {
            _cache = cache;
            _debug = debug;
        }

        public bool MessageRepetValid(string key)
        {
            if (_debug.IsDebug)
                return true;

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
