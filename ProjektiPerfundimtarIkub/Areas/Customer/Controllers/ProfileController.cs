using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Customer.Controllers
{   [Authorize(Roles ="Customer")]
    public class ProfileController : Controller
    {
        IUsersService uService;
        public ProfileController(IUsersService us)
        {
          this.uService = us;
        }
        public ActionResult MyProfile()
        {
          string currentid = User.Identity.GetUserId();
          ApplicationUser currentUser = uService.GetUserByUserId(currentid);
          var nrFollowing = currentUser.Following.Count();
          ArtistProfileViewModel model = new ArtistProfileViewModel()
          {
            user = currentUser,
            nrfollowing = nrFollowing,
            nrfollowers = 0 //sepse nje klient nuk mund te ndiqet nga ndonje user tjeter
          };
          return View(model);
        }
    }
}