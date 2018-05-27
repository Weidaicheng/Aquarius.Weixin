using System;
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
        public string GetAccessToken()
        {
            string token = _cache.Get("AccessToken");
            lock(locker)
            {
                if (string.IsNullOrEmpty(token))
                {
                    lock(locker)
                    {
                        var accessToken = _oAuthInterfaceCaller.GetAccessToken();
                        _cache.Set("AccessToken", accessToken.access_token, TimeSpan.FromSeconds(accessToken.expires_in));
                        token = accessToken.access_token;
                    }
                }
            }

            return token;
        }
    }
}
