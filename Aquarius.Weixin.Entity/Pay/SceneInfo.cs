namespace Aquarius.Weixin.Entity.Pay
{
    /// <summary>
    /// 场景信息
    /// </summary>
    public class SceneInfo
    {
        /// <summary>
        /// 门店id
        /// (MaxLength:32)
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 门店名称 
        /// (MaxLength:64)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 门店行政区划码
        /// (MaxLength:6)
        /// </summary>
        public string area_code { get; set; }

        /// <summary>
        /// 门店详细地址
        /// (MaxLength:128)
        /// </summary>
        public string address { get; set; }

        #region override
        public override string ToString()
        {
            if(string.IsNullOrEmpty(id) && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(area_code) && string.IsNullOrEmpty(address))
            {
                return null;
            }

            return $"{{\"id\":\"{id}\",\"name\":\"{name}\",\"area_code\":\"{area_code}\",\"address\":\"{address}\"}}";
        }
        #endregion
    }
}
