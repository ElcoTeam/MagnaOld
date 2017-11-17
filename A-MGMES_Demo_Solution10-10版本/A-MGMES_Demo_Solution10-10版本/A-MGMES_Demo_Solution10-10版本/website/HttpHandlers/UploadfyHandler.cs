using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.IO;

public class UploadfyHandler : IHttpHandler
{

    HttpRequest Request = null;
    HttpResponse Response = null;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        Request = context.Request;
        Response = context.Response;

        string method = context.Request.Params["method"];
        switch (method)
        {
            case "uploadOperatorImg":
                UploadOperatorImg();
                break;

            case "uploadBOMImg":
                UploadBOMImg();
                break;

            case "uploadStepImg":
                UploadStepImg();
                break;

            case "uploadStationImg":
                UploadStationImg();
                break;
            case "uploadStationPDF":
                UploadStationPDF();
                break;

                
        }


    }

    void UploadStationPDF()
    {
        UpdaloadImg("StationPDF");
    }
    
    

    void UploadStationImg()
    {
        UpdaloadImg("StationImages");
    }

    void UploadStepImg()
    {
        UpdaloadImg("StepImages");
    }

    void UploadBOMImg()
    {
        UpdaloadImg("BOMImages");
    }


    void UploadOperatorImg()
    {
        UpdaloadImg("OperatorImages");
    }

    void UpdaloadImg(string path)
    {
        //接收上传后的文件
        HttpPostedFile file = Request.Files["Filedata"];
        string datePath = DateTime.Now.Date.ToString();
        //其他参数
        //string somekey = context.Request["someKey"];
        //string other = context.Request["someOtherKey"];
        //获取文件的保存路径
        string uploadPath = HttpContext.Current.Server.MapPath("\\UploadImages\\" + path + "\\");
        
        //判断上传的文件是否为空
        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            int dotIndex = file.FileName.LastIndexOf('.');
            string extStr = file.FileName.Substring(file.FileName.LastIndexOf('.'), file.FileName.Length - dotIndex);
            string beforStr = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
            string newFilename = beforStr + "_" + NumericParse.StringToDateTime(DateTime.Now.ToString(), "yyyyMMddhhmmss") + extStr;
            FileInfo defile = new FileInfo(uploadPath + newFilename);
            if (defile.Exists)
            {
                defile.Delete();
            }

            //保存文件
            file.SaveAs(uploadPath + newFilename);
            
            
            Response.Write("/UploadImages/" + path + "/" + newFilename);
        }
        else
        {
            Response.Write("0");
        }
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