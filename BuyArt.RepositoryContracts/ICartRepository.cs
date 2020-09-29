using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface ICartRepository
  {
    List<Artwork> CartArtworks(int cartId);
    Cart GetCartByUserId(string uid);
    void CreateCart(string uid);
    void AddArtworkInCart(int artwId, int cartId);
    void RemoveArtworkFromCart(int artwId, int cartId);
  }
}
