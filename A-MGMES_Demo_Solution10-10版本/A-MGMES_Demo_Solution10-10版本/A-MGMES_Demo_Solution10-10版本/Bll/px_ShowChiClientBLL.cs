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
    public class px_ShowChiClientBLL
    {
         
        public static DataTable GetAllData()
        {
            return  px_ShowChiClientDAL.GetAllData();
        }

        public static string SaveShowChiClient(px_ShowChiClientModel model)
        {
            return model.SID == 0 ? AddShowChiClient(model) : UpdateShowChiClient(model);
        }

        private static string UpdateShowChiClient(px_ShowChiClientModel model)
        {
            int count = px_ShowChiClientDAL.UpdateShowChiClient(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddShowChiClient(px_ShowChiClientModel model)
        {
            int count = px_ShowChiClientDAL.AddShowChiClient(model);
            return count > 0 ? "true" : "false";
        }

        public static string queryShowChiClientList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List< px_ShowChiClientModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

             px_ShowChiClientPageModel model = new  px_ShowChiClientPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize< px_ShowChiClientPageModel>(model);
            return jsonStr;
        }

        private static List< px_ShowChiClientModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List< px_ShowChiClientModel> list =  px_ShowChiClientDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List< px_ShowChiClientModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List< px_ShowChiClientModel> list =  px_ShowChiClientDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeleteShowChiClient(string SID)
        {
            int count = px_ShowChiClientDAL.DeleteShowChiClient(SID);
            return count > 0 ? "true" : "false";
        }

        public static string QueryMaterialSortingListForPart()
        {
            string jsonStr = "[]";
            List<px_ShowChiClientModel> list = px_ShowChiClientDAL.Querypx_ShowChiClientListForPart();
            jsonStr = JSONTools.ScriptSerialize<List< px_ShowChiClientModel>>(list);
            return jsonStr;
        }

        public static List< px_ShowChiClientModel> QueryMaterialSortingList()
        {
            return px_ShowChiClientDAL.Querypx_ShowChiClientListForPart();
        }
    }

    class px_ShowChiClientPageModel
    {
        public string total;
        public List<px_ShowChiClientModel> rows;
    }
}
