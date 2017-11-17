using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminCMS_AdminIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Literal1.Text = "服务器地址: " + Request.ServerVariables.Get("Remote_Host").ToString() + "<br/><br/>"
            + "浏览器: " + Request.Browser.Browser + "<br/><br/>"
            + "浏览器版本号: " + Request.Browser.MajorVersion + "<br/><br/>"
            + "客户端平台: " + Request.Browser.Platform + "<br/><br/>"
            + "服务器ip: " + Request.ServerVariables.Get("Local_Addr").ToString() + "<br/><br/>"
            + "服务器名： " + Request.ServerVariables.Get("Server_Name").ToString() + "<br/><br/>"
            + "服务器地址： " + Request.ServerVariables["Url"].ToString() + "<br/><br/>"
            + "客户端提供的路径信息： " + Request.ServerVariables["Path_Info"].ToString() + "<br/><br/>"
            + "与应用程序元数据库路径相应的物理路径： " + Request.ServerVariables["Appl_Physical_Path"].ToString() + "<br/><br/>"
            + "通过由虚拟至物理的映射后得到的路径： " + Request.ServerVariables["Path_Translated"].ToString() + "<br/><br/>"
            + "执行脚本的名称： " + Request.ServerVariables["Script_Name"].ToString() + "<br/><br/>"
            + "接受请求的服务器端口号： " + Request.ServerVariables["Server_Port"].ToString() + "<br/><br/>"
            + "发出请求的远程主机的IP地址： " + Request.ServerVariables["Remote_Addr"].ToString() + "<br/><br/>"
            + "发出请求的远程主机名称IP地址： " + Request.ServerVariables["Remote_Host"].ToString() + "<br/><br/>"
            + "返回接受请求的服务器地址IP地址： " + Request.ServerVariables["Local_Addr"].ToString() + "<br/><br/>"
            + "返回服务器地址IP地址： " + Request.ServerVariables["Http_Host"].ToString() + "<br/><br/>"
            + "服务器的主机名、DNS地址或IP地址： " + Request.ServerVariables["Server_Name"].ToString() + "<br/><br/>"
            + "提出请求的方法比如GET、HEAD、POST等等： " + Request.ServerVariables["Request_Method"].ToString() + "<br/><br/>"
            + "如果接受请求的服务器端口为安全端口时，则为1，否则为0： " + Request.ServerVariables["Server_Port_Secure"].ToString() + "<br/><br/>"
            + "服务器使用的协议的名称和版本： " + Request.ServerVariables["Server_Protocol"].ToString() + "<br/><br/>"
            + "应答请求并运行网关的服务器软件的名称和版本： " + Request.ServerVariables["Server_Software"].ToString() + "<br/><br/>"
            ;


            if (Request.Cookies["admininfo"] != null)

            {
                this.namelit.Text = Request.Cookies["admininfo"]["name"];
                this.tellit.Text = HttpUtility.UrlDecode(Request.Cookies["admininfo"]["user_posiid_name"]);
            }


            SetttingMenu();


        }
    }

    private void SetttingMenu()
    {
        this.m1.Visible = false;
        this.m1_1.Visible = false;
        this.m1_2.Visible = false;
        this.m1_3.Visible = false;
        this.m1_4.Visible = false;
        this.m1_5.Visible = false;
        this.m1_6.Visible = false;

        this.m2.Visible = false;
        this.m2_1.Visible = false;
        this.m2_2.Visible = false;
        this.m2_3.Visible = false;
        this.m2_4.Visible = false;
        this.m2_5.Visible = false;
        this.m2_6.Visible = false;
        this.m2_7.Visible = false;
        this.m2_8.Visible = false;
        this.m2_9.Visible = false;
        this.m2_10.Visible = false;
        this.m2_11.Visible = false;

        this.m3.Visible = false;
        this.m3_1.Visible = false;
        this.m3_2.Visible = false;
        this.m3_3.Visible = false;


        this.m4.Visible = false;
        this.m4_1.Visible = false;
        this.m4_3.Visible = false;

        this.m5.Visible = false;
        this.m5_1.Visible = false;
        this.m5_2.Visible = false;
        this.m5_3.Visible = false;
        this.m5_4.Visible = false;
        this.m5_5.Visible = false;
        this.m5_6.Visible = false;
        this.m5_7.Visible = false;
        this.m5_8.Visible = false;
        this.m5_9.Visible = false;
        this.m5_10.Visible = false;
        this.m5_11.Visible = false;

        this.m6.Visible = false;
        this.m6_1.Visible = false;

        string menuids = "";
        if (Request.Cookies["admininfo"] != null)
        {
            menuids = HttpUtility.UrlDecode(Request.Cookies["admininfo"]["menuids"]);
        }
        string[] menuidArr = menuids.Split(',');
        foreach (string item in menuidArr)
        {
            //14 23 34 42 53
            switch (item)
            {
                case "m1": this.m1.Visible = true; break;
                case "m1_1": this.m1_1.Visible = true; break;
                case "m1_2": this.m1_2.Visible = true; break;
                case "m1_3": this.m1_3.Visible = true; break;
                case "m1_4": this.m1_4.Visible = true; break;
                case "m1_5": this.m1_5.Visible = true; break;
                case "m1_6": this.m1_6.Visible = true; break;
                    
                case "m2": this.m2.Visible = true; break;
                case "m2_1": this.m2_1.Visible = true; break;
                case "m2_2": this.m2_2.Visible = true; break;
                case "m2_3": this.m2_3.Visible = true; break;
                case "m2_4": this.m2_4.Visible = true; break;
                case "m2_5": this.m2_5.Visible = true; break;
                case "m2_6": this.m2_6.Visible = true; break;
                case "m2_7": this.m2_7.Visible = true; break;
                case "m2_8": this.m2_8.Visible = true; break;
                case "m2_9": this.m2_9.Visible = true; break;
                case "m2_10": this.m2_10.Visible = true; break;
                case "m2_11": this.m2_11.Visible = true; break;

                case "m3": this.m3.Visible = true; break;
                case "m3_1": this.m3_1.Visible = true; break;
                case "m3_2": this.m3_2.Visible = true; break;
                case "m3_3": this.m3_3.Visible = true; break;

                // case "m3_4": this.m3_4.Visible = true; break;
                case "m4": this.m4.Visible = true; break;
                case "m4_1": this.m4_1.Visible = true; break;
                case "m4_3": this.m4_3.Visible = true; break;

                //case "m4_2": this.m4_2.Visible = true; break;
                case "m5": this.m5.Visible = true; break;
                case "m5_1": this.m5_1.Visible = true; break;
                case "m5_2": this.m5_2.Visible = true; break;
                case "m5_3": this.m5_3.Visible = true; break;
                case "m5_4": this.m5_4.Visible = true; break;
                case "m5_5": this.m5_5.Visible = true; break;
                case "m5_6": this.m5_6.Visible = true; break;
                case "m5_7": this.m5_7.Visible = true; break;
                case "m5_8": this.m5_8.Visible = true; break;
                case "m5_9": this.m5_9.Visible = true; break;
                case "m5_10": this.m5_10.Visible = true; break;
                case "m5_11": this.m5_11.Visible = true; break;

                case "m6": this.m6.Visible = true; break;
                case "m6_1": this.m6_1.Visible = true; break;
        }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["admininfo"] != null)
            Request.Cookies["admininfo"].Expires = DateTime.Now.AddDays(-1);

        System.Web.Security.FormsAuthentication.SignOut();

        Session.Abandon();
        Session.RemoveAll();
        Session.Clear();

        this.Response.Redirect("/AdminIndex.aspx");
        this.Response.End();
    }
}