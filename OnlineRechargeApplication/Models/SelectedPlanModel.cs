using System.ComponentModel.DataAnnotations;

namespace OnlineRechargeApplication.Models
{
    public class SelectedPlanModel
    {
        [Key]
        public int SelectedPlanId { get; set; }

        public PlanModel plan { get; set; }

        public CustomerModel customer { get; set; }
    }
}
