using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Aquarius.Weixin.Cache;
using Aquarius.Weixin.Core.Message;
using Aquarius.Weixin.Core.Message.Handler;
using Aquarius.Weixin.Core.Message.Processer;
using Aquarius.Weixin.Core.Message.Reply;
using Aquarius.Weixin.Entity.WeixinMessage;
using Aquarius.Weixin.Entity.Configuration;

namespace Aquarius.Weixin.Core.Test
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void MessageRepetTest()
        {
            BaseSettings baseSettings = new BaseSettings()
            {
                Debug = true
            };
            MessageRepetHandler messageRepetHandler = new MessageRepetHandler(null, baseSettings);
            Assert.IsTrue(messageRepetHandler.MessageRepetValid("key"));
        }
    }
}
