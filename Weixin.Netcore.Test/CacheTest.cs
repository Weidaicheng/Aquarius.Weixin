using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Weixin.Netcore.Cache;

namespace Weixin.Netcore.Core.Test
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void RedisCacheTest()
        {
            ICache cache = new RedisCache("127.0.0.1", 6379, "123456");
            cache.Set("city", "nanjing", TimeSpan.FromSeconds(5));
            Thread.Sleep(5000);
            Console.WriteLine(cache.Get("date"));
        }
    }
}
