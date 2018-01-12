using System.Text;
using Dal;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;
using System.Transactions;

namespace Bll
{
    public class DeliveryOrderBLL
    {
        public static DataTable getTable(string currentpage, string pagesize,string orderno, out int total)
        {
            return DeliveryOrderDAL.getTable(currentpage, pagesize,orderno, out total);
                
        }

        public static string EditDeliveryOrder(string PRODN, string  OrderIsHistory)
        {
            int count = DeliveryOrderDAL.EditDeliveryOrder(PRODN,OrderIsHistory);
            if (count > 0)
            {
                return "true";
            }
            else if (count == 0)
            {
                return "exsit";
            }
            else return "false";
        }
    }
}
