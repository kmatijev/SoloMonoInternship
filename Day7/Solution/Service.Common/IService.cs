using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Grade;
using Model.Student;

namespace Service.Common
{
    public interface IService
    {
        Task<List<Grade>> GetGrade(int id);
        Task<Grade> PostGrade(int id, string value);
        Task<string> PutGrade(int id, string value);
        Task<string> DeleteGrade(int id);

        Task<List<Student>> GetStudent(int id);
        Task<Student> PostStudent(int id, int gradeId, string value);
        Task<string> PutStudent(int id, string value);
        Task<string> DeleteStudent(int id);
    }
}
