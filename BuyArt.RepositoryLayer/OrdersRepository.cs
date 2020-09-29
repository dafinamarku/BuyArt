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
  public class OrdersRepository:IOrdersRepository
  {
    ProjectContext db;

    public OrdersRepository()
    {
      this.db = new ProjectContext();
    }

    public List<Order> GetUserOrders(string uid)
    {
      //porosite e anuluara nuk i shfaqen klientit, ato i shfaqen vetem artistit te veprave perkatese
      //i cili me pas mund ti fshije
      return db.Orders.Where(x => x.CustomerId == uid&&x.OrderStatus!="Canceled").ToList();
    }

    public Order GetOrderByOrderId(int id)
    {
      return db.Orders.Where(x => x.OrderID == id).FirstOrDefault();
    }

    //nje klient mund te porosite nje veper vetem nqs ajo ekziston dhe 
    //AvailabilityStatus i saj eshte true
    public bool CanCustomerOrderArtwork(int artworkId)
    {
      Artwork artw = db.Artworks.Where(x => x.ArtworkId == artworkId).FirstOrDefault();
      if (artw!=null && artw.AvailabilityStatus == true)
        return true;
      return false;
    }

    public void CreateOrder(Order o)
    {
      var existingOrder = db.Orders
        .Where(x=>x.ArtworkId==o.ArtworkId && x.CustomerId==o.CustomerId && (x.OrderStatus == "Canceled"||x.OrderStatus=="Rejected"))
        .FirstOrDefault();
      if (existingOrder == null)
      {
        db.Orders.Add(o);
      }
      else
      {//ne kete rast klienti mund ta kete anuluar porosine dhe thjesht bejme editimin e rekordit
        existingOrder.OrderStatus = o.OrderStatus;
        existingOrder.ShipAddress = o.ShipAddress;
        existingOrder.ShipCity = o.ShipCity;
        existingOrder.OrderDate = o.OrderDate;
        existingOrder.RequiredDate = o.RequiredDate;
      }
      //kur nje veper shtohet ne nje porosi Availability is saj behet false
      Artwork a=db.Artworks.Where(x => x.ArtworkId == o.ArtworkId).FirstOrDefault();
      a.AvailabilityStatus = false;
      db.SaveChanges();
    }

    public List<Order> GetArtistsOrderedArtworks(string uid)
    {
      //artistit nuk i shfaqen me porosite qe ai i ka bere reject

      var list = db.Orders.Where(x => x.OrderedArtwork.ArtistId == uid && x.OrderStatus != "Rejected").ToList();
      return list;
    }

    public void ChangeOrderStatus(string newOrderStatus, int orderId)
    {
      var order=this.GetOrderByOrderId(orderId);
      if (order != null)
      {
        if (newOrderStatus == "Canceled" || newOrderStatus=="Rejected")
        {
          //kur nje porosi anulohet vepra duhet te behet perseri e disponueshme per blerje
          var artw = db.Artworks.Where(a => a.ArtworkId == order.ArtworkId).FirstOrDefault();
          artw.AvailabilityStatus = true;
        }
        else
        {
          if (newOrderStatus == "Delivered")
          {
            order.DeliveredDate = DateTime.Now;
          }
        }
        order.OrderStatus = newOrderStatus;
        db.SaveChanges();
      }
    }

    public void DeleteOrder(int id)
    {
      var existingOrder = db.Orders.Where(x => x.OrderID == id).FirstOrDefault();
      if (existingOrder != null)
      {
        db.Orders.Remove(existingOrder);
        db.SaveChanges();
      }
    }
  }
}
