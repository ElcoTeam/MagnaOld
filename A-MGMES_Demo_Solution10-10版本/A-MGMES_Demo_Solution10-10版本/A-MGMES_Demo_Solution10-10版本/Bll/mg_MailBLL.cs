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
   public class mg_MailBLL
    {
       public static string queryMailList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_MailModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_MailPageModel model = new mg_MailPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_MailPageModel>(model);
            return jsonStr;
        }

        private static List<mg_MailModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_MailModel> list = mg_MailDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_MailModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_MailModel> list = mg_MailDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SaveMail(mg_MailModel model)
        {
            return model.mail_id == 0 ? AddMail(model) : UpdateMail(model);
        }
        private static string UpdateMail(mg_MailModel model)
        {
            bool flag = mg_MailDAL.UpdateMail(model);
            return flag ? "true" : "false";
        }

        private static string AddMail(mg_MailModel model)
        {
            bool flag = mg_MailDAL.AddMail(model);
            return flag ? "true" : "false";
        }
        public static string DeleteMail(string ps_id)
        {
            int count = mg_MailDAL.DeleteMail(ps_id);
            return count > 0 ? "true" : "false";
        }
    }
   class mg_MailPageModel
   {
       public string total;
       public List<mg_MailModel> rows;
   }
}
