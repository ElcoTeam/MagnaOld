using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bll;
using Tools;
using System.IO;
using System.Text;

namespace website.SortManagent
{
    public partial class ShowChiClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindData();
        }
        void BindData() 
        {
           // this.Repeater1.DataSource = px_ShowChiClientBLL.GetAllData();
           // this.Repeater1.DataBind();
        }
       
    }
}