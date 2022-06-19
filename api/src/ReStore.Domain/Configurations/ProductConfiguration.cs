using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReStore.Domain.Entities;

namespace ReStore.Domain.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
                builder.Property(x => x.Brand).IsRequired();
                builder.Property(x => x.PictureUrl).IsRequired();
                builder.Property(x => x.QuantityInStock).IsRequired();
        }
}
