using BuyArt.DomainModels;
using BuyArt.DomainModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface IUsersRepository
  {
    List<ApplicationUser> GetUsers();
    ApplicationUser GetUserByUserId(string UserId);
    bool InsertUser(ApplicationUser p, string role);
    bool UpdateUser(ApplicationUser p);
    bool DeleteUser(string UserId);
    void FollowUnfollowUser(string currentUid, string uid);
    string GetUserRole(string uid);
    List<ApplicationUser> GetArtists();
    List<Artwork> GetAllArtworks();
  }
}
