using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Model.Student;
using System.Threading.Tasks;
using Service.Common;

namespace WebAPI.Controllers
{
    public class StudentController : ApiController
    {
        public IService StudentService { get; set; }
        public StudentController(IService service) => StudentService = service;
        public class RESTStudent
        {
            public int id { get; set; }
            public string name { get; set; }
            public int gradeId { get; set; }
        }

        public RESTStudent StudentToREST(Student domainStudent)
        {

            RESTStudent RestStudent = new RESTStudent
            {
                gradeId = domainStudent.gradeId,
                id = domainStudent.id,
                name = domainStudent.name
            };

            return RestStudent;
        }


        public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        // GET api/student/{id}
        [HttpGet]
        [Route("api/student/{id}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            List<RESTStudent> StudentList = (await StudentService.GetStudent(id)).ConvertAll(StudentToREST);

            string combinedString = "";

            if (StudentList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No student for given id.");
                return Msg;
            }
            else
            {
                foreach (RESTStudent x in StudentList)
                {
                    combinedString += String.Format("Student ID: {0}, Student Name: {1}, Grade ID: {2}", x.id, x.name, x.gradeId);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }

        [HttpGet]
        [Route("api/students/sort_by{order}={sort}/pageination={page}/where{atribute}={filter}")]
        public async Task<HttpResponseMessage> GetAll(string order, string sort, int page, string atribute, string filter)
        {
            List<RESTStudent> StudentList = (await StudentService.GetAllStudents(order, sort, page, atribute, filter)).ConvertAll(StudentToREST);

            string combinedString = "";

            if (StudentList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No student for given id.");
                return Msg;
            }
            else
            {
                foreach (RESTStudent x in StudentList)
                {
                    combinedString += String.Format("Student ID: {0}, Student Name: {1}, Grade ID: {2}    ", x.id, x.name, x.gradeId);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/student/{id}/{gradeId}")]
        public async Task<HttpResponseMessage> Post(int id, int gradeId, [FromBody] string value)
        {
            RESTStudent returnStudent = StudentToREST(await StudentService.PostStudent(id, gradeId, value));


            if (returnStudent.id == 0 && returnStudent.name == "" && returnStudent.gradeId == 0)
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.BadRequest, "Such student already exists.");
                return MsgBad;
            }

            string returnString = "Student ID: " + returnStudent.id + ", Student Name:" + returnStudent.name + " ,Grade ID: " + returnStudent.gradeId;

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.Created, returnString);
            return MsgGood;
        }

        // PUT api/student/5
        [HttpPut]
        [Route("api/student/{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody] string value)
        {
            string combinedString = await StudentService.PutStudent(id, value);

            if (combinedString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Student with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
            return Msg;
        }
        // DELETE api/student/5
        [HttpDelete]
        [Route("api/student/{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            string returnString = await StudentService.DeleteStudent(id);

            if (returnString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Student with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, returnString);
            return Msg;
        }
    }
}

