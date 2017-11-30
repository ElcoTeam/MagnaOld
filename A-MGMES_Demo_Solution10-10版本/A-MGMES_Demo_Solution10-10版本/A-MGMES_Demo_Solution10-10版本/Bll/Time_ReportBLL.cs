using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dal;
namespace Bll
{
  public  class Time_ReportBLL
    {
      public static DataTable TimeProducts(string dti1, string dti2, int flag, string st_no)  //时间报表的
      {
          return Time_ReportDal.TimeProducts(dti1, dti2, flag, st_no);
          
      }
    }
}
