namespace Aquarius.Weixin.Entity.WeixinMenu.Button
{
    /// <summary>
    /// 拍照或图片选择按钮
    /// </summary>
    public class SinglePicPhotoOrAlbumButton : SingleButton
    {
        public SinglePicPhotoOrAlbumButton(string name) : base(name)
        {
            type = "pic_photo_or_album";
        }

        public SinglePicPhotoOrAlbumButton(string name, string key) : this(key)
        {
            this.key = key;
        }

        public string key { get; set; }
    }
}
