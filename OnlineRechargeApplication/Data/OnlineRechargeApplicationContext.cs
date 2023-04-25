using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineRechargeApplication.Models;

namespace OnlineRechargeApplication.Data
{
    public class OnlineRechargeApplicationContext : DbContext
    {
        public OnlineRechargeApplicationContext (DbContextOptions<OnlineRechargeApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<OnlineRechargeApplication.Models.ServiceProviderModel> ServiceProviderModel { get; set; } = default!;

        public DbSet<OnlineRechargeApplication.Models.CustomerModel>? CustomerModel { get; set; }

        public DbSet<OnlineRechargeApplication.Models.AdminModel>? AdminModel { get; set; }

        public DbSet<OnlineRechargeApplication.Models.PlanModel>? PlanModel { get; set; }

        public DbSet<OnlineRechargeApplication.Models.SelectedPlanModel>? SelectedPlanModel { get; set; }
    }
}
