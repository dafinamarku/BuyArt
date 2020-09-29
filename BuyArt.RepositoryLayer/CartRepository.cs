using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryLayer
{
  public class CartRepository:ICartRepository
  {
    ProjectContext db;
    public CartRepository()
    {
      this.db = new ProjectContext();
    }

    public List<Artwork> CartArtworks(int cartId)
    {
      return db.Carts.Where(x => x.CartId == cartId).FirstOrDefault().CartArtworks.ToList();
    }

    public Cart GetCartByUserId(string uid)
    {
      return db.Carts.Where(x => x.User.Id == uid).FirstOrDefault();
    }

    public void CreateCart(string uid)
    {
      var user = db.Users.Where(x => x.Id == uid).FirstOrDefault();
      Cart newCart = new Cart
      {
        User = user
      };
      db.Carts.Add(newCart);
      db.SaveChanges();
    }

    public void AddArtworkInCart(int artwId, int cartId)
    {
      var cart = db.Carts.Where(x => x.CartId == cartId).FirstOrDefault();
      Artwork a = db.Artworks.Where(x => x.ArtworkId == artwId).FirstOrDefault();
      if (cart != null && a!=null)
      {
        List<Artwork> artwOfCart = this.CartArtworks(cartId);
        if (artwOfCart == null)
          artwOfCart = new List<Artwork>();
        if (artwOfCart.Contains(a) == false)
        {
          a.AvailabilityStatus = false;
          artwOfCart.Add(a);
          db.SaveChanges();
        }
      }
    }

    public void RemoveArtworkFromCart(int artwId, int cartId)
    {
      var cart = db.Carts.Where(x => x.CartId == cartId).FirstOrDefault();
      Artwork a = db.Artworks.Where(x => x.ArtworkId == artwId).FirstOrDefault();
      if (cart != null && a != null)
      {
        List<Artwork> artwOfCart = this.CartArtworks(cartId);
        if (artwOfCart.Contains(a) == true)
        {
          a.AvailabilityStatus = true;
          artwOfCart.Remove(a);
          db.SaveChanges();
        }
      }
    }
  }
}
