using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReStore.Domain.Entities;

namespace ReStore.Domain.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
        public void Configure(EntityTypeBuilder<Category> builder)
        {
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                #region ForeingKey

                builder.HasMany(x => x.Products)
                            .WithOne(x => x.Category)
                            .HasForeignKey(x => x.CategoryId)
                            .OnDelete(DeleteBehavior.Restrict);

                #endregion
        }
}
