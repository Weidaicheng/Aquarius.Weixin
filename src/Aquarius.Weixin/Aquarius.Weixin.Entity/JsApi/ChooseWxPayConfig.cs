namespace Aquarius.Weixin.Entity.JsApi
{
    /// <summary>
    /// chooseWXPay方法配置
    /// </summary>
    public class ChooseWxPayConfig
    {
        /// <summary>
        /// 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 支付签名随机串，不长于 32 位
        /// </summary>
        public string nonceStr { get; set; }

        private string _package;
        /// <summary>
        /// 统一支付接口返回的prepay_id参数值，
        /// prepay_id已在内部封装，直接传prepay_id即可
        /// </summary>
        public string package
        {
            get
            {
                return _package;
            }
            set
            {
                _package = $"prepay_id={value}";
            }
        }

        /// <summary>
        /// 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
        /// </summary>
        public string signType { get; set; }

        /// <summary>
        /// 支付签名
        /// </summary>
        public string paySign { get; set; }
    }
}
