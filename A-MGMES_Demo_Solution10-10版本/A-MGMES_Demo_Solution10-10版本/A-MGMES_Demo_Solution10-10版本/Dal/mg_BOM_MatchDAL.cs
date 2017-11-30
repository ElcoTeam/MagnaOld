using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using DbUtility;
using Tools;

namespace DAL
{
    public class mg_BOM_MatchDAL
    {

        public static int RecordBOMCode(mg_BOM_MatchModel model)
        {
            string sql = @"INSERT INTO [mg_BOM_Match]
                                   ([BOMNO]
                                   ,[MatchResult]
                                   ,[ScanCode]
                                   ,[RecordDate]
                                   ,[UID]
                                   ,[VIN])
                             VALUES
                                   ('" + model.BOMNO + @"'
                                   ,1
                                   ,'" + model.ScanCode + @"'
                                   ,'" + DateTime.Now.ToString() + @"'
                                   ,'" + model.UID + @"'
                                   ,'" + model.VIN + @"')";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
        }

        public static int UpdateBOMCode(mg_BOM_MatchModel model)
        {
            string sql = @"UPDATE  [mg_BOM_Match]  SET [TraceCode] = '"+model.TraceCode+@"'
                                 WHERE BOMNO='"+model.BOMNO+"' and VIN='"+model.VIN+"'";
            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
        }
    }
}


