using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll;
using Model;

namespace website.HttpHandlers
{
    /// <summary>
    /// AndonTaglinesHandler 的摘要说明
    /// </summary>
    public class AndonTaglinesHandler : IHttpHandler
    {
        private HttpRequest _request = null;
        private HttpResponse _response = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            _request = context.Request;
            _response = context.Response;

            string method = _request.Params["method"];
            switch (method)
            {
                case "QueryTaglinesList":
                    QueryTaglinesList();
                    break;

                case "SaveTaglines":
                    SaveTaglines();
                    break;
            }
        }

        //保存数据
        private void SaveTaglines()
        {
            string id = _request.Params["taglinesId"];
            string taglinesType = _request.Params["taglinesType"];
            string taglinesText = _request.Params["taglinesText"];
            Andon_Taglines taglines = new Andon_Taglines()
            {
                ID = int.Parse(id),
                TaglinesType = taglinesType,
                TaglinesText = taglinesText,
            };
            string result = AndonTaglinesBLL.SaveTaglines(taglines);
            _response.Write(result);
            _response.End();
        }

        //查数据
        private void QueryTaglinesList()
        {
            string jsonStr = AndonTaglinesBLL.QueryAndonTaglinesList();
            _response.Write(jsonStr);
            _response.End();
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