﻿<%@ WebHandler Language="C#" Class="CustomerOrderHandler" %>

using System;
using System.Web;
using BLL;
using Model;
using Tools;

public class CustomerOrderHandler : IHttpHandler
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


            case "queryCuttedList":
                QueryCuttedList();
                break;
            case "saveCustomerOrder":
                SaveCustomerOrder();
                break;

            case "queryUnCuttedList":
                QueryUnCuttedList();
                break;

            case "deleteCustomerOrder":
                DeleteCustomerOrder();
                break;
                
                 case "cuttingOrder":
                CuttingOrder();
                break;

                
                
            //case "sorting":
            //    Sorting();
            //    break;
        }
    }
    //void QueryODSByStepid()
    //{
    //    string step_id = Request.Params["step_id"];


    //    string json = mg_StepBLL.QueryODSByStepid(step_id);
    //    Response.Write(json);
    //    Response.End();
    //}



    void CuttingOrder()
    {
        string co_id = Request.Params["co_id"];

        string json = mg_CustomerOrderBLL.CuttingOrder(co_id);
        Response.Write(json);
        Response.End();
    }


    void DeleteCustomerOrder()
    {
        string co_id = Request.Params["co_id"];

        string json = mg_CustomerOrderBLL.DeleteCustomerOrder(co_id);
        Response.Write(json);
        Response.End();
    }

    void QueryUnCuttedList()
    {
        //string fl_id = Request.Params["fl_id"];
        //string st_id = Request.Params["st_id"];
        //string part_id = Request.Params["part_id"];
        
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

        string json = mg_CustomerOrderBLL.QueryList(page, pagesize,"0");
        Response.Write(json);
        Response.End();
    }

    void QueryCuttedList()
    {
        //string fl_id = Request.Params["fl_id"];
        //string st_id = Request.Params["st_id"];
        //string part_id = Request.Params["part_id"];

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

        string json = mg_CustomerOrderBLL.QueryList(page, pagesize, "1");
        Response.Write(json);
        Response.End();
    }


    void SaveCustomerOrder()
    {
        string co_id = Request.Params["co_id"];
        string co_cutomerid = Request.Params["co_cutomerid"];
        string co_no = Request.Params["co_no"];
        string all_ids = Request.Params["all_ids"];
        string co_counts = Request.Params["co_counts"];
        string co_isCutted = Request.Params["co_isCutted"];

        mg_CustomerOrderModel model = new mg_CustomerOrderModel();
        model.co_id = NumericParse.StringToInt(co_id);
        model.co_cutomerid = NumericParse.StringToInt(co_cutomerid);
        model.co_no = co_no;
        model.all_ids = all_ids;
        model.co_counts = co_counts;
        model.co_isCutted = NumericParse.StringToInt(co_isCutted);

        string json = mg_CustomerOrderBLL.saveCustomerOrder(model);
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