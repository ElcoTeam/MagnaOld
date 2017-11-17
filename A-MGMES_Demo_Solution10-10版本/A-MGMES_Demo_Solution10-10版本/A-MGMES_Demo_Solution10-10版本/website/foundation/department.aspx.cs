using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll;
using Tools;

public partial class foundation_department : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        this.grd_department.DataSource = mg_DepartmentBLL.GetAllData();
        this.grd_department.DataBind();
        this.grd_department.SelectedIndex = -1;
        this.txt_name.Text = "";
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        #region
        if (this.txt_name.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称不能为空！');</script>");
            this.txt_name.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称不能包含单引号！');</script>");
            this.txt_name.Focus();
            return;
        }
        #endregion
        string depname = this.txt_name.Text.Trim();
        bool IsExit = mg_DepartmentBLL.CheckDepartmentByName(1, 0, depname);
        if (IsExit)
        {
            bool flag = mg_DepartmentBLL.AddDepartmentByName(depname);
            if (flag)
            {
                BindData();
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称重复，保存失败！');</script>");
            return;
        }

        
    }
    //开启编辑行
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row  =imgBtn.Parent.Parent as GridViewRow;
        this.grd_department.EditIndex = row.RowIndex;
        BindData();
    }


    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
    {
        TextBox depname = (TextBox)grd_department.Rows[grd_department.EditIndex].FindControl("TextBox2");
        string name = depname.Text.Trim();
        Label deid = (Label)grd_department.Rows[grd_department.EditIndex].FindControl("Label11");
        int id = NumericParse.StringToInt( deid.Text);

        #region
        if (name == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称不能为空！');</script>");
            depname.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(name) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称不能包含单引号！');</script>");
            depname.Focus(); 
            return;
        }
        #endregion
        bool IsExit = mg_DepartmentBLL.CheckDepartmentByName(2,id, name);
        if (IsExit)
        {
            bool flag = mg_DepartmentBLL.UpdateDepartmentByName(id, name);
            if (flag)
            {
                grd_department.EditIndex = -1;
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部门名称重复，保存失败！');</script>");
            return;
        }

        
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        grd_department.EditIndex = -1;
        BindData();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton23_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.grd_department.EditIndex = row.RowIndex;
        Label aa = (Label)grd_department.Rows[row.RowIndex].FindControl("Label1");
        int id = NumericParse.StringToInt(aa.Text);
        bool flag= mg_DepartmentBLL.DelDepartmentByName(id);
        if (flag)
        {
            grd_department.EditIndex = -1;
            BindData();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
            return;
        }
    }
    protected void grd_department_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grd_department.PageIndex = e.NewPageIndex;
        BindData();
    }
}