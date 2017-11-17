using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Tools;
using System.Data;
using Dal;
using Bll;

namespace Bll
{
   public class mg_BOMTestGroupBLL
    {
       public static string queryGroupidForBOMTest()
       {
           string jsonStr = "[]";
           List<mg_BOMTestGroupModel> list = mg_BOMTestGroupDAL.queryGroupidForBOMTest();
           jsonStr = JSONTools.ScriptSerialize<List<mg_BOMTestGroupModel>>(list);
           return jsonStr;
       }
        public static string QueryBOMTestGroupList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_BOMTestGroupModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_BOMTestGroupPageModel model = new mg_BOMTestGroupPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_BOMTestGroupPageModel>(model);
            return jsonStr;
        }
       private static List<mg_BOMTestGroupModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_BOMTestGroupModel> list = mg_BOMTestGroupDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_BOMTestGroupModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_BOMTestGroupModel> list = mg_BOMTestGroupDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SaveBOMTestGroup(mg_BOMTestGroupModel model)
        {
            return model.group_id == 0 ? AddBOMTestGroup(model) : UpdateBOMTestGroup(model);
        }
        private static string AddBOMTestGroup(mg_BOMTestGroupModel model)
        {
            int count = mg_BOMTestGroupDAL.AddBOMTestGroup(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdateBOMTestGroup(mg_BOMTestGroupModel model)
        {
           int count = mg_BOMTestGroupDAL.UpdateBOMTestGroup(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeleteBOMTestGroup(string group_id)
        {
            int count = mg_BOMTestGroupDAL.DeleteBOMTestGroup(group_id);
            return count > 0 ? "true" : "false";
        }
    }
       class mg_BOMTestGroupPageModel
        {
          public string total;
          public List<mg_BOMTestGroupModel> rows;
        }
    }

