using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // For auto populating the Roles table using migrations
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
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

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser 
                {
                    Id = "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "Admin@1"),
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Admin",
                    DateOfBirth = new DateOnly(2000,01,30)
                });

            // associate the AdminRole with Admin User
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> 
            {
                RoleId = "25a693d9-0574-4b12-bfec-12e015fe453e",
                UserId = "b33f8105-61d2-4cdc-816e-3d96573cbd34"
            });
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}
