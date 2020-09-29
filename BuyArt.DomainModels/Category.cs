using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Category
  {
    public int CategoryId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Category can not contain more than 20 letters.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Category must contain only letters.")]
    [Display(Name = "Category")]
    public string CategoryName { get; set; }
    public virtual ICollection<Artwork> Artworks { get; set; }
  }
}