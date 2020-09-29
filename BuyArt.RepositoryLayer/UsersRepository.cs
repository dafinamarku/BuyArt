using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.DomainModels.ViewModels;
using BuyArt.RepositoryContracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryLayer
{
  public class UsersRepository:IUsersRepository
  {
    ProjectContext db;
    ApplicationUserStore userStore;
    ApplicationUserManager userManager;

    public UsersRepository()
    {
      this.db = new ProjectContext();
      this.userStore = new ApplicationUserStore(this.db);
      this.userManager = new ApplicationUserManager(this.userStore);
    }

    public List<ApplicationUser> GetUsers()
    {
      List<ApplicationUser> users = db.Users.ToList();
      return users;
    }

    public ApplicationUser GetUserByUserId(string UserId)
    {
      ApplicationUser u = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
      return u;
    }

    //kthen true nqs krijohet useri dhe false ne te kundert
    public bool InsertUser(ApplicationUser u, string role)
    {
      ApplicationUser sameEmailUser = userManager.FindByEmail(u.Email);
      ApplicationUser sameUname = db.Users.Where(x => x.UserName == u.UserName).FirstOrDefault();
      //nuk lejohen usera me te njejtin email ose username
      if (sameEmailUser != null || sameUname != null)
        return false;
      else
      {
        IdentityResult result = userManager.Create(u);
        if (result.Succeeded)
        {
          userManager.AddToRole(u.Id, role);
          return true;
        }
        else
          return false;
      }
    }

    public bool UpdateUser(ApplicationUser u)
    {
      List<ApplicationUser> sameEmailOrUnameUsers = db.Users.Where(x=>x.Email==u.Email||x.UserName==u.UserName).ToList();
      //nje user nuk mund te kete email ose username te njejte me nje tjeter
      if (sameEmailOrUnameUsers.Count()>1)
        return false;
      else
      {
        ApplicationUser existingUser = userManager.FindById(u.Id);
        existingUser.UserName = u.UserName;
        existingUser.Email = u.Email;
        existingUser.PasswordHash = u.PasswordHash;
        existingUser.Bio = u.Bio;
        IdentityResult result = userManager.Update(existingUser);
        if (result.Succeeded)
          return true;
        else
          return false;
      }
    
    }

    public bool DeleteUser(string UserId)
    {
      ApplicationUser existingApplicationUser = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
      IdentityResult result = userManager.Delete(existingApplicationUser);
      if (result.Succeeded)
        return true;
      else
        return false;
    }

    public void FollowUnfollowUser(string currentUid, string uid)
    {
      var currentUser = db.Users.Find(currentUid);
      var theOtherUser = db.Users.Find(uid);
      //useri tjeter eshte ne listen following te userit aktual=>
      //pra ai ka klikuar butonin per ta hequr ate nga kjo liste
      if (currentUser.Following.Contains(theOtherUser))
      {
        currentUser.Following.Remove(theOtherUser);
        theOtherUser.Followers.Remove(currentUser);
      }
      else
      {
        currentUser.Following.Add(theOtherUser);
        theOtherUser.Followers.Add(currentUser);
      }
      db.SaveChanges();
    }

    public string GetUserRole(string uid)
    {
      string role="";
      if (userManager.IsInRole(uid, "Customer"))
        role = "Customer";
      else if (userManager.IsInRole(uid, "Artist"))
        role = "Artist";
      else if (userManager.IsInRole(uid, "Admin"))
        role = "Admin";
      
      return role;
     
    }
    public List<Artwork> GetAllArtworks()
    {
      return db.Artworks.ToList();
    }

    public List<ApplicationUser> GetArtists()
    {
      //Id e Rolit te Artistit
      var artistRoleId = db.Roles.Where(x => x.Name == "Artist").Select(x => x.Id).FirstOrDefault();
      var artists = db.Users.Where(x => x.Roles.Any(r => r.RoleId == artistRoleId) == true).ToList();
      
      return artists;
    }
  }
}
