using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface IStylesService
  {
    List<Style> GetStyles();
    Style GetStyleByStyleId(int StyleId);
    bool InsertStyle(Style s);
    bool UpdateStyle(Style s);
    void DeleteStyle(int StyleId);
  }
}
