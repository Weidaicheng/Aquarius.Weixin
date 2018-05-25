namespace Aquarius.Weixin.Entity.Ticket
{
    /// <summary>
    /// 票据
    /// </summary>
    public class Ticket : Error
    {
        /// <summary>
        /// 票据
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int expires_in { get; set; }
    }
}
