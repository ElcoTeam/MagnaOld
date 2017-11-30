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
    public class mg_machinestepDAL
    {

        public static DataTable GetMachineStepByMachineNOAndStepAndBomtype(string machineNO, int workstep, string productType)
        {
            string sql = @"select * from [mg_machinestep] where machineNO = '" + machineNO + "' and StepNo = " + workstep + " and bomtype = '" + productType + "'";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
        }

    }
}

