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
    public class mg_workqueueDAL
    {

        public static DataTable GetProductInforForVIN(string vin)
        {
            string sql = @"SELECT  b.ProductType,b.PartNO,b.PartDesc
                              FROM [mg_workqueue] w
                              inner join [mg_BOM_ZuoYi] b
                              on w.Product_Type=b.ProductType
                              where [Product_VIN]='" + vin + "'";
            return SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql, null);
        }
    }
}

