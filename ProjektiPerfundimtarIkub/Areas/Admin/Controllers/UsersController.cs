using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        ProjectContext db;
        IUsersService uService;

        public UsersController(IUsersService us)
        {
          this.db = new ProjectContext();
          this.uService = us;
        }
        // GET: Admin/Users
        public ActionResult Index()
        {
            //admini i logguar nuk perfshihet ne listen e userave qe do ti shfaqen ketij admini
            string loggedUserId = User.Identity.GetUserId();
            return View(uService.GetUsers(loggedUserId));
        }

        public ActionResult Details(string id)
        {
          ApplicationUser user = uService.GetUserByUserId(id);
          List<string> roles = new List<string>();
          foreach(var i in user.Roles)
          {
            string role = db.Roles.Where(r => r.Id == i.RoleId).Select(r => r.Name).First();
            roles.Add(role);
          }
          ViewBag.Roles = roles;
          if (user != null)
            return View(user);
          else
            return new HttpNotFoundResult();
        }

        public ActionResult Create()
        {
          return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterViewModel rvm)
        {
          if (ModelState.IsValid)
          {
              ApplicationUser usr = new ApplicationUser
              {
                UserName=rvm.Username,
                Email=rvm.Email,
                PasswordHash= Crypto.HashPassword(rvm.Password),
                Bio=rvm.Bio
              };
              var profileType = rvm.ProfileType;
              if (profileType == "Artist" || profileType == "Admin" || profileType == "Cusomer")
              {
                bool result = uService.InsertUser(usr, profileType);
                if (result == false)//useri nuk eshte shtuar ne db
                {
                  ModelState.AddModelError("My Error", "A user with the same Username OR Email already exists.");
                  return View(rvm);
                }
                else
                {
                  return RedirectToAction("Index");
                }
              }
              else
              {
                ModelState.AddModelError("My Error", "Wrong Profile Type.");
                return View(rvm);
              }
          }
          else
          {
            return View(rvm);
          }
        }

        [HttpPost]
        public ActionResult Delete(string id, ApplicationUser u)
        {
          bool result = uService.DeleteUser(id);
          if (result)
            return RedirectToAction("Index");
          else
          {
            ModelState.AddModelError("MyError", "Delete action didn't complete succesfully.");
            return View(u);
          }
        }
  }
}