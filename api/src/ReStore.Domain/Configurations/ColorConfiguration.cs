//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using ReStore.Domain.Entities;

//namespace ReStore.Domain.Configurations;

//public class ColorConfiguration : IEntityTypeConfiguration<Color>
//{
//        public void Configure(EntityTypeBuilder<Color> builder)
//        {
//                builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

//                #region ForeingKey

//                builder.HasMany(x => x.Products)
//                            .WithOne(x => x.Color)
//                            .HasForeignKey(x => x.ColorId)
//                            .IsRequired(false)
//                            .OnDelete(DeleteBehavior.Restrict);

//                #endregion

//        }
//}
