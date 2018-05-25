namespace Aquarius.Weixin.Entity.TemplateMessage
{
    /// <summary>
    /// 模板列表
    /// </summary>
    public class TemplateList
    {
        public Template[] template_list { get; set; }
    }

    /// <summary>
    /// 模板
    /// </summary>
    public class Template
    {
        /// <summary>
        /// 模板Id
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }

        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
    }
}
