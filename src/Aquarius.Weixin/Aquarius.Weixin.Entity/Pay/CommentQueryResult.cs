using System;
using System.Collections.Generic;

namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 拉取评论结果
    /// </summary>
    public class CommentQueryResult
    {
        public int Offset { get; set; }
        public List<CommentDetail> CommentDetails { get; set; }
    }

    /// <summary>
    /// 评论详情
    /// </summary>
    public class CommentDetail
    {
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime? CommentTime { get; set; }

        /// <summary>
        /// 评论订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 评论星级
        /// </summary>
        public int? Star { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentDesc { get; set; }
    }
}
