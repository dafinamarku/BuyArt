using BuyArt.DomainModels;
using BuyArt.RepositoryContracts;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceLayer
{
  public class ArtworksService:IArtworksService
  {
    IArtworkRepository rep;

    public ArtworksService(IArtworkRepository r)
    {
      this.rep = r;
    }

    public List<Artwork> GetArtistArtworks(string uid)
    {
      return rep.GetArtistArtworks(uid);
    }

    public Artwork GetArtworkByArtworkId(int id)
    {
      return rep.GetArtworkByArtworkId(id);
    }

    public bool InsertArtwork(Artwork a)
    {
      return rep.InsertArtwork(a);
    }
    
    public bool UpdateArtwork(Artwork a)
    {
        return rep.UpdateArtwork(a);
    }

    public void DeleteArtwork(int ArtworkId)
    {
      rep.DeleteArtwork(ArtworkId);
    }

    public List<Style> GetArtworkStyles(List<Style> styles)
    {
      return rep.GetArtworkStyles(styles);
    }

    //Punimet mund te modifikohen/fshihen vetem nga autoret e tyre
    public bool CanUpdateOrDelete(string uid, int artworkId)
    {
      Artwork a = rep.GetArtworkByArtworkId(artworkId);
      if (a != null && a.ArtistId == uid)
        return true;
      return false;
    }

    public List<Comment> GetArtworkLikes(int artworkId)
    {
      return rep.GetArtworkLikes(artworkId);
    }

    public void LikeOrUnlikeArtwork(int artworkId, string uid)
    {
      rep.LikeOrUnlikeArtwork(artworkId, uid);
    }

    public bool UserLikesArtwork(int artworkId, string uid)
    {
      return rep.UserLikesArtwork(artworkId, uid);
    }

    public List<Artwork> SearchArtworks(string type, string name)
    {
      if (type == "category")
        return rep.FindArtworksByCategoryName(name);
      else if (type == "subject")
        return rep.FindArtworksBySubjectName(name);
      else 
        return rep.FindArtworksByStyleName(name);
    }

    public List<Artwork> FavoriteArtworks(string uid)
    {
      return rep.FavoriteArtworks(uid);
    }
  }
}
