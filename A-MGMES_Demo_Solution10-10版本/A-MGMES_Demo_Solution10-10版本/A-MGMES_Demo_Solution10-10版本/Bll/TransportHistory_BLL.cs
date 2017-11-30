using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using System.Data;
namespace Bll
{
  public  class TransportHistory_BLL
    {
     
           public static DataTable getTable(int Pagesize,int pageIndex, int StartIndex, int EndIndex, string SortFlag, string sortOrder, string wherestr,out int total)
        {
            return TransportHistory_Dal.getTable(Pagesize, pageIndex,StartIndex, EndIndex, SortFlag, sortOrder, wherestr,out total);
        }
           public static DataTable getTableExcel( string SortFlag, string sortOrder, string wherestr, out int total)
           {
               return TransportHistory_Dal.getTableExcel( SortFlag, sortOrder, wherestr, out total);
           }
      
    }
}
