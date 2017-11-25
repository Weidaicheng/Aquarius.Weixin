using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Weixin.Netcore.Core.Message;
using Weixin.Netcore.Core.Message.Handler;
using Weixin.Netcore.Core.Message.Processer;
using Weixin.Netcore.Core.MessageRepet;
using Weixin.Netcore.Extensions.Message.Handler;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Model.WeixinMessage.Reply;

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
            IMessageRepetHandler messageRepetHandler = new MessageRepetHandler(null);
            IMessageReply<TextMessage> messageReply = new TextMessageReply();
            IClickEvtMessageHandler clickEvtMessageHandler = new ClickEventReplyTextExtension(messageReply);
            IMessageProcesser processer = new ClickEvtMessageProcesser(messageRepetHandler, clickEvtMessageHandler);
            Console.WriteLine(processer.ProcessMessage(message));
        }
    }
}
