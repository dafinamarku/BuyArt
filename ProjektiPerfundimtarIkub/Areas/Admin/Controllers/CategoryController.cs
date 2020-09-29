using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyArt.DataLayer;
using BuyArt.DomainModels;
using BuyArt.ServiceContracts;

namespace ProjektiPerfundimtarIkub.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        ICategoriesService cService;

        public CategoryController(ICategoriesService s)
        {
          this.cService = s;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(cService.GetCategories());
        }

        public ActionResult Create()
        {
          return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="CategoryId, CategoryName")]Category c)
        {
          if (ModelState.IsValid)
          {
            bool inserted=cService.InsertCategory(c);
            //kategoria nuk eshte shtuar per shkak se ekziston nje kategori tjeter me te njejtin emer
            if (inserted == false)
            {
              ModelState.AddModelError("Err", "This category already exists.");
              return View(c);
            }
            else
            {
              return RedirectToAction("Index");
            }
          }
          return View(c);
        }

        public ActionResult Edit(int id)
        {
          Category c=cService.GetCategoryByCategoryId(id);
          if (c != null)
            return View(c);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include ="CategoryId, CategoryName")]Category c)
        {
          if (ModelState.IsValid)
          {
            bool result = cService.UpdateCategory(c);
            if (result == false)
            {
              ModelState.AddModelError("Err", "This category already exists.");
              return View(c);
            }
            else
            {
              return RedirectToAction("Index");
            }
          }
          else
            return View(c);
        }

        public ActionResult Delete(int id)
        {
          Category c = cService.GetCategoryByCategoryId(id);
          if (c != null)
            return View(c);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Category c)
        {
          cService.DeleteCategory(c.CategoryId);
          return RedirectToAction("Index");
        }
  }
}