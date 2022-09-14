using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class GroceryStoreDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUser user1 = new ApplicationUser { 
                UserName = "LenPayne",
                Email = "len.payne@lambtoncollege.ca",
                DateOfBirth = new DateTime(2000, 1, 1)
            };
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            IdentityRole role1 = new IdentityRole { Name = "Admin" };
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(role1);
            userManager.Create(user1, "testing1234");
            userManager.AddToRole(user1.Id, "Admin");

            GroceryItem g1 = new GroceryItem
            {
                Id = 1,
                Name = "Spaghetti",
                isAlochol = false,
                Department = "Dry Goods",
                Owner = user1
            };
            context.GroceryItems.Add(g1);
            user1.GroceryItems.Add(g1);

            context.GroceryItems.Add(new GroceryItem
            {
                Id = 2,
                Name = "Beer",
                isAlochol = true,
                Department = "Canned Goods"
            });

            base.Seed(context);
        }
    }
}