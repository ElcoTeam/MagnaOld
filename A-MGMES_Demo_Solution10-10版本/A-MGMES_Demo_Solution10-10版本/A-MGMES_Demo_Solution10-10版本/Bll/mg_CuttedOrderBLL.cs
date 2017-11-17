using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;
using System.Transactions;

namespace Bll
{
    public class mg_CuttedOrderBLL
    {
        #region RSB/RSC

        public static DataTable GetData(string tablename,string preChar)
        {
            // return mg_CuttedOrderDAL.GetFSDData(isFSDzhu);
            return mg_CuttedOrderDAL.GetData(tablename, preChar) ;
        }
        public static void IsPublished(string co_id, string ofsd_id,string tablename,string preChar)
        {
            // mg_CuttedOrderDAL.IsPublished(co_id, ofsd_id, isFSDzhu);
            mg_CuttedOrderDAL.IsPublished(tablename, preChar, co_id, ofsd_id);
        }

        #endregion


        #region 主线
        public static DataTable GetFSDData(bool iszhu)
        {
            // return mg_CuttedOrderDAL.GetFSDData(isFSDzhu);
            return iszhu ? mg_CuttedOrderDAL.GetData("mg_Order_FSD", "fsd") : mg_CuttedOrderDAL.GetData("mg_Order_FSP", "fsp");
        }
        public static DataTable GetFSBDData(bool iszhu)
        {
            // return mg_CuttedOrderDAL.GetFSDData(isFSDzhu);
            return iszhu ? mg_CuttedOrderDAL.GetData("mg_Order_FSDB", "fsdb") : mg_CuttedOrderDAL.GetData("mg_Order_FSPB", "fspb");
        }
        public static DataTable GetFSCDData(bool iszhu)
        {
            // return mg_CuttedOrderDAL.GetFSDData(isFSDzhu);
            return iszhu ? mg_CuttedOrderDAL.GetData("mg_Order_FSDC", "fsdc") : mg_CuttedOrderDAL.GetData("mg_Order_FSPC", "fspc");
        }

        public static void IsFSPublished(string co_id, string ofsd_id, bool isFSDzhu)
        {
           // mg_CuttedOrderDAL.IsPublished(co_id, ofsd_id, isFSDzhu);
            if (isFSDzhu)
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSD", "fsd", co_id, ofsd_id);
            }
            else
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSP", "fsp", co_id, ofsd_id);

            }
        }
        public static void IsFSBPublished(string co_id, string ofsd_id, bool iszhu)
        {
            // mg_CuttedOrderDAL.IsPublished(co_id, ofsd_id, isFSDzhu);
            if (iszhu)
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSDB", "fsdb", co_id, ofsd_id);
            }
            else
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSPB", "fspb", co_id, ofsd_id);

            }
        }
        public static void IsFSCPublished(string co_id, string ofsd_id, bool iszhu)
        {
            // mg_CuttedOrderDAL.IsPublished(co_id, ofsd_id, isFSDzhu);
            if (iszhu)
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSDC", "fsdc", co_id, ofsd_id);
            }
            else
            {
                mg_CuttedOrderDAL.IsPublished("mg_Order_FSPC", "fspc", co_id, ofsd_id);

            }
        }

        public static bool CheckFSzhu()
        {
            return CheckTheMasterLine("mg_Order_FSD", "fsd", "mg_Order_FSP", "fsp");
        }
        public static bool CheckFSBzhu()
        {
            return CheckTheMasterLine("mg_Order_FSDB", "fsdb", "mg_Order_FSPB", "fspb");
        }
        public static bool CheckFSCzhu()
        {
            return CheckTheMasterLine("mg_Order_FSDC", "fsdc", "mg_Order_FSPC", "fspc");
        }

        private static bool CheckTheMasterLine(string tablenameZhu, string preCharZhu, string tablename, string preChar)
        {
            DataTable dt = mg_CuttedOrderDAL.GetzhuData(tablenameZhu, preCharZhu, tablename, preChar);
            if (DataHelper.HasData(dt))
            {
                DataRow row = dt.Rows[0];
                string fuplc = DataHelper.GetCellDataToStr(row, "fuplc");

                if (fuplc == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
