namespace Aquarius.Weixin.Entity.Argument
{
    /// <summary>
    /// 批量获取用户信息参数类
    /// </summary>
    public class BatchGetUserInfoArg
    {
        public string openid { get; set; }
        public string lang { get; set; }
    }
}
