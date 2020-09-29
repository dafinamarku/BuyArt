using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class ApplicationUser:IdentityUser
  {
    public string Bio { get; set; }
    
    public virtual Cart ShoppingCart { get; set; }
    public virtual ICollection<Artwork> Artworks { get; set; }
    public virtual ICollection<ApplicationUser> Followers { get; set; }
    public virtual ICollection<ApplicationUser> Following { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    //public virtual ICollection<Artwork> LikedArtworks { get; set; }
  }
}