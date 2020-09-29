using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BuyArt.DomainModels
{
  public class Comment
  {
    public int id { get; set; }
    //vlerat e mundshme: comment, like=>indikon nqs rekordi perkates permban nje koment ose nje like
    public string CommentOrLike { get; set; }
    public Nullable<bool> Like { get; set; }
    public string CommentText { get; set; }
    public string AuthorId { get; set; }
    public int ArtworkId { get; set; }

    public virtual ApplicationUser Author { get; set; }
    public virtual Artwork Artwork { get; set; }

  }
}