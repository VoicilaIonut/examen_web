

using examen_web.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using examen_web.Model.One_to_Many;

namespace examen_web.Data
{
    public class AppDbContext : DbContext
    {
        // One to Many
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<OrderProduct> OrdersProducts { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=database_test;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Orders)
                        .WithOne(u => u.User);

            modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrdersProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrdersProducts)
                .HasForeignKey(op => op.ProductId);

        }

    }
}
