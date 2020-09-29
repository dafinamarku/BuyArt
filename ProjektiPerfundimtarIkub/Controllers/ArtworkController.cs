using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Controllers
{
    
    public class ArtworkController : Controller
    {
        IArtworksService artService;
        public ArtworkController(IArtworksService serv)
        {
          this.artService = serv;
        }

       public ActionResult Details(int id)
       {
          Artwork artw = artService.GetArtworkByArtworkId(id);
          if (artw == null)
            return HttpNotFound();
          ViewBag.NrOfLikes = artService.GetArtworkLikes(id).Count();
          ViewBag.DoesUserLikeArtwork = artService.UserLikesArtwork(id, User.Identity.GetUserId());
          return View(artw);
       }

      public ActionResult LikeOrUnlikeArtwork(int id)
      {
        Artwork currentArtwork = artService.GetArtworkByArtworkId(id);
        if (currentArtwork != null)
        {
          string currentUid = User.Identity.GetUserId();
          artService.LikeOrUnlikeArtwork(id, currentUid);
        }
      
        return RedirectToAction("Details", new { id = id });
      }

      public ActionResult FavoriteArtworks(int PageNo=1)
      {
        string currentUid = User.Identity.GetUserId();
        List<Artwork> favorites = artService.FavoriteArtworks(currentUid);
        /* Pagination */
        int NoOfRecordsPerPage = 6;
        int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(favorites.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
        int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
        ViewBag.PageNo = PageNo;
        ViewBag.NoOfPages = NoOfPages;
        favorites = favorites.Take(NoOfRecordsPerPage).Skip(NoOfRecordsToSkip).ToList();
      return View(favorites);
      }
    }
}