using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BuyArt.DomainModels.CustomValidators;

namespace BuyArt.DomainModels
{
  public class Artwork
  {
    public int ArtworkId { get; set; }
    [Required]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "{0} must contain only letters (space is allowed).")]
    [StringLength(40)]
    public string Title { get; set; }
    [Display(Name = "Photo")]
    public string Photo { get; set; }
    [Required]
    [MinValue(0, ErrorMessage ="Price must be greater than 0.")]
    public Nullable<double> Price { get; set; }
    [Required]
    [Range(1,100000, ErrorMessage ="{0} must be between 1 and 100000")]
    //cm
    public Nullable<double> Width { get; set; }
    [Required]
    [Range(1, 100000, ErrorMessage = "{0} must be between 1 and 100000")]
    public Nullable<double> Height { get; set; }
    public bool AvailabilityStatus { get; set; }
    //custom validator
    [MaxWords(80, ErrorMessage ="{0} can contain up to 80 words.")]
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int SubjectId { get; set; }
    //[ForeignKey("Artist")]
    public string ArtistId { get; set; }

    public virtual ApplicationUser Artist { get; set; }
    //public virtual ICollection<ApplicationUser> LikedBy { get; set; }
    public virtual Category ArworkCategory { get; set; }
    public virtual Subject ArtworkSubject { get; set; }
    public virtual ICollection<Style> ArtworkStyles { get; set; }
    public virtual  ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Order> InOrders { get; set; }
    public virtual ICollection<Cart> Carts { get; set; }
  }
}