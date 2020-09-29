using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface ICategoryRepository
  {
    List<Category> GetCategories();
    Category GetCategoryByCategoryId(int CategoryId);
    bool InsertCategory(Category c);
    bool UpdateCategory(Category c);
    void DeleteCategory(int CategoryId);
  }
}
