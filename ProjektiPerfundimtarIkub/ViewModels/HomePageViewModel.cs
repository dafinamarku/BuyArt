using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class HomePageViewModel
  {
    public List<string> categories { get; set; }
    public List<string> styles { get; set; }
    public List<string> subjects { get; set; }
    //per rezultatet e kerkimit
    public List<ApplicationUser> artists { get; set; }
    public List<Artwork> artworks { get; set; }
    public List<Artwork> TenMostLikedArtworks { get; set; }
  }
}