using System.Collections.Generic;
using Model.Grade;
using Model.Student;
using Service.Common;
using WebRepository;

namespace DService
{
    public class Service : IService
    {
        private Repository Repo = new Repository();
        public List<Grade> GetGrade(int id) => Repo.GetGrade(id);
        public Grade PostGrade(int id, string value) => Repo.PostGrade(id, value);
        public string PutGrade(int id, string value) => Repo.PutGrade(id, value);
        public string DeleteGrade(int id) => Repo.DeleteGrade(id);

        public List<Student> GetStudent(int id) => Repo.GetStudent(id);
        public Student PostStudent(int id, int gradeId, string value) => Repo.PostStudent(id, gradeId, value);
        public string PutStudent(int id, string value) => Repo.PutStudent(id, value);
        public string DeleteStudent(int id) => Repo.DeleteStudent(id);
    }
}