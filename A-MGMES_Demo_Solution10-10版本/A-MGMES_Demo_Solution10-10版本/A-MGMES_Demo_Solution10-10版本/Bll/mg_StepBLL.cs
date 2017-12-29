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
    public class mg_StepBLL
    {
        public static bool AddStepByName(string name,int clock, string desc, string pic,int stid,int bomid,int bomcount)
        {
            return mg_StepDAL.AddStepByName(name, clock, desc, pic, stid, bomid, bomcount) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_StepDAL.GetAllData();
        }

        public static bool UpdateStepByName(int id,string name, int clock, string desc, string pic,int stid, int bomid, int count)
        {
            return mg_StepDAL.UpdateStepByName(id, name, clock, desc, pic,stid,bomid,count) > 0 ? true : false;
        }

        public static bool DelStepByName(int step_id)
        {
            return mg_StepDAL.DelStepByName(step_id) > 0 ? true : false;
        }

        public static bool CheckStepByName(int a, int stepid, string name)
        {
            return mg_StepDAL.CheckStepByName(a, stepid, name) == 0 ? true : false;
        }

        public static DataTable GetStationID()
        {
            return mg_StepDAL.GetStationID();
        }

        public static DataTable GetBomName()
        {
            return mg_StepDAL.GetBomName();
        }

        public static bool CheckPicName(string name)
        {
            return mg_StepDAL.CheckPicName(name);
        }



        /*
       * 
       *      姜任鹏
       * 
       */
        public static string saveStepAndODS(mg_StepModel model)
        {
            return model.step_id == 0 ? AddStepAndODS(model) : UpdateStepAndODS(model);
        }

        public static string saveStep(mg_StepModel model)
        {
            return model.step_id == 0 ? AddStep(model) : UpdateStep(model);
        }

        private static string UpdateStep(mg_StepModel model)
        {
            int count = mg_StepDAL.UpdateStep(model);
            return count > 0 ? "true" : "false";
        }

        private static string AddStep(mg_StepModel model)
        {
            int count = mg_StepDAL.AddStep(model);
            return count > 0 ? "true" : "false";
        }
        private static string AddStepAndODS(mg_StepModel model)
        {
            int count = mg_StepDAL.AddStepAndODS(model);
            return count > 0 ? "true" : "false";
        }
        private static string UpdateStepAndODS(mg_StepModel model)
        {
            int count = mg_StepDAL.UpdateStepAndODS(model);
            return count > 0 ? "true" : "false";
        }


        public static string QueryStepList(string page, string pagesize, string fl_id, string st_id, string part_id)
        {
            string jsonStr = "[]";
            List<mg_StepModel> list = null;
            string total = "0";
           
            list = QueryListForFirstPage(page,pagesize, out total, fl_id, st_id, part_id);
            

            mg_stepPageModel model = new mg_stepPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_stepPageModel>(model);
            return jsonStr;
        }

       

         private static List<mg_StepModel> QueryListForFirstPage(string currentpage,string pagesize, out string total, string fl_id, string st_id, string part_id)
        {
            List<mg_StepModel> list = mg_StepDAL.QueryListForFirstPage(currentpage,pagesize, out total, fl_id, st_id, part_id);
            return list;
        }

         public static string DeleteStep(string step_id)
         {
             int count = mg_StepDAL.DeleteStep(step_id);
             return count > 0 ? "true" : "false";
         }

         public static string Sorting(string step_id, string step_order, string point)
         {
             int count = -1;
             if (point == "top")
             {
                 count = mg_StepDAL.SortingTop(step_id, step_order);
             }
             else if (point == "bottom")
             {
                 count = mg_StepDAL.SortingBottom(step_id, step_order);
             }
             return count != -1 ? "true" : "false";
         }

         public static DataSet  QeuryALLData()
         {
             return mg_StepDAL.QueryAllData();
         }

         public static DataTable GetAllStepForPartAndStation(int fl_id, int st_id, int part_id)
         {
             return mg_StepDAL.GetAllStepForPartAndStation(fl_id, st_id, part_id);
         }
         public static DataTable GetAllStepForPartAndStation(string st_ids, string partno)
         {
             return mg_StepDAL.GetAllStepForPartAndStation(st_ids, partno);
         }

         public static string GetIDsByMac(string mac)
         {
             return mg_StepDAL.GetIDsByMac(mac);
         }

         public static string QueryODSByStepid(string step_id)
         {
             string jsonStr = "[]";
             List<mg_ODSModel> list = mg_StepDAL.QueryODSByStepid(step_id);
             jsonStr = JSONTools.ScriptSerialize<List<mg_ODSModel>>(list);
             return jsonStr;
         }
    }

    class mg_stepPageModel
    {
        public string total;
        public List<mg_StepModel> rows;
    }

   

}
