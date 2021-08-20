using Model.Common.Grade;

namespace Model.Grade
{
    public class Grade : IGrade
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}