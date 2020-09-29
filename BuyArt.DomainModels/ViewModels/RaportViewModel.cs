using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels.ViewModels
{
  public class RaportViewModel
  {
    
    public string UserName { get; set; }
    [Display(Name ="Nr of Delivered Artworks")]
    public int NrOfDeliveredArtworks { get; set; }
    [Display(Name = "Earned")]
    public double TotalAmountOfMoney { get; set; }
  }
}