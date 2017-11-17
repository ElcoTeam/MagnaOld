using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using System.Web.Script.Serialization;
using System.Text;

namespace website.HttpHandlers
{
    /// <summary>
    /// InsertCustomOrderHandler 的摘要说明
    /// </summary>
    public class InsertCustomOrderHandler : IHttpHandler
    {
        HttpRequest Request = null;
        HttpResponse Response = null;

        public void ProcessRequest(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;

            context.Response.ContentType = "text/plain";
            string method = Request.Params["method"];
            switch (method)
            {
                case "queryProduct":
                    QueryProduct();
                    break;

                case "queryPart":
                    if(Request.Params["ProductList"] != null)
                        QueryPart(Request.Params["ProductList"].ToString());
                    else
                        QueryPart("");
                    break;
                case "save":
                    Save();
                    break;
                case "queryList":
                    QueryList();
                    break;
                case "delete":
                    Delete();
                    break;
            }
        }

        void QueryProduct()
        {
            List<mg_insertco_selectlist> ml = new List<mg_insertco_selectlist>();
            DataTable dt;
            string sql = @"select ID as 'id', ProductName as 'text' from mg_Product where IsUseing=1";
            dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            foreach (DataRow dr in dt.Rows)
            {
                mg_insertco_selectlist itm = new mg_insertco_selectlist();
                itm.id = Convert.ToInt32(dr["id"]);
                itm.text = dr["text"].ToString();
                ml.Add(itm);
            }
            string jsonStr = JSONTools.ScriptSerialize<List<mg_insertco_selectlist>>(ml);
            Response.Write(jsonStr);
            Response.End();
        }

        void QueryPart(string strProductList)
        {
            mg_insertco_parttable pt = new mg_insertco_parttable();

            DataTable dt;
            string sql = @"
                SELECT part_id, part_no, part_name, part_desc FROM mg_part
                 WHERE charindex (',' + CONVERT (NVARCHAR (20), productID) + ',', '," + strProductList + @",') > 0";
            dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            foreach (DataRow dr in dt.Rows)
            {
                mg_insertco_part itm = new mg_insertco_part();
                itm.part_id = Convert.ToInt32(dr["part_id"]);
                itm.part_no = dr["part_no"].ToString();
                itm.part_name = dr["part_name"].ToString();
                itm.part_desc = dr["part_desc"].ToString();
                pt.rows.Add(itm);
            }
            pt.total = dt.Rows.Count.ToString();
            string jsonStr = JSONTools.ScriptSerialize<mg_insertco_parttable>(pt);
            Response.Write(jsonStr);
            Response.End();
        }

        void QueryList()
        {
            string page = Request.Params["page"];
            string pagesize = Request.Params["rows"];
            if (string.IsNullOrEmpty(page))
            {
                page = "1";
            }
            if (string.IsNullOrEmpty(pagesize))
            {
                pagesize = "15";
            }

            mg_insertco_customerordertable cot = new mg_insertco_customerordertable();
            string sql = @"
                            SELECT co.OrderID,
                                   isnull (co.CustomerNumber, '') AS CustomerNumber,
                                   isnull (co.JITCallNumber, '') AS JITCallNumber,
                                   isnull (co.SerialNumber, '') AS SerialNumber,
                                   isnull (co.VinNumber, '') AS VinNumber,
                                   isnull (co.PlanDeliverTime, '') AS PlanDeliverTime,
                                   isnull (prod.ProductNo, '') AS ProductNo,
                                   isnull (prod.ProductName, '') AS ProductName
                              FROM mg_CustomerOrder_3 co
                                   INNER JOIN mg_Customer_Product coprod
                                      ON co.OrderID = coprod.CustomerOrderID
                                   INNER JOIN mg_Product prod ON coprod.ProductID = prod.ID
                             WHERE co.OrderType = 3 and datediff(day,co.CreateTime,getdate())<15 and prod.IsUseing=1
                            ORDER BY co.CreateTime DESC";
            DataTable dt;
            dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
            int iPage = Convert.ToInt32(page);
            int iPageZize = Convert.ToInt32(pagesize);
            for(int i=0; i< dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (i < (iPage - 1) * iPageZize || i >= (iPage * iPageZize))
                    continue;
                mg_insertco_customerorder itm = new mg_insertco_customerorder();
                itm.OrderID = Convert.ToInt32(dr["OrderID"]);
                itm.CustomerNumber = dr["CustomerNumber"].ToString();
                itm.JITCallNumber = dr["JITCallNumber"].ToString();
                itm.SerialNumber = dr["SerialNumber"].ToString();
                itm.VinNumber = dr["VinNumber"].ToString();
                itm.PlanDeliverTime = dr["PlanDeliverTime"].ToString();
                itm.ProductNo = dr["ProductNo"].ToString();
                itm.ProductName = dr["ProductName"].ToString();
                cot.rows.Add(itm);
            }
            cot.total = dt.Rows.Count.ToString();
            string jsonStr = JSONTools.ScriptSerialize<mg_insertco_customerordertable>(cot);
            Response.Write(jsonStr);
            Response.End();

        }

        void Save()
        {
            string strCustomerNumber = Request.Params["CustomerNumber"];
            string strJITCallNumber = Request.Params["JITCallNumber"];
            string strSerialNumber = Request.Params["SerialNumber"];
            string strVinNumber = Request.Params["VinNumber"];
            string strPlanDeliverTime = Request.Params["PlanDeliverTime"];
            string strPartIDList = Request.Params["PartIDList"];

            string sql = @"InsertCustomerOrder";
            SqlParameter[] sqlParms = new SqlParameter[6];
            sqlParms[0] = new SqlParameter("CustomerNumber", SqlDbType.NVarChar);
            sqlParms[0].Value = strCustomerNumber;
            sqlParms[1] = new SqlParameter("JITCallNumber", SqlDbType.NVarChar);
            sqlParms[1].Value = strJITCallNumber;
            sqlParms[2] = new SqlParameter("SerialNumber", SqlDbType.NVarChar);
            sqlParms[2].Value = strSerialNumber;
            sqlParms[3] = new SqlParameter("VinNumber", SqlDbType.NVarChar);
            sqlParms[3].Value = strVinNumber;
            sqlParms[4] = new SqlParameter("PlanDeliverTime", SqlDbType.NVarChar);
            sqlParms[4].Value = strPlanDeliverTime;
            sqlParms[5] = new SqlParameter("PartIDList", SqlDbType.NVarChar);
            sqlParms[5].Value = strPartIDList;
            try
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.StoredProcedure, "InsertCustomerOrder", sqlParms);
                Response.Write("true");
            }
            catch (Exception ex)
            {
                Response.Write("false");
            }
            Response.End();
            return;
        }

        void Delete()
        {

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class mg_insertco_selectlist
    {
        public int id;
        public string text;
    }

    public class mg_insertco_part
    {
        public int part_id;
        public string part_no;
        public string part_name;
        public string part_desc;
    }

    class mg_insertco_parttable
    {
        public string total;
        public List<mg_insertco_part> rows;

        public mg_insertco_parttable()
        {
            rows = new List<mg_insertco_part>();
        }
    }

    public class mg_insertco_customerorder
    {
        public int OrderID;
        public string CustomerNumber;
        public string JITCallNumber;
        public string SerialNumber;
        public string VinNumber;
        public string PlanDeliverTime;
        public string ProductNo;
        public string ProductName;
    }

    class mg_insertco_customerordertable
    {
        public string total;
        public List<mg_insertco_customerorder> rows;

        public mg_insertco_customerordertable()
        {
            rows = new List<mg_insertco_customerorder>();
        }
    }
}