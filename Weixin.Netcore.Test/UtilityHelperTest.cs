using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Model.Pay;
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
    }
}
