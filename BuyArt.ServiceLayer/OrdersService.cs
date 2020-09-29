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
  public class OrdersService:IOrdersService
  {
    IOrdersRepository oRep;
    public OrdersService(IOrdersRepository oR)
    {
      this.oRep = oR;
    }

    public List<Order> GetUserOrders(string uid)
    {
      return oRep.GetUserOrders(uid).OrderByDescending(x=>x.OrderID).ToList();
    }

    public Order GetOrderByOrderId(int id)
    {
      return oRep.GetOrderByOrderId(id);
    }

    //nje customer mund te shohe detajet e nje porosie vetem nqs ajo ekziston
    // dhe ai eshte personi qe e ka bere ate porosi
    public bool CanCustomerViewOrder(int orderId, string uid)
    {
      Order o = oRep.GetOrderByOrderId(orderId);
      if (o!=null && o.CustomerId == uid)
        return true;
      return false;
    }

    public bool CanCustomerOrderArtwork(int artworkId)
    {
      return oRep.CanCustomerOrderArtwork(artworkId);
    }

    public void CreateOrder(Order o)
    {
      oRep.CreateOrder(o);
    }

    //nje klient mund te anuloje nje porosi vetem nqs ajo porosi ekziston, nqs ai vete e ka bere kete
    //porosi dhe nqs OrderStatus!=Delivered
    public bool CanCustomerCancelOrder(string uid, int orderId)
    {
      Order o = oRep.GetOrderByOrderId(orderId);
      if (o != null && o.CustomerId == uid && o.OrderStatus!="Delivered")
        return true;
      return false;
    }

    public List<Order> GetArtistsOrderedArtworks(string uid)
    {
      return oRep.GetArtistsOrderedArtworks(uid);
    }

    public bool CanStartDelivering(string uid, int orderId)
    {
      var order = oRep.GetOrderByOrderId(orderId);
      if (order != null && order.OrderedArtwork.ArtistId == uid && order.OrderStatus == "Ordered")
        return true;
      return false;
    }

    public void ChangeOrderStatus(string newOrderStatus, int orderId)
    {
      oRep.ChangeOrderStatus(newOrderStatus, orderId);
    }

    public bool CanArtistViewOrder(string uid, int orderId)
    {
      var order = oRep.GetOrderByOrderId(orderId);
      if (order != null && order.OrderedArtwork.ArtistId == uid && order.OrderStatus != "Rejected")
        return true;
      return false;
    }

    public bool CanChangeOrderStatusTo(string neworderStatus, string uid, int orderId)
    {
      var order= oRep.GetOrderByOrderId(orderId);
      if (order != null)
      {
        if(order.OrderedArtwork.ArtistId == uid)
        {
          //nje user mund ta beje statusin e porosise delivering nqs ai eshte autori i vepres qe eshte 
          //porositur dhe porosia ka statusin Ordered
          if (neworderStatus == "Delivering")
          {
            if (order.OrderStatus == "Ordered")
              return true;
          }
          else
          {
            if (neworderStatus == "Delivered")
            {
              if (order.OrderStatus == "Delivering")
                return true;
            }
            else
            {
              if (neworderStatus == "Rejected")
              {
                if (order.OrderStatus == "Ordered" || order.OrderStatus == "Delivering")
                  return true;
              }
            }
          }
        }
        else
        {
          if (order.CustomerId == uid)
          {
            if (neworderStatus == "Canceled")
            {
              if (order.OrderStatus == "Ordered" || order.OrderStatus == "Delivering")
                return true;
            }
          }
        }
      }
      return false;
    }

    public void DeleteOrder(int id)
    {
      oRep.DeleteOrder(id);
    }
  }
}
