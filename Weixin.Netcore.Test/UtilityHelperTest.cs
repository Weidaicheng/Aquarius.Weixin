using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Entity.Pay;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class UtilityHelperTest
    {
        [TestMethod]
        public void GenerateTradeNo_Test_NoLine()
        {
            var result = UtilityHelper.GenerateTradeNo();
            Assert.IsFalse(result.Contains("-"));
        }

        [TestMethod]
        public void GenerateTradeNo_Test_LengthIs32()
        {
            var result = UtilityHelper.GenerateTradeNo();
            Assert.IsTrue(result.Length == 32);
        }

        [TestMethod]
        public void SpliceXmlTest()
        {
            OrderQuery orderQuery = new OrderQuery()
            {
                appid = "appid",
                mch_id = "mchid",
                nonce_str = "noncestr",
                out_trade_no = "tradeno",
                sign = "sign",
            };

            var xml = UtilityHelper.Obj2Xml(orderQuery);
        }

        [TestMethod]
        public void Obj2DictionaryTest()
        {
            OrderQuery orderQuery = new OrderQuery()
            {
                appid = "appid",
                mch_id = "mchid",
                nonce_str = "noncestr",
                out_trade_no = "tradeno",
                sign = "sign",
            };

            var dic = UtilityHelper.Obj2Dictionary(orderQuery);
        }

        [TestMethod]
        public void SHA256EncryptTest()
        {
            string key = "192006250b4c09247ec02edce69f6a2d";
            string str = "appid=wxd930ea5d5a258f4f&body=test&device_info=1000&mch_id=10000100&nonce_str=ibuaiVcKdpRxkhJA" + $"&key={key}";
            Assert.AreEqual("6A9AE1657590FD6257D693A078E1C3E4BB6BA4DC30B23E0EE2496E54170DACD6",
                UtilityHelper.SHA256Encrypt(str, key).ToUpper());
        }
    }
}
