using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace Bll
{
    public class mg_allpart_part_relBLL
    {
        public static bool AddAllPartByName(int allid,int partid)
        {
            return mg_allpart_part_relDAL.AddAllPartByName(allid,partid) > 0 ? true : false;
        }
        public static DataTable GetAllData()
        {
            return mg_allpart_part_relDAL.GetAllData();
        }
    }
}
