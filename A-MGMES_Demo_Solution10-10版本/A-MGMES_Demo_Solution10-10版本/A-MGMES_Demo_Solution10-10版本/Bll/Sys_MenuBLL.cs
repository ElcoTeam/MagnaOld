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
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="menutitle"></param>
        /// <returns></returns>
        public static string GetMenuList(string menutitle,string currentuser)
        {
            string jsonStr = "[]";
            List<Sys_Menu> menuList = Sys_MenuDAL.GetUserMenuList(menutitle,currentuser);
            jsonStr = JSONTools.ScriptSerialize<List<Sys_Menu>>(menuList);
            return jsonStr;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public static string GetMenuTree()
        {
            string jsonStr = "[]";
            List<MenuTree> menuList = Sys_MenuDAL.GetMenuTree();
            jsonStr = JSONTools.ScriptSerialize<List<MenuTree>>(menuList);
            return jsonStr;
        } 


    }
}
