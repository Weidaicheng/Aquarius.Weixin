namespace Weixin.Netcore.Core.MaintainContainer
{
    /// <summary>
    /// Access Token容器
    /// </summary>
    public interface IAccessTokenContainer
    {
        /// <summary>
        /// 获取Access Token
        /// </summary>
        /// <returns></returns>
        string GetAccessToken();
    }
}
