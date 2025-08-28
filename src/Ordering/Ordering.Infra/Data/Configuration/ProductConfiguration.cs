using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;

namespace Ordeing.Infra.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       builder.HasKey(p => p.Id);

       builder.Property(o=> o.Id).HasConversion(
        ProductId => ProductId.Value,
        dbId => ProductId.Of(dbId)
       );

       builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

    }
}