using System;
using System.Collections.Generic;
using System.Text;
using Weixin.Netcore.Cache;
using Weixin.Netcore.Model.WeixinMessage;

namespace Weixin.Netcore.Core.Message
{
    /// <summary>
    /// 菜单按钮点击消息处理
    /// </summary>
    public class MenuClickMessageHandler : IClickEventHandler, IRepetValid
    {
        private readonly ICache _cache;
        private readonly IMessageReceive _messageReceive;

        public MenuClickMessageHandler(ICache cache, IMessageReceive messageReceive) 
        {
            _cache = cache;
            _messageReceive = messageReceive;
        }

        public string ClickEventHandler(Dictionary<string, string> dictionary)
        {
            throw new NotImplementedException();
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
