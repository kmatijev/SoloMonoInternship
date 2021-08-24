using Model.Grade;
using Model.Student;
using WebRepository.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace WebRepository
{
    public class Repository : IRepository
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        public async Task<List<Grade>> GetGrade(int id)
        {
            List<Grade> GradeList = new List<Grade>();

            using (connection)
            {
                connection.Open();
                string queryString = String.Format("SELECT GradeName, GradeID FROM Grade WHERE GradeID = {0};", id);
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Grade S = new Grade();
                        S.name = Convert.ToString(reader.GetString(0));
                        S.id = id;

                        GradeList.Add(S);
                    }
                    reader.NextResult();
                }
                connection.Close();
            }
            
            return GradeList;
        }

        public async Task<Grade> PostGrade(int id, string value)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("INSERT INTO GRADE(GradeID, GradeName) VALUES({0}, '{1}');", id, value);

                adapter.InsertCommand = new SqlCommand(queryString, connection);
                try
                {
                    adapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch
                {
                    connection.Close();
                    Grade G = new Grade()
                    {
                        id = 0,
                        name = ""
                    };

                    return G;
                }
            }

            Grade returnGrade = new Grade
            {
                id = id,
                name = value
            };

            return returnGrade;
        }

        public async Task<string> PutGrade(int id, string value)
        {
            using (connection)
            {
                try
                {
                    string sql = String.Format("UPDATE GRADE SET GradeName = '{0}' where GradeID ={1}", value, id);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = sql;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    return String.Format("Updated Grade with Grade ID = {0}, Grade Name = {1}.", id, value);
                }
                catch
                {
                    return "";
                }
            }
        }
        public async Task<string> DeleteGrade(int id)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("DELETE GRADE WHERE GradeID = {0}", id);

                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                try
                {
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                    string returnString = "Deleted: Grade ID: " + id;

                    return returnString;
                }
                catch
                {
                    return "";
                }
            }
        }

        public async Task<List<Student>> GetStudent(int id)
        {
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

                    connection.Close();
                }
            }

            return StudentList;
        }

        public async Task<Student> PostStudent(int id, int gradeId, string value)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("INSERT INTO STUDENT(StudentID, StudentName, GradeID) VALUES({0}, '{1}', {2});", id, value, gradeId);

                adapter.InsertCommand = new SqlCommand(queryString, connection);
                try
                {
                    adapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch
                {
                    connection.Close();
                    Student S = new Student()
                    {
                        id = 0,
                        gradeId = 0,
                        name = ""
                    };

                    return S;
                }
            }

            Student returnStudent = new Student()
            {
                id = id,
                name = value,
                gradeId = gradeId
            };

            return returnStudent;
        }

        public async Task<string> PutStudent(int id, string value)
        {
            using (connection)
            {
                try
                {
                    string sql = String.Format("UPDATE STUDENT SET StudentName = '{0}' where StudentID ={1}",value, id);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = sql;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    return String.Format("Updated Student with Student ID = {0}, Student Name = {1}.",id, value);
                }
                catch
                {
                    return "";
                }
            }
        }

        public async Task<string> DeleteStudent(int id)
        {
            using (connection)
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();

                string queryString = String.Format("DELETE STUDENT WHERE StudentID = {0}", id);

                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                try
                {
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                    string returnString = "Deleted: StudentID: " + id;

                    return returnString;
                }
                catch
                {
                    return "";
                }
            }
        }

    }
}
