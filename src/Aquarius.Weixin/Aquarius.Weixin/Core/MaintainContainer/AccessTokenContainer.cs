using System;
using System.Threading;
using System.Threading.Tasks;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.InterfaceCaller;

namespace Aquarius.Weixin.Core.MaintainContainer
{
    /// <summary>
    /// Access Token容器
    /// </summary>
    public class AccessTokenContainer
    {
        #region .ctor
        private readonly ICache _cache;
        private readonly OAuthInterfaceCaller _oAuthInterfaceCaller;

        private static readonly object locker = new object();
        #endregion

        public AccessTokenContainer(ICache cache, OAuthInterfaceCaller oAuthInterfaceCaller)
        {
            _cache = cache;
            _oAuthInterfaceCaller = oAuthInterfaceCaller;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        [Obsolete("推荐使用异步方法")]
        public string GetAccessToken()
        {
            string token = _cache.Get("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                lock (locker)
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        var accessToken = _oAuthInterfaceCaller.GetAccessToken();
                        _cache.Set("AccessToken", accessToken.access_token, TimeSpan.FromSeconds(accessToken.expires_in));
                        token = accessToken.access_token; 
                    }
                }
            }

            return token;
        }

        /// <summary>
        /// 获取AccessToken-异步
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            string token = _cache.Get("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                Monitor.Enter(locker);
                try
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        var accessToken = await _oAuthInterfaceCaller.GetAccessTokenAsync();
                        _cache.Set("AccessToken", accessToken.access_token, TimeSpan.FromSeconds(accessToken.expires_in));
                        token = accessToken.access_token;
                    }
                }
                finally
                {
                    Monitor.Exit(locker); 
                }
            }

            return token;
        }
    }
}
