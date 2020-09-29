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
  public class CommentService:ICommentService
  {
    ICommentRepository comRep;
    public CommentService(ICommentRepository rep)
    {
      this.comRep = rep;
    }

    public void InsertComment(Comment c)
    {
      comRep.InsertComment(c);
    }

    public void DeleteComment(int id)
    {
      comRep.DeleteComment(id);
    }

    public Comment GetCommentById(int id)
    {
      return comRep.GetCommentById(id);
    }
  }
}
