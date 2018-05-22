using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weixin.Netcore.Entity;
using Weixin.Netcore.Entity.Enums;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void WxPaySignTypeIsMD5()
        {
            var signType = WxPaySignType.MD5;
            Assert.AreEqual("MD5", signType.GetDescription());
        }

        [TestMethod]
        public void WxPaySignTypeIsHMAC_SHA256()
        {
            var signType = WxPaySignType.SHA256;
            Assert.AreEqual("HMAC-SHA256", signType.GetDescription());
        }
    }
}
