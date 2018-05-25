namespace Aquarius.Weixin.Entity.UserManage
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class UserList
    {
        public int count { get; set; }
        public OpenIds data { get; set; }
        public string next_openid { get; set; }
    }
}
