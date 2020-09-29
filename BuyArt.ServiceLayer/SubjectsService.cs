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
  public class SubjectsService:ISubjectsService
  {
    ISubjectRepository rep;

    public SubjectsService(ISubjectRepository r)
    {
      this.rep = r;
    }

    public List<Subject> GetSubjects()
    {
      List<Subject> Subjects = rep.GetSubjects();
      return Subjects;
    }

    public Subject GetSubjectBySubjectId(int SubjectId)
    {
      Subject c = rep.GetSubjectBySubjectId(SubjectId);
      return c;
    }

    public bool InsertSubject(Subject s)
    {
      return rep.InsertSubject(s);
    }

    public bool UpdateSubject(Subject s)
    {
      return rep.UpdateSubject(s);
    }

    public void DeleteSubject(int SubjectId)
    {
      rep.DeleteSubject(SubjectId);
    }
  }
}
