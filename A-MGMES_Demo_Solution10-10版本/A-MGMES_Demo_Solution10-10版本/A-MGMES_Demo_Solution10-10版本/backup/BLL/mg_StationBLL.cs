using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Tools;
using Model;


namespace BLL
{
    public class mg_StationBLL
    {
        public static bool AddStByName(string st_no, string st_name, int fl_id, int st_ispre)
        {
            return mg_StationDAL.AddStByName(st_no, st_name, fl_id, st_ispre) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_StationDAL.GetAllData();

        }

        public static bool CheckStByName(int a, int st_id, string st_no, string st_name, int fl_id)
        {
            return mg_StationDAL.CheckStByName(a, st_id, st_no, st_name, fl_id) == 0 ? true : false;
        }

        public static bool UpdateStByName(int st_id, string st_no, string st_name, int fl_id, int st_ispre)
        {
            return mg_StationDAL.UpDateStByName(st_id, st_no, st_name, fl_id, st_ispre) > 0 ? true : false;
        }

        public static bool DelStByName(int st_id)
        {
            return mg_StationDAL.DelStByName(st_id) > 0 ? true : false;
        }



        /*
        * 
        *      姜任鹏
        * 
        */

        public static string QueryStationForOperatorEditing()
        {
            string jsonStr = "[]";
            List<mg_stationModel> list = mg_StationDAL.QueryStationForOperatorEditing();
            jsonStr = JSONTools.ScriptSerialize<List<mg_stationModel>>(list);
            return jsonStr;
        }

        public static string GenerateNO()
        {
            return mg_StationDAL.GetMaxNO();
        }

        public static string saveStation(mg_stationModel model)
        {
            return model.st_id == 0 ? AddStation(model) : UpdateStation(model);
        }
        private static string UpdateStation(mg_stationModel model)
        {
            int count = mg_StationDAL.UpdateStation(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddStation(mg_stationModel model)
        {
            int count = mg_StationDAL.AddStation(model);
            return count > 0 ? "true" : "false";
        }


        public static string QueryStationList(string page, string pagesize, string fl_id)
        {
            string jsonStr = "[]";
            List<mg_stationModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total, fl_id);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total, fl_id);
            }

            mg_stationPageModel model = new mg_stationPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_stationPageModel>(model);
            return jsonStr;
        }

        private static List<mg_stationModel> QueryListForPaging(string page, string pagesize, out string total, string fl_id)
        {
            List<mg_stationModel> list = mg_StationDAL.QueryListForPaging(page, pagesize, out total, fl_id);
            return list;
        }

        private static List<mg_stationModel> QueryListForFirstPage(string pagesize, out string total, string fl_id)
        {
            List<mg_stationModel> list = mg_StationDAL.QueryListForFirstPage(pagesize, out total, fl_id);
            return list;
        }

        public static string DeleteStation(string st_id)
        {
            int count = mg_StationDAL.DeleteStation(st_id);
            return count > 0 ? "true" : "false";
        }

        public static string Sorting(string st_id, string st_order, string point)
        {
            int count = -1;
            if (point == "top")
            {
                count = mg_StationDAL.SortingTop(st_id, st_order);
            }
            else if (point == "bottom")
            {
                count = mg_StationDAL.SortingBottom(st_id, st_order);
            }
            return count != -1 ? "true" : "false";
        }

        public static string QueryStationForStepEditing(string fl_id)
        {
            string jsonStr = "[]";
            List<mg_stationModel> list = mg_StationDAL.QueryStationForStepEditing(fl_id);
            jsonStr = JSONTools.ScriptSerialize<List<mg_stationModel>>(list);
            return jsonStr;
        }

        public static mg_stationModel GetStationByMac(string mac)
        {
            return mg_StationDAL.GetStationByMac(mac);
        }
    }


    class mg_stationPageModel
    {
        public string total;
        public List<mg_stationModel> rows;
    }

}
