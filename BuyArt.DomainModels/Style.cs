using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Style
  {
    public int StyleId { get; set; }
    [Required(ErrorMessage = "You have to choose at least one style for your artwork.")]
    [StringLength(20, ErrorMessage = "Style can not have more than 20 letters.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Style must contain only letters.")]
    [Display(Name = "Style")]
    public string StyleName { get; set; }
    //perdoret per listen e checkboxeve
    public bool IsSelected { get; set; }
    public virtual ICollection<Artwork> Artworks { get; set; }
  }
}