using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbUtility;
using Tools;
namespace Dal
{
   public  class DelJetSEQNR_Dal
    {
        public static string select()
        {
            string sql = "select * from mg_DelJet_SEQNR ";
            DataTable table = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            string result = JSONTools.DataTableToJSON(table);
            return result;
        }
        public static string edit(string seqnr)
        {
            string sql = "update mg_DelJet_SEQNR  set SEQNR ="+Convert.ToDecimal(seqnr);
            int a = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            if(a>0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}
