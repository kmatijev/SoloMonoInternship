using Model.Common;

namespace Model.Student
{
    public class Student : IStudent
    {
        public int id { get; set; }

        public string name { get; set; }

        public int gradeId { get; set; }
    }
}