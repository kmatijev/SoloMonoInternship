using Model.Grade;
using Model.Student;
using WebRepository.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;

namespace WebRepository
{
    public class Repository : IRepository
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        public List<Grade> GetGrade(int id)
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

        public Grade PostGrade(int id, string value)
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

        public string PutGrade(int id, string value)
        {
         
            string combinedString = "";

            using (connection)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT GradeID, GradeName FROM GRADE", connection);

                adapter.UpdateCommand = new SqlCommand("UPDATE Grade SET GradeName = @GradeName " + "WHERE GradeID = @GradeID", connection);

                adapter.UpdateCommand.Parameters.Add("@GradeName", SqlDbType.NVarChar, 15).Value = value;


                SqlParameter parameter = adapter.UpdateCommand.Parameters.Add("@GradeID", SqlDbType.Int);
                parameter.SourceColumn = "GradeID";
                parameter.SourceVersion = DataRowVersion.Original;

                DataTable categoryTable = new DataTable();
                adapter.Fill(categoryTable);


                try
                {
                    DataRow categoryRow = categoryTable.Rows[0];
                    categoryRow["GradeName"] = value;

                    adapter.Update(categoryTable);


                    foreach (DataRow row in categoryTable.Rows)
                    {
                        {
                            combinedString += String.Format("{0}: {1}, ", row[0], row[1]);
                        }
                    }

                    return combinedString;

                }
                catch
                {
                    return "";
                }
            }
        }
        public string DeleteGrade(int id)
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

                    string returnString = "Deleted: GradeID: " + id;

                    return returnString;
                }
                catch
                {
                    return "";
                }
            }
        }

        public List<Student> GetStudent(int id)
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

        public Student PostStudent(int id, int gradeId, string value)
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

        public string PutStudent(int id, string value)
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

                string combinedString = "";

                try
                {
                    DataRow categoryRow = categoryTable.Rows[0];
                    categoryRow["GradeName"] = value;

                    adapter.Update(categoryTable);


                    foreach (DataRow row in categoryTable.Rows)
                    {
                        {
                            combinedString += String.Format("{0}: {1} {2}, ", row[0], row[1], row[2]);
                        }
                    }

                    return combinedString;

                }
                catch
                {
                    return "";
                }
            }
        }

        public string DeleteStudent(int id)
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
