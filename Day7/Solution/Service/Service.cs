using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Grade;
using Model.Student;
using Service.Common;
using WebRepository;
using WebRepository.Common;

namespace DService
{
    public class PDService : IService
    {
        protected IRepository Repo { get; set; }

        public PDService(IRepository repo) => Repo = repo;

        public async Task<List<Grade>> GetGrade(int id) => await Repo.GetGrade(id);
        public async Task<List<Grade>> GetAllGrades(string order, string sort, int pageSize, int pageNum, string atribute, string filter) => await Repo.GetAllGrades(order, sort, pageSize, pageNum, atribute, filter);
        public async Task<Grade> PostGrade(int id, string value) => await Repo .PostGrade(id, value);
        public async Task<string> PutGrade(int id, string value) => await Repo.PutGrade(id, value);
        public async Task<string> DeleteGrade(int id) => await Repo .DeleteGrade(id);

        public async Task<List<Student>> GetStudent(int id) => await Repo .GetStudent(id);
        public async Task<List<Student>> GetAllStudents(string order, string sort, int pageSize, int pageNum, string atribute, string filter) => await Repo.GetAllStudents(order, sort, pageSize, pageNum, atribute, filter);
        public async Task<Student> PostStudent(int id, int gradeId, string value) => await Repo.PostStudent(id, gradeId, value);
        public async Task<string> PutStudent(int id, string value) => await Repo.PutStudent(id, value);
        public async Task<string> DeleteStudent(int id) => await Repo.DeleteStudent(id);
    }
}