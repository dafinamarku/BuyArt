using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Cart
  {
    [Key]
    public int CartId { get; set; }

    public virtual ICollection<Artwork> CartArtworks { get; set; }
    [Required]
    public virtual ApplicationUser User { get; set; }
  }
}