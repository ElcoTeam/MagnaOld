using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;
using SortManagent.Util;
namespace Bll
{
    public class px_MaterialsortprintingBLL
    {
        
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

        public static string printtestHas_getWLName(string id, string zfj, string erweima, bool iscd, string getWLName, string getWLName123)
        {

            GetModel getmodel = new GetModel();

            var print = px_PrintBLL.Querypx_PrintList().Where(s => s.PXID.Equals(erweima)).ToList();
            if (print.Count > 0)
            {
                List<GetIndex> pxlist = new List<GetIndex>();
                foreach (var item in print)
                {
                    GetIndex gi = new GetIndex();
                    gi.订单号 = item.orderid;
                    gi.车身号 = item.orderid;
                    gi.主副驾 = item.XF;
                    gi.等级 = item.cartype;
                    gi.零件号 = item.LingjianHao;
                    gi.零件号描述 = item.ordername;
                    pxlist.Add(gi);
                }
                bool flag = CallPrint.PrintM(pxlist, erweima, getWLName123, getWLName, false);
                if (flag)
                {

                    return "1";
                }

                return "0";

            }
            else
            {
                List<GetIndex> pxlist = getmodel.GetIndexWJ(id, zfj);
                List<GetIndex> searchlist = new List<GetIndex>();
                searchlist = pxlist.Where(s => s.mg_partorder_ordertype == 4).ToList();
                searchlist.AddRange(pxlist.ToList());
                pxlist = searchlist;
                bool flag = CallPrint.PrintM(pxlist, erweima, zfj + id, getWLName);
                if (flag)
                {
                    return "1";
                }

                return "0";
            }
        }
        class GetSPPageModel
        {
            public string total;
            public List<GetSP> rows;
        }
    }

}
