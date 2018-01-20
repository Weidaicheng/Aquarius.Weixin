namespace Weixin.Netcore.Entity
{
    /// <summary>
    /// 基础设置
    /// </summary>
    public sealed class BaseSettings
    {
        /// <summary>
        /// 调试模式
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// AES Key
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户ApiKey
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// p12证书路径
        /// </summary>
        public string CertRoot { get; set; }

        /// <summary>
        /// p12证书密码
        /// </summary>
        public string CertPass { get; set; }
    }
}
