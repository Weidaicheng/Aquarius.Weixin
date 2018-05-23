using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Core.Message;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.Message.Processer;
using Weixin.Netcore.Core.Message.Reply;
using Weixin.Netcore.Extensions.Message.Handler;
using Weixin.Netcore.Entity;
using Weixin.Netcore.Entity.WeixinMessage;
using Weixin.Netcore.Entity.Configuration;

namespace Weixin.Netcore.Core.Test
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void ClickEventReplyTextTest()
        {
            string xml = @"<xml>
                        <ToUserName><![CDATA[toUser]]></ToUserName>
                        <FromUserName><![CDATA[FromUser]]></FromUserName>
                        <CreateTime>123456789</CreateTime>
                        <MsgType><![CDATA[event]]></MsgType>
                        <Event><![CDATA[CLICK]]></Event>
                        <EventKey><![CDATA[TestKey]]></EventKey>
                        </xml>";

            IMessage message = MessageParser.ParseMessage(xml);
            BaseSettings baseSettings = new BaseSettings()
            {
                Debug = true
            };
            ICache cache = new RedisCache(new Microsoft.Extensions.Caching.Redis.RedisCache(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions() { Configuration = "127.0.0.1:6379,password=123456" }));
            MessageRepetHandler messageRepetHandler = new MessageRepetHandler(cache, baseSettings);
            IMessageReply<TextMessage> messageReply = new TextMessageReply();
            ClickEvtMessageHandlerBase clickEvtMessageHandler = new ClickEventReplyTextHandler(messageReply);
            MessageProcesser processer = new MessageProcesser(messageRepetHandler, new TextMessageHandlerBase(), new ImageMessageHandlerBase(), new VoiceMessageHandlerBase(), new VideoMessageHandlerBase(), new ShortVideoMessageHandlerBase(), new LocationMessageHandlerBase(), new LinkMessageHandlerBase(), new SubscribeEvtMessageHandlerBase(), new UnsubscribeEvtMessageHandlerBase(), new ScanEvtMessageHandlerBase(), new LocationEvtMessageHandlerBase(), clickEvtMessageHandler, new ScanSubscribeEvtMessageHandlerBase());
            Console.WriteLine(processer.ProcessMessage(message));
        }

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
