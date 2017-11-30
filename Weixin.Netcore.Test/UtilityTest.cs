using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void GenerateNonceTest()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(UtilityHelper.GenerateNonce()); 
            }
        }
    }
}
