using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using DBUtility;
using Tools;

namespace DAL
{
    public class mg_ODSDAL
    {
        public static DataTable Getmg_ODSByMachineNO(string machineNO)
        {
            string sql = @"select * from [mg_ODS] where machineNO = '" + machineNO + "' order by BOMTYPE,machineNO,ODSNO";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
        }
    }
}

