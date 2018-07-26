using Aquarius.Weixin.Core.InterfaceCaller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aquarius.Weixin.Test
{
    [TestClass]
    public class OAuthInterfaceCallerTest
    {
        [TestMethod]
        public void CheckTokenAsync_Should_Be_False()
        {
            var result = getCheckTokenResult().Result;
            Console.WriteLine(result.Item2);
            Assert.IsFalse(result.Item1);
        }

        private async Task<(bool, string)> getCheckTokenResult()
        {
            var oauthInterfaceCaller = new OAuthInterfaceCaller(null);
            var result = await oauthInterfaceCaller.CheckTokenAsync("aaa", "aaa");
            return result;
        }
    }
}
