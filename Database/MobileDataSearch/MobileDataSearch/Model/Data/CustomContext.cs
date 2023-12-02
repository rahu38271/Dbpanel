using Microsoft.EntityFrameworkCore;
using MobileDataSearch.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Model.Data
{
    public class CustomContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string MasterDBconstr = "Server=184.168.194.78;Database=EnrolleMasterQA;User Id=EnrolleMasterQA; Password=EnrolleMasterQA@123;pooling=false;";
                string MasterDBconstr = "Data Source=192.168.29.92;Database=IndiaData;User Id=Maharashtra_New; Password=ElectionAlerts@1112;pooling=false;Connection Timeout=3600";
                optionsBuilder.UseSqlServer(MasterDBconstr,opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds));
                //optionsBuilder.UseSqlServer(MasterDBconstr);
            }

        }
        public DbSet<Maharashtra> Maharashtras { get; set; }
        public DbSet<MaharashtraDTO> MaharashtraDTOs { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<TotalCountbyPincode> TotalCountbyPincodes { get; set; }
        public DbSet<CustomerName> CustomerNames { get; set; }
        public DbSet<CustomerCount> CustomerCounts { get; set; }
        public DbSet<MobileMatch> MobileMatches { get; set; }
        public DbSet<MobilePinCodeCount> mobilePinCodes { get; set; }
        public DbSet<District> Districts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maharashtra>().HasNoKey();
            modelBuilder.Entity<MaharashtraDTO>().HasNoKey();
            modelBuilder.Entity<TotalCountbyPincode>().HasNoKey();
            modelBuilder.Entity<CustomerCount>().HasNoKey();
            modelBuilder.Entity<MobileMatch>().HasNoKey();
            modelBuilder.Entity<MobilePinCodeCount>().HasNoKey();
            modelBuilder.Entity<District>().HasNoKey();
        }
    }
}
