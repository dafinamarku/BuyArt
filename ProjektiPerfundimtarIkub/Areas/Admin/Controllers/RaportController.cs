using BuyArt.DomainModels.ViewModels;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RaportController : Controller
    {
        IUsersService uService;
        public RaportController(IUsersService us)
        {
          this.uService = us;
        }

        public ActionResult AdminRaport()
        {
            List<RaportViewModel> raport = uService.AdminRaport();
            return View(raport);
        }
    }
}