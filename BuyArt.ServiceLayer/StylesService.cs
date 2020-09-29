using BuyArt.DataLayer;
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
  public class StylesService:IStylesService
  {
    IStyleRepository rep;

    public StylesService(IStyleRepository r)
    {
      this.rep = r; 
    }

    public List<Style> GetStyles()
    {
      List<Style> Styles = rep.GetStyles();
      return Styles;
    }

    public Style GetStyleByStyleId(int StyleId)
    {
      Style c = rep.GetStyleByStyleId(StyleId);
      return c;
    }

    public bool InsertStyle(Style s)
    {
      return rep.InsertStyle(s);
    }

    public bool UpdateStyle(Style s)
    {
      return rep.UpdateStyle(s);
    }

    
    public void DeleteStyle(int StyleId)
    {
      ProjectContext db = new ProjectContext();
      Style styleToDelete = rep.GetStyleByStyleId(StyleId);
      //veprat qe kane kete stil do te fshihen vetem ne rast se nuk kane stile te tjera pervec atij stili qe do te fshihet
      List<Artwork> artw = db.Artworks.Where(x => x.ArtworkStyles.Where(s=>s.StyleId==StyleId).Count()==1 && x.ArtworkStyles.Count==1).ToList();
      db.Artworks.RemoveRange(artw);
      db.SaveChanges();
      rep.DeleteStyle(StyleId);
    }
  }
}
