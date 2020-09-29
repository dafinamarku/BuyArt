using BuyArt.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyArt.ServiceContracts
{
  public interface ISubjectsService
  {
    List<Subject> GetSubjects();
    Subject GetSubjectBySubjectId(int SubjectId);
    bool InsertSubject(Subject a);
    bool UpdateSubject(Subject a);
    void DeleteSubject(int SubjectId);
  }
}
