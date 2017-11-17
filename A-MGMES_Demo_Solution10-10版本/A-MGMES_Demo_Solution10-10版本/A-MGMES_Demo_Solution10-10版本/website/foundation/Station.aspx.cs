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
using DBUtility;

public partial class foundation_Station : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    BindData();
        //    ddlBind(DropDownList1,"");
            
        //}
    }

    private void BindData()
    {
        //this.GridView1.DataSource = mg_StationBLL.GetAllData();
        //this.GridView1.DataBind();
        //this.GridView1.SelectedIndex = -1;
        //this.txt_no.Text = "";
        //this.txt_name.Text = "";
        //DropDownList1.SelectedIndex = -1;
        //CheckBox1.Checked = false;
    }

    private void ddlBind(DropDownList dr,string str)
    {
        //dr.Items.Clear();
        //dr.Items.Add("");
        //string sql = @"select * from [mg_FlowLine] " + str +" order by fl_id asc";
        //DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

        //if (tb.Rows.Count != 0)
        //{
        //    for (int i = 0; i < tb.Rows.Count; i++)
        //    {
        //        dr.Items.Add(tb.Rows[i]["fl_name"].ToString());
        //    }
        //}
        //tb.Dispose();
    }

    //private int flid(string flname)
    //{
    //    //int id=0;
    //    //string sql = @"select * from [mg_FlowLine] where fl_name='"+flname+"'";
    //    //DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
    //    //if (tb.Rows.Count != 0)
    //    //{
    //    //    for (int i = 0; i < tb.Rows.Count; i++)
    //    //    {
    //    //        id =NumericParse.StringToInt( tb.Rows[i]["fl_id"].ToString());
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    id = 0;
    //    //}
    //    //tb.Dispose();
    //    //return id;
    //}

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        //int ispre=0;
        //#region

        //if (this.txt_no.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号不能为空！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_no.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号不能包含单引号！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}

        //if (this.txt_name.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位名称不能为空！');</script>");
        //    this.txt_name.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位名称不能包含单引号！');</script>");
        //    this.txt_name.Focus();
        //    return;
        //}

        //if (DropDownList1.SelectedValue.ToString() == "" || DropDownList1.SelectedValue.ToString() == null)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('请选择流水线！');</script>");
        //    this.DropDownList1.Focus();
        //    return;
        //}
        //#endregion

        //if (this.CheckBox1.Checked==true)
        //{
        //    ispre=1;
        //}
        //else
        //{
        //    ispre=0;
        //}

        //bool IsExit = mg_StationBLL.CheckStByName(1, 0, this.txt_no.Text.Trim(),this.txt_name.Text.Trim(), flid(DropDownList1.SelectedValue));
        //if (IsExit)
        //{
        //    bool flag = mg_StationBLL.AddStByName(this.txt_no.Text.Trim(), this.txt_name.Text.Trim(), flid(DropDownList1.SelectedValue), ispre);
        //    if (flag)
        //    {
        //        BindData();
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号或工位名称已存在！');</script>");
        //    return;
        //}
    }

    /// <summary>
    /// 开启编辑行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //Label flname = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label3");
        //string odlflname= flname.Text;

        //Label ipre = (Label) GridView1.Rows[GridView1.EditIndex].FindControl("Label4");

        //bool oldipre = ipre.Text=="是" ? true : false;
        //BindData();
        //DropDownList eflid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        //ddlBind(eflid, "");
        //eflid.SelectedValue = odlflname;

        //CheckBox newCheckBox = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("CheckBox3");

        //newCheckBox.Checked = oldipre;
        ////BindData();
        ////DropDownList estid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");

    }

    /// <summary>
    /// 编辑保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtSave_Click(object sender, ImageClickEventArgs e)
    {
        //int ispre;
        //int st_id;

        //Label id = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
        //st_id = NumericParse.StringToInt(id.Text);
        //TextBox tbno = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
        //string no = tbno.Text;
        //TextBox tbname = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox2");
        //string name = tbname.Text;
        //DropDownList flname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        //string fname = flname.SelectedValue;

        //CheckBox cb = (CheckBox)GridView1.Rows[GridView1.EditIndex].FindControl("CheckBox3");
        //if(cb.Checked==true)
        //{
        //    ispre = 1;
        //}
        //else
        //{
        //    ispre = 0;
        //}

        //#region

        //if (no == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号不能为空！');</script>");
            
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(no) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号不能包含单引号！');</script>");
        //    return;
        //}

        //if (name=="")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位名称不能为空！');</script>");
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(name) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位名称不能包含单引号！');</script>");
        //    return;
        //}

        //if (string.IsNullOrEmpty(fname))
        ////if (fname == "" || fname == null)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('请选择流水线！');</script>");
        //    return;
        //}
        //#endregion

        //bool IsExit = mg_StationBLL.CheckStByName(2, st_id, no, name, flid(fname));
        //if (IsExit)
        //{
        //    bool flag = mg_StationBLL.UpdateStByName(st_id, no, name, flid(fname), ispre);
        //    if (flag)
        //    {
        //        GridView1.EditIndex = -1;
        //        BindData();
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工位号或工位名称重复，保存失败！');</script>");
        //    return;
        //}
    }
    protected void BtDel_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("Label1");
        //int id = NumericParse.StringToInt(aa.Text);
        //bool flag = mg_StationBLL.DelStByName(id);
        //if (flag)
        //{
        //    GridView1.EditIndex = -1;
        //    BindData();
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
        //    return;
        //}
    }
    /// <summary>
    /// 取消编辑保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtCancel_Click(object sender, ImageClickEventArgs e)
    {
        //GridView1.EditIndex = -1;
        //BindData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //BindData();
    }
}