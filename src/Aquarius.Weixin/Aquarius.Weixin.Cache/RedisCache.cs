using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Aquarius.Weixin.Cache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {
        #region .ctor
        private readonly IDistributedCache _distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion

        public string Get(string key)
        {
            return _distributedCache.GetString(key);
        }

        public void Set(string key, string value)
        {
            _distributedCache.SetString(key, value);
        }

        public void Set(string key, string value, TimeSpan ts)
        {
            _distributedCache.SetString(key, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = ts});
        }
    }
}
