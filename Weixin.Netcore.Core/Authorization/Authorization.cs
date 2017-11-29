using Weixin.Netcore.Model.Enums;

namespace Weixin.Netcore.Core.Authorization
{
    /// <summary>
    /// 认证
    /// todo:当前未进行任何转换与处理
    /// </summary>
    public class Authorization : IAuthorization
    {
        public string GetToken(string id, AuthType type)
        {
            return id;
        }

        public string GetUserId(string token, AuthType type)
        {
            return token;
        }
    }
}
