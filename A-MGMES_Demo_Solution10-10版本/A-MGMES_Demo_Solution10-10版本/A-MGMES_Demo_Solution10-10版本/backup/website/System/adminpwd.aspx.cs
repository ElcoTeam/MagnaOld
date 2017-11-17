using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using System.Text;

public partial class AdminCMS_System_adminpwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Cookies["admininfo"] != null)
            {
                //this.pwd1.Text = Request.Cookies["admininfo"]["password"];
                this.HiddenField1.Value = Request.Cookies["admininfo"]["id"];
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
       
    }
}