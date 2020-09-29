using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using ProjektiPerfundimtarIkub.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Artist.Controllers
{
    [Authorize(Roles ="Artist")]
    public class ArtistOrdersController : Controller
    {
        IOrdersService oService;
        public ArtistOrdersController(IOrdersService os)
        {
          this.oService = os;
        }
        // GET: Artist/ArtistOrders
        public ActionResult Orders(string search, int PageNo = 1)
        {
            string currentUid = User.Identity.GetUserId();
            List<Order> orders = oService.GetArtistsOrderedArtworks(currentUid);
            if (!string.IsNullOrEmpty(search))
            {
              search = search.ToUpper();
              orders = orders.Where(s => s.OrderedArtwork.Title.ToUpper().Contains(search)).ToList();
            }
            /* Pagination */
            int NoOfRecordsPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(orders.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;

            orders = orders.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(orders);
        }

        public ActionResult StartDeliveringOrder(int id)
        {
          string currentUid = User.Identity.GetUserId();
          if (oService.CanChangeOrderStatusTo("Delivering", currentUid, id) == false)
            return HttpNotFound();
          oService.ChangeOrderStatus("Delivering", id);
          this.AddNotification("Delivering started.", NotificationType.SUCCESS);
          return RedirectToAction("Orders");
        }

        public ActionResult OrderDetails(int id)
        {
          string currentUid = User.Identity.GetUserId();
          //nje customer mund te shohe detajet e nje porosie vetem nqs ajo ekziston
          // dhe ai eshte personi qe e ka bere ate porosi
          if (oService.CanArtistViewOrder(currentUid,id) == false)
            return HttpNotFound();
          Order order = oService.GetOrderByOrderId(id);
          return View(order);
        }
        //nje porosi mund te fshihet vtm nqs ajo ka statusin Canceled
        //dhe vepra e porositur ka si autor useri qe po tenton te bej fshirjen
        public ActionResult DeleteOrder(int id)
        {
          var order = oService.GetOrderByOrderId(id);
          if (order == null || order.OrderedArtwork.ArtistId != User.Identity.GetUserId() || order.OrderStatus != "Canceled"||order.OrderStatus=="Delivered")
            return HttpNotFound();
          oService.DeleteOrder(id);
          this.AddNotification("Order deleted", NotificationType.SUCCESS);
          return RedirectToAction("Orders");
        }

        public ActionResult RejectOrder(int id)
        {
          string currentUid = User.Identity.GetUserId();
          if (oService.CanChangeOrderStatusTo("Rejected", currentUid, id)==false)
            return HttpNotFound();
          oService.ChangeOrderStatus("Rejected", id);
          this.AddNotification("Order rejected.", NotificationType.SUCCESS);
          return RedirectToAction("Orders");
        }

        public ActionResult MarkAsDelivered(int id)
        {
          string currentUid = User.Identity.GetUserId();
          if (oService.CanChangeOrderStatusTo("Delivered", currentUid, id) == false)
            return HttpNotFound();
          oService.ChangeOrderStatus("Delivered", id);
          this.AddNotification("Order delivered.", NotificationType.SUCCESS);
          return RedirectToAction("Orders");
        }
    }
}