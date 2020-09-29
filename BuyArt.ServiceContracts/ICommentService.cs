using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface ICommentService
  {
    void DeleteComment(int id);
    void InsertComment(Comment c);
    Comment GetCommentById(int id);
  }
}
