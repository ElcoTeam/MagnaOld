using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace BLL
{
    public class mg_PositionBLL
    {
        public static bool AddPositionByName(string flname)
        {
            return mg_PositionDAL.AddPositionByName(flname) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_PositionDAL.GetAllData();
        }

        public static bool UpdatePositionByName(int flid, string flname)
        {
            return mg_PositionDAL.UpDatePositionByName(flid, flname) > 0 ? true : false;
        }

        public static bool DelPositionByName(int flid)
        {
            return mg_PositionDAL.DelPositionByName(flid) > 0 ? true : false;
        }

        public static bool CheckPositionByName(int a, int flid, string flname)
        {
            return mg_PositionDAL.CheckPositionByName(a, flid, flname) == 0 ? true : false;
        }

        public static string QueryPositionsForUser()
        {
            string jsonStr = "[]";
            List<mg_PositionModel> list = mg_PositionDAL.QueryPositionsForUser();
            jsonStr = JSONTools.ScriptSerialize<List<mg_PositionModel>>(list);
            return jsonStr;
        }

        
    }
}
