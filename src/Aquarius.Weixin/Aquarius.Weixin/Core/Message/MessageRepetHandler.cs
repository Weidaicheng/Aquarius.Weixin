using System;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Entity.Configuration;

namespace Aquarius.Weixin.Core.Message
{
    /// <summary>
    /// 消息重复性处理
    /// </summary>
    public class MessageRepetHandler
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly BaseSettings _baseSettings;

        public MessageRepetHandler(ICache cache, BaseSettings baseSettings)
        {
            _cache = cache;
            _baseSettings = baseSettings;
        }
        #endregion

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
            if (_baseSettings.Debug)
                return true;

            if (!_baseSettings.IsRepetValid)
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
