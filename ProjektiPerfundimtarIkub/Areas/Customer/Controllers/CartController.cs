using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Customer.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CartController : Controller
    {
        ICartService cartService;
        public CartController(ICartService cs)
        {
          this.cartService = cs;
        }

        public ActionResult MyCart()
        {
            string uid = User.Identity.GetUserId();
            List<Artwork> cartArtworks = new List<Artwork>();
            if (cartService.GetCartByUserId(uid) == null)
            {
              cartService.CreateCart(uid);
            }
            else
            {
              var ucart = cartService.GetCartByUserId(uid);
               cartArtworks= cartService.CartArtworks(ucart.CartId);
            }
            return View(cartArtworks);
        }

        public ActionResult AddArtworkToMyCart(int id)//artworkId
        {
          string uid = User.Identity.GetUserId();
          if (cartService.GetCartByUserId(uid) == null)
          {
            cartService.CreateCart(uid);
          }
          else
          {
            var ucart = cartService.GetCartByUserId(uid);
            cartService.AddArtworkInCart(id, ucart.CartId);
          }
          return RedirectToAction("MyCart");
        }
    }
}