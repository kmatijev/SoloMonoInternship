using System;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Model.Common;
using Model.Student;
using Service;

namespace Day4.WebAPI.Controllers
{
    public class StudentController : ApiController
    {
        public SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        // GET api/student/{id}
        [HttpGet]
        [Route("api/student/{id}")]
        public HttpResponseMessage Get(int id)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

            List<Student> StudentList = new List<Student>();

            using (connection)
            {
                connection.Open();
                string queryString = String.Format("SELECT StudentName, GradeID FROM STUDENT WHERE StudentID = {0};", id);
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student S = new Student();
                        S.name = Convert.ToString(reader.GetString(0));
                        S.gradeId = Convert.ToInt32(reader.GetInt32(1));
                        S.id = id;

                        StudentList.Add(S);
                    }
                    reader.NextResult();
                    string combinedString = "";
                    foreach (Student x in StudentList)
                    {
                        combinedString += String.Format("Student ID: {0}, Student Name: {1}, Grade ID: {2}", x.id, x.name, x.gradeId);
                    }
                    connection.Close();

                    HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                    return Msg;
                }
                else
                {
                    connection.Close();
                    HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.BadRequest, "No student for given id.");
                    return Msg;
                }
            }
        }
        // POST api/student/{id}
        [HttpPost]
        [Route("api/student/{id}/{gradeId}")]
        public HttpResponseMessage Post(int id, int gradeId, [FromBody] string value)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("INSERT INTO STUDENT(StudentID, StudentName, GradeID) VALUES({0}, '{1}', {2});", id, value, gradeId);

                adapter.InsertCommand = new SqlCommand(queryString, connection);
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
            }

            string returnString = "StudentID: " + id + ", StudentName:" + value + " ,StudentID: " + gradeId;

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.Created, returnString);
            return Msg;
        }

        // PUT api/student/5
        [HttpPut]
        [Route("api/student/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] string value)
        {
            using (connection)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT StudentID, StudentName FROM STUDENT", connection);

                adapter.UpdateCommand = new SqlCommand("UPDATE STUDENT SET StudentName = @StudentName " + "WHERE StudentID = @StudentID", connection);

                adapter.UpdateCommand.Parameters.Add("@StudentName", SqlDbType.NVarChar, 15).Value = value;


                SqlParameter parameter = adapter.UpdateCommand.Parameters.Add("@StudentID", SqlDbType.Int);
                parameter.SourceColumn = "StudentID";
                parameter.SourceVersion = DataRowVersion.Original;

                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);

                DataRow categoryRow = categoryTable.Rows[0];
                categoryRow["StudentName"] = value;

                adapter.Update(categoryTable);

                string combinedString = "";

                foreach (DataRow row in categoryTable.Rows)
                {
                    {
                        combinedString += String.Format("{0}: {1}, ", row[0], row[1]);
                    }
                }
                HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, combinedString);
                return Msg;
            }
        }
        // DELETE api/student/5
        [HttpDelete]
        [Route("api/student/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("DELETE STUDENT WHERE StudentID = {0}", id);

                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                adapter.DeleteCommand.ExecuteNonQuery();
                connection.Close();
            }

            string returnString = "Deleted: StudentID: " + id;

            HttpResponseMessage Msg = Request.CreateResponse(HttpStatusCode.OK, returnString);
            return Msg;
        }
    }
}
