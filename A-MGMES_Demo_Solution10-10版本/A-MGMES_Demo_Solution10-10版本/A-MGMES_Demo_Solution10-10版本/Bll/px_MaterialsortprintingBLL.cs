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
    public class px_MaterialsortprintingBLL
    {
        GetModel getmodel = new GetModel();
        public static List<classess> Querymg_classes()
        {
            return px_MaterialsortprintingDAL.Querymg_classes();
        }
        public static List< px_ShowChiClientModel> QueryMaterialSortingList()
        {
            return px_ShowChiClientDAL.Querypx_ShowChiClientListForPart();
        }
        public static string queryMaterialsortprintingList(string page, string pagesize, DateTime? starttime, DateTime? endtime, string csh)
        {
            string jsonStr = "[]";
            List<GetSP> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total,  starttime, endtime,  csh);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total, starttime, endtime, csh);
            }

            GetSPPageModel model = new GetSPPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<GetSPPageModel>(model);
            return jsonStr;
        }
        private static List<GetSP> QueryListForPaging(string page, string pagesize, out string total, DateTime? starttime, DateTime? endtime, string csh)
        {
            List<GetSP> list = px_MaterialsortprintingDAL.QueryListForPaging(page, pagesize, out total, starttime, endtime, csh);
            return list;
        }
        private static List<GetSP> QueryListForFirstPage(string pagesize, out string total, DateTime? starttime, DateTime? endtime, string csh)
        {
            List<GetSP> list = px_MaterialsortprintingDAL.QueryListForFirstPage(pagesize, out total, starttime, endtime, csh);
            return list;
        }
        class GetSPPageModel
        {
            public string total;
            public List<GetSP> rows;
        }
    }

}
