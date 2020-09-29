using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface IArtworksService
  {
    List<Artwork> GetArtistArtworks(string uid);
    Artwork GetArtworkByArtworkId(int ArtworkId);
    bool InsertArtwork(Artwork a);
    bool CanUpdateOrDelete(string uid, int artworkId);
    bool UpdateArtwork(Artwork a);
    void DeleteArtwork(int ArtworkId);
    List<Style> GetArtworkStyles(List<Style> styles);
    List<Comment> GetArtworkLikes(int artworkId);
    void LikeOrUnlikeArtwork(int artworkId, string uid);
    bool UserLikesArtwork(int artworkId, string uid);
    List<Artwork> SearchArtworks(string type, string name);
    List<Artwork> FavoriteArtworks(string uid);
  }
}
