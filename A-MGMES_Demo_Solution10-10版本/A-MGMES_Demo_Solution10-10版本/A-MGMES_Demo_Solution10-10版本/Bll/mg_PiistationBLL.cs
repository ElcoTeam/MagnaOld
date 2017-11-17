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
   public class mg_PiistationBLL
    {
       public static string queryPiistationList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_PiistationModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_piistationPageModel model = new mg_piistationPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_piistationPageModel>(model);
            return jsonStr;
        }

        private static List<mg_PiistationModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_PiistationModel> list = mg_PiistationDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_PiistationModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_PiistationModel> list = mg_PiistationDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SavePiiStation(mg_PiistationModel model)
        {
            return model.ps_id == 0 ? AddPiiStation(model) : UpdatePiiStation(model);
        }
        private static string UpdatePiiStation(mg_PiistationModel model)
        {
            bool flag = mg_PiistationDAL.UpdatePiiStation(model);
            return flag ? "true" : "false";
        }

        private static string AddPiiStation(mg_PiistationModel model)
        {
            bool flag = mg_PiistationDAL.AddPiiStation(model);
            return flag ? "true" : "false";
        }
        public static string DeletePiistation(string ps_id)
        {
            int count = mg_PiistationDAL.DeletePiistation(ps_id);
            return count > 0 ? "true" : "false";
        }
    }
   class mg_piistationPageModel
   {
       public string total;
       public List<mg_PiistationModel> rows;
   }
}
