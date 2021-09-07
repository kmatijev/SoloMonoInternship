using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Pager
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public Pager(int pagesize = 10, int pagenum = 1)
        {
            if (pagesize == 0)
            {
                pagesize = 10;
            }
            pageNum = pagenum;
            pageSize = pagesize;
        }

        public int Offset { get => (pageNum - 1) * pageSize; }
        public string AddPage() => String.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY ", Offset ,pageSize);
    }

    public class Sorter
    {
        public string order { get; set; }
        public string sort { get; set; }

        public Sorter(string newOrder, string newSort)
        {
            order = newOrder;
            sort = newSort;
        }
        public string SortBy()
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
        public string atribute { get; set; }
        public string filter { get; set; }
        public StudentFilter(string newAtribute, string newFilter)
        {
            atribute = newAtribute;
            filter = newFilter;
        }
        public string Filter()
            {
                if (atribute == "StudentName")
                    return String.Format(" WHERE {0} LIKE '%{1}%' ", atribute, filter);
                else
                    return "";
            }
        }

        public class GradeFilter
        {

        public string atribute { get; set; }
        public string filter { get; set; }

        public GradeFilter(string newAtribute, string newFilter)
        {
            atribute = newAtribute;
            filter = newFilter;
        }
        public string Filter()
            {
                if (atribute == "GradeName")
                    return String.Format(" WHERE {0} LIKE '%{1}%' ", atribute, filter);
                else
                    return "";
            }
        }
    }
