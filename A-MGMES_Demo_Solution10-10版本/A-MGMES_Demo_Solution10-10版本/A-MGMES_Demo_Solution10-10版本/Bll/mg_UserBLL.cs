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
    public class mg_UserBLL
    {
        public static mg_userModel GetUserForUID(string uid)
        {
            return mg_UserDAL.GetUserForUID(uid);
        }

        public static mg_userModel GetUserForUName(string uname)
        {
            return mg_UserDAL.GetUserForUName(uname);
        }

        public static bool AddUserByName(string name, string pwd, string rfid, string email, int depid, int posiid, string pic, string menuids)
        {
            return mg_UserDAL.AddUserByName(name, pwd, rfid, email, depid, posiid, pic, menuids) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_UserDAL.GetAllData();
        }

        public static bool UpdateUserByName(int id, string name, string pwd, string rfid, string email, string menuids, int depid, int posiid, string pic)
        {
            return mg_UserDAL.UpdateUserByName(id, name, pwd, rfid, email, menuids, depid, posiid, pic) > 0 ? true : false;
        }

        public static bool DelUserByName(int user_id)
        {
            return mg_UserDAL.DelUserByName(user_id) > 0 ? true : false;
        }

        public static bool CheckUserByName(int a, int userid, string name)
        {
            return mg_UserDAL.CheckUserByName(a, userid, name) == 0 ? true : false;
        }

        public static DataTable GetDepName()
        {
            return mg_UserDAL.GetDepName();
        }

        public static DataTable GetPosiName()
        {
            return mg_UserDAL.GetPosiName();
        }

        public static DataTable GetUserName()
        {
            return mg_UserDAL.GetUserName();
        }

        public static bool CheckPicName(string name)
        {
            return mg_UserDAL.CheckPicName(name);
        }

        public static string saveUser(mg_userModel model)
        {
            return model.user_id == "" ? AddUser(model) : UpdateUser(model);
        }

        private static string UpdateUser(mg_userModel model)
        {
            int count = mg_UserDAL.UpdateUser(model);
            if (count == -1)
            {
                return "exit";
            }
            return count > 0 ? "true" : "false";
        }

        private static string AddUser(mg_userModel model)
        {
            int count = mg_UserDAL.AddUser(model);
            if (count==-1)
            {
                return "exit";
            }
            return count > 0 ? "true" : "false";
        }

        public static string QueryUserList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_userModel> list = null;
            string total = "0";
            
            list = QueryListForFirstPage(page,pagesize, out total);
           
            mg_userDataModel model = new mg_userDataModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_userDataModel>(model);
            return jsonStr;
        }
        //private static List<mg_userModel> QueryListForPaging(string page, string pagesize, out string total)
        //{
        //    List<mg_userModel> list = mg_UserDAL.QueryListForPaging(page, pagesize, out total);
        //    return list;
        //}

        private static List<mg_userModel> QueryListForFirstPage(string currentpage,string pagesize, out string total)
        {
            List<mg_userModel> list = mg_UserDAL.QueryListForFirstPage(currentpage,pagesize, out total);
            return list;
        }

        public static string DeleteUser(string user_id)
        {
            int count = mg_UserDAL.DeleteUser(user_id);
            return count > 0 ? "true" : "false";
        }
    }

    class mg_userDataModel
    {
        public string total;
        public List<mg_userModel> rows;
    }
}
