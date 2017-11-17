using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Tools;
using Model;


namespace Bll
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

        /// <summary>
        /// 保存颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SaveColor(mg_PropertyModel model)
        {
            return model.prop_id == 0 ? AddColor(model) : UpdateColor(model);
        }

        /// <summary>
        /// 新增颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddColor(mg_PropertyModel model)
        {
            int count = mg_PropertyDAL.AddColor(model);
            return count > 0 ? "true" : "false";
        }

        /// <summary>
        /// 更新颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateColor(mg_PropertyModel model)
        {
            int count = mg_PropertyDAL.UpdateColor(model);
            return count > 0 ? "true" : "false";
        }

        /// <summary>
        /// 删除颜色
        /// lx 2017-06-22
        /// </summary>
        /// <param name="prop_id"></param>
        /// <returns></returns>
        public static string DeleteColor(string prop_id)
        {
            int count = mg_PropertyDAL.DeleteColor(prop_id);
            return count > 0 ? "true" : "false";
        }
    }
}
