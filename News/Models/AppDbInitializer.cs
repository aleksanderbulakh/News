using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Створюємо ролі для викладача, студента та адміна
            var role1 = new IdentityRole { Name = "admin" }; 
            var role2 = new IdentityRole { Name = "editor" };
            var role3 = new IdentityRole { Name = "journalist" };

            //Додаємо ролі в БД
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            //Створюємо адміністратора
            var admin = new ApplicationUser { UserName = "Admin" };
            string password = "qwerty1234";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                //В разі успішного створення адміністратора, додаємо йому нові ролі.
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
            }

            base.Seed(context);
        }
    }
}