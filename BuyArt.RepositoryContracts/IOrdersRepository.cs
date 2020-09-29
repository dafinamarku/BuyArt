using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface IOrdersRepository
  {
    List<Order> GetUserOrders(string uid);
    Order GetOrderByOrderId(int id);
    bool CanCustomerOrderArtwork(int artworkId);
    void CreateOrder(Order o);
    List<Order> GetArtistsOrderedArtworks(string uid);
    void ChangeOrderStatus(string newOrderStatus, int orderId);
    void DeleteOrder(int id);
  }
}
