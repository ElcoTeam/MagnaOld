using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BLL;
using Tools;

public partial class foundation_Position : System.Web.UI.Page
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
        this.grd_position.DataSource = mg_PositionBLL.GetAllData();
        this.grd_position.DataBind();
        this.grd_position.SelectedIndex = -1;
        this.txt_name.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        #region
        if (this.txt_name.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('职位名称不能为空！');</script>");
            this.txt_name.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('职位名称不能包含单引号！');</script>");
            this.txt_name.Focus();
            return;
        }
        #endregion


        string posiname = this.txt_name.Text.Trim();

        bool IsExit = mg_PositionBLL.CheckPositionByName(1, 0, posiname);
        if (IsExit)
        {
            bool flag = mg_PositionBLL.AddPositionByName(posiname);
            if (flag)
            {
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
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.grd_position.EditIndex = row.RowIndex;
        BindData();
    }
    protected void BtSave_Click(object sender, ImageClickEventArgs e)
    {
        TextBox posiname = (TextBox)grd_position.Rows[grd_position.EditIndex].FindControl("TextBox1");
        string name = posiname.Text.Trim();
        Label posiid = (Label)grd_position.Rows[grd_position.EditIndex].FindControl("Label2");
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
        bool IsExit = mg_PositionBLL.CheckPositionByName(2,id, name);
        if (IsExit)
        {
            bool flag = mg_PositionBLL.UpdatePositionByName(id, name);
            if (flag)
            {
                grd_position.EditIndex = -1;
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
    protected void BtCancel_Click(object sender, ImageClickEventArgs e)
    {
        grd_position.EditIndex = -1;
        BindData();
    }
    protected void BtDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.grd_position.EditIndex = row.RowIndex;
        Label aa = (Label)grd_position.Rows[row.RowIndex].FindControl("Label1");
        int id = NumericParse.StringToInt(aa.Text);
        bool flag = mg_PositionBLL.DelPositionByName(id);
        if (flag)
        {
            grd_position.EditIndex = -1;
            BindData();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
            return;
        }
    }
    protected void grd_position_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_position.PageIndex = e.NewPageIndex;
        BindData();
    }
}