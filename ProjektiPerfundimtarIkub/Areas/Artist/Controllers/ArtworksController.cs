using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using ProjektiPerfundimtarIkub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjektiPerfundimtarIkub.Extensions;

namespace ProjektiPerfundimtarIkub.Areas.Artist.Controllers
{
    [Authorize(Roles ="Artist")]
    public class ArtworksController : Controller
    {
        ProjectContext db;
        IArtworksService artService;

        public ArtworksController(IArtworksService s)
        {
          this.db = new ProjectContext();
          this.artService = s;
        }

        public ActionResult Create()
        {
          ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
          ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
          List<Style> styles=db.Styles.ToList();
          ArtworkCreateUpdateViewModel model = new ArtworkCreateUpdateViewModel();
          model.TheArtworkStyles = styles;
          model.TheArtwork = new Artwork();
          return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file,ArtworkCreateUpdateViewModel artwork)
        {
              FileClass f = new FileClass();
              if (file != null)
              {
                if (!f.ValidateFile(file))
                {
                  ModelState.AddModelError("Photo", "File must be image.");
                }
              }
              else
              {
                ModelState.AddModelError("Photo", "Choose an image.");
              }
              if (ModelState.IsValid)
              {
                try
                {
                  f.StoreFile(file);
                }
                catch (Exception)
                {
                  ModelState.AddModelError("Photo", "Could not store file. Try again.");
                }
                Artwork artw = new Artwork
                {
                  Title = artwork.TheArtwork.Title,
                  Description = artwork.TheArtwork.Description,
                  Photo = file.FileName,
                  Price = artwork.TheArtwork.Price,
                  Height = artwork.TheArtwork.Height,
                  Width = artwork.TheArtwork.Width,
                  CategoryId = artwork.TheArtwork.CategoryId,
                  SubjectId = artwork.TheArtwork.SubjectId,
                  AvailabilityStatus = true,
                  ArtistId = User.Identity.GetUserId(),
                  ArtworkStyles = artService.GetArtworkStyles(artwork.TheArtworkStyles)
                };
                //kthen false nqs ka nje punim tjeter me te njejtin titull
                if (artService.InsertArtwork(artw))
                {
                  this.AddNotification("Artwork created.", NotificationType.SUCCESS);
                  return Redirect("/CustomerArtist/VisitProfile/" + User.Identity.GetUserId());
                }
                else
                {
                  ModelState.AddModelError("SameTitle", "There is already an artwork with the same title.");
                }
              } 
          ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.TheArtwork.CategoryId);
          ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", artwork.TheArtwork.SubjectId);
          return View(artwork);
        }


        public ActionResult Edit(int id)
        {
          var currentUid = User.Identity.GetUserId();
          //faqet e editimit mund te aksesohen vetem nga autoret e veprave
          if (artService.CanUpdateOrDelete(currentUid, id)==false)
          {
            return HttpNotFound();
          }
          Artwork artwork = artService.GetArtworkByArtworkId(id);
          if (artwork.AvailabilityStatus == false)
            return HttpNotFound();
          ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", artwork.CategoryId);
          ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", artwork.SubjectId);
          List<Style> styles = db.Styles.ToList();
          foreach(var i in artwork.ArtworkStyles)
          {
            styles.Where(x=>x.StyleId==i.StyleId).FirstOrDefault().IsSelected = true;
          }
          ArtworkCreateUpdateViewModel model = new ArtworkCreateUpdateViewModel();
          model.TheArtworkStyles = styles;
          model.TheArtwork = artwork;
          return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, ArtworkCreateUpdateViewModel artwork)
        {
          FileClass f = new FileClass();
          if (file != null && !f.ValidateFile(file))
          {
              ModelState.AddModelError("Photo", "File must be image.");
          }
          if (ModelState.IsValid)
          {
            Artwork artw = new Artwork();
            if (file != null)
            {
              try
              {
                f.StoreFile(file);
                artw.Photo = file.FileName;
              }
              catch (Exception)
              {
                ModelState.AddModelError("Photo", "Could not store file. Try again.");
                return View(artwork);
              }
            }
            else
            {
              //id e vepres merret nga url e kerkeses,  dhe mqs nuk eshte zgjedhur nje imazh i ri, emri i fotos
              //nuk ndryshon
              int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
              artw.Photo = artService.GetArtworkByArtworkId(id).Photo;

            }
            artw.ArtworkId = artwork.TheArtwork.ArtworkId;
            artw.ArtistId = User.Identity.GetUserId();
            artw.Title = artwork.TheArtwork.Title;
            artw.Description = artwork.TheArtwork.Description;
            artw.Price = artwork.TheArtwork.Price;
            artw.Height = artwork.TheArtwork.Height;
            artw.Width = artwork.TheArtwork.Width;
            artw.CategoryId = artwork.TheArtwork.CategoryId;
            artw.SubjectId = artwork.TheArtwork.SubjectId;
            artw.ArtworkStyles = artService.GetArtworkStyles(artwork.TheArtworkStyles);
            if (artService.UpdateArtwork(artw))
            {
              this.AddNotification("Artwork edited.", NotificationType.SUCCESS);
              return Redirect("/Artwork/Details/" + artwork.TheArtwork.ArtworkId);
            }
            else
              ModelState.AddModelError("SameTitle", "There is already an artwork with the same title.");
          }
          ViewBag.CategoryId = new SelectList(db.Categories, "id", "kategoria", artwork.TheArtwork.CategoryId);
          ViewBag.SubjectId = new SelectList(db.Subjects, "id", "subjekti", artwork.TheArtwork.SubjectId);
          return View(artwork);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
          var artwork = artService.GetArtworkByArtworkId(id);
          if (artwork==null|| artwork.AvailabilityStatus == false)
            return HttpNotFound();
          artService.DeleteArtwork(id);
          this.AddNotification("Artwork deleted.", NotificationType.SUCCESS);
          return Redirect("/CustomerArtist/VisitProfile/"+User.Identity.GetUserId());
        }

  }
}