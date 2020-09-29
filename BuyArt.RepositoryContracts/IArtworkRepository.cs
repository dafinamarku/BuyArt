using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface IArtworkRepository
  {
    List<Artwork> GetArtistArtworks(string artistId);
    Artwork GetArtworkByArtworkId(int ArtworkId);
    bool InsertArtwork(Artwork a);
    bool UpdateArtwork(Artwork a);
    void DeleteArtwork(int ArtworkId);
    List<Style> GetArtworkStyles(List<Style> selectedStyles);
    List<Comment> GetArtworkLikes(int artworkId);
    void LikeOrUnlikeArtwork(int artworkId, string uid);
    bool UserLikesArtwork(int artworkId, string uid);
    List<Artwork> FindArtworksByCategoryName(string categoryName);
    List<Artwork> FindArtworksByStyleName(string styleName);
    List<Artwork> FindArtworksBySubjectName(string subjectName);
    List<Artwork> FavoriteArtworks(string uid);
  }
}
