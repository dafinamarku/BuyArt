using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class ArtistProfileViewModel
  {
    public ApplicationUser user { get; set; }
    public int nrfollowers { get; set; }
    public int nrfollowing { get; set; }
  }
}