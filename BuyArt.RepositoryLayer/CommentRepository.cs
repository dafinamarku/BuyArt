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
  public class CommentRepository:ICommentRepository
  {
    ProjectContext db;

    public CommentRepository()
    {
      this.db = new ProjectContext();
    }

    public void DeleteComment(int commentId)
    {
      Comment existingComment = db.Comments.Where(x => x.id == commentId).FirstOrDefault();
      db.Comments.Remove(existingComment);
      db.SaveChanges();
    }

    public void InsertComment(Comment c)
    {
      db.Comments.Add(c);
      db.SaveChanges();
    }

    public Comment GetCommentById(int id)
    {
      return db.Comments.Where(x => x.id == id).FirstOrDefault();
    }
  }
}
