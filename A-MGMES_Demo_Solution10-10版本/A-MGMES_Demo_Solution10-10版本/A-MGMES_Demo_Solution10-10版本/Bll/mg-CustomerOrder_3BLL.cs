using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;
using System.Transactions;
using Dal;
namespace Bll
{
    public class mg_CustomerOrder_3BLL
    {
        public static string EditDeliveryOrder(mg_CustomerOrder_3 model)
        {
            int count = mg_CustomerOrder_3DAL.EditDeliveryOrder(model);
            return count > 0 ? "true" : "false";
        }
    }
}
