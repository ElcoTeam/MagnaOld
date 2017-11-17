using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace Bll
{
   public class mg_TestPartBLL
    {
       public static string queryTestPartList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_TestPartModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_TestPartPageModel model = new mg_TestPartPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_TestPartPageModel>(model);
            return jsonStr;
        }

        private static List<mg_TestPartModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_TestPartModel> list = mg_TestPartDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_TestPartModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_TestPartModel> list = mg_TestPartDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SaveTestPart(mg_TestPartModel model)
        {
            return model.p_id == 0 ? AddTestPart(model) : UpdateTestPart(model);
        }
        private static string UpdateTestPart(mg_TestPartModel model)
        {
            bool flag = mg_TestPartDAL.UpdateTestPart(model);
            return flag ? "true" : "false";
        }

        private static string AddTestPart(mg_TestPartModel model)
        {
            bool flag = mg_TestPartDAL.AddTestPart(model);
            return flag ? "true" : "false";
        }
        public static string DeleteTestPart(string ps_id)
        {
            int count = mg_TestPartDAL.DeleteTestPart(ps_id);
            return count > 0 ? "true" : "false";
        }
    }
   class mg_TestPartPageModel
   {
       public string total;
       public List<mg_TestPartModel> rows;
   }
}
