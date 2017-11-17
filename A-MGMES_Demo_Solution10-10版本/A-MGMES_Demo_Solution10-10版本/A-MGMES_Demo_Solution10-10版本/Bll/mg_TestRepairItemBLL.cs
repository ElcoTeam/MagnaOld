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
   
        public class mg_TestRepairItemBLL
    {

        public static string QueryTestRepairItemList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_TestRepairItemModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_TestRepairItemPageModel model = new mg_TestRepairItemPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_TestRepairItemPageModel>(model);
            return jsonStr;
        }
        private static List<mg_TestRepairItemModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_TestRepairItemModel> list = mg_TestRepairItemDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_TestRepairItemModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_TestRepairItemModel> list = mg_TestRepairItemDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string saveTestRepairItem(mg_TestRepairItemModel model)
        {
            return model.tr_id == 0 ? AddTestRepairItem(model) : UpdateTestRepairItem(model);
        }
        private static string AddTestRepairItem(mg_TestRepairItemModel model)
        {
            int count = mg_TestRepairItemDAL.AddTestRepairItem(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdateTestRepairItem(mg_TestRepairItemModel model)
        {
            int count = mg_TestRepairItemDAL.UpdateTestRepairItem(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeleteTestRepairItem(string tr_id)
        {
            int count = mg_TestRepairItemDAL.DeleteTestRepairItem(tr_id);
            return count > 0 ? "true" : "false";
        }
    }
    class mg_TestRepairItemPageModel
        {
          public string total;
          public List<mg_TestRepairItemModel> rows;
        }
    

    }

