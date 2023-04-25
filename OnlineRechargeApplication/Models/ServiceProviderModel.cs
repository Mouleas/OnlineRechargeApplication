using System.ComponentModel.DataAnnotations;

namespace OnlineRechargeApplication.Models
{
    public class ServiceProviderModel
    {
        [Key]
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
    }
}
