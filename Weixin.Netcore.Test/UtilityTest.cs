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

        [TestMethod]
        public void GenerateWxPaySignatureTest()
        {
            string xml = @"
<xml>
   <appid>wx2421b1c4370ec43b</appid>
   <attach>支付测试</attach>
   <body>JSAPI支付测试</body>
   <mch_id>10000100</mch_id>
   <detail><![CDATA[{ 'goods_detail':[ { 'goods_id':'iphone6s_16G', 'wxpay_goods_id':'1001', 'goods_name':'iPhone6s 16G', 'quantity':1, 'price':528800, 'goods_category':'123456', 'body':'苹果手机' }, { 'goods_id':'iphone6s_32G', 'wxpay_goods_id':'1002', 'goods_name':'iPhone6s 32G', 'quantity':1, 'price':608800, 'goods_category':'123789', 'body':'苹果手机' } ] }]]></detail>
   <nonce_str>1add1a30ac87aa2db72f57a2375d8fec</nonce_str>
   <notify_url>http://wxpay.wxutil.com/pub_v2/pay/notify.v2.php</notify_url>
   <openid>oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</openid>
   <out_trade_no>1415659990</out_trade_no>
   <spbill_create_ip>14.23.150.211</spbill_create_ip>
   <total_fee>1</total_fee>
   <trade_type>JSAPI</trade_type>
</xml>";
           var dic = UtilityHelper.Xml2Dictionary(xml);
            var sign = UtilityHelper.GenerateWxPaySignature(dic, "aaa");
            Console.WriteLine(sign);
        }

        [TestMethod]
        public void ReplaceTest()
        {
            var source = "<xml></xml>";
            var after = source.Replace("<xml>", $"<{typeof(Model.Pay.WxPayResult).Name}>").Replace("</xml>", $"</{typeof(Model.Pay.WxPayResult).Name}>");
            Assert.AreEqual("<WxPayResult></WxPayResult>", after);
        }
    }
}
