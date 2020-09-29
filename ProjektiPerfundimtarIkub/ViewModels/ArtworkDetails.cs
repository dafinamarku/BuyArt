using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class ArtworkDetails
  {
    public Artwork artwork { get; set; }
    public IEnumerable<Comment> comments { get; set; }

  }
}