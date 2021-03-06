using Model.Student;
using Model.Grade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebRepository.Common
{
    public interface IRepository // sve sql naredbe
    {
        Task<Grade> GetGrade(int id);
        Task<List<Grade>> GetAllGrades();
        Task<Grade> PostGrade(int id, string value);
        Task<string> PutGrade(int id, string value);
        Task<string> DeleteGrade(int id);

        Task<List<Student>> GetStudent(int id);
        Task<List<Student>> GetAllStudents(string order, string sort, int pageSize, int pageNum, string atribute, string filter);
        Task<Student> PostStudent(int id, int gradeId, string value);
        Task<string> PutStudent(int id, string value);
        Task<string> DeleteStudent(int id);
    }
}