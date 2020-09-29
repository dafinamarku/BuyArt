using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class ApplicationUserManager:UserManager<ApplicationUser>
  {
    public ApplicationUserManager(ApplicationUserStore store):base(store)
    {

    }
  }
}