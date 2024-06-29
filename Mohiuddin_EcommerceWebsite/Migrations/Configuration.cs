namespace Mohiuddin_EcommerceWebsite.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Mohiuddin_EcommerceWebsite.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mohiuddin_EcommerceWebsite.DAL.MohiuddinEcommerceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Mohiuddin_EcommerceWebsite.DAL.MohiuddinEcommerceContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }


            if (userManager.FindByName("admin@example.com") == null)
            {
                var adminUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                var result = userManager.Create(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        
                    }
                }
            }

            if (userManager.FindByName("customer@example.com") == null)
            {
                var customerUser = new ApplicationUser { UserName = "customer@example.com", Email = "customer@example.com" };
                var result = userManager.Create(customerUser, "Customer@123");

                if (result.Succeeded)
                {
                    userManager.AddToRole(customerUser.Id, "Customer");
                }
                else
                {
                    // Handle user creation failure
                    foreach (var error in result.Errors)
                    {
                        // Log or handle each error as needed
                    }
                }
            }
        }
    }
}
