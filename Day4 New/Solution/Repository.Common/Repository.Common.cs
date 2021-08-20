using Model.Student;
using Model.Grade;
using System.Collections.Generic;

namespace WebRepository.Common
{
    public interface IRepository // sve sql naredbe
    {
        List<Grade> GetGrade(int id);
        Grade PostGrade(int id, string value);
        string PutGrade(int id, string value);
        string DeleteGrade(int id);

        List<Student> GetStudent(int id);
        Student PostStudent(int id, int gradeId, string value);
        string PutStudent(int id, string value);
        string DeleteStudent(int id);
    }
}