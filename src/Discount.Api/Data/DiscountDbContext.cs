using Discount.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class DiscountDbContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountDbContext(DbContextOptions<DiscountDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Coupon>().HasData(
        new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
        new Coupon { Id = 2, ProductName = "Samsung S24", Description = "Samsung Discount", Amount = 100 }
    );
    }
}