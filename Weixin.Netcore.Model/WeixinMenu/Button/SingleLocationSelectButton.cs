namespace Weixin.Netcore.Model.WeixinMenu.Button
{
    /// <summary>
    /// 调起地理位置选择按钮
    /// </summary>
    public class SingleLocationSelectButton : SingleButton
    {
        public SingleLocationSelectButton(string name) : base(name)
        {
            type = "location_select";
        }

        public string key { get; set; }
    }
}
