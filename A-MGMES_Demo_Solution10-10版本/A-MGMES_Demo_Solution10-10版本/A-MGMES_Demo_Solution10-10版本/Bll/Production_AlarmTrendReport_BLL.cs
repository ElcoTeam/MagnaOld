using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Data;
using Dal;
using Model;
using DbUtility;
namespace Bll
{
   public  class Production_AlarmTrendReport_BLL
    {
       public static DataTable GetWaringDataTable(string fl_id, string StartTime, string EndTime, int startIndex, int endIndex)
       {
           string jsonStr = "[]";
           DataTable userList = Production_AlarmTrendDAL.GetWaringDataTable(fl_id, StartTime, EndTime, startIndex, endIndex);
           return userList;

       }
       public static DataListModel<Production_AlarmModel> GetWaringListNew(string fl_id,string StartTime, string EndTime, int startIndex, int endIndex)
       {
           string jsonStr = "[]";
           DataListModel<Production_AlarmModel> userList = Production_AlarmTrendDAL.GetWaringListNew(fl_id,StartTime, EndTime, startIndex, endIndex);
           return userList;

       }
       public static DataListModel<Production_AlarmModel> GetListNew(string StartTime,string EndTime,int startIndex,int endIndex)
       {
           string jsonStr = "[]";
           DataListModel<Production_AlarmModel> userList = Production_AlarmTrendDAL.GetListNew(StartTime,EndTime,startIndex,endIndex);
           return userList;
          
       }
        public static DataListModel<Production_AlarmModel> GetList(int page, int pagesize, string sidx, string sord, string Where)
        {
            string jsonStr = "[]";
            DataListModel<Production_AlarmModel> userList = Production_AlarmTrendDAL.GetList(page, pagesize, sidx, sord, Where);
            return userList;
            //List<UserM_Menu> menuList = UserM_MenuDAL.GetUserMenuList();
            //jsonStr = JSONTools.ScriptSerialize<DataListModel<Production_Model>>(userList);
            //return jsonStr;
        }

        public static DataTable getTable(String StartTime, string EndTime,int PageSize, int StartIndex, int EndIndex, string sort, string order, string wherestr, out int totalcount)
        {
            return Production_AlarmTrendDAL.getTable(StartTime, EndTime,PageSize, StartIndex, EndIndex, sort, order, wherestr, out totalcount);
        }
    }
}
