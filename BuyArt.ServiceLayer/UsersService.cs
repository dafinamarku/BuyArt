using BuyArt.DomainModels;
using BuyArt.DomainModels.ViewModels;
using BuyArt.RepositoryContracts;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceLayer
{
  public class UsersService:IUsersService
  {
    IUsersRepository rep;

    public UsersService(IUsersRepository r)
    {
      this.rep = r;
    }

    //kthen te gjithe userat duke hequr nga lista userin e loguar
    public List<ApplicationUser> GetUsers(string uid)
    {
      return rep.GetUsers().Where(x=>x.Id!=uid).ToList();
    }

    public ApplicationUser GetUserByUserId(string UserId)
    {
      return rep.GetUserByUserId(UserId);
    }

    public bool InsertUser(ApplicationUser u, string role)
    {
      return rep.InsertUser(u, role);
    }

    public bool UpdateUser(ApplicationUser u)
    {
      return rep.UpdateUser(u);
    }

    public bool DeleteUser(string UserId)
    {
      return rep.DeleteUser(UserId);
    }

    public string GetUserRole(string uid)
    {
      return rep.GetUserRole(uid);
    }

    public void FollowUnfollowUser(string currentUid, string uid)
    {
      rep.FollowUnfollowUser(currentUid,uid);
    }

    //Userat qe jane ne rolin Artist mund te vizitojne/ndjekin profilet e njeri-tjetrit
    //Userat qe jane ne rolin Cutomer mund te vizitojne/ndejkin vetem profilet e userave qe jane ne rolin Artist
    public bool CanVisitOrFollowUserProfile(string currentUid, string uid)
    {
      string currentUserRole = rep.GetUserRole(currentUid);
      string theOtherUserRole = rep.GetUserRole(uid);
      if (currentUserRole == "Artist" && theOtherUserRole == "Artist")
        return true;
      else if (currentUserRole == "Customer" && theOtherUserRole == "Artist")
        return true;
      return false;
    }

    public List<ApplicationUser> GetArtists()
    {
      return rep.GetArtists();
    }

    public List<RaportViewModel> AdminRaport()
    {
      List<RaportViewModel> raport = new List<RaportViewModel>();
      var artists = rep.GetArtists();
      foreach(var i in artists)
      {
        RaportViewModel r = new RaportViewModel
        {
          UserName = i.UserName,
          NrOfDeliveredArtworks = rep.GetAllArtworks().Where(x => x.InOrders.Any(o => o.OrderStatus == "Delivered") == true && x.ArtistId == i.Id).Count(),
          TotalAmountOfMoney =(double)rep.GetAllArtworks().Where(x => x.InOrders.Any(o => o.OrderStatus == "Delivered") == true && x.ArtistId == i.Id).Sum(x => x.Price)
        };
        raport.Add(r);
      }

      return raport;
    }

  }
}
