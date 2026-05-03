using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            // associate the AdminRole with Admin User
            builder.HasData(new IdentityUserRole<string>
            {
                RoleId = "25a693d9-0574-4b12-bfec-12e015fe453e",
                UserId = "b33f8105-61d2-4cdc-816e-3d96573cbd34"
            });
        }
    }
}
