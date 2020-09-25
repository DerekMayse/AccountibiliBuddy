using System;
using System.Collections.Generic;
using System.Text;
using AccountibiliBuddy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountibiliBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Goal> Goal { get; set; }

        public DbSet<GoalType> GoalType { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Admina",
                LastName = "Straytor",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<GoalType>().HasData(
               new GoalType()
               {
                   GoalTypeId = 1,
                   Type = "Daily Goal",
                   PointValue = 1
               },
               new GoalType()
               {
                   GoalTypeId = 2,
                   Type = "Weekly Goal",
                   PointValue = 5
               },
               new GoalType()
               {
                   GoalTypeId = 3,
                   Type = "Long Term Goal",
                   PointValue = 10
               }
               );
             base.OnModelCreating(modelBuilder);
        }
    }
}
