using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Model.WeixinMessage;
using Weixin.Netcore.Utility;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 菜单按钮点击消息处理
    /// </summary>
    public class MenuClickMessageHandler : IClickEventHandler, IRepetValid
    {
        private readonly ICache _cache;
        private readonly IMessageReceive<ClickEvtMessage> _messageReceive;

        public MenuClickMessageHandler(ICache cache, IMessageReceive<ClickEvtMessage> messageReceive) 
        {
            _cache = cache;
            _messageReceive = messageReceive;
        }

        public string ClickEventHandler(string xml)
        {
            ClickEvtMessage receiveMsd = _messageReceive.GetEntity(xml);

            //消息重复
            if (!MessageRepetValid(receiveMsd.FromUserName + receiveMsd.CreateTime))
            {
                return "success";
            }

            TextMessage responseMsg = new TextMessage()
            {
                ToUserName = receiveMsd.FromUserName,
                FromUserName = receiveMsd.ToUserName,
                CreateTime = UtilityHelper.GetTimeStamp(),
                Content = $"你点击了{receiveMsd.EventKey}按钮"
            };

            return responseMsg.CreateXml();
        }

        public bool MessageRepetValid(string key)
        {
            var value = _cache.Get(key);

            if (string.IsNullOrEmpty(value))
            {
                _cache.Set(key, key, TimeSpan.FromSeconds(5));
                return true;
            }

            return false;
        }
    }
}
