using Weixin.Netcore.Model.Enums;

namespace Weixin.Netcore.Model.Pay
{
    /// <summary>
    /// 代金券
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 代金券类型
        /// </summary>
        public CouponType CouponType { get; set; }

        /// <summary>
        /// 代金券Id
        /// </summary>
        public string CouponId { get; set; }

        /// <summary>
        /// 单个代金券支付金额
        /// </summary>
        public int CouponFee { get; set; }
    }
}
