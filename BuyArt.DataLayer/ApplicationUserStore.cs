using BuyArt.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyArt.DataLayer
{
  public class ApplicationUserStore:UserStore<ApplicationUser>
  {
    public ApplicationUserStore(ProjectContext db):base(db)
    {

    }
  }
}