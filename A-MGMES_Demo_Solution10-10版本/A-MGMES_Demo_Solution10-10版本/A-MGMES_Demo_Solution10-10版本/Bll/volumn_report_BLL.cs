using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using System.Data;
namespace Bll
{
   public class volumn_report_BLL
    {
        public static DataTable GetAddupProducts(string method, string dti1, string dti2, int flag, string st_no, out DataTable outtable)//产量报表
       {
           return volumn_report_Dal.GetAddupProducts(method, dti1, dti2, flag, st_no, out outtable);
       }
    }
}
