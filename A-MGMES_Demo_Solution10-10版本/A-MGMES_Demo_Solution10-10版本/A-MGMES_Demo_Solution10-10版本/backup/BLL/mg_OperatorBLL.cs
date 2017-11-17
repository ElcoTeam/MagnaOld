using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace BLL
{
    public class mg_OperatorBLL
    {
        public static bool AddOperatorByName(string name, string rfid, int stid, int isoperator, string pic)
        {
            return mg_OperatorDAL.AddOperatorByName(name, rfid, stid, isoperator, pic) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_OperatorDAL.GetAllData();
        }

        public static bool UpdateOperatorByName(int id, string name, string rfid, int stid, int isoperator, string pic)
        {
            return mg_OperatorDAL.UpdateOperatorByName(id, name, rfid, stid, isoperator, pic) > 0 ? true : false;
        }

        public static bool DelOperatorByName(int user_id)
        {
            return mg_OperatorDAL.DelOperatorByName(user_id) > 0 ? true : false;
        }

        public static bool CheckOperatorByName(int a, int userid, string name)
        {
            return mg_OperatorDAL.CheckOperatorByName(a, userid, name) == 0 ? true : false;
        }

        public static DataTable GetStName()
        {
            return mg_OperatorDAL.GetStName();
        }

        public static bool CheckPicName(string name)
        {
            return mg_OperatorDAL.CheckPicName(name);
        }



        /*
       *      姜任鹏
       * 
       * 
       */ 

        public static string GenerateNO()
        {
            string no = mg_OperatorDAL.GetMaxNO();
            StringBuilder sbStr = new StringBuilder();
            for (int i = 0; i < 6 - no.Length; i++)
            {
                sbStr.Append("0");
            }
            return sbStr.Append(no).ToString();
        }
        public static string SaveOperator(mg_OperatorModel model)
        {
            return model.op_id == 0 ? AddOperator(model) : UpdateOperator(model);
        }
        public static string AddOperator(mg_OperatorModel model)
        {
            int count = mg_OperatorDAL.AddOperator(model);
            return count > 0 ? "true" : "false";
        }
        public static string UpdateOperator(mg_OperatorModel model)
        {
            int count = mg_OperatorDAL.UpdateOperator(model);
            return count > 0 ? "true" : "false";
        }

        public string QueryOperatorList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_OperatorModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_OperatorDataModel model = new mg_OperatorDataModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_OperatorDataModel>(model);
            return jsonStr;
        }
        private List<mg_OperatorModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_OperatorModel> list = mg_OperatorDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private List<mg_OperatorModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_OperatorModel> list = mg_OperatorDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeleteOperator(string op_id)
        {
            int count = mg_OperatorDAL.DeleteOperator(op_id);
            return count > 0 ? "true" : "false";
        }

        public static mg_OperatorModel GetOperaterForCardNO(string cardNO)
        {
            return mg_OperatorDAL.GetOperaterForCardNO(cardNO);
        }
    }


    public class mg_OperatorDataModel
    {
        public string total;
        public List<mg_OperatorModel> rows;
    }
}
