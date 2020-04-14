using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ElectricityBillGeneration.Models
{
    public class EBBillContext : DbContext
    {
        public EBBillContext() : base("name=ElectricityBillConnectionString")
        {

        }
        public DbSet<ElectricityBill> ElectricityBill { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}