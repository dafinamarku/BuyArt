using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface IOrdersService
  {
    List<Order> GetUserOrders(string uid);
    Order GetOrderByOrderId(int id);
    bool CanCustomerViewOrder(int orderId, string uid);
    bool CanCustomerOrderArtwork(int artworkId);
    void CreateOrder(Order o);
    void DeleteOrder(int id);
    List<Order> GetArtistsOrderedArtworks(string uid);
    void ChangeOrderStatus(string newOrderStatus, int orderId);
    bool CanArtistViewOrder(string uid, int orderId);
    bool CanChangeOrderStatusTo(string orderStatus, string uid, int orderId);
  }
}
