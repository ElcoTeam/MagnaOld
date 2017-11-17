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
using Tools;


public partial class foundation_allpart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    BindData();
        //}
    }

    private void BindData()
    {
        //this.GridView1.DataSource = mg_allpartBLL.GetAllData();
        //this.GridView1.DataBind();
        //this.GridView1.SelectedIndex = -1;
        //this.txt_no.Text = "";
        //this.txt_rate.Text = "";
        //this.txt_color.Text = "";
        //this.txt_metaname.Text = "";
        //this.txt_desc.Text = "";
    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {
        //#region
        //if (this.txt_no.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能为空！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_no.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能包含单引号！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}

        //if (this.txt_rate.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('配置不能为空！');</script>");
        //    this.txt_rate.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_rate.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('配置不能包含单引号！');</script>");
        //    this.txt_rate.Focus();
        //    return;
        //}

        //if (this.txt_color.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能为空！');</script>");
        //    this.txt_color.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_color.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('颜色不能包含单引号！');</script>");
        //    this.txt_color.Focus();
        //    return;
        //}

        //if (this.txt_metaname.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('材质不能为空！');</script>");
        //    this.txt_metaname.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_metaname.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('材质不能包含单引号！');</script>");
        //    this.txt_metaname.Focus();
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
        //#endregion

        //bool isExit = mg_allpartBLL.CheckAllByName(1, 0, this.txt_no.Text.Trim());
        //if (isExit)
        //{
        //    bool flag = mg_allpartBLL.AddAllByName(this.txt_no.Text.Trim(), this.txt_rate.Text.Trim(),
        //        this.txt_color.Text.Trim(), this.txt_metaname.Text.Trim(),this.txt_desc.Text.Trim());
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
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号已存在！');</script>");
        //    this.txt_no.Focus();
        //    return;
        //}
    }
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //BindData();
    }
    protected void BtSave_Click(object sender, ImageClickEventArgs e)
    {
        //int aid;

        //Label id = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
        //aid = NumericParse.StringToInt(id.Text);
        //TextBox ano = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
        //string allno = ano.Text;
        //TextBox tbrate = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox2");
        //string allrate = tbrate.Text;
        //TextBox tbcolor = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox3");
        //string allcolor = tbcolor.Text;
        //TextBox tbmeta = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox2");
        //string allmeta = tbmeta.Text;
        //TextBox tbdesc = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("TextBox3");
        //string alldesc = tbdesc.Text;

        //#region
        //if (allno == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能为空！');</script>");
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(allno) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能包含单引号！');</script>");
        //    return;
        //}

        //if (allrate == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('配置不能为空！');</script>");
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(allrate) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('配置不能包含单引号！');</script>");
        //    return;
        //}

        //if (allcolor == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅编号不能为空！');</script>");
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(allcolor) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('颜色不能包含单引号！');</script>");
        //    return;
        //}

        //if (allmeta == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('材质不能为空！');</script>");
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(allmeta) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('材质不能包含单引号！');</script>");
        //    return;
        //}

        //if (!string.IsNullOrEmpty(alldesc))
        //{
        //    if (FormatHelper.CheckPunctuation(alldesc) == false)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('描述不能包含单引号！');</script>");
        //        this.txt_desc.Focus();
        //        return;
        //    }
        //}
        //#endregion

        //bool isExit = mg_allpartBLL.CheckAllByName(2, aid, allno);
        //if (isExit)
        //{
        //    bool flag = mg_allpartBLL.UpdateAllByName(aid, allno, allrate, allcolor,allmeta,alldesc);
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
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('大部件号或大部件名称已存在！');</script>");
        //    return;
        //}
    }
    protected void BtCancel_Click(object sender, ImageClickEventArgs e)
    {
        //GridView1.EditIndex = -1;
        //BindData();
    }
    protected void BtDel_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("Label1");
        //int id = NumericParse.StringToInt(aa.Text);
        //bool flag = mg_allpartBLL.DelAllByName(id);
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //BindData();
    }
}