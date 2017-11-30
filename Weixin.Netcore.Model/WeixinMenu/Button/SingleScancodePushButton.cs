﻿namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// 调起扫一扫工具按钮
    /// </summary>
    public class SingleScancodePushButton : SingleButton
    {
        public SingleScancodePushButton(string name) : base(name)
        {
            type = "scancode_push";
        }

        public string key { get; set; }
    }
}
