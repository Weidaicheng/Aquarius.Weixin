using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
