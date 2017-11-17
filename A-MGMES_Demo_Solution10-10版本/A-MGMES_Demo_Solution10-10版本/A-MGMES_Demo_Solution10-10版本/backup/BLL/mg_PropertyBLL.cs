using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Tools;
using Model;


namespace BLL
{
    public class mg_PropertyBLL
    {

        public static string queryRateForEditing()
        {
            return queryJSONStringByPropertyType("1");
        }

        public static string queryColorForEditing()
        {
            return queryJSONStringByPropertyType("2");
        }

        public static string queryMetaForEditing()
        {
            return queryJSONStringByPropertyType("3");
        }

        static string queryJSONStringByPropertyType(string type)
        {
            string jsonStr = "[]";
            List<mg_PropertyModel> list = mg_PropertyDAL.queryJSONStringByPropertyType(type);
            jsonStr = JSONTools.ScriptSerialize<List<mg_PropertyModel>>(list);
            return jsonStr;
        }


       public  static string queryJSONStringByPropertyType(mg_PropertyEnum propEnum)
        {
            string jsonStr = "[]";
            List<mg_PropertyModel> list = mg_PropertyDAL.queryJSONStringByPropertyType(propEnum);
            jsonStr = JSONTools.ScriptSerialize<List<mg_PropertyModel>>(list);
            return jsonStr;
        }



        public static string QueryCategoryForPart()
        {
            return queryJSONStringByPropertyType("5");
        }
    }
}
