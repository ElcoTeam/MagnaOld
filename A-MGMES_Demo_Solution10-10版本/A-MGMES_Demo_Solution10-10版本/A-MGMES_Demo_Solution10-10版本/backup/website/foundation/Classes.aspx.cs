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


public partial class foundation_Classes : System.Web.UI.Page
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
        //BindStationID();
        this.GridView1.DataSource = mg_ClassesBLL.GetAllData(); 
        this.GridView1.DataBind();
        this.GridView1.SelectedIndex = -1;
        this.txt_name.Text = "";
        this.txt_starttime.Text = "";
        this.txt_endtime.Text = "";
    }
    //private void BindStationID()
    //{
    //    this.drp_stname.DataSource = mg_StationBLL.GetStationID();
    //    this.drp_stname.DataValueField = "st_name";
    //    this.drp_stname.DataBind();
    //}
    protected bool CkText_Box()
    {
        if (this.txt_name.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称不能为空！');</script>");
            this.txt_name.Focus();
            return false;
        }
        if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称不能包含单引号！');</script>");
            this.txt_name.Focus();
            return false;
        }

        if (this.txt_starttime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "上班时间", "<script language='javascript'> alert('上班时间不能为空！');</script>");
            this.txt_name.Focus();
            return false;
        }
        if (FormatHelper.IsTime(this.txt_starttime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "上班时间", "<script language='javascript'> alert('上班时间格式不正确！');</script>");
            this.txt_starttime.Focus();
            return false;
        }

        if (this.txt_endtime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间不能为空！');</script>");
            this.txt_name.Focus();
            return false;
        }
        if (FormatHelper.IsTime(this.txt_endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间格式不正确！');</script>");
            this.txt_endtime.Focus();
            return false;
        }
        return true;
    }
    protected void BtSave_Click(object sender, EventArgs e)
    {
        if (CkText_Box())
        {
            string classesname = this.txt_name.Text.Trim();
            string starttime = Convert.ToDateTime(this.txt_starttime.Text).ToLongTimeString().ToString();
            string endtime = Convert.ToDateTime(this.txt_endtime.Text).ToLongTimeString().ToString();
            

            bool IsExit = mg_ClassesBLL.CheckPositionByName(1, 0, classesname);
            if (IsExit)
            {
                bool flag = mg_ClassesBLL.AddPositionByName(classesname, starttime, endtime);
                if (flag)
                {
                    BindData();
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称重复，保存失败！');</script>");
                return;
            }
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
        bool flag = mg_ClassesBLL.DelPositionByName(id);
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
        TextBox classesname = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
        string name = classesname.Text.Trim();
        Label classesid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
        int id = NumericParse.StringToInt(classesid.Text);
        TextBox starttime = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox2");
        string stime = starttime.Text.Trim();
        TextBox endtime = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox3");
        string etime = endtime.Text.Trim();

        if (classesname.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称不能为空！');</script>");
            classesname.Focus();
            return;
        }
        if (FormatHelper.CheckPunctuation(classesname.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称不能包含单引号！');</script>");
            classesname.Focus();
            return;
        }

        if (starttime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "上班时间", "<script language='javascript'> alert('上班时间不能为空！');</script>");
            starttime.Focus();
            return;
        }
        if (FormatHelper.IsTime(starttime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "上班时间", "<script language='javascript'> alert('上班时间格式不正确！');</script>");
            starttime.Focus();
            return;
        }

        if (endtime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间不能为空！');</script>");
            endtime.Focus();
            return;
        }
        if (FormatHelper.IsTime(endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间格式不正确！');</script>");
            endtime.Focus();
            return;
        }


        bool IsExit = mg_ClassesBLL.CheckPositionByName(2, id, name);
        if (IsExit)
        {
            bool flag = mg_ClassesBLL.UpdatePositionByName(id, name, stime, etime);
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('班次名称重复，保存失败！');</script>");
            return;
        }
        


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindData();
    }

}