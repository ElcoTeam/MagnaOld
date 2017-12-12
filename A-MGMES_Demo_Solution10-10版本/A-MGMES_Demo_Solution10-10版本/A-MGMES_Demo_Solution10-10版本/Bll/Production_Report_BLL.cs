using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dal;
using Model;
using DbUtility;
namespace Bll
{
    public class Production_Report_BLL
    {
        public static DataListModel<Production_Model> GetList(int page, int pagesize, string sidx, string sord, string Where)
        {
            string jsonStr = "[]";
            DataListModel<Production_Model> userList = Production_ModelDAL.GetList(page, pagesize, sidx, sord, Where);
            return userList;
            //List<UserM_Menu> menuList = UserM_MenuDAL.GetUserMenuList();
            //jsonStr = JSONTools.ScriptSerialize<DataListModel<Production_Model>>(userList);
            //return jsonStr;
        }
        public static DataTable GetClassInfo()
        {
            return Production_ModelDAL.GetClassInfo();
        }
        public static DataTable getTable(int PageSize, int StartIndex, int EndIndex, string sort, string order,string wherestr,  out int totalcount)
    {
        return Production_ModelDAL.getTable(PageSize, StartIndex, EndIndex, sort, order, wherestr, out totalcount);
    }
    }
}
