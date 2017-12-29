using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Dal;
namespace Bll
{
    public class checkRepair_BLL
    {
        public static String getTableString(string StartTime,string EndTime,string OrderCode, string StationNo, int PageIndex, out int totalcount)
        {
            return checkRepair_Dal.getTableString(StartTime, EndTime, OrderCode, StationNo, PageIndex, out totalcount);
        }
        public static DataTable getTableExcel(string StartTime, string EndTime, string OrderCode, string StationNo, int PageIndex, out int totalcount)
        {
            return checkRepair_Dal.getTableExcel(StartTime, EndTime, OrderCode, StationNo, PageIndex, out totalcount);
        }
        public static String GetListNew(string StartTime, string EndTime, string OrderCode, string StationNo,int  PageIndex, int PageSize, out int totalcount)
        {
            return checkRepair_Dal.GetListNew(StartTime, EndTime, OrderCode, StationNo, PageIndex, PageSize, out totalcount);
        }
    }
}
