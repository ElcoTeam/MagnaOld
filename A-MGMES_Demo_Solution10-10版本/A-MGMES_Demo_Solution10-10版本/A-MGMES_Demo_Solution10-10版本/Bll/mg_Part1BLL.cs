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
    public class mg_Part1BLL
    {
        public static string QueryPart1List(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_partModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_Part1PageModel model = new mg_Part1PageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_Part1PageModel>(model);
            return jsonStr;
        }
        private static List<mg_partModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_partModel> list = mg_Part1DAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_partModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_partModel> list = mg_Part1DAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SavePart1(mg_partModel model)
        {
            return model.part_id == 0 ? AddPart1(model) : UpdatePart1(model);
        }
        private static string AddPart1(mg_partModel model)
        {
            int count = mg_Part1DAL.AddPart1(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdatePart1(mg_partModel model)
        {
           int count = mg_Part1DAL.UpdatePart1(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeletePart1(string part_id)
        {
            int count = mg_Part1DAL.DeletePart1(part_id);
            return count > 0 ? "true" : "false";
        }
    }
    class mg_Part1PageModel
    {
        public string total;
        public List<mg_partModel> rows;
    }
}
