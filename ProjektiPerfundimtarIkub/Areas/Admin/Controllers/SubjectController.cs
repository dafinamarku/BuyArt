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
    public class SubjectController : Controller
    {
        ISubjectsService subjectService;

        public SubjectController(ISubjectsService s)
        {
          this.subjectService = s;
        }

        // GET: Admin/Subject
        public ActionResult Index()
        {
          return View(subjectService.GetSubjects());
        }

        public ActionResult Create()
        {
          return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="SubjectId, SubjectName")]Subject s)
        {
          if (ModelState.IsValid)
          {
              bool result = subjectService.InsertSubject(s);
              //subjektet nuk mund te kene emra te njejte
              if (result == false)
              {
                ModelState.AddModelError("Err", "This subject already exists.");
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
          Subject s = subjectService.GetSubjectBySubjectId(id);
          if (s != null)
            return View(s);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include ="SubjectId, SubjectName")]Subject s)
        {
          if (ModelState.IsValid)
          {
              bool result = subjectService.UpdateSubject(s);
              //subjektet nuk mund te kene emra te njejte
              if (result == false)
              {
                ModelState.AddModelError("Err", "This subject already exists.");
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
          Subject s = subjectService.GetSubjectBySubjectId(id);
          if (s != null)
            return View(s);
          else
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Subject s)
        {
          subjectService.DeleteSubject(s.SubjectId);
          return RedirectToAction("Index");
        }
  }
}