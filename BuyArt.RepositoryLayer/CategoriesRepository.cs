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
  public class CategoriesRepository:ICategoryRepository
  {
    ProjectContext db;

    public CategoriesRepository()
    {
      db = new ProjectContext();
    }

    public List<Category> GetCategories()
    {
      List<Category> categories = db.Categories.ToList();
      return categories;
    }

    public Category GetCategoryByCategoryId(int CategoryId)
    {
      Category c = db.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
      return c;
    }

    public bool InsertCategory(Category c)
    {
      List<Category> sameNameCat = db.Categories.Where(x => x.CategoryName == c.CategoryName).ToList();
      if (sameNameCat.Count > 0)
        return false;
      else
      {
        db.Categories.Add(c);
        db.SaveChanges();
        return true;
      }
    }

    public bool UpdateCategory(Category c)
    {
      List<Category> sameNameCat = db.Categories.Where(x => x.CategoryName == c.CategoryName).ToList();
      if (sameNameCat.Count > 0)
        return false;
      else
      {
        Category existingCategory = db.Categories.Where(x => x.CategoryId == c.CategoryId).FirstOrDefault();
        existingCategory.CategoryName = c.CategoryName;
        db.SaveChanges();
        return true;
      }
      
    }

    public void DeleteCategory(int CategoryId)
    {
      Category existingCategory = db.Categories.Where(x => x.CategoryId == CategoryId).FirstOrDefault();
      db.Categories.Remove(existingCategory);
      db.SaveChanges();
    }
  }
}
