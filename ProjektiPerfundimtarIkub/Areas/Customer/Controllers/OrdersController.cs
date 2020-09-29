using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using ProjektiPerfundimtarIkub.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Customer.Controllers
{
    [Authorize(Roles ="Customer")]
    public class OrdersController : Controller
    {
        IOrdersService oService;
        public OrdersController(IOrdersService os)
        {
          this.oService = os;
        }

        public ActionResult MyOrders(string search, int PageNo = 1)
        {
            string currentUid = User.Identity.GetUserId();
            List<Order> userOrders = oService.GetUserOrders(currentUid);
            if (!string.IsNullOrEmpty(search))
            {
              search = search.ToUpper();
              userOrders = userOrders.Where(s => s.OrderedArtwork.Title.ToUpper().Contains(search)).ToList();
            }
            /* Pagination */
            int NoOfRecordsPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(userOrders.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;

            userOrders = userOrders.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(userOrders);
        }

        public ActionResult OrderDetails(int id)
        {
          string currentUid = User.Identity.GetUserId();
          //nje customer mund te shohe detajet e nje porosie vetem nqs ajo ekziston
          // dhe ai eshte personi qe e ka bere ate porosi
          if (oService.CanCustomerViewOrder(id, currentUid) == false)
            return HttpNotFound();
          Order order = oService.GetOrderByOrderId(id);
          return View(order);
        }

        public ActionResult OrderArtwork(int id)
        {
          //nje klient mund te porosite nje veper vetem nqs ajo ekziston dhe 
          //AvailabilityStatus i saj eshte true
          if (oService.CanCustomerOrderArtwork(id) == false)
            return HttpNotFound();
          ViewBag.ArtworkId = id;
          return View(); //view ku do te plotesohen fushat per krijimin e porosise
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderArtwork(Order model)
        {
          if (ModelState.IsValid)
          {
            if (oService.CanCustomerOrderArtwork(model.ArtworkId) == false)
              return HttpNotFound();
            Order newOrder = new Order()
            {
              ArtworkId = model.ArtworkId,
              RequiredDate = model.RequiredDate,
              ShipAddress = model.ShipAddress,
              ShipCity = model.ShipCity,
              CustomerId = User.Identity.GetUserId(),
              OrderDate = DateTime.Now,
              OrderStatus = "Ordered" //statusi me pas mund te ndryshohet nga artisti i vepres se porositur
            };
            oService.CreateOrder(newOrder);
            this.AddNotification("New order created succesfully.", NotificationType.SUCCESS);
            return RedirectToAction("MyOrders");
          }
          return View();
         
        }

    public ActionResult CancelOrder(int id)
    {
      string currentUid = User.Identity.GetUserId();
      //nje customer mund te anuloje vetem porosite e tij dhe vetem ne rast se ajo ka statusin ordered ose delivering
      if (oService.CanChangeOrderStatusTo("Canceled",currentUid, id) == false)
        return HttpNotFound();
      oService.ChangeOrderStatus("Canceled", id);
      this.AddNotification("Order canceled.", NotificationType.SUCCESS);
      return RedirectToAction("MyOrders");
    }
    }
}