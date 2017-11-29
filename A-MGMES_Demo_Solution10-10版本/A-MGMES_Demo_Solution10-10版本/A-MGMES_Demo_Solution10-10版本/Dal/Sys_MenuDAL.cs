using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DBUtility;
using Tools;

namespace Dal
{
    public class Sys_MenuDAL
    {
        public static List<Sys_Menu> GetUserMenuList(string menutitle)
        {
            List<Sys_Menu> menulist = new List<Sys_Menu>();
            using (var conn = new SqlConnection(SqlHelper.SqlConnString))
            {
                if(string.IsNullOrEmpty(menutitle))
                {
                    string sql = "select * from Sys_Menu where ParentNo='0000'";
                    DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
                    foreach (DataRow row in dt.Rows)
                    {
                        Sys_Menu model = new Sys_Menu();

                        model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
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
                else
                {

                    string sql1 = @"select * from Sys_Menu  where ParentNo in (select MenuNo from Sys_Menu where MenuName='{0}' and ParentNo='0000') ";
                    string sql = string.Format(sql1, menutitle);

                    DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
                    foreach (DataRow row in dt.Rows)
                    {
                        Sys_Menu model = new Sys_Menu();

                        model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
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
        }
    }
}
