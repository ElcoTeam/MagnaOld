using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;
using System.Transactions;

namespace Bll
{
    public class mg_CustomerOrderBLL
    {
        public static bool AdddOrder(string co_no, int all_id, int co_count, string co_customer)
        {
            return mg_CustomerOrderDAL.AddOrder(co_no, all_id, co_count, co_customer) > 0 ? true : false;
        }

        public static DataTable GetAllData(string strstate)
        {
            return mg_CustomerOrderDAL.GetAllData(strstate);
        }

        public static bool UpdateOrder(int coid, string co_no, int allid, int co_count, string co_customer)
        {
            return mg_CustomerOrderDAL.UpDateOrder(coid, co_no, allid, co_count, co_customer) > 0 ? true : false;
        }

        public static bool CheckdOrder(int a, int co_id, string co_no)
        {
            return mg_CustomerOrderDAL.CheckOrder(a, co_id, co_no) == 0 ? true : false;
        }

        public static bool DelOrder(int coid)
        {
            return mg_CustomerOrderDAL.DelOrder(coid) > 0 ? true : false;
        }


        public static string saveCustomerOrder(mg_CustomerOrderModel model)
        {
            int id = (int)model.co_id;
            int isCutted = (int)model.co_isCutted;

            if (id != 0 && isCutted == 0)      //更新订单信息，不拆单
            {
                return UpdateCutomerOrderWithoutCutting(model);
            }
            else if (id == 0 && isCutted == 0)//新增订单信息，不拆单
            {
                return AddCutomerOrderWithoutCutting(model) ;
            }
            else if (id != 0 && isCutted != 0)//更新订单信息，拆单 (未完成)
            {
                return UpdateCutomerOrderWithCutting(model);
            }
            else if (id == 0 && isCutted != 0)//新增订单信息，拆单 (未完成)
            {
                return AddCutomerOrderWithCutting(model);
            }
            return "false";
        }

        //新增订单信息，拆单
        private static string AddCutomerOrderWithCutting(mg_CustomerOrderModel model)
        {
            int count = mg_CustomerOrderDAL.AddCutomerOrderWithCutting(model);
            return count > 0 ? "true" : "false";
        }

        //更新订单信息，拆单
        private static string UpdateCutomerOrderWithCutting(mg_CustomerOrderModel model)
        {
            throw new NotImplementedException();
        }

        //新增订单信息，不拆单
        private static string AddCutomerOrderWithoutCutting(mg_CustomerOrderModel model)
        {
            int count = mg_CustomerOrderDAL.AddCutomerOrderWithoutCutting(model);
            return count > 0 ? "true" : "false";
        }

        //更新订单信息，不拆单
        private static string UpdateCutomerOrderWithoutCutting(mg_CustomerOrderModel model)
        {
            int count = mg_CustomerOrderDAL.UpdateCutomerOrderWithoutCutting(model);
            return count > 0 ? "true" : "false";
        }

        //订单列表
        public static string QueryList(string page, string pagesize,string isCutted)
        {
            string jsonStr = "[]";
            List<mg_CustomerOrderModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total,isCutted);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total, isCutted);
            }

            mg_CustomerOrderPageModel model = new mg_CustomerOrderPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_CustomerOrderPageModel>(model);
            return jsonStr;
        }
        private static List<mg_CustomerOrderModel> QueryListForPaging(string page, string pagesize, out string total, string isCutted)
        {
            List<mg_CustomerOrderModel> list = mg_CustomerOrderDAL.QueryListForPaging(page, pagesize, out total, isCutted);
            return list;
        }

        private static List<mg_CustomerOrderModel> QueryListForFirstPage(string pagesize, out string total, string isCutted)
        {
            List<mg_CustomerOrderModel> list = mg_CustomerOrderDAL.QueryListForFirstPage(pagesize, out total, isCutted);
            return list;
        }

        public static string DeleteCustomerOrder(string co_id)
        {
            int count = mg_CustomerOrderDAL.DeleteCustomerOrder(co_id);
            return count > 0 ? "true" : "false";
        }

        //拆单
        public static string CuttingOrder(string co_id)
        {
            //FS  FB    FC   B4   B6   RC
        //      FS_Drive = 19,
        //FSB_Drive = 43,
        //FSC_Drive = 44,
        //RSB40 = 45,
        //RSB60 = 46,
        //RSC = 47,
        //FS_Passenger = 48,
        //FSB_Passenger = 49,
        //FSC_Passenger = 50


            using (TransactionScope ts = new TransactionScope())
            {
                DataTable orderDT = mg_CustomerOrderDAL.GetCustomerOrderDetail(co_id);
                mg_CustomerOrderDAL.GenerateTempInfo(co_id);
                DataView dv = new DataView(orderDT);
                DataTable dt2 = dv.ToTable(true,"all_id","co_count");

                string flag= mg_CustomerOrderDAL.CuttingOrder(orderDT, dt2)?"true":"false";
                ts.Complete();
                return flag;
            }
        }
    }


    class mg_CustomerOrderPageModel
    {
        public string total;
        public List<mg_CustomerOrderModel> rows;
    }
}
