using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Model;
using System.Data;
using System.Data.SqlClient;
namespace Bll
{
   public class mg_alarmBLL
    {
       public static List<mg_alarm> getfsabydateNew(string dtb, string dta)
       {
           return mg_alarmDAL.getfsabydateNew( dtb,  dta);
       }
        public static DataTable getfsabydateexcel(string dtb, string dta)
        {
    
	       return mg_alarmDAL.getfsabydateexcel(dtb,dta);
           
        }
    }
}
