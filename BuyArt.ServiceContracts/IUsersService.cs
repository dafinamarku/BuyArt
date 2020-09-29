using BuyArt.DomainModels;
using BuyArt.DomainModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface IUsersService
  {
    //kthen listen e userave ku nuk perfshihet useri me id e kaluar si parameter
    List<ApplicationUser> GetUsers(string uid);
    ApplicationUser GetUserByUserId(string UserId);
    bool InsertUser(ApplicationUser p, string role);
    bool UpdateUser(ApplicationUser p);
    bool DeleteUser(string UserId);
    string GetUserRole(string uid);
    void FollowUnfollowUser(string currentUid, string uid);
    bool CanVisitOrFollowUserProfile(string currentUid, string uid);
    List<ApplicationUser> GetArtists();
    List<RaportViewModel> AdminRaport();
  }
}
