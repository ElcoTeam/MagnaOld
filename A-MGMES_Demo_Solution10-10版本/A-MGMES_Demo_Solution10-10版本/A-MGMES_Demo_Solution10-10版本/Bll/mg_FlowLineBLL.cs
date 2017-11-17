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
    public class mg_FlowLineBLL
    {
        public static bool AddFlowlineByName(string fl_name)
        {
            return mg_FlowLineDAL.AddFlowlineByName(fl_name) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_FlowLineDAL.GetAllData();
        }

        public static bool UpdateFlowlineByName(int fl_id, string fl_name)
        {
            return mg_FlowLineDAL.UpdateFlowlineByName(fl_id, fl_name) > 0 ? true : false;
        }

        public static bool DelFlowlineByName(int fl_id)
        {
            return mg_FlowLineDAL.DelFlowlineByID(fl_id) > 0 ? true : false;
        }

        public static bool CheckFlowlineByName(int a, int fl_id, string fl_name)
        {
            return mg_FlowLineDAL.CheckFlowlineByName(a, fl_id, fl_name) == 0 ? true : false;
        }

        public static string QueryFlowlingForEditing()
        {
            string jsonStr = "[]";
            List<mg_FlowlingModel> list = mg_FlowLineDAL.QueryFlowlingForEditing();
            jsonStr = JSONTools.ScriptSerialize<List<mg_FlowlingModel>>(list);
            return jsonStr;
        }

        public static string QueryFlowlingForStepEditing()
        {
            string jsonStr = "[]";
            List<mg_FlowlingModel> list = mg_FlowLineDAL.QueryFlowlingForStepEditing();
            jsonStr = JSONTools.ScriptSerialize<List<mg_FlowlingModel>>(list);
            return jsonStr;
        }
    }
}
