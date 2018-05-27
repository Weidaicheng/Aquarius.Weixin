namespace Aquarius.Weixin.Entity.Configuration
{
    /// <summary>
    /// 基础设置
    /// </summary>
    public sealed class BaseSettings
    {
        #region field
        private bool debug;
        private bool isRepetValid;
        #endregion

        /// <summary>
        /// 调试模式
        /// </summary>
        public bool Debug
        {
            get
            {
                return debug;
            }
            set
            {
                debug = value;
                if (value)
                    isRepetValid = false;
            }
        }

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

        /// <summary>
        /// 是否启用消息重复验证
        /// </summary>
        public bool IsRepetValid
        {
            get
            {
                return isRepetValid;
            }
            set
            {
                if (debug)
                {
                    isRepetValid = false;
                    return;
                }
                isRepetValid = value;
            }
        }
    }
}
