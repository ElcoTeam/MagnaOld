using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.Data;
using website;
using DbUtility;
using System.Windows.Forms;
using website.excel;
using System.Collections.Generic;

public class BOMHandler : IHttpHandler
{

    HttpRequest Request = null;
    HttpResponse Response = null;
    int pagesize;
    int page;
    string OrderPn;
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = System.Web.HttpContext.Current.Request;
        int Num = Convert.ToInt32(request["Num"]);
        OrderPn = request["orderPn"];
        pagesize = Convert.ToInt32(request["rows"]);
        page = Convert.ToInt32(request["page"]);

        context.Response.ContentType = "text/plain";

        Request = context.Request;
        Response = context.Response;

        string method = Request.Params["method"];
        switch (method)
        {


            case "saveBOM":
                SaveBOM();
                break;
            case "queryBOMList":
                if (Num == 2)
                {
                    QueryBOMFororderPn();
                }
                else
                {
                    QueryBOMList();
                }

                break;
            case "deleteBOM":
                DeleteBOM();
                break;
            case "getBomRelation":
                GetBomRelation();
                break;
            case "getAllPartRelation":
                GetAllPartRelation();
                break;
            case "getPartRelation":
                GetPartRelation();
                break;
            case "queryBOMForStepEditing":
                QueryBOMForStepEditing();
                break;
        }
    }
    DataTable dt2;
    void QueryBOMFororderPn()
    {
        if (page == 1)
        {
            string sql1 = @"select count([bom_id]) total from [mg_BOM];";
            string sql2 = @" 
                              with data as 
                                  (
	                                 select p.part_id,p.part_no,pbrel.bom_id from mg_part_bom_rel pbrel left join mg_part p on pbrel.part_id=p.part_id
	                                 )
                                SELECT [bom_id]
                                      ,[bom_PN]
                                      ,[bom_customerPN]
                                      ,[bom_isCustomerPN]
	                                   ,case [bom_isCustomerPN]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end bom_isCustomerPNName
                                      ,[bom_leve]
 ,l.prop_name [bom_leveName]
                                      ,[bom_materialid]
                                      ,m.prop_name [bom_material]
                                      ,[bom_suppllerid]
                                      ,s.prop_name [bom_suppller]
                                      ,[bom_categoryid]
                                      ,ca.prop_name [bom_category]
                                      ,[bom_storeid]
                                      ,st.prop_name [bom_storeName]
                                      ,[bom_colorid]
                                      ,co.prop_name [bom_colorname]
                                      ,[bom_profile]
                                      ,[bom_weight]
                                      ,[bom_desc]
                                      ,[bom_descCH]
                                      ,[bom_picture]
	                                   ,STUFF((SELECT ','+cast (part_id as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partIDs
	                                    ,STUFF((SELECT ','+cast (part_no as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partNOs
                                      ,b.barcode_start
                                      ,b.barcode_number  
                                  FROM [mg_BOM] b
                                  left join mg_Property m on b.bom_materialid=m.prop_id
                                  left join mg_Property s on b.bom_suppllerid=s.prop_id
                                  left join mg_Property ca on b.bom_categoryid=ca.prop_id
                                  left join mg_Property co on b.bom_colorid=co.prop_id
  left join mg_Property l on b.bom_leve=l.prop_id
  left join mg_Property st on b.bom_storeid=st.prop_id
                                    order by b.bom_id desc;
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            DataTable dt1 = ds.Tables["count"];
            //total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
            dt2 = ds.Tables["data"];
        }
        else
        {
            string sql1 = @"select count([bom_id]) total from [mg_BOM];";
            string sql2 = @" 
                              with data as 
                                  (
	                                 select p.part_id,p.part_no,pbrel.bom_id from mg_part_bom_rel pbrel left join mg_part p on pbrel.part_id=p.part_id
	                                 )
                                SELECT [bom_id]
                                      ,[bom_PN]
                                      ,[bom_customerPN]
                                      ,[bom_isCustomerPN]
	                                   ,case [bom_isCustomerPN]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end bom_isCustomerPNName
                                      ,[bom_leve]
 ,l.prop_name [bom_leveName]
                                      ,[bom_materialid]
                                      ,m.prop_name [bom_material]
                                      ,[bom_suppllerid]
                                      ,s.prop_name [bom_suppller]
                                      ,[bom_categoryid]
                                      ,ca.prop_name [bom_category]
                                      ,[bom_storeid]
                                      ,st.prop_name [bom_storeName]
                                      ,[bom_colorid]
                                      ,co.prop_name [bom_colorname]
                                      ,[bom_profile]
                                      ,[bom_weight]
                                      ,[bom_desc]
                                      ,[bom_descCH]
                                      ,[bom_picture]
	                                   ,STUFF((SELECT ','+cast (part_id as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partIDs
	                                    ,STUFF((SELECT ','+cast (part_no as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partNOs
                                      ,b.barcode_start 
                                      ,b.barcode_number  
                                  FROM [mg_BOM] b
                                  left join mg_Property m on b.bom_materialid=m.prop_id
                                  left join mg_Property s on b.bom_suppllerid=s.prop_id
                                  left join mg_Property ca on b.bom_categoryid=ca.prop_id
                                  left join mg_Property co on b.bom_colorid=co.prop_id
  left join mg_Property l on b.bom_leve=l.prop_id
  left join mg_Property st on b.bom_storeid=st.prop_id
                                    order by b.bom_id desc;
                                ";
            DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
            DataTable dt1 = ds.Tables["count"];
            //string total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
            dt2 = ds.Tables["data"];

        }

        DataTable dt = dt2;  //这里是填充DataTable数据
        DataTable dtNew = dt.Copy();  //复制dt表数据结构
        dtNew.Clear();  //清楚数据
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["bom_PN"].ToString() == OrderPn)
            {
                dtNew.Rows.Add(dt.Rows[i].ItemArray);  //添加数据行
            }
        }
        DataTable dt4 = GetPagedTable(dtNew, 1, 10);
        int total = dtNew.Rows.Count;
        string json = FunCommon.DataTableToJson2(total, dt4);
        //ExcelHelper.ExportDTtoExcel(dtNew, "HeaderText", HttpContext.Current.Request.MapPath("~/App_Data/excel2776.xlsx"));
        ListView a = new ListView();
        a = dataTableToListview.dataTableToListview1(a, dtNew);
        List<int> list = new List<int>() { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18 };
        dataTableToListview.ReportToExcel(a, list, "HeaderText");



        Response.Write(json);
        Response.End();
    }

    void dayin()
    {
        //pagesize = 290;   //295目前就不能导出了，290 没问题
        string sql1 = @"select count([bom_id]) total from [mg_BOM];";
        string sql2 = @" 
                              with data as 
                                  (
	                                 select p.part_id,p.part_no,pbrel.bom_id from mg_part_bom_rel pbrel left join mg_part p on pbrel.part_id=p.part_id
	                                 )
                                SELECT [bom_id]
                                      ,[bom_PN]
                                      ,[bom_customerPN]
                                      ,[bom_isCustomerPN]
	                                   ,case [bom_isCustomerPN]
		                                                            when 1 then '是'
		                                                            else '否'
		                                                            end bom_isCustomerPNName
                                      ,[bom_leve]
 ,l.prop_name [bom_leveName]
                                      ,[bom_materialid]
                                      ,m.prop_name [bom_material]
                                      ,[bom_suppllerid]
                                      ,s.prop_name [bom_suppller]
                                      ,[bom_categoryid]
                                      ,ca.prop_name [bom_category]
                                      ,[bom_storeid]
                                      ,st.prop_name [bom_storeName]
                                      ,[bom_colorid]
                                      ,co.prop_name [bom_colorname]
                                      ,[bom_profile]
                                      ,[bom_weight]
                                      ,[bom_desc]
                                      ,[bom_descCH]
                                      ,[bom_picture]
	                                   ,STUFF((SELECT ','+cast (part_id as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partIDs
	                                    ,STUFF((SELECT ','+cast (part_no as varchar) from data p where p.bom_id=b.bom_id  for xml path('')),1,1,'')partNOs
                                      ,b.barcode_start
                                      ,b.barcode_number  
                                  FROM [mg_BOM] b
                                  left join mg_Property m on b.bom_materialid=m.prop_id
                                  left join mg_Property s on b.bom_suppllerid=s.prop_id
                                  left join mg_Property ca on b.bom_categoryid=ca.prop_id
                                  left join mg_Property co on b.bom_colorid=co.prop_id
  left join mg_Property l on b.bom_leve=l.prop_id
  left join mg_Property st on b.bom_storeid=st.prop_id
                                    order by b.bom_id desc;
                                ";
        DataSet ds = SqlHelper.GetDataSetTableMapping(SqlHelper.SqlConnString, System.Data.CommandType.Text, sql1 + sql2, new string[] { "count", "data" }, null);
        DataTable dt1 = ds.Tables["count"];
        //total = DataHelper.GetCellDataToStr(dt1.Rows[0], "total");
        dt2 = ds.Tables["data"];

        //ExcelHelper.ExportDTtoExcel(dt2, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2776.xlsx"));
        ListView a = new ListView();
        a = dataTableToListview.dataTableToListview1(a, dt2);
        List<int> list = new List<int>() { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18 };
        dataTableToListview.ReportToExcel(a, list, "HeaderText");

        Response.End();

    }
    void QueryBOMForStepEditing()
    {
        string part_id = Request.Params["part_id"];
        string json = mg_BomBLL.QueryBOMForStepEditing(part_id);
        Response.Write(json);
        Response.End();
    }
    //void QueryPartListForBOM()
    //{

    //    string json = mg_PartBLL.QueryPartListForBOM();
    //    Response.Write(json);
    //    Response.End();
    //}


    //void QueryAllPartListForPart()
    //{
    //    string json = mg_allpartBLL.QueryAllPartListForPart();
    //    Response.Write(json);
    //    Response.End();
    //}

    void GetPartRelation()
    {
        string id = Request.Params["id"];
        string key = "p.part_id";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void GetAllPartRelation()
    {
        string id = Request.Params["id"];
        string key = "ap.[all_id]";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void GetBomRelation()
    {
        string id = Request.Params["id"];
        string key = "b.bom_id";
        string json = mg_BomBLL.GetRelation(key, id);
        Response.Write(json);
        Response.End();
    }
    void QueryBOMList()
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

        string json = mg_BomBLL.QueryBOMList(page, pagesize);
        Response.Write(json);
        dayin();
        Response.End();
    }

    void SaveBOM()
    {

        string bom_id = Request.Params["bom_id"];
        string bom_PN = Request.Params["bom_PN"];
        string bom_customerPN = Request.Params["bom_customerPN"];
        string bom_isCustomerPN = Request.Params["bom_isCustomerPN"];
        string bom_picture = Request.Params["bom_picture"];
        string bom_weight = Request.Params["bom_weight"];
        string bom_leve = Request.Params["bom_leve"];
        string bom_colorid = Request.Params["bom_colorid"];
        string bom_materialid = Request.Params["bom_materialid"];
        string bom_categoryid = Request.Params["bom_categoryid"];
        string bom_storeid = Request.Params["bom_storeid"];
        string bom_suppllerid = Request.Params["bom_suppllerid"];
        string bom_profile = Request.Params["bom_profile"];
        string bom_descCH = Request.Params["bom_descCH"];
        string bom_desc = Request.Params["bom_desc"];
        string partIDs = Request.Params["partIDs"];
        string barcode_start = Request.Params["barcode_start"];
        string barcode_number = Request.Params["barcode_number"];

        mg_BOMModel model = new mg_BOMModel();
        model.bom_id = NumericParse.StringToInt(bom_id);
        model.bom_PN = bom_PN;
        model.bom_customerPN = bom_customerPN;
        model.bom_isCustomerPN = NumericParse.StringToInt(bom_isCustomerPN);
        model.bom_picture = bom_picture;
        model.bom_weight = NumericParse.StringToInt(bom_weight);
        model.bom_leve = NumericParse.StringToInt(bom_leve);
        model.bom_colorid = NumericParse.StringToInt(bom_colorid);
        model.bom_materialid = NumericParse.StringToInt(bom_materialid);
        model.bom_categoryid = NumericParse.StringToInt(bom_categoryid);
        model.bom_storeid = NumericParse.StringToInt(bom_storeid);
        model.bom_suppllerid = NumericParse.StringToInt(bom_suppllerid);
        model.bom_profile = bom_profile;
        model.bom_descCH = bom_descCH;
        model.bom_desc = bom_desc;
        model.partIDs = partIDs;
        model.barcode_start = NumericParse.StringToInt(barcode_start);
        model.barcode_number = NumericParse.StringToInt(barcode_number);

        string json = mg_BomBLL.SaveBOM(model);
        Response.Write(json);
        Response.End();
    }

    void DeleteBOM()
    {
        string bom_id = Request.Params["bom_id"];

        string json = mg_BomBLL.DeleteBOM(bom_id);
        Response.Write(json);
        Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
    {
        if (PageIndex == 0)
            return dt;//0页代表每页数据，直接返回

        DataTable newdt = dt.Copy();
        newdt.Clear();//copy dt的框架

        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;

        if (rowbegin >= dt.Rows.Count)
            return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }
        return newdt;
    }

}