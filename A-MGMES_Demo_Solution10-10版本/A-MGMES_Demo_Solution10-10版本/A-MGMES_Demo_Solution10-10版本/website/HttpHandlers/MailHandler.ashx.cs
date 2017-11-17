using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;
using Tools;


namespace website.HttpHandlers
{
    /// <summary>
    /// MailHandler 的摘要说明
    /// </summary>
    public class MailHandler : IHttpHandler
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
                case "QueryMailList":
                    queryMailList();
                    break;
                case "saveMail":
                    saveMail();
                    break;
                case "DeleteMail":
                    DeleteMail();
                    break;

            }
        }
        void queryMailList()
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

            string json = mg_MailBLL.queryMailList(page, pagesize);
            Response.Write(json);
            Response.End();
        }
        void saveMail()
        {
            string mail_id = Request.Params["mail_id"];
            string ReceiptType = Request.Params["ReceiptType"];
            //string sorting = Request.Params["sorting"];
            string RecipientType = Request.Params["RecipientType"];
            string MailName = Request.Params["Mail"];

            mg_MailModel model = new mg_MailModel();
            model.mail_id = NumericParse.StringToInt(mail_id);
            model.ReceiptType = NumericParse.StringToInt(ReceiptType);

            //model.sorting = NumericParse.StringToInt(sorting);
            model.RecipientType = NumericParse.StringToInt(RecipientType);
            model.MailName = MailName;


            string json = mg_MailBLL.SaveMail(model);
            Response.Write(json);
            Response.End();
        }
        void DeleteMail()
        {
            string mail_id = Request.Params["mail_id"];

            string json = mg_MailBLL.DeleteMail(mail_id);
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