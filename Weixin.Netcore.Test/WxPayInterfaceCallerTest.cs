using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Core.InterfaceCaller;
using Weixin.Netcore.Model.Pay;

namespace Weixin.Netcore.Test
{
    [TestClass]
    public class WxPayInterfaceCallerTest
    {
        [TestMethod]
        public void QueryOrderTest()
        {
            WxPayInterfaceCaller interfaceCaller = new WxPayInterfaceCaller(new RestClient());
            var orderQuery = new OrderQuery()
            {
                appid = "wx2421b1c4370ec43b",
                mch_id = "10000100",
                nonce_str = "ec2316275641faa3aacf3cc599e8730f",
                transaction_id = "1008450740201411110005820873",
                sign = "FDD167FAA73459FD921B144BAF4F4CA2"
            };
            interfaceCaller.QueryOrder(orderQuery);
        }
    }
}
