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
  public class StylesRepository:IStyleRepository
  {
    ProjectContext db;

    public StylesRepository()
    {
      this.db = new ProjectContext();
    }

    public List<Style> GetStyles()
    {
      List<Style> styles = db.Styles.ToList();
      return styles;
    }

    public Style GetStyleByStyleId(int StyleId)
    {
      Style s = db.Styles.Where(x => x.StyleId == StyleId).FirstOrDefault();
      return s;
    }

    public bool InsertStyle(Style s)
    {
      List<Style> sameNameStyle = db.Styles.Where(x => x.StyleName == s.StyleName).ToList();
      if (sameNameStyle.Count > 0)
        return false;
      else
      {
        db.Styles.Add(s);
        db.SaveChanges();
        return true;
      }
      
    }

    public bool UpdateStyle(Style s)
    {
      List<Style> sameNameStyle = db.Styles.Where(x => x.StyleName == s.StyleName).ToList();
      if (sameNameStyle.Count > 0)
        return false;
      else
      {
        Style existingStyle = db.Styles.Where(x => x.StyleId == s.StyleId).FirstOrDefault();
        existingStyle.StyleName = s.StyleName;
        db.SaveChanges();
        return true;
      }
    }

    public void DeleteStyle(int StyleId)
    {
      Style existingStyle = db.Styles.Where(x => x.StyleId == StyleId).FirstOrDefault();
      db.Styles.Remove(existingStyle);
      db.SaveChanges();
    }
  }
}
