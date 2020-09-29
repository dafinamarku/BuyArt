using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Subject
  {
    public int SubjectId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "{0} can not have more than 20 letters.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "{0} must contain only letters.")]
    [Display(Name = "Subject")]
    public string SubjectName { get; set; }
    public virtual ICollection<Artwork> Artworks { get; set; }
  }
}