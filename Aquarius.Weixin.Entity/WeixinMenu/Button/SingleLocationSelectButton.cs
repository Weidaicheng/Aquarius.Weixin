namespace Aquarius.Weixin.Entity.WeixinMenu.Button
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

        public SingleLocationSelectButton(string name, string key) : this(name)
        {
            this.key = key;
        }

        public string key { get; set; }
    }
}
