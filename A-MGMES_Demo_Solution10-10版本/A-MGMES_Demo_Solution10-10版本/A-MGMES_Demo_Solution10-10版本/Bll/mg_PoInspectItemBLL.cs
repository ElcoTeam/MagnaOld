using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Dal;
using Tools;
using System.Data;


namespace Bll
{
   
        public class mg_PoInspectItemBLL
    {

        public static string QueryPoInspectItemList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_PoInspectItemModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_PoInspectItemPageModel model = new mg_PoInspectItemPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_PoInspectItemPageModel>(model);
            return jsonStr;
        }
        public static string QueryPoInspectItemListALL()
        {
            string jsonStr = "[]";
            List<mg_PoInspectItemModel> list = null;
            string total = "0";
            list = QueryListALL(out  total);
            mg_PoInspectItemPageModel model = new mg_PoInspectItemPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_PoInspectItemPageModel>(model);
            return jsonStr;
        }
         private static List<mg_PoInspectItemModel> QueryListALL( out string total)
        {
            List<mg_PoInspectItemModel> list = mg_PoInspectItemDAL.QueryListALL( out total);
            return list;
        }
        private static List<mg_PoInspectItemModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_PoInspectItemModel> list = mg_PoInspectItemDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_PoInspectItemModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_PoInspectItemModel> list = mg_PoInspectItemDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string savePoInstpectItem(mg_PoInspectItemModel model)
        {
            return model.pi_id == 0 ? AddPoInstpectItem(model) : UpdatePoInstpectItem(model);
        }
        private static string AddPoInstpectItem(mg_PoInspectItemModel model)
        {
            int count = mg_PoInspectItemDAL.AddPoInstpectItem(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdatePoInstpectItem(mg_PoInspectItemModel model)
        {
            int count = mg_PoInspectItemDAL.UpdatePoInstpectItem(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeletePoInspectItem(string pi_id)
        {
            int count = mg_PoInspectItemDAL.DeletePoInspectItem(pi_id);
            return count > 0 ? "true" : "false";
        }
    }
    class mg_PoInspectItemPageModel
        {
          public string total;
          public List<mg_PoInspectItemModel> rows;
        }
    

    }

