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
  public class CategoriesService:ICategoriesService
  {
    ICategoryRepository rep;

    public CategoriesService(ICategoryRepository r)
    {
      this.rep = r;
    }

    public List<Category> GetCategories()
    {
      List<Category> categories = rep.GetCategories();
      return categories;
    }

    public Category GetCategoryByCategoryId(int CategoryId)
    {
      Category c = rep.GetCategoryByCategoryId(CategoryId);
      return c;
    }

    public bool InsertCategory(Category c)
    {
      return rep.InsertCategory(c);
    }

    public bool UpdateCategory(Category c)
    {
      return rep.UpdateCategory(c);
    }

    public void DeleteCategory(int CategoryId)
    {
      rep.DeleteCategory(CategoryId);
    }
  }
}
