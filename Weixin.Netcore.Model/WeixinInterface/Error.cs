﻿namespace Weixin.Netcore.Model.WeixinInterface
{
    /// <summary>
    /// 微信错误返回
    /// </summary>
    public class Error
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
