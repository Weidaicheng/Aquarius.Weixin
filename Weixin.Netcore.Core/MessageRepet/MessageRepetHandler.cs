using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Entity;

namespace Weixin.Netcore.Core.MessageRepet
{
    /// <summary>
    /// 消息重复性处理
    /// </summary>
    public class MessageRepetHandler : IMessageRepetHandler
    {
        private readonly ICache _cache;
        private readonly BaseSettings _baseSettings;

        public MessageRepetHandler(ICache cache, BaseSettings baseSettings)
        {
            _cache = cache;
            _baseSettings = baseSettings;
        }

        public bool MessageRepetValid(string key)
        {
            if (_baseSettings.Debug)
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
