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
  public class SubjectsRepository:ISubjectRepository
  {
    ProjectContext db;

    public SubjectsRepository()
    {
      this.db = new ProjectContext();
    }

    public List<Subject> GetSubjects()
    {
      List<Subject> subjects = db.Subjects.ToList();
      return subjects;
    }

    public Subject GetSubjectBySubjectId(int SubjectId)
    {
      Subject s = db.Subjects.Where(x => x.SubjectId == SubjectId).FirstOrDefault();
      return s;
    }

    public bool InsertSubject(Subject s)
    {
      List<Subject> sameNameSubject = db.Subjects.Where(x => x.SubjectName == s.SubjectName).ToList();
      if (sameNameSubject.Count > 0)
        return false;
      else
      {
        db.Subjects.Add(s);
        db.SaveChanges();
        return true;
      }
    }

    public bool UpdateSubject(Subject s)
    {
      List<Subject> sameNameSubject = db.Subjects.Where(x => x.SubjectName == s.SubjectName).ToList();
      if (sameNameSubject.Count > 0)
        return false;
      else
      {
        Subject existingSubject = db.Subjects.Where(x => x.SubjectId == s.SubjectId).FirstOrDefault();
        existingSubject.SubjectName = s.SubjectName;
        db.SaveChanges();
        return true;
      } 
    }

    public void DeleteSubject(int SubjectId)
    {
      Subject existingSubject = db.Subjects.Where(x => x.SubjectId == SubjectId).FirstOrDefault();
      db.Subjects.Remove(existingSubject);
      db.SaveChanges();
    }
  }
}
