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
    public class mg_allpartBLL
    {
        public static bool AddAllByName(string all_no, string all_ratename, string all_colorname, string all_metaname,string all_desc)
        {
            return mg_allpartDAL.AddAllByName( all_no,  all_ratename,  all_colorname, all_metaname,all_desc) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_allpartDAL.GetAllData();
        }
        public static bool CheckAllByName(int a, int allid,string allno)
        {
            return mg_allpartDAL.CheckAllByName(a,allid,allno) == 0 ? true : false;
        }
        public static bool UpdateAllByName(int allid, string allno,string rate, string allcolor, string allmeta,string desc)
        {
            return mg_allpartDAL.UpDateAllByName(allid, allno, rate, allcolor, allmeta, desc) > 0 ? true : false;
        }

        public static bool DelAllByName(int allid)
        {
            return mg_allpartDAL.DelAllByName( allid) > 0 ? true : false;
        }


        /*
         * 
       *      姜任鹏
       * 
       */ 

        public static string SaveAllPart(mg_allpartModel model)
        {
            return model.all_id == 0 ? AddAllPart(model) : UpdateAllPart(model);
        }

        private static string UpdateAllPart(mg_allpartModel model)
        {
            int count = mg_allpartDAL.UpdateAllPart(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddAllPart(mg_allpartModel model)
        {
            int count = mg_allpartDAL.AddAllPart(model);
            return count > 0 ? "true" : "false";
        }

        public static string QueryAllPartList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_allpartModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_allpartPageModel model = new mg_allpartPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_allpartPageModel>(model);
            return jsonStr;
        }

        private static List<mg_allpartModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_allpartModel> list = mg_allpartDAL.QueryListForPaging(page,pagesize, out total);
            return list;
        }

        private static List<mg_allpartModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_allpartModel> list = mg_allpartDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeleteAllPart(string all_id)
        {
            int count = mg_allpartDAL.DeleteAllPart(all_id);
            return count > 0 ? "true" : "false";
        }

        public static string QueryAllPartListForPart()
        {
            string jsonStr = "[]";
            List<mg_allpartModel> list = mg_allpartDAL.QueryAllPartListForPart();
            jsonStr = JSONTools.ScriptSerialize<List<mg_allpartModel>>(list);
            return jsonStr;
        }
    }

    class mg_allpartPageModel
    {
        public string total;
        public List<mg_allpartModel> rows;
    }
}
