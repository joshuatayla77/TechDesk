using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TechDesk.Models;


namespace TechDesk.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options) { }


        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>().HasData(
             new Device { Id = 1, AssetTag = "ECC-0042",Name = "Dell Latitude 5440", Category = "Laptop", PurchaseYear = 2023, IsOnLoan = "yes" },
             new Device { Id = 2, AssetTag = "ECC-0007",Name = "HP Probook 450", Category = "Laptop", PurchaseYear = 2019, IsOnLoan = "no" },
             new Device { Id = 3, AssetTag = "ECC-0115",Name = "Ipad 9th Generation", Category = "Tablet", PurchaseYear = 2022, IsOnLoan = "yes" },
             new Device { Id = 4, AssetTag = "ECC-0090",Name = "Samsung Galaxy Tab", Category = "Tablet", PurchaseYear = 2021, IsOnLoan = "no" },
             new Device { Id = 5, AssetTag = "ECC-0023",Name = "Lenovo ThinkPad E14", Category = "Laptop", PurchaseYear = 2017, IsOnLoan = "no" });


            
        }
    }
}

