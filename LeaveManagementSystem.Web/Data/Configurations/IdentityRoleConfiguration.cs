using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "72ab4a98-c2d3-4a97-b3e7-73d12f5ab1c1",
                    Name = "Employee",
                    NormalizedName = "Employee"
                },
                new IdentityRole
                {
                    Id = "c8a7359c-f6af-4355-9310-3f394e8f37f7",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "25a693d9-0574-4b12-bfec-12e015fe453e",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
