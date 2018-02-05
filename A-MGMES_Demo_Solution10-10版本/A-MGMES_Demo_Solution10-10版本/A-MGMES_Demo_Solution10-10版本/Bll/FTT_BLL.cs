using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using System.Data;
namespace Bll
{
   public class FTT_BLL
    {
       public static DataTable getTable(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
       {
           return FTT_Dal.getTable(Pagesize, pageIndex, StartIndex, EndIndex, SortFlag, sortOrder, wherestr, out total);
       }
       public static DataTable getTableExcel(int Pagesize, int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr, out int total)
       {
           return FTT_Dal.getTableExcel(Pagesize, pageIndex, StartIndex, EndIndex, SortFlag, sortOrder, wherestr, out total);
       }
    }
}
