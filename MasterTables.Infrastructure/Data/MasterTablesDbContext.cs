using MasterTables.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterTables.Infrastructure.Data
{
    public class MasterTablesDbContext : DbContext
    {
        public MasterTablesDbContext(DbContextOptions<MasterTablesDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Vendor>().HasKey(p => p.Id);
            modelBuilder.Entity<Customer>().HasKey(p => p.Id);
            modelBuilder.Entity<Tax>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }
    }

}
