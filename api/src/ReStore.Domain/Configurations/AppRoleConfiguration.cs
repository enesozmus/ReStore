using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReStore.Domain.Entities;

namespace ReStore.Domain.Configurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
                #region SeedData

                var role1 = new AppRole() { Id = 1, Name = "Member", NormalizedName = "MEMBER" };
                var role2 = new AppRole() { Id = 2, Name = "Admin", NormalizedName = "ADMIN" };


                builder.HasData(role1, role2);

                #endregion
        }
}
