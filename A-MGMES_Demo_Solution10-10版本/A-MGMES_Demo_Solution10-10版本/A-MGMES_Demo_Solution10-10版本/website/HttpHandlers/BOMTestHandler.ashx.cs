using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;
using Tools;
using Bll;

namespace website.HttpHandlers
{
    /// <summary>
    /// BOMTest 的摘要说明
    /// </summary>
    
    public class BOMTest : IHttpHandler
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
                case "saveBOMTest":
                    SaveBOMTest();
                    break;
                case "QueryBOMTestList":
                    QueryBOMTestList();
                    break;
                case "DeleteBOMTest":
                    DeleteBOMTest();
                    break;
            }
        }
        void SaveBOMTest()
        {

            string test_id = Request.Params["test_id"];
            string testgroupid = Request.Params["testgroupid"];
            string testpage = Request.Params["testpage"];
            string testtype = Request.Params["testtype"];
            string testcalculatetype = Request.Params["testcalculatetype"];
            string testcaption = Request.Params["testcaption"];
            string testvaluemin = Request.Params["testvaluemin"];
            string testvaluemax = Request.Params["testvaluemax"];
            string testvalueiscontain = Request.Params["testvalueiscontain"];
            string testvalueunit = Request.Params["testvalueunit"];
            string plcname = Request.Params["plcname"];
            string plcvaluetype = Request.Params["plcvaluetype"];
            string plcoutmultiple = Request.Params["plcoutmultiple"];


            mg_BOMTestModel model = new mg_BOMTestModel();
            model.test_id = NumericParse.StringToInt(test_id);
            model.testgroupid = NumericParse.StringToInt(testgroupid);
            model.testpage = NumericParse.StringToInt(testpage);
            model.testtype = NumericParse.StringToInt(testtype);
            model.testcalculatetype = NumericParse.StringToInt(testcalculatetype);
            model.testcaption = testcaption;
            model.testvaluemin = float.Parse(testvaluemin);
            model.testvaluemax = float.Parse(testvaluemax); ;
            model.testvalueiscontain = NumericParse.StringToInt(testvalueiscontain);
            model.testvalueunit = testvalueunit;
            model.plcname = plcname;
            model.plcvaluetype = plcvaluetype;
            model.plcoutmultiple = NumericParse.StringToInt(plcoutmultiple);
            
            string json = mg_BOMTestBLL.SaveBOMTest(model);
            Response.Write(json);
            Response.End();
        }

        void QueryBOMTestList()
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
            string json = mg_BOMTestBLL.QueryBOMTestList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void DeleteBOMTest()
        {
            string test_id = Request.Params["test_id"];

            string json = mg_BOMTestBLL.DeleteBOMTest(test_id);
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
}