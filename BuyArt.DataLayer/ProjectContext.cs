using BuyArt.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BuyArt.DataLayer
{
  public class ProjectContext: IdentityDbContext<ApplicationUser>
  {
    public ProjectContext():base("ArtCollectionCon")
    { }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Style> Styles { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Cart> Carts { get; set; }

  }
}