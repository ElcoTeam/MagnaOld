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


public partial class foundation_Operator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
        }
    }

    private void BindData()
    {
        //BindStationName();
        //BindIsoperator();
        //this.GridView1.DataSource = mg_OperatorBLL.GetAllData();
        //this.GridView1.DataBind();
        //this.GridView1.SelectedIndex = -1;
        //this.txt_name.Text = "";
        //this.txt_rfid.Text = "";
    }

    private void BindStationName()
    {
        //this.drp_stname.DataSource = mg_OperatorBLL.GetStName();
        //this.drp_stname.DataValueField = "st_id";
        //this.drp_stname.DataTextField = "st_name";
        //this.drp_stname.DataBind();
    }

    private void BindIsoperator()
    {
        //this.drp_isoperator.DataValueField = "isoperator";
        //this.drp_isoperator.DataValueField = "op_isoperator";
        //this.drp_isoperator.Items.Clear();
        //ListItem l1=new ListItem();
        //l1.Value="1";
        //l1.Text="True";
        //ListItem l2=new ListItem();
        //l2.Value = "0";
        //l2.Text = "False";
        //this.drp_isoperator.Items.Add(l1);
        //this.drp_isoperator.Items.Add(l2);
    }

    protected bool CkText_Box()
    {
        //if (this.txt_name.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('操作工姓名不能为空！');</script>");
        //    this.txt_name.Focus();
        //    return false;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('操作工姓名不能包含单引号！');</script>");
        //    this.txt_name.Focus();
        //    return false;
        //}

        //if (this.txt_rfid.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RFID", "<script language='javascript'> alert('员工RFID不能为空！');</script>");
        //    this.txt_rfid.Focus();
        //    return false;
        //}

        //if (Fld_pic.HasFile)//判断控件是否有文件路径
        //{
        //    string filename = Fld_pic.FileName;//取得文件名
        //    string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
        //    if (filenametype.ToLower() != "jpg")//判断类型
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
        //        this.Fld_pic.Focus();
        //        return false;
        //    }
        //    if (mg_OperatorBLL.CheckPicName(filename))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
        //        this.Fld_pic.Focus();
        //        return false;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('请选择图片！');</script>");
        //    this.Fld_pic.Focus();
        //    return false;
        //}
        return true;
    }
    protected void BtSave_Click(object sender, EventArgs e)
    {
        //if (CkText_Box())
        //{
        //    string name = this.txt_name.Text.Trim();
        //    string rfid = this.txt_rfid.Text.Trim();

        //    int stid = Convert.ToInt32(this.drp_stname.SelectedItem.Value);
        //    int isoperator = Convert.ToInt32(this.drp_isoperator.SelectedItem.Value);
        //    //string pic = this.Fld_pic.FileName;
        //    //string picurl = Server.MapPath("~/foundation/image/operator/" + pic);
        //    //传到根目录的images文件夹+重命名的文件名，也可以用原来的图片的名称，自己定。上传成功；

        //    bool IsExit = mg_OperatorBLL.CheckOperatorByName(1, 0, name);
        //    if (IsExit)
        //    {
        //        //bool flag = mg_OperatorBLL.AddOperatorByName(name, rfid, stid, isoperator, pic);
        //        //if (flag)
        //        //{
        //        //    //Fld_pic.SaveAs(picurl);
        //        //    BindData();
        //        //}
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "姓名", "<script language='javascript'> alert('操作工姓名重复，保存失败！');</script>");
        //        return;
        //    }
        //}
    }
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;

        //Label stno = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_stno");
        //string old_stno = stno.Text;
        //Label isoperator = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_isoperator");
        //string old_isoperator = isoperator.Text;

        //BindData();
        //DropDownList estname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_estname");
        //estname.DataSource = mg_OperatorBLL.GetStName();
        //estname.DataValueField = "st_id";
        //estname.DataTextField = "st_name";
        //estname.DataBind();
        //estname.SelectedValue = old_stno;

        //DropDownList eisoperator = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_eisoperator");
        //ListItem l1 = new ListItem();
        //l1.Value = "1";
        //l1.Text = "True";
        //ListItem l2 = new ListItem();
        //l2.Value = "0";
        //l2.Text = "False";
        //eisoperator.Items.Add(l1);
        //eisoperator.Items.Add(l2);
        //eisoperator.SelectedValue = old_isoperator;
    }
    protected void BtDel_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;
        //Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("lb_id");
        //int id = NumericParse.StringToInt(aa.Text);
        ////删除上传图片
        //Label pic = (Label)GridView1.Rows[row.RowIndex].FindControl("lb_pic");
        //string dpic = pic.Text;
        //string dpicurl = Server.MapPath("~/foundation/image/operator/" + dpic);

        //bool flag = mg_OperatorBLL.DelOperatorByName(id);
        //if (flag)
        //{
        //    System.IO.File.Delete(dpicurl);
        //    GridView1.EditIndex = -1;
        //    BindData();
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
        //    return;
        //}
    }
    protected void BtSave_Click1(object sender, ImageClickEventArgs e)
    {
        //Label opid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_eid");
        //TextBox ename = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ename");
        //TextBox erfid = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_erfid");
        //DropDownList estname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_estname");
        //DropDownList eisoperator = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_eisoperator");
        //FileUpload epic = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("upd_pic");
        //string pic = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lbl_opic")).Text.Trim();
        //string picurl = "";
        //string dpicurl = "";

        //if (ename.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('操作工姓名不能为空！');</script>");
        //    ename.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(ename.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('操作工姓名不能包含单引号！');</script>");
        //    ename.Focus();
        //    return;
        //}

        //if (erfid.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RFID", "<script language='javascript'> alert('员工RFID不能为空！');</script>");
        //    erfid.Focus();
        //    return;
        //}

        //if (epic.HasFile)//判断控件是否有文件路径
        //{
        //    dpicurl = Server.MapPath("~/foundation/image/operator/" + pic);
        //    string filename = epic.FileName;//取得文件名
        //    string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
        //    if (filenametype.ToLower() != "jpg")//判断类型
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    if (mg_OperatorBLL.CheckPicName(filename))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    pic = epic.FileName;
        //    picurl = Server.MapPath("~/foundation/image/operator/" + pic);
        //}

        //int id = NumericParse.StringToInt(opid.Text);
        //string name = ename.Text.Trim();
        //string rfid = erfid.Text.Trim();

        //int stid = Convert.ToInt32(estname.SelectedValue);
        //int isoperator = Convert.ToInt32(eisoperator.SelectedValue);


        //bool IsExit = mg_OperatorBLL.CheckOperatorByName(2, id, name);
        //if (IsExit)
        //{
        //    bool flag = mg_OperatorBLL.UpdateOperatorByName(id, name, rfid, stid, isoperator, pic);
        //    if (flag)
        //    {
        //        if (dpicurl!="")
        //        {
        //            //删除原有图片
        //            System.IO.File.Delete(dpicurl);
        //            //上传图片
        //            epic.SaveAs(picurl);
        //        }
        //        GridView1.EditIndex = -1;
        //        BindData();
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "姓名", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "姓名", "<script language='javascript'> alert('姓名重复，保存失败！');</script>");
        //    return;
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //BindData();
    }

}