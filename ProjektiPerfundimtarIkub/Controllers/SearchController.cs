using BuyArt.DomainModels;
using BuyArt.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektypeerfundimtarIkub.Controllers
{
    public class SearchController : Controller
    {
    IArtworksService artService;
    public SearchController(IArtworksService s)
    {
      this.artService = s;
    }
    //type mund te marre vlerat 'category', 'subject', ose 'style'
    //ndersa name merr namen e kategorise/stilit ose subjektit perkates te klikuar
    [Route("{search:string}/{type:string}/{name:string}")]
    public ActionResult Explore(string search, string type, string name, string SortColumn = "Price", string IconClass = "fas fa-sort-down", int PageNo = 1)
    {
      if (type == null || name == null &&(type!="category"&&type!="style"&&type!="subject"))
      {
        return HttpNotFound();
      }
      ViewBag.type = type;
      ViewBag.name = name;
      List<Artwork> searchResult = artService.SearchArtworks(type, name);
      /*Renditja*/
      ViewBag.SortColumn = SortColumn;
      ViewBag.IconClass = IconClass;
      if (ViewBag.SortColumn == "Title")
      {
        if (ViewBag.IconClass == "fas fa-sort-down")
          searchResult = searchResult.OrderByDescending(temp => temp.Title).ToList();
        else
          searchResult = searchResult.OrderBy(temp => temp.Title).ToList();
      }
      else if (ViewBag.SortColumn == "Price")
      {
        if (ViewBag.IconClass == "fas fa-sort-down")
          searchResult = searchResult.OrderByDescending(temp => temp.Price).ToList();
        else
          searchResult = searchResult.OrderBy(temp => temp.Price).ToList();
      }

      if (!string.IsNullOrEmpty(search))
      {
        search = search.ToUpper();
        searchResult = searchResult.Where(s => s.Title.ToUpper().Contains(search) || (s.Description != null && s.Description.ToUpper().Contains(search))).ToList();
      }
      /* Pagination */
      int NoOfRecordsPerPage = 6;
      int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(searchResult.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
      int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
      ViewBag.PageNo = PageNo;
      ViewBag.NoOfPages = NoOfPages;
    
      searchResult = searchResult.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
      return View(searchResult);
    }
  }
}