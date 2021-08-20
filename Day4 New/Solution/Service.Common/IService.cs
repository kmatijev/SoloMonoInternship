using System;
using System.Collections.Generic;
using Model.Grade;
using Model.Student;

namespace Service.Common
{
    public interface IService
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
