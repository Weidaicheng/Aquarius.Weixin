namespace Aquarius.Weixin.Entity.TemplateMessage
{
    /// <summary>
    /// 行业
    /// </summary>
    public class Industry
    {
        public IndustryDetail primary_industry { get; set; }
        public IndustryDetail secondary_industry { get; set; }
    }

    /// <summary>
    /// 行业详情
    /// </summary>
    public class IndustryDetail
    {
        public string first_class { get; set; }
        public string second_class { get; set; }
    }
}
