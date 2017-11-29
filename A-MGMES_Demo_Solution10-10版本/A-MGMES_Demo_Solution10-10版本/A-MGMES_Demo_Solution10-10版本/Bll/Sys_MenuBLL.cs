using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Tools;
using Dal;
namespace Bll
{
    public class Sys_MenuBLL
    {
        public static string GetMenuList(string menutitle)
        {
            string jsonStr = "[]";
            List<Sys_Menu> menuList = Sys_MenuDAL.GetUserMenuList(menutitle);
            jsonStr = JSONTools.ScriptSerialize<List<Sys_Menu>>(menuList);
            return jsonStr;
        } 
    }
}
