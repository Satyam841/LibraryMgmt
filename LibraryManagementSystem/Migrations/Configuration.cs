namespace LibraryManagementSystem.Migrations
{
    using LibraryManagementSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryManagementSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryManagementSystem.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            var user = new ApplicationUser()
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                PhoneNumber = "9876543210",
                LockoutEnabled = true
            };

            var userResult = userManager.Create(user, "Admin@123");

            if (userResult.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
