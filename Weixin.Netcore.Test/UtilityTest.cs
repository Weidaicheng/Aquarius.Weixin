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

        [TestMethod]
        public void GenerateJsApiSignatureTest()
        {
            string ticket = "sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg";
            string noncestr = "Wm3WZYTPz0wzccnW";
            long timestamp = 1414587457;
            string url = "http://mp.weixin.qq.com?params=value";
            var signature = UtilityHelper.GenerateJsApiSignature(ticket, noncestr, timestamp, url);
            Assert.AreEqual("0f9de62fce790f9a083d5c99e95740ceb90c27ed", signature, false);
        }
    }
}
