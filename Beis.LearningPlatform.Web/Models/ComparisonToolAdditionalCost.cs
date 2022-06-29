namespace Beis.LearningPlatform.Web.Models
{
    public class ComparisonToolAdditionalCost
    {
        public ComparisonToolAdditionalCost() { }
        public ComparisonToolAdditionalCost(additional_cost x)
        {
            CostDescription = x.additional_cost_desc?.additional_costDesc;
            CostAndFrequency = x.additional_cost_display_value;
            Mandatory = x.additional_cost_mandatory_flag;
        }

        public string CostDescription { get; set; }
        public string CostAndFrequency { get; set; }
        public bool Mandatory { get; set; }
    }
}
