using BuyArt.DomainModels.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Order
  {
    [Key]
    public int OrderID { get; set; }
    [DataType(DataType.DateTime)]
    [Display(Name ="Order Date")]
    public DateTime OrderDate { get; set; }
    [Required]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "{0} must contain only letters.")]
    [StringLength(30)]
    [Display(Name ="Shipping City")]
    public string ShipCity { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name ="Shipping Address")]
    public string ShipAddress { get; set; }
    [Display(Name ="Order Status")]
    //Mund te marre vlerat: Ordered, Delivering, Delivered, Canceled, Rejected(ne rast nuk pranohet nga artisti i vepres qe eshte porositur)
    public string OrderStatus { get; set; }
    [Required]
    [Display(Name = "Required Date")]
    [DataType(DataType.DateTime)]
    //custom validator
    [FourOrMoreDaysAfterToday(ErrorMessage ="Required Date must at least 4 days after today's date.")]
    public DateTime RequiredDate { get; set; }
    [Display(Name="Delivered Date")]
    [DataType(DataType.DateTime)]
    public Nullable<DateTime> DeliveredDate { get; set; }
    public string CustomerId { get; set; }
    public int ArtworkId { get; set; }

    public virtual ApplicationUser Customer { get; set; }
    public virtual Artwork OrderedArtwork { get; set; }
  }
}