using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using DbUtility;
using Tools;
namespace Dal
{
  public  class EditPassword_DAL
    {
      public static ResultMsg_User EditPsw(mg_userModel dataEntity, ResultMsg_User result)
      {
          try
          {

              string strSql = " SELECT COUNT(1) AS SM FROM [Sys_UserInfo] WHERE Lower(user_name) = '" + dataEntity.user_name.ToLower().Trim() + "'  and user_pwd='" + dataEntity.user_pwd + "'";
             
              DataTable dt = new DataTable();
              dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, strSql, null);
              int num = NumericParse.StringToInt(DataHelper.GetCellDataToStr(dt.Rows[0], "SM"));
              if (dt != null && dt.Rows.Count > 0 && num > 0 )
              {
                  result.result = "";
                  result.msg = "";
              }
              else
              {
                  result.result = "failed";
                  result.msg = "原密码不正确!";
              }

              if (result.result == "")
              {

                  strSql = "update Sys_UserInfo set user_pwd='" + dataEntity.user_NewPassword + "',lasteditpwdtime=getdate()  where Lower(user_name)='" + dataEntity.user_name.ToLower() + "'";
                  int re = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, strSql, null);
                  if (re == 1)
                  {
                      result.result = "success";
                      result.msg = "修改密码成功!";
                  }

              }
          }
          catch (Exception ex)
          {
             
              result.result = "failed";
              result.msg = "保存失败! \n" + ex.Message;
          }

          return result;
      }
    }
}
