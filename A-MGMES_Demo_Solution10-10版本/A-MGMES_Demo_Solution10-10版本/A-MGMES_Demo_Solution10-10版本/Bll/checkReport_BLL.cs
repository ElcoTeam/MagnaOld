using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dal;
namespace Bll
{
    public class checkReport_BLL
    {
        public static DataTable getTable(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
        {
            return checkReport_Dal.getTable(Pagesize, pageIndex, StartIndex, EndIndex, SortFlag, sortOrder, wherestr, out total);
        }
        public static DataTable getTableExcel(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
        {
            return checkReport_Dal.getTableExcel(Pagesize, pageIndex, StartIndex, EndIndex, SortFlag, sortOrder, wherestr, out total);
        }
    }
}
