using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Bll;
using DbUtility;
using Tools;

public partial class foundation_mg_allpart_part_rel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddrBind();
            ddrPartBind();
            BindData();
        }
    }

    private void BindData()
    {
        this.GridView1.DataSource = mg_allpart_part_relBLL.GetAllData();
        this.GridView1.DataBind();
        this.GridView1.SelectedIndex = -1;
        this.ddlAll.SelectedIndex = -1;
        this.ddlPart.SelectedIndex = -1; 
    }

    private void ddrBind()
    {
        ddlAll.Items.Clear();
        ddlAll.Items.Add("");
        string sql = @"select [all_no] from [mg_allpart]  order by all_id asc";
        DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

        if (tb.Rows.Count != 0)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ddlAll.Items.Add(tb.Rows[i]["all_no"].ToString());
            }
        }
        tb.Dispose();
    }

    private void ddrPartBind()
    {
        ddlPart.Items.Clear();
        ddlPart.Items.Add("");
        string sql = @"select [part_name] from [mg_part]  order by part_id asc";
        DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

        if (tb.Rows.Count != 0)
        {
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ddlPart.Items.Add(tb.Rows[i]["part_name"].ToString());
            }
        }
        tb.Dispose();
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {

    }
}