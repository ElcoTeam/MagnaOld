using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Tools;
using System.Data;

namespace Bll
{
    public class mg_DepartmentBLL
    {

        public static bool AddDepartmentByName(string depname)
        {
            return mg_DepartmentDAL.AddDepartmentByName(depname) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_DepartmentDAL.GetAllData();
        }

        public static bool UpdateDepartmentByName(int dep_id, string dep_name)
        {
            return mg_DepartmentDAL.UpDateDepartmentByName(dep_id, dep_name) > 0 ? true : false;
        }

        public static bool DelDepartmentByName(int dep_id)
        {
            return mg_DepartmentDAL.DelDepartmentByName(dep_id) > 0 ? true : false;
        }

        public static bool CheckDepartmentByName(int a, int dep_id, string dep_name)
        {
            return mg_DepartmentDAL.CheckDepartmentByName(a,dep_id, dep_name) == 0 ? true : false;
        }

        public static string QueryDepartmentsForUser()
        {
            string jsonStr = "[]";
            List<mg_DepartmentModel> list = mg_DepartmentDAL.QueryDepartmentsForUser();
            jsonStr = JSONTools.ScriptSerialize<List<mg_DepartmentModel>>(list);
            return jsonStr;
        }
    }


}
