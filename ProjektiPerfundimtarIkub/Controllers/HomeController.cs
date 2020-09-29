using BuyArt.DataLayer;
using BuyArt.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Controllers
{
  public class HomeController : Controller
  {
    

    public ActionResult Index(string search)
    {
      ProjectContext db = new ProjectContext();
      string currentUid = User.Identity.GetUserId();
      ApplicationUserStore ustore = new ApplicationUserStore(db);
      ApplicationUserManager uManager = new ApplicationUserManager(ustore);
      HomePageViewModel homeModel = new HomePageViewModel();
      List<ApplicationUser> artists = null;
      List<Artwork> artworks = null;
      List<Artwork> mostLiked = db.Artworks.Where(x=>x.AvailabilityStatus==true)
             .OrderByDescending(x => x.Comments.Where(c => c.CommentOrLike == "like" && c.Like == true)
             .Count()).Take(10).ToList();
      if (!string.IsNullOrEmpty(search))
      {
        search = search.ToUpper();
        //UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        //artists = db.Users.Where(u => u.UserName.ToUpper().Contains(search) && uManager.IsInRole(u.Id, "Artist")==true).ToList();
        artists = db.Users.Where(u => u.UserName.ToUpper().Contains(search)).ToList();
        artworks = db.Artworks.Where(a => a.Title.ToUpper().Contains(search)
          || (a.Description!=null&& a.Description.ToUpper().Contains(search))).ToList();
      }
    
      homeModel.artists = artists;
      homeModel.artworks = artworks;
      homeModel.TenMostLikedArtworks = mostLiked;
      homeModel.categories = db.Categories.Select(n => n.CategoryName).ToList();
      homeModel.styles = db.Styles.Select(n => n.StyleName).ToList();
      homeModel.subjects = db.Subjects.Select(n => n.SubjectName).ToList();
      return View(homeModel);
    }

    public ActionResult About()
    {
      return View();
    }
  }
}