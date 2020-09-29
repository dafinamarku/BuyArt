using BuyArt.DomainModels;
using ProjektiPerfundimtarIkub.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektiPerfundimtarIkub.ViewModels
{
  public class ArtworkCreateUpdateViewModel
  {
    public Artwork TheArtwork { get; set; }
    [Display(Name ="Styles")]
    [MinNrOfStyles(1, ErrorMessage ="You must select at least 1 style.")]
    public List<Style> TheArtworkStyles { get; set; }
  }
}