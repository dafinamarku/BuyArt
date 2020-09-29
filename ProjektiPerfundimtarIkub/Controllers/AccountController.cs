using BuyArt.DataLayer;
using BuyArt.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Controllers
{
    public class AccountController : Controller
    {
    // GET: Account/Register
    public ActionResult Register()
    {
      return View();
    }

    // POST: Account/Register
    [HttpPost]
    public ActionResult Register(RegisterViewModel rvm)
    {
      if (ModelState.IsValid)
      {
        //register
        var appDbContext = new ProjectContext();
        List<ApplicationUser> searchUsers = appDbContext.Users.Where(m => m.UserName == rvm.Username || m.Email == rvm.Email).ToList();
        if (searchUsers.Count > 0)
        {
          ModelState.AddModelError("My Error", "A user with the same Username OR Email already exists.");
          return View();
        }
        var userStore = new ApplicationUserStore(appDbContext);
        var userManager = new ApplicationUserManager(userStore);
        var passwordHash = Crypto.HashPassword(rvm.Password);
        ApplicationUser user = new ApplicationUser() { Email = rvm.Email, UserName = rvm.Username, PasswordHash = passwordHash, Bio=rvm.Bio };
        IdentityResult result = userManager.Create(user);

        if (result.Succeeded)
        {
          if(rvm.ProfileType=="Customer")
            userManager.AddToRole(user.Id, "Customer");
          else
            userManager.AddToRole(user.Id, "Artist");

          return RedirectToAction("Login");
        }
        return RedirectToAction("Index", "Home");
      }
      else
      {
        return View();
      }
    }

    // GET: Account/Login
    public ActionResult Login()
    {
      return View();
    }

    // POST: Account/Login
    [HttpPost]
    public ActionResult Login(LoginViewModel lvm)
    {
      if (ModelState.IsValid)
      {
        //login
        var appDbContext = new ProjectContext();
        var userStore = new ApplicationUserStore(appDbContext);
        var userManager = new ApplicationUserManager(userStore);
        var user = userManager.Find(lvm.Username, lvm.Password);
        if (user != null)
        {
          //login
          var authenticationManager = HttpContext.GetOwinContext().Authentication;
          var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
          authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

          if (userManager.IsInRole(user.Id, "Admin"))
          {
            return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
          }
          else if(userManager.IsInRole(user.Id, "Artist"))
          {
            return Redirect("/CustomerArtist/VisitProfile/" + user.Id);
          }
          else //Customer
          {
            return Redirect("/Customer/Profile/MyProfile");
          }
        }
        else
        {
          ModelState.AddModelError("myerror", "Invalid username or password");
          return View();
        }
      }
       ModelState.AddModelError("myerror", "");
      return View();

    }

    // GET: Account/Logout
    public ActionResult Logout()
    {
      var authenticationManager = HttpContext.GetOwinContext().Authentication;
      authenticationManager.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}