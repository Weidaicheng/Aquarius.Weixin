using Microsoft.Extensions.Caching.Memory;
using System;

namespace Aquarius.Weixin.Cache
{
    /// <summary>
    /// Asp.net Core中自带的InMemory缓存
    /// </summary>
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string Get(string key)
        {
            return _memoryCache.Get<string>(key);
        }

        public void Set(string key, string value)
        {
            _memoryCache.Set<string>(key, value);
        }

        public void Set(string key, string value, TimeSpan ts)
        {
            _memoryCache.Set<string>(key, value, ts);
        }
    }
}
