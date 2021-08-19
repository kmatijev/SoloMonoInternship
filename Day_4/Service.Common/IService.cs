using System;
using System.Collections.Generic;
using Model.Common;
using Model.Grade;
using Model.Student;

namespace Service.Common
{
    public interface IService
    {
        List<Grade> GetGrade(int id);
        bool PostGrade(int id, string value);
        Grade PutGrade(int id, string value);
        string DeleteGrade(int id);

        List<Student> GetStudent(int id);
        bool PostStudent(int id, int gradeId, string value);
        Student PutStudent(int id, string value);
        string DeleteStudent(int id);
    }
}
