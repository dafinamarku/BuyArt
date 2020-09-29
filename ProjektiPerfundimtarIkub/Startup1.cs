using System;
using System.Threading.Tasks;
using BuyArt.DataLayer;
using BuyArt.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ProjektiPerfundimtarIkub.Startup1))]

namespace ProjektiPerfundimtarIkub
{
  public class Startup1
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login") });
    }

    public void CreateRolesAndUsers()
    {
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
      var appDbContext = new ProjectContext();
      var appUserStore = new ApplicationUserStore(appDbContext);
      var userManager = new ApplicationUserManager(appUserStore);

      //Create Admin Role
      if (roleManager.RoleExists("Admin")==null)
      {
        var role = new IdentityRole();
        role.Name = "Admin";
        roleManager.Create(role);
      }

      //Create Artist Role
      if (roleManager.RoleExists("Artist") == null)
      {
        var role = new IdentityRole();
        role.Name = "Artist";
        roleManager.Create(role);
      }

      //Create Customer
      if (roleManager.RoleExists("Customer") == null)
      {
        var role = new IdentityRole();
        role.Name = "Customer";
        roleManager.Create(role);
      }

      //Create Admin User
      if (userManager.FindByName("admin") == null)
      {
        var user = new ApplicationUser();
        user.UserName = "admin";
        user.Email = "admin@gmail.com";
        string userPassword = "admin123";
        var chkUser = userManager.Create(user, userPassword);
        if (chkUser.Succeeded)
        {
          userManager.AddToRole(user.Id, "Admin");
        }
      }
    }
  }
}
