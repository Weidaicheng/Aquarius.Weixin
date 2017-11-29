using Weixin.Netcore.Model.Enums;

namespace Weixin.Netcore.Model
{
    /// <summary>
    /// 基础设置
    /// </summary>
    public sealed class BaseSettings
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 认证类型
        /// </summary>
        public AuthType AuthType { get; set; }
    }
}
