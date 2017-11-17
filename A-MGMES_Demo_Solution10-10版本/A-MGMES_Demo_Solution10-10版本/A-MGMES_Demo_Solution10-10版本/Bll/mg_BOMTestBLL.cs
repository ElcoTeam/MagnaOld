using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using Tools;
using System.Data;
using Dal;

namespace Bll
{
    public class mg_BOMTestBLL
    {
        public static string QueryBOMTestList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_BOMTestModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_BOMTestPageModel model = new mg_BOMTestPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_BOMTestPageModel>(model);
            return jsonStr;
        }
        private static List<mg_BOMTestModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_BOMTestModel> list = mg_BOMTestDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_BOMTestModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_BOMTestModel> list = mg_BOMTestDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SaveBOMTest(mg_BOMTestModel model)
        {
            return model.test_id == 0 ? AddBOMTest(model) : UpdateBOMTest(model);
        }
        private static string AddBOMTest(mg_BOMTestModel model)
        {
            int count = mg_BOMTestDAL.AddBOMTest(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdateBOMTest(mg_BOMTestModel model)
        {
           int count = mg_BOMTestDAL.UpdateBOMTest(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeleteBOMTest(string test_id)
        {
            int count = mg_BOMTestDAL.DeleteBOMTest(test_id);
            return count > 0 ? "true" : "false";
        }
    }
    class mg_BOMTestPageModel
    {
        public string total;
        public List<mg_BOMTestModel> rows;
    }
}
