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
    public class mg_PartBLL
    {
        public static bool AddPartByName(string part_no, string part_name, string part_desc)
        {
            return mg_PartDAL.AddPartByName( part_no, part_name, part_desc) > 0 ? true : false;
        }

        public static bool Addrel(int allid, int partid)
        {
            return mg_PartDAL.Addrel(allid, partid) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_PartDAL.GetAllData();
        }

        public static bool UpdatePartByName(int p_id, string p_no, string p_name, string p_desc)
        {
            return mg_PartDAL.UpDatePartByName(p_id, p_no, p_name, p_desc) > 0 ? true : false;
        }

        public static bool Updaterel( int newallid, int newpartid)
        {
            return mg_PartDAL.Updaterel( newallid, newpartid) > 0 ? true : false;
        }

        public static bool Delrel(int allid, int partid)
        {
            return mg_PartDAL.Delrel(allid, partid) > 0 ? true : false;
        }

        public static bool DelPartByName(int p_id)
        {
            return mg_PartDAL.DelPartByName(p_id) > 0 ? true : false;
        }

        public static bool CheckPartByName(int a, int part_id, string part_no, string part_name, string part_desc,string allno)
        {
            return mg_PartDAL.CheckPartByName(a, part_id, part_no, part_name, part_desc, allno) == 0 ? true : false;
        }



        /*
         * 
       *      姜任鹏
       * 
       */ 


        public static string SavePart(mg_partModel model)
        {
            return model.part_id == 0 ? AddPart(model) : UpdatePart(model);
        }
        private static string UpdatePart(mg_partModel model)
        {
            bool flag = mg_PartDAL.UpdatePart(model);
            return flag ? "true" : "false";
        }

        private static string AddPart(mg_partModel model)
        {
            bool flag = mg_PartDAL.AddPart(model);
            return flag ? "true" : "false";
        }

        public static string QueryAllPartList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_partModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_partPageModel model = new mg_partPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_partPageModel>(model);
            return jsonStr;
        }

        private static List<mg_partModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_partModel> list = mg_PartDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_partModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_partModel> list = mg_PartDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }

        public static string DeletePart(string part_id)
        {
            int count = mg_PartDAL.DeletePart(part_id);
            return count > 0 ? "true" : "false";
        }

        public static string QueryPartListForBOM()
        {
            List<mg_partModel> list = mg_PartDAL.QueryPartListForBOM();
            string jsonStr = JSONTools.ScriptSerialize<List<mg_partModel>>(list);
            return jsonStr;
        }

        public static string QueryPartForStepEditing()
        {
            string jsonStr = "[]";
            List<mg_partModel> list = mg_PartDAL.QueryPartForStepEditing();
            jsonStr = JSONTools.ScriptSerialize<List<mg_partModel>>(list);
            return jsonStr;
        }

        public static string queryPartForStepSearching()
        {
            string jsonStr = "[]";
            List<mg_partModel> list = mg_PartDAL.queryPartForStepSearching();
            jsonStr = JSONTools.ScriptSerialize<List<mg_partModel>>(list);
            return jsonStr;
        }

        public static mg_partModel GetPartModelByPartNO(string partNO)
        {
            return mg_PartDAL.GetPartModelByPartNO(partNO);
        }
    }

    class mg_partPageModel
    {
        public string total;
        public List<mg_partModel> rows;
    }
}
