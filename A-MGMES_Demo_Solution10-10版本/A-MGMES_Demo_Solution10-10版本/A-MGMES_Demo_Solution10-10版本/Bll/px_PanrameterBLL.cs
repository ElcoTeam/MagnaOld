using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace Bll
{
    public class px_PanrameterBLL
    {
        public static bool AddAllByName(string all_no, string all_ratename, string all_colorname, string all_metaname,string all_desc)
        {
            return px_PanrameterDAL.AddAllByName(all_no, all_ratename, all_colorname, all_metaname, all_desc) > 0 ? true : false;
        }
        public  static bool UpPanrameterId(int id,string value)
        {
            return px_PanrameterDAL.UpPanrameterId(id, value) > 0 ? true : false;
         }
        public static bool DownPanrameterId(int id, string value)
        {
            return px_PanrameterDAL.DownPanrameterId(id, value) > 0 ? true : false;
        }
        public static bool sendPanrameter(int id, string value)
        {
            return px_PanrameterDAL.sendPanrameter(id, value) > 0 ? true : false;
        }
        public static bool PrintPanrameter(int id, string value)
        {
            return px_PanrameterDAL.PrintPanrameter(id, value) > 0 ? true : false;
        }
        public static bool AscordescPanrameter(int id, string value)
        {
            return px_PanrameterDAL.AscordescPanrameter(id, value) > 0 ? true : false;
        }     
        public static DataTable GetAllData()
        {
            return px_PanrameterDAL.GetAllData();
        }
        public static bool CheckAllByName(int a, int allid,string allno)
        {
            return px_PanrameterDAL.CheckAllByName(a, allid, allno) == 0 ? true : false;
        }
        public static bool UpdateAllByName(int allid, string allno,string rate, string allcolor, string allmeta,string desc)
        {
            return px_PanrameterDAL.UpDateAllByName(allid, allno, rate, allcolor, allmeta, desc) > 0 ? true : false;
        }

        public static bool DelAllByName(int allid)
        {
            return px_PanrameterDAL.DelAllByName(allid) > 0 ? true : false;
        }


        /*
         * 
       *      姜任鹏
       * 
       */

        public static string SavePanrameter(px_PanrameterModel model)
        {
            return model.SerialID == 0 ? AddPanrameter(model) : UpdatePanrameter(model);
        }

        private static string UpdatePanrameter(px_PanrameterModel model)
        {
            int count = px_PanrameterDAL.UpdatePanrameter(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddPanrameter(px_PanrameterModel model)
        {
            int count = px_PanrameterDAL.AddPanrameter(model);
            return count > 0 ? "true" : "false";
        }

        public static string QueryPanrameterList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<px_PanrameterModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            px_PanrameterPageModel model = new px_PanrameterPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<px_PanrameterPageModel>(model);
            return jsonStr;
        }

        private static List<px_PanrameterModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<px_PanrameterModel> list = px_PanrameterDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<px_PanrameterModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<px_PanrameterModel> list = px_PanrameterDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeletePanrameter(string all_id)
        {
            int count = px_PanrameterDAL.DeletePanrameter(all_id);
            return count > 0 ? "true" : "false";
        }

        public static string QueryPanrameterListForPart()
        {
            string jsonStr = "[]";
            List<px_PanrameterModel> list = px_PanrameterDAL.QueryPanrameterListForPart();
            jsonStr = JSONTools.ScriptSerialize<List<px_PanrameterModel>>(list);
            return jsonStr;
        }

        public static List<px_PanrameterModel> QueryPanrameterList()
        {
            return px_PanrameterDAL.QueryPanrameterListForPart();
        }
    }   
    class px_PanrameterPageModel
    {
        public string total;

        public List<px_PanrameterModel> rows;
    }
   
}
