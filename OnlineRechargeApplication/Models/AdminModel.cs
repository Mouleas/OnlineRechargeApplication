using System.ComponentModel.DataAnnotations;

namespace OnlineRechargeApplication.Models
{
    public class AdminModel
    {
        [Key]
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public string AdminPassword { get; set; }

    }
}
