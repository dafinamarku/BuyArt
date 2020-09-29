
using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektiPerfundimtarIkub.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class StyleController : Controller
    {
        IStylesService styleService;

        public StyleController(IStylesService ss)
        {
          this.styleService = ss;
        }
        // GET: Admin/Style
        public ActionResult Index()
        {
          return View(styleService.GetStyles());
        }

        public ActionResult Create()
        {
          return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="StyleId, StyleName")]Style s)
        {
          if (ModelState.IsValid)
          {
              bool result = styleService.InsertStyle(s);
              if (result == false)
              {
                ModelState.AddModelError("Err", "This style already exists.");
                return View(s);
              }
              else
              {
                return RedirectToAction("Index");
              }
          }
          return View(s);
        }

        public ActionResult Edit(int id)
        {
          Style s = styleService.GetStyleByStyleId(id);
          if (s != null)
            return View(s);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include ="StyleId, StyleName")]Style s)
        {
          if (ModelState.IsValid)
          {
              bool result = styleService.UpdateStyle(s);
              if (result == false)
              {
                ModelState.AddModelError("Err", "This style already exists.");
                return View(s);
              }
              else
              {
                return RedirectToAction("Index");
              }
          }
          else
            return View(s);

        }

        public ActionResult Delete(int id)
        {
          Style s = styleService.GetStyleByStyleId(id);
          if (s != null)
            return View(s);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Style s)
        {
          styleService.DeleteStyle(s.StyleId);
          return RedirectToAction("Index");
        }
  }
}