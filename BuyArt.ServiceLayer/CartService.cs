using BuyArt.DomainModels;
using BuyArt.RepositoryContracts;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceLayer
{
  public class CartService:ICartService
  {
    ICartRepository rep;

    public CartService(ICartRepository r)
    {
      this.rep = r;
    }

    public List<Artwork> CartArtworks(int cardId)
    {
      return rep.CartArtworks(cardId);
    }

    public Cart GetCartByUserId(string uid)
    {
      return rep.GetCartByUserId(uid);
    }

    public void CreateCart(string uid)
    {
      rep.CreateCart(uid);
    }

    public void AddArtworkInCart(int artwId, int cartId)
    {
      rep.AddArtworkInCart(artwId, cartId);
    }

    public void RemoveArtworkFromCart(int artwId, int cartId)
    {
      rep.RemoveArtworkFromCart(artwId, cartId);
    }

    //nje klient mund te shtoje ose te heq nga shporta vetem nqs shporta eshte e tij
    public bool CanCustomerAddOrRemoveFromCart(string uid, int cartId)
    {
      var cart = rep.GetCartByUserId(uid);
      if (cart != null && cart.User.Id == uid)
        return true;
      return false;
    }
  }
}
