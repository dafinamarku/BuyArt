using BuyArt.DataLayer;
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
    [Authorize(Roles ="Artist, Customer")]
    public class CommentController : Controller
    {
    ICommentService comService;
    ProjectContext db;
    public CommentController(ICommentService cs)
    {
      db = new ProjectContext();
      this.comService = cs;
    }
    public ActionResult CommentArtwork(string commentText, int artworkId)
    {
      if (db.Artworks.Where(x => x.ArtworkId == artworkId).FirstOrDefault() == null)
        return HttpNotFound();
      string currentUid = User.Identity.GetUserId();
      Comment newComment = new Comment()
      {
        CommentText = commentText,
        ArtworkId = artworkId,
        CommentOrLike = "comment",
        AuthorId = currentUid
      };
      comService.InsertComment(newComment);
      return RedirectToAction("Details", "Artwork", new { id = artworkId });
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      var comment = comService.GetCommentById(id);
      if (comment!=null)
      {
        comService.DeleteComment(id);
        return RedirectToAction("Details", "Artwork", new { id = comment.ArtworkId });
      }
      return HttpNotFound();
    }
  }
}