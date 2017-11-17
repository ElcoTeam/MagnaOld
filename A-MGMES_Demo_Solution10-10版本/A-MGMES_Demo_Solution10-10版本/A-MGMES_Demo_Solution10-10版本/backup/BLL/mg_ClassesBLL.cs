using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class mg_ClassesBLL
    {
        public static bool AddPositionByName(string classesname,string starttime, string endtime)
        {
            return mg_ClassesDAL.AddflByName(classesname, starttime, endtime) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_ClassesDAL.GetAllData();
        }

        public static bool UpdatePositionByName(int posi_id, string posi_name,string stime,string etime)
        {
            return mg_ClassesDAL.UpDateflByName(posi_id, posi_name, stime, etime) > 0 ? true : false;
        }

        public static bool DelPositionByName(int posi_id)
        {
            return mg_ClassesDAL.DelflByName(posi_id) > 0 ? true : false;
        }

        public static bool CheckPositionByName(int a, int classesid, string classesname)
        {
            return mg_ClassesDAL.CheckflByName(a, classesid, classesname) == 0 ? true : false;
        }
    }
}
