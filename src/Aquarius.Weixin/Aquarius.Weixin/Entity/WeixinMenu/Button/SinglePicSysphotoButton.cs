namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 调起系统相机按钮
    /// </summary>
    public class SinglePicSysPhotoButton : SingleButton
    {
        public SinglePicSysPhotoButton(string name) : base(name)
        {
            type = "pic_sysphoto";
        }

        public SinglePicSysPhotoButton(string name, string key) : this(name)
        {
            this.key = key;
        }

        public string key { get; set; }
    }
}
