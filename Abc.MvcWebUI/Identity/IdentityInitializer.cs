using Abc.MvcWebUI.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(i => i.Name == "Yönetici"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "Yönetici",
                    Description = "Yönetici Rolü"
                };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "Kullanıcı"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole()
                {
                    Name = "Kullanıcı",
                    Description = "Kullanıcı Rolü"
                };
                manager.Create(role);
            }


            if (!context.Users.Any(i => i.Name == "berkaykanca"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name="Berkay",
                    Surname="Kanca",
                    UserName="berkaykanca",
                    Email="berkaykanca@hotmail.com"
                };
                manager.Create(user,"123456789");
                manager.AddToRole(user.Id,"Yönetici");
                manager.AddToRole(user.Id, "Kullanıcı");
            }

            if (!context.Users.Any(i => i.Name == "berfinkanca"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Berfin",
                    Surname = "Kanca",
                    UserName = "berfinkanca",
                    Email = "brkyknc@hotmail.com"
                };
                manager.Create(user, "123456789");
                manager.AddToRole(user.Id, "Kullanıcı");
            }

            base.Seed(context);
        }
    }
}