using System.ComponentModel.DataAnnotations;

namespace OnlineRechargeApplication.Models
{
    public class PlanModel
    {
        [Key]
        public int PlanId { get; set; }

        public string PlanName { get; set; }

        public int PlanPrice { get; set; }

        public int PlanValidity { get; set; }

        public string PlanDescription { get; set; }

        public ServiceProviderModel ServiceProvider { get; set; }

    }
}
