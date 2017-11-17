using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Tools;
using System.Data;
using Dal;
using Bll;

namespace Bll
{
   public class mg_FlowLine1BLL
    {
       
        public static string QueryFlowLine1List(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_FlowlingModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_FlowLine1PageModel model = new mg_FlowLine1PageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_FlowLine1PageModel>(model);
            return jsonStr;
        }
        public static string queryFlowLineidForPart()
        {
            string jsonStr = "[]";
            List<mg_FlowlingModel> list = mg_FlowLine1DAL.queryFlowLineidForPart();
            jsonStr = JSONTools.ScriptSerialize<List<mg_FlowlingModel>>(list);
            return jsonStr;
        }
       private static List<mg_FlowlingModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_FlowlingModel> list = mg_FlowLine1DAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_FlowlingModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_FlowlingModel> list = mg_FlowLine1DAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string SaveFlowLine1(mg_FlowlingModel model)
        {
            return model.fl_id == 0 ? AddFlowLine1(model) : UpdateFlowLine1(model);
        }
        private static string AddFlowLine1(mg_FlowlingModel model)
        {
            int count = mg_FlowLine1DAL.AddFlowLine1(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdateFlowLine1(mg_FlowlingModel model)
        {
           int count = mg_FlowLine1DAL.UpdateFlowLine1(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeleteFlowLine1(string fl_id)
        {
            int count = mg_FlowLine1DAL.DeleteFlowLine1(fl_id);
            return count > 0 ? "true" : "false";
        }
    }
       class mg_FlowLine1PageModel
        {
          public string total;
          public List<mg_FlowlingModel> rows;
        }
    }

