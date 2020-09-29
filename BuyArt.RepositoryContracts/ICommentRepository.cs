using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.RepositoryContracts
{
  public interface ICommentRepository
  {
    void DeleteComment(int commentId);
    void InsertComment(Comment c);
    Comment GetCommentById(int id);
  }
}
