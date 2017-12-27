using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
public class Materialsortprinting : IHttpHandler
{

    HttpRequest Request = null;
    HttpResponse Response = null;

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        Request = context.Request;
        Response = context.Response;

        string method = Request.Params["method"];
        switch (method)
        {
            case "RePrintByCarID":
                RePrintByCarID();
                break;
            case "queryMaterialsortprintingList":
                queryMaterialsortprintingList();
                break;
           
        }
    }

    public string RePrintByCarID()
    {
        string carid = Request.Params["carid"];
        string selectwuliao = Request.Params["selectwuliao"];
        string result = "";
        string zfj = selectwuliao.Split('-')[0];
        string id = selectwuliao.Split('-')[1];
        string erweima, getWLName = "", getWLName123 = "";

        var print = px_PrintBLL.Querypx_PrintList().Where(s => s.XF.Equals(zfj) && s.ordername.Equals(id) && s.orderid.Equals(carid)).ToList();
        if (zfj.Equals("前排"))
            print = px_PrintBLL.Querypx_PrintList().Where(s => (s.XF.Equals("主驾") || s.XF.Equals("副驾")) && s.ordername.Equals(id) && s.orderid.Equals(carid)).ToList();

        if (print.Count > 0)
        {
            if (zfj.Equals("主驾") || zfj.Equals("副驾") || zfj.Equals("前排"))
            {
                getWLName = "前排" + id;
                getWLName123 = "前排" + id;
            }
            else if (id.Equals("扶手"))
            {
                getWLName = "后排中央扶手";
                getWLName123 = "后60扶手";
            }
            else if (id.Equals("中头枕"))
            {
                getWLName = "后排中央头枕";
                getWLName123 = "后60中头枕";
            }
            else if (id.Equals("坐垫面套"))
            {
                getWLName = "后排坐垫面套";
                getWLName123 = "后坐垫坐垫面套";
            }
            else if (id.Equals("靠背面套") || id.Equals("侧头枕"))
            {
                getWLName = zfj + "%" + id;
                getWLName123 = zfj + id;
            }

            erweima = print[0].PXID;
            string content = px_MaterialsortprintingBLL.printtestHas_getWLName(id, zfj, erweima, false, getWLName, getWLName123);
            if (content == "1")
                result = "打印操作成功" + erweima;
            else
                result = "打印操作失败，请检查打印机配置及环境";

        }
        else
        {
            result = "补打印失败，未找到补打印单记录。";
        }
        //  return this.Index(null, null, null);

        return result;
    }
    void queryMaterialsortprintingList()
    {
        string page = Request.Params["page"];
        string pagesize = Request.Params["rows"];
        DateTime? starttime = NumericParse.StringToDateTime(Request.Params["starttime"]);
        DateTime? endtime = NumericParse.StringToDateTime(Request.Params["endtime"]);
        string csh = Request.Params["csh"];
        if (string.IsNullOrEmpty(page))
        {
            page = "1";
        }
        if (string.IsNullOrEmpty(pagesize))
        {
            pagesize = "15";
        }

        string json = px_MaterialsortprintingBLL.queryMaterialsortprintingList(page, pagesize,starttime,endtime,csh);
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

}