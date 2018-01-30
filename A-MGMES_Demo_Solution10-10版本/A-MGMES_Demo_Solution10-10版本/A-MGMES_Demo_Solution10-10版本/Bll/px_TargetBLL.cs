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
    public class px_TargetBLL
    {


        public static string Savepx_Target(px_TargetModel model)
        {
            return model.pxtarget_id == 0 ? Addpx_Target(model) : Updatepx_Target(model);
        }

        private static string Updatepx_Target(px_TargetModel model)
        {
            int count = px_TargetDAL.UpdateTarget(model);
            return count > 0 ? "true" : "false";
        }

        private static string Addpx_Target(px_TargetModel model)
        {
            int count = 0;
            return count > 0 ? "true" : "false";
        }

        public static string Querypx_TargetList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<px_TargetModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            px_TargetPageModel model = new px_TargetPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<px_TargetPageModel>(model);
            return jsonStr;
        }

        private static List<px_TargetModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<px_TargetModel> list = px_TargetDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<px_TargetModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<px_TargetModel> list = px_TargetDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

 
    }

    class px_TargetPageModel
    {
        public string total;
        public List<px_TargetModel> rows;
    }
}
