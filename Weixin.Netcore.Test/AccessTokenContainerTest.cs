using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Core.MaintainContainer;
using Weixin.Netcore.Model;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class AccessTokenContainerTest
    {
        [TestMethod]
        public void GetAccessTokenTest()
        {
            ICache cache = new InMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            //ICache cache = new RedisCache(new Microsoft.Extensions.Caching.Redis.RedisCache(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions() { Configuration = "127.0.0.1:6379,password=123456" }));
            BaseSettings weixinSetting = new BaseSettings()
            {
                AppId = "wx6eff55d0d76e210f",
                AppSecret = "60ab768429e8fc6b86abaa9cfd1c6565"
            };
            OAuthInterfaceCaller oAuthInterface = new OAuthInterfaceCaller(new RestClient(), weixinSetting);
            var container = new AccessTokenContainer(cache, oAuthInterface);
            List<string> tokens = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                tokens.Add(container.GetAccessToken());
            }
            foreach(var item in tokens)
            {
                Console.WriteLine(item);
            }
        }
    }
}
