using System.ComponentModel.DataAnnotations;

namespace OnlineRechargeApplication.Models
{
    public class SelectedPlanModel
    {
        [Key]
        public int SelectedPlanId { get; set; }

        public int PlanId { get; set;}

        public int CustomerId { get; set; }
    }
}
