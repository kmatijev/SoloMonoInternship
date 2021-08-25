using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Pager
    {
        public string AddPage(int page) => String.Format(" LIMIT {0} ", page);
    }

    public class Sorter
    {
        public string SortBy(string sort, string order)
        {
            if (order == "asc")
                return String.Format(" ORDER BY {0} ASC ", sort);
            else if (order == "desc")
                return String.Format(" ORDER BY {0} DESC ", sort);
            else
                return "";
        }
    }

        public class StudentFilter
        {
            public string Filter(string atribute, string filter)
            {
                if (atribute == "StudentName")
                    return String.Format(" WHERE {0} LIKE '%{1}%' ", atribute, filter);
                else
                    return "";
            }
        }

        public class GradeFilter
        {
            public string Filter(string atribute, string filter)
            {
                if (atribute == "GradeName")
                    return String.Format(" WHERE {0} LIKE '%{1}%' ", atribute, filter);
                else
                    return "";
            }
        }
    }
