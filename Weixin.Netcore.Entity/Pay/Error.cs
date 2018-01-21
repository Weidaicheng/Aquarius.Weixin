namespace Weixin.Netcore.Entity.Pay
{
    /// <summary>
    /// 微信支付错误
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }
    }
}
