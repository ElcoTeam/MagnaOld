using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Tools;
using DbUtility;

namespace Dal
{
    public class Sys_MenuDAL
    {
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="menutitle"></param>
        /// <returns></returns>
        public static List<Sys_Menu> GetUserMenuList(string menutitle,string currentuser)
        {
            List<Sys_Menu> menulist = new List<Sys_Menu>();
            string sql = "";
            using (var conn = new SqlConnection(SqlHelper.SqlConnString))
            {
                if(string.IsNullOrEmpty(menutitle))
                {
                    sql = "select MenuNo,MenuName,MenuAddr,ParentNo,MenuTag,Image from View_Sys_UserLimitInfo where ParentNo='0000' and user_name= '"+currentuser+"'";                 
                }
                else
                {

                    string sql1 = @"select MenuNo,MenuName,MenuAddr,ParentNo,MenuTag,Image from View_Sys_UserLimitInfo  where ParentNo in (select MenuNo from Sys_MenuInfo where MenuName='{0}' and ParentNo='0000')  and user_name= '" + currentuser + "'";     
                    sql = string.Format(sql1, menutitle);
                }

                DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    Sys_Menu model = new Sys_Menu();

                    //model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                    model.MenuNo = DataHelper.GetCellDataToStr(row, "MenuNo");
                    model.MenuName = DataHelper.GetCellDataToStr(row, "MenuName");
                    model.MenuAddr = DataHelper.GetCellDataToStr(row, "MenuAddr");
                    model.ParentNo = DataHelper.GetCellDataToStr(row, "ParentNo");
                    model.MenuTag = DataHelper.GetCellDataToStr(row, "MenuTag");
                    model.Image = DataHelper.GetCellDataToStr(row, "Image");

                    menulist.Add(model);
                }
                return menulist;
            }
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public static List<MenuTree> GetMenuTree()
        {
            List<MenuTree> menutree = new List<MenuTree>();
            
            string sql = "";
            using (var conn = new SqlConnection(SqlHelper.SqlConnString))
            {

                sql = "select MenuNo,MenuName,ParentNo from Sys_MenuInfo where ParentNo='0000'";
               
                DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    MenuTree model = new MenuTree();
                    List<MenuTree> childrenmenutree = new List<MenuTree>();
                    model.id = DataHelper.GetCellDataToStr(row, "MenuNo");
                    model.text = DataHelper.GetCellDataToStr(row, "MenuName");
                    model.state = "closed";
                   
                    string childrensql = @"select MenuNo,MenuName,ParentNo from Sys_MenuInfo where ParentNo={0}";
                    DataTable childrendt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text,string.Format(childrensql,model.id), null);
                    foreach (DataRow childrenrow in childrendt.Rows)
                    {
                        
                        childrenmenutree.Add(new MenuTree 
                        {
                             id = DataHelper.GetCellDataToStr(childrenrow, "MenuNo"),
                             text = DataHelper.GetCellDataToStr(childrenrow, "MenuName")
                        });
                        model.children = childrenmenutree;
                    }
                   
                    menutree.Add(model);
                }
                return menutree;
            }
        }
    }
}
