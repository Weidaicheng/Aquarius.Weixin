using System.Collections.Generic;
using Aquarius.Weixin.Entity.Enums;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 退款结果
    /// </summary>
    public class RefundResult
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 退款总金额,单位为分,可以做部分退款
        /// </summary>
        public int refund_fee { get; set; }

        /// <summary>
        /// 应结退款金额
        /// 去掉非充值代金券退款金额后的退款金额，退款金额=申请退款金额-非充值代金券退款金额，退款金额小于等于申请退款金额
        /// </summary>
        public int? settlement_refund_fee { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 应结订单金额，去掉非充值代金券金额后的订单总金额，应结订单金额=订单金额-非充值代金券金额，应结订单金额小于等于订单金额
        /// </summary>
        public int? settlement_total_fee { get; set; }

        /// <summary>
        /// 标价币种
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 标价币种
        /// </summary>
        public FeeType  FeeType { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金支付币种 
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 现金支付币种
        /// </summary>
        public FeeType CashFeeType { get; set; }

        /// <summary>
        /// 现金退款金额，单位为分
        /// </summary>
        public int? cash_refund_fee { get; set; }

        /// <summary>
        /// 代金券退款总金额
        /// 代金券退款金额小于等于退款金额，退款金额-代金券或立减优惠退款金额为现金
        /// </summary>
        public int? coupon_refund_fee { get; set; }

        /// <summary>
        /// 退款代金券使用数量
        /// </summary>
        public int? coupon_refund_count { get; set; }

        /// <summary>
        /// 代金券
        /// </summary>
        public List<Coupon> Coupons { get; set; }
    }
}
