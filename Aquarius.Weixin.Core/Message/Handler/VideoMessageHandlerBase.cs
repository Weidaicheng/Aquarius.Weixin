﻿using Aquarius.Weixin.Entity.WeixinMessage;

namespace Aquarius.Weixin.Core.Message.Handler
{
    /// <summary>
    /// 视频消息处理
    /// </summary>
    public class VideoMessageHandlerBase
    {
        public virtual string VideoMessageHandler(VideoMessage message)
        {
            return "success";
        }
    }
}
