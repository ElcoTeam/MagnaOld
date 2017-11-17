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

public partial class foundation_FlowLine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        this.GridView1.DataSource = mg_FlowLineBLL.GetAllData();
        this.GridView1.DataBind();
        this.GridView1.SelectedIndex = -1;
        this.txt_name.Text = "";
    }

    protected void BtSave_Click(object sender, EventArgs e)
    {
        #region
        if (this.txt_name.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('流水线名称不能为空！');</script>");
            this.txt_name.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('流水线名称不能包含单引号！');</script>");
            this.txt_name.Focus();
            return;
        }
        #endregion


        string flname = this.txt_name.Text.Trim();

        bool IsExit = mg_FlowLineBLL.CheckFlowlineByName(1, 0, flname);
        if (IsExit)
        {
            bool flag = mg_FlowLineBLL.AddFlowlineByName(flname);
            if (flag)
            {
                BindData();
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('流水线名称重复，保存失败！');</script>");
            return;
        }
    }
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.GridView1.EditIndex = row.RowIndex;
        BindData();
    }
    protected void BtDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.GridView1.EditIndex = row.RowIndex;
        Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("Label1");
        int id = NumericParse.StringToInt(aa.Text);
        bool flag = mg_FlowLineBLL.DelFlowlineByName(id);
        if (flag)
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
            return;
        }
    }
    protected void BtSave_Click1(object sender, ImageClickEventArgs e)
    {
        TextBox posiname = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
        string name = posiname.Text.Trim();
        Label posiid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
        int id = NumericParse.StringToInt(posiid.Text);

        #region
        if (name == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('职位名称不能为空！');</script>");
            posiname.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(name) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('职位名称不能包含单引号！');</script>");
            posiname.Focus();
            return;
        }
        #endregion
        bool IsExit = mg_FlowLineBLL.CheckFlowlineByName(2, id, name);
        if (IsExit)
        {
            bool flag = mg_FlowLineBLL.UpdateFlowlineByName(id, name);
            if (flag)
            {
                GridView1.EditIndex = -1;
                BindData();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('职位名称重复，保存失败！');</script>");
            return;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void BtCancel_Click(object sender, ImageClickEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }
}