using System;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Model.Student;
using Model.Grade;
using DService;

namespace WebAPI.Controllers
{

    public class StudentController : ApiController
    {
        private Service SerStud = new Service();

        public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        // GET api/student/{id}
        [HttpGet]
        [Route("api/student/{id}")]
        public HttpResponseMessage Get(int id)
        {
            List<Student> StudentList = SerStud.GetStudent(id);

            string combinedString = "";

            if (StudentList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No student for given id.");
                return Msg;
            }
            else
            {
                foreach (Student x in StudentList)
                {
                    combinedString += String.Format("Student ID: {0}, Student Name: {1}, Grade ID: {2}", x.id, x.name, x.gradeId);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/student/{id}/{gradeId}")]
        public HttpResponseMessage Post(int id, int gradeId, [FromBody] string value)
        {
            Student returnStudent = SerStud.PostStudent(id, gradeId, value);


            if (returnStudent.id == 0 && returnStudent.name == "" && returnStudent.gradeId == 0)
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.BadRequest, "Such student already exists.");
                return MsgBad;
            }

            string returnString = "StudentID: " + returnStudent.id + ", StudentName:" + returnStudent.name + " ,StudentID: " + returnStudent.gradeId;

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.Created, returnString);
            return MsgGood;
        }

        // PUT api/student/5
        [HttpPut]
        [Route("api/student/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            string combinedString = SerStud.PutStudent(id, value);

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
        public HttpResponseMessage Delete(int id)
        {
            string returnString = SerStud.DeleteStudent(id);

            if (returnString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Student with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, returnString);
            return Msg;
        }
    }

    public class GradeController : ApiController
    {
        private Service SerGrade = new Service();

        public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        // GET api/student/{id}
        [HttpGet]
        [Route("api/grade/{id}")]
        public HttpResponseMessage Get(int id)
        {

            List<Grade> GradeList = SerGrade.GetGrade(id);

            string combinedString = "";

            if (GradeList.Capacity == 0)
            {
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No grade for given id.");
                return Msg;
            }
            else
            {
                foreach (Grade x in GradeList)
                {
                    combinedString += String.Format("Grade ID: {0}, Grade Name: {1}", x.id, x.name);
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/grade/{id}")]
        public HttpResponseMessage Post(int id, [FromBody] string value)
        {
            Grade returnGrade = SerGrade.PostGrade(id, value);

            if (returnGrade.id == 0 && returnGrade.name == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.BadRequest, "Such grade already exists.");
                return MsgBad;
            }

            string returnString = "GradeID: " + returnGrade.id + ", GradeName:" + returnGrade.name;

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.Created, returnString);
            return MsgGood;
        }

        // PUT api/student/5
        [HttpPut]
        [Route("api/grade/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            string combinedString = SerGrade.PutGrade(id, value);

            if (combinedString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Grade with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
            return Msg;
        }
        // DELETE api/student/5
        [HttpDelete]
        [Route("api/grade/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            string returnString = SerGrade.DeleteGrade(id);

            if(returnString == "")
            {
                HttpResponseMessage MsgBad = Request.CreateResponse(HttpStatusCode.NotFound, "Grade with given id does not exist.");
                return MsgBad;
            }

            HttpResponseMessage MsgGood = Request.CreateResponse(HttpStatusCode.OK, returnString);
            return MsgGood;
        }
    }
}

