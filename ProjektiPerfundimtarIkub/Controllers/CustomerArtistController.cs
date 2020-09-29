using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using ProjektiPerfundimtarIkub.Extensions;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Controllers
{
    [Authorize(Roles ="Customer, Artist")]
    public class CustomerArtistController : Controller
    {
    IUsersService uService;
    public CustomerArtistController( IUsersService s)
    {
      this.uService = s;
    }
      
    public ActionResult VisitProfile(string id)
    {
      string currentid = User.Identity.GetUserId();

      if (currentid == id && User.IsInRole("Customer"))// ne kete rast useri do te ridrejtohet tek profili i tij
        return Redirect("/Customer/Profile/MyProfile");
      
      if (uService.CanVisitOrFollowUserProfile(currentid, id))
      {
        ApplicationUser userToVisit = uService.GetUserByUserId(id);
        if (userToVisit == null)
          return HttpNotFound();
        ViewBag.VisitorId = currentid;
        var nrFollowers = userToVisit.Followers.Count();
        var nrFollowing = userToVisit.Following.Count();
        ArtistProfileViewModel model = new ArtistProfileViewModel()
        {
          user = userToVisit,
          nrfollowers = nrFollowers,
          nrfollowing = nrFollowing
        };
        return View(model);
      }

      return HttpNotFound();
    }

    public ActionResult FollowUnfollowUser(string id)
    {
      string currentid = User.Identity.GetUserId();
      if (string.IsNullOrEmpty(id) || id == currentid)//nje user nuk mund te bej follow vetveten
        return HttpNotFound();
      uService.FollowUnfollowUser(currentid, id);
      return RedirectToAction("VisitProfile", new { id = id });

    }

    public ActionResult Timeline()
    {
      string currentid = User.Identity.GetUserId();
      ApplicationUser currentUser = uService.GetUserByUserId(currentid);
      //merren veprat e artisteve qe ndjek useri dhe renditen ne rendin zbrites sipas id per te nxjerre ne 
      //fillim te listes veprat e postuara se fundi
      var artworks = currentUser.Following.SelectMany(x => x.Artworks).OrderByDescending(x => x.ArtworkId);
      return View(artworks);
    }

    public ActionResult EditProfile()
    {
      ApplicationUser appUser = uService.GetUserByUserId(User.Identity.GetUserId());
      EditProfileViewModel model = new EditProfileViewModel
      {
        Username = appUser.UserName,
        Password = "",
        ConfirmPassword = "",
        Email = appUser.Email,
        Bio=appUser.Bio
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditProfile(EditProfileViewModel model)
    {
      if (ModelState.IsValid)
      {
        ApplicationUser appUser = uService.GetUserByUserId(User.Identity.GetUserId());
        //nqs fusha e passwordit eshte bosh ai nuk ndryshon
        if (!string.IsNullOrEmpty(model.Password))
        {
          var passwordHash = Crypto.HashPassword(model.Password);
          appUser.PasswordHash = passwordHash;
        }

        appUser.Email = model.Email;
        appUser.UserName = model.Username;
        appUser.Bio = model.Bio;
        if (uService.UpdateUser(appUser) == false)
        {
          ModelState.AddModelError("Err", "There is already a user with the same name or Email.");
          return View(model);
        }
        this.AddNotification("Profile edited.", NotificationType.SUCCESS);
        return RedirectToAction("VisitProfile", new { id=appUser.Id});
      }
      return View(model);

    }


  }
}