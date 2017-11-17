using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;
using Bll;
using DBUtility;
using Tools;


public partial class foundation_Piistation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    ddlBind(DropDownList1);
        //    BindData();
        //}
    }

    private void BindData()
    {
        //this.GridView1.DataSource = mg_PartBLL.GetAllData();
        //this.GridView1.DataBind();
        //FormatHelper.MergeRows(GridView1, 0, "Label6");
        //FormatHelper.MergeRows(GridView1, 1, "Label8");
        //this.GridView1.SelectedIndex = -1;
        //this.txt_no.Text = "";
        //this.txt_name.Text = "";
        //this.txt_desc.Text = "";
        //DropDownList1.SelectedIndex = -1;
    }

    private void ddlBind(DropDownList dr)
    {
        //dr.Items.Clear();
        //dr.Items.Add("");
        //string sql = @"select [all_no] from [mg_allpart] order by all_id asc";
        //DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

        //if (tb.Rows.Count != 0)
        //{
        //    for (int i = 0; i < tb.Rows.Count; i++)
        //    {
        //        dr.Items.Add(tb.Rows[i]["all_no"].ToString());
        //    }
        //}
        //tb.Dispose();
    }

    private int allid(string allno)
    {
        //int allid=0;

        //string sql=@"select [all_id] from [mg_allpart] where all_no='"+ allno +"'";
        //DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        //if (dt.Rows.Count!=0)
        //{
        //    allid = NumericParse.StringToInt(dt.Rows[0]["all_id"].ToString());
        //}
        //else
        //{
        //    allid = 0;
        //}
        //return allid;
        return 0;
    }
    private int partid(string partno)
    {
        //int partid = 0;

        //string sql = @"select [part_id] from [mg_part] where part_no='" + partno + "'";
        //DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
        //if (dt.Rows.Count != 0)
        //{
        //    partid = NumericParse.StringToInt(dt.Rows[0]["part_id"].ToString());
        //}
        //else
        //{
        //    partid = 0;
        //}
        //return partid;
        return 0;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //int id1, id2;
        //#region
        //if (this.txt_no.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号不能为空！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_no.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号不能包含单引号！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}

        //if (this.txt_name.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件名称不能为空！');</script>");
        //    this.txt_name.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件名称不能包含单引号！');</script>");
        //    this.txt_name.Focus();
        //    return;
        //}

        //if (!string.IsNullOrEmpty(this.txt_desc.Text.Trim()))
        //{
        //    if (FormatHelper.CheckPunctuation(this.txt_desc.Text.Trim()) == false)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('描述不能包含单引号！');</script>");
        //        this.txt_desc.Focus();
        //        return;
        //    }
        //}

        //if (string.IsNullOrEmpty(DropDownList1.SelectedValue))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('请选择座椅号！');</script>");
        //    this.DropDownList1.Focus();
        //    return;
        //}
        //#endregion

        //bool IsExit = mg_PartBLL.CheckPartByName(1,0, this.txt_no.Text.Trim(), this.txt_name.Text.Trim(), this.txt_desc.Text.Trim(), DropDownList1.SelectedValue);
        //if (IsExit)
        //{
        //    bool flag = mg_PartBLL.AddPartByName(this.txt_no.Text.Trim(), this.txt_name.Text.Trim(),
        //        this.txt_desc.Text.Trim());
        //    if (flag)
        //    {
        //        id1=allid(DropDownList1.SelectedValue);
        //        id2 = partid(this.txt_no.Text.Trim());
        //        bool flag1 = mg_PartBLL.Addrel(id1, id2);
        //        if (flag1)
        //        {
        //            BindData();
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //            return;
        //        }


        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号或大部件名称已存在！');</script>");
        //    return;
        //}
    }
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //Label lballno = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label8");
        //string oldallno = lballno.Text;
        //BindData();
        //DropDownList drallno= (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        //ddlBind(drallno);
        //drallno.SelectedValue = oldallno;

    }
    protected void BtSave_Click(object sender, ImageClickEventArgs e)
    {
        //int oldallid, oldpartid, newallid, newpartid;
        //int pid;

        //Label id = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
        //pid = NumericParse.StringToInt(id.Text);
        //TextBox tbno = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
        //string pno = tbno.Text;
        //TextBox tbname = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox2");
        //string pname = tbname.Text;
        //TextBox tbdesc = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox3");
        //string pdesc = tbdesc.Text;
        //DropDownList drall = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList2");
        //string allno = drall.SelectedValue;
        //Label lball = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label7");


        //oldallid = NumericParse.StringToInt(lball.Text);
        //oldpartid = pid;
        //#region
        //if (pno == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号不能为空！');</script>");
        //    tbno.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(pno) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号不能包含单引号！');</script>");
        //    tbno.Focus();
        //    return;
        //}

        //if (pname == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件名称不能为空！');</script>");
        //    tbname.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(pname) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件名称不能包含单引号！');</script>");
        //    tbname.Focus();
        //    return;
        //}

        //if (!string.IsNullOrEmpty(pdesc))
        //{
        //    if (FormatHelper.CheckPunctuation(pdesc) == false)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('工大部件不能包含单引号！');</script>");
        //        tbdesc.Focus();
        //        return;
        //    }
        //}

        //if (string.IsNullOrEmpty(allno))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅号不能为空！');</script>");
        //    drall.Focus();
        //    return;
        //}
        //#endregion
        //bool IsExit = mg_PartBLL.CheckPartByName(2, pid, pno, pname, pdesc,DropDownList1.SelectedValue);
        //if (IsExit)
        //{
        //    bool flag = mg_PartBLL.UpdatePartByName(pid, pno, pname, pdesc);
        //    if (flag)
        //    {
        //        newallid = allid(allno);
        //        newpartid = partid(pno);
        //        bool flag1 = mg_PartBLL.Delrel(oldallid, oldpartid);
        //        if (flag1)
        //        {
        //            bool flag2 = mg_PartBLL.Updaterel(newallid, newpartid);
        //            if (flag2)
        //            {
        //                GridView1.EditIndex = -1;
        //                BindData();
        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //                return; 
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号或大部件名称已存在！');</script>");
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

        //Label bb = (Label)GridView1.Rows[row.RowIndex].FindControl("Label6");
        //int allid = NumericParse.StringToInt(bb.Text);

        //bool flag = mg_PartBLL.DelPartByName(id);
        //if (flag)
        //{
        //    bool flag1 = mg_PartBLL.Delrel(allid, id);
        //    if (flag1)
        //    {
        //        GridView1.EditIndex = -1;
        //        BindData();
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
        //    return;
        //}
    }
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