using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryLayer
{
  public class ArtworksRepository:IArtworkRepository
  {
    ProjectContext db;

    public ArtworksRepository()
    {
      this.db = new ProjectContext();
    }

    public List<Artwork> GetArtistArtworks(string artistId)
    {
      List<Artwork> artworks = db.Artworks.Where(x=>x.ArtistId==artistId).ToList();
      return artworks;
    }

    public Artwork GetArtworkByArtworkId(int ArtworkId)
    {
      Artwork a = db.Artworks.Where(x => x.ArtworkId == ArtworkId).FirstOrDefault();
      return a;
    }

    //Punimi do te shtohet vetem ne rast se nuk ka nje punim tjeter me te njejtin titull
    public bool InsertArtwork(Artwork a)
    {
      List<Artwork> sameTitle = db.Artworks.Where(x => x.Title == a.Title).ToList();
      if (sameTitle.Count > 0)
      {
        return false;
      }
      else
      {
        db.Artworks.Add(a);
        db.SaveChanges();
        return true;
      }
    }

    public bool UpdateArtwork(Artwork a)
    {
      List<Artwork> sameTitle = db.Artworks.Where(x => x.Title == a.Title).ToList();
      if (sameTitle.Count > 1)
      {
        return false;
      }
      else
      {
        Artwork existingArtwork = db.Artworks.Where(x => x.ArtworkId == a.ArtworkId).FirstOrDefault();
        existingArtwork.Title = a.Title;
        existingArtwork.Width = a.Width;
        existingArtwork.Height = a.Height;
        existingArtwork.Photo = a.Photo;
        existingArtwork.Price = a.Price;
        existingArtwork.SubjectId = a.SubjectId;
        existingArtwork.CategoryId = a.CategoryId;
        existingArtwork.Description = a.Description;
        //ne listen e stileve te vepres do te shtohen ato stile qe nuk ishin me pare, por qe jane 
        //ne parametrin e marre   dhe do te hiqen ato stile qe ishin me pare por nuk jane ne parametrin a
        var stylestToRemove=existingArtwork.ArtworkStyles.Where(x => a.ArtworkStyles.Contains(x) == false).ToList();
        var stylesToAdd = a.ArtworkStyles.Where(x => !existingArtwork.ArtworkStyles.Contains(x)).ToList();
        if (stylesToAdd != null)
        {
          foreach (var i in stylesToAdd)
            existingArtwork.ArtworkStyles.Add(i);
        }
        if (stylestToRemove != null)
        {
          foreach (var i in stylestToRemove)
            existingArtwork.ArtworkStyles.Remove(i);
        } 
        db.SaveChanges();
        return true;
      }
    }

    //me fshirjen e nje vepre fshihen automatikisht edhe porosite, komentet, like-t, dhe gjithashtu  
    //hiqen nga shportat e blerjeve ku ato bejne pjese
    public void DeleteArtwork(int ArtworkId)
    {
      Artwork existingartwork = this.GetArtworkByArtworkId(ArtworkId);
      db.Artworks.Remove(existingartwork);
      db.SaveChanges();
    }

    //parametri i marre do te permbaje listen e stileve te perzgjedhura ne forme (IsSelected==true)
    //dhe kthen listen me stiet korresponduese nga db
    public List<Style> GetArtworkStyles(List<Style> selectedStyles){
      List<Style> artwStyles = new List<Style>();
      foreach(var i in selectedStyles)
      {
        if (i.IsSelected == true)
        {
          var style = db.Styles.Where(s => s.StyleId == i.StyleId).FirstOrDefault();
          artwStyles.Add(style);
        }
      }
      return artwStyles;
    }

    public List<Comment> GetArtworkLikes(int artworkId)
    {
      Artwork artwork = db.Artworks.Where(x => x.ArtworkId == artworkId).FirstOrDefault();
      List<Comment> likes = new List<Comment>();
      if (artwork != null)
        likes = db.Comments.Where(x => x.CommentOrLike == "like" && x.ArtworkId == artworkId && x.Like == true).ToList();
      return likes;
    }

    public void LikeOrUnlikeArtwork(int artworkId, string uid)
    {
        Comment likesOrNo = db.Comments.Where(c => c.AuthorId == uid && c.CommentOrLike == "like" && c.ArtworkId == artworkId).FirstOrDefault();
        //ne kete rast useri tashme e pelqen vepren dhe ka klikuar butonin per te hequr like
        if (likesOrNo != null)
        {
          db.Comments.Remove(likesOrNo);
          db.SaveChanges();
        }
        else
        {
          Comment newLike = new Comment()
          {
            CommentOrLike = "like",
            Like = true,
            AuthorId = uid,
            ArtworkId = artworkId
          };
          db.Comments.Add(newLike);
          db.SaveChanges();

      }
    }

    public bool UserLikesArtwork(int artworkId, string uid)
    {
      var likes = db.Comments.Where(x => x.CommentOrLike == "like" && x.Like == true && x.AuthorId == uid && x.ArtworkId == artworkId).FirstOrDefault();
      if (likes != null)
        return true;
      return false;
    }

    public List<Artwork> FindArtworksByCategoryName(string categoryName)
    {
      return db.Artworks.Where(a => a.ArworkCategory.CategoryName == categoryName).ToList();
    }

    public List<Artwork> FindArtworksBySubjectName(string subjectName)
    {
      return db.Artworks.Where(a => a.ArtworkSubject.SubjectName == subjectName).ToList();
    }

    public List<Artwork> FindArtworksByStyleName(string styleName)
    {
      return db.Artworks
        .Where(a => a.ArtworkStyles.Where(s => s.StyleName == styleName)
        .Count() == 1).ToList();
    }

    public List<Artwork> FavoriteArtworks(string uid)
    {
      return db.Artworks
        .Where(a => a.Comments.Where(c => c.Like == true && c.AuthorId == uid)
        .Count() == 1).ToList();
    }
  }
}
