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
    public class px_InternetPrinterBLL
    {
      

        public static DataTable GetAllData()
        {
            return px_InternetPrinterDAL.GetAllData();
        }
        public static string SaveInternetPrinter(px_InternetPrinterModel model)
        {
            return model.IID == 0 ? AddInternetPrinter(model) : UpdateInternetPrinter(model);
        }

        private static string UpdateInternetPrinter(px_InternetPrinterModel model)
        {
            int count = px_InternetPrinterDAL.UpdateInternetPrinter(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddInternetPrinter(px_InternetPrinterModel model)
        {
            int count = px_InternetPrinterDAL.AddInternetPrinter(model);
            return count > 0 ? "true" : "false";
        }

        public static string QueryInternetPrinterList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<px_InternetPrinterModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            px_InternetPrinterPageModel model = new px_InternetPrinterPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<px_InternetPrinterPageModel>(model);
            return jsonStr;
        }

        private static List<px_InternetPrinterModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<px_InternetPrinterModel> list = px_InternetPrinterDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<px_InternetPrinterModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<px_InternetPrinterModel> list = px_InternetPrinterDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeleteInternetPrinter(string all_id)
        {
            int count = px_InternetPrinterDAL.DeleteInternetPrinter(all_id);
            return count > 0 ? "true" : "false";
        }

        public static string QueryInternetPrinterListForPart()
        {
            string jsonStr = "[]";
            List<px_InternetPrinterModel> list = px_InternetPrinterDAL.QueryInternetPrinterListForPart();
            jsonStr = JSONTools.ScriptSerialize<List<px_InternetPrinterModel>>(list);
            return jsonStr;
        }

        public static List<px_InternetPrinterModel> QueryInternetPrinterList()
        {
            return px_InternetPrinterDAL.QueryInternetPrinterListForPart();
        }
    }

    class px_InternetPrinterPageModel
    {
        public string total;
        public List<px_InternetPrinterModel> rows;
    }
}
