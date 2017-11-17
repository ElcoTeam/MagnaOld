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


public partial class foundation_Step : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    BindData();
        //}
    }

    //private void BindData()
    //{
    //    BindStationName();
    //    BindBomName();
    //    this.GridView1.DataSource = mg_StepBLL.GetAllData();
    //    this.GridView1.DataBind();
    //    this.GridView1.SelectedIndex = -1;
    //    this.txt_name.Text = "";
    //    this.txt_bomcount.Text = "";
    //    this.txt_clock.Text = "";
    //    this.txt_desc.Text = "";
    //}
    //private void BindStationName()
    //{
    //    this.drp_stname.DataSource = mg_StepBLL.GetStationID();
    //    this.drp_stname.DataValueField = "st_id";
    //    this.drp_stname.DataTextField = "st_name";
    //    this.drp_stname.DataBind();
    //}

    //private void BindBomName()
    //{
    //    this.drp_bomanme.DataSource = mg_StepBLL.GetBomName();
    //    this.drp_bomanme.DataValueField = "bom_id";
    //   // this.drp_bomanme.DataTextField = "bom_name";
    //    this.drp_bomanme.DataBind();
    //}

    //protected bool CkText_Box()
    //{
    //    if (this.txt_name.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称不能为空！');</script>");
    //        this.txt_name.Focus();
    //        return false;
    //    }
    //    if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称不能包含单引号！');</script>");
    //        this.txt_name.Focus();
    //        return false;
    //    }

    //    if (this.txt_bomcount.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "数量", "<script language='javascript'> alert('部件数量不能为空！');</script>");
    //        this.txt_bomcount.Focus();
    //        return false;
    //    }
    //    if (FormatHelper.IsInteger(this.txt_bomcount.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "数量", "<script language='javascript'> alert('部件数量必须是整数！');</script>");
    //        this.txt_bomcount.Focus();
    //        return false;
    //    }

    //    if (this.txt_clock.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "节拍", "<script language='javascript'> alert('步骤节拍不能为空！');</script>");
    //        this.txt_clock.Focus();
    //        return false;
    //    }
    //    if (FormatHelper.IsInteger(this.txt_clock.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "节拍", "<script language='javascript'> alert('步骤节拍必须是整数！');</script>");
    //        this.txt_clock.Focus();
    //        return false;
    //    }

    //    if (this.txt_desc.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('步骤描述不能为空！');</script>");
    //        this.txt_desc.Focus();
    //        return false;
    //    }
    //    if(this.txt_desc.Text.Length>500)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('步骤描述不能超过500个字符！');</script>");
    //        this.txt_desc.Focus();
    //        return false;
    //    }

    //    if (FileUpload1.HasFile)//判断控件是否有文件路径
    //    {
    //        string filename = FileUpload1.FileName;//取得文件名
    //        string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
    //        if (filenametype.ToLower() != "jpg")//判断类型
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
    //            this.FileUpload1.Focus();
    //            return false;
    //        }
    //        if (mg_StepBLL.CheckPicName(filename))
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
    //            this.FileUpload1.Focus();
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('请选择图片！');</script>");
    //        this.FileUpload1.Focus();
    //        return false;
    //    }
    //    return true;
    //}
    //protected void BtSave_Click(object sender, EventArgs e)
    //{
    //    if (CkText_Box())
    //    {
    //        string name = this.txt_name.Text.Trim();
    //        int clock = Convert.ToInt32(this.txt_clock.Text.Trim());
    //        string desc = this.txt_desc.Text.Trim();
    //        string pic = this.FileUpload1.FileName;
    //        string picurl = Server.MapPath("~/foundation/image/step/" + pic);
    //        //传到根目录的images文件夹+重命名的文件名，也可以用原来的图片的名称，自己定。上传成功；
    //        FileUpload1.SaveAs(picurl);
    //        int stid = Convert.ToInt32(this.drp_stname.SelectedItem.Value);
    //        int bomid = Convert.ToInt32(this.drp_bomanme.SelectedItem.Value);
    //        int bomcount = Convert.ToInt32(this.txt_bomcount.Text.Trim());
    //        bool IsExit = mg_StepBLL.CheckStepByName(1, 0, name);
    //        if (IsExit)
    //        {
    //            bool flag = mg_StepBLL.AddStepByName(name, clock, desc, pic, stid, bomid, bomcount);
    //            if (flag)
    //            {
    //                BindData();
    //            }
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称重复，保存失败！');</script>");
    //            return;
    //        }
    //    }
    //}
    //protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton imgBtn = sender as ImageButton;
    //    GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
    //    this.GridView1.EditIndex = row.RowIndex;

    //    Label stid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_stid");
    //    string old_stid = stid.Text;
    //    Label bomid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_bomid");
    //    string old_bomid = bomid.Text;

    //    BindData();
    //    DropDownList estid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_stname");
    //    estid.DataSource = mg_StepBLL.GetStationID();
    //    estid.DataValueField = "st_id";
    //    estid.DataTextField = "st_name";
    //    estid.DataBind();
    //    estid.SelectedValue = old_stid;

    //    DropDownList ebomname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_bomname");
    //    ebomname.DataSource = mg_StepBLL.GetBomName();
    //    ebomname.DataValueField = "bom_id";
    //    ebomname.DataTextField = "bom_name";
    //    ebomname.DataBind();
    //    ebomname.SelectedValue = old_bomid;
    //}
    //protected void BtDel_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton imgBtn = sender as ImageButton;
    //    GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
    //    this.GridView1.EditIndex = row.RowIndex;
    //    Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("Label1");
    //    int id = NumericParse.StringToInt(aa.Text);
    //    //删除上传图片
    //    Label pic = (Label)GridView1.Rows[row.RowIndex].FindControl("lbl_pic1");
    //    string dpic = pic.Text;
    //    string dpicurl = Server.MapPath("~/foundation/image/step/" + dpic);

    //    bool flag = mg_StepBLL.DelStepByName(id);
    //    if (flag)
    //    {
    //        System.IO.File.Delete(dpicurl);
    //        GridView1.EditIndex = -1;
    //        BindData();
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
    //        return;
    //    }
    //}
    //protected void BtSave_Click1(object sender, ImageClickEventArgs e)
    //{
    //    TextBox ename = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ename");
    //    Label stepid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("Label2");
    //    TextBox eclock = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_eclock");
    //    TextBox edesc = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_edesc");
    //    DropDownList estid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_stname");
    //    DropDownList ebomid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_bomname");
    //    TextBox ecount = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ecount");
    //    FileUpload epic = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("upd_pic");
    //    string pic = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lbl_pic")).Text.Trim();
    //    string picurl = "";
    //    string dpicurl = "";
    //    if (ename.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称不能为空！');</script>");
    //        ename.Focus();
    //        return;
    //    }
    //    if (FormatHelper.CheckPunctuation(ename.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称不能包含单引号！');</script>");
    //        ename.Focus();
    //        return;
    //    }

    //    if (ecount.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "数量", "<script language='javascript'> alert('部件数量不能为空！');</script>");
    //        ecount.Focus();
    //        return;
    //    }
    //    if (FormatHelper.IsInteger(ecount.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "数量", "<script language='javascript'> alert('部件数量必须是整数！');</script>");
    //        ecount.Focus();
    //        return;
    //    }

    //    if (eclock.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "节拍", "<script language='javascript'> alert('步骤节拍不能为空！');</script>");
    //        eclock.Focus();
    //        return;
    //    }
    //    if (FormatHelper.IsInteger(eclock.Text.Trim()) == false)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "节拍", "<script language='javascript'> alert('步骤节拍必须是整数！');</script>");
    //        eclock.Focus();
    //        return;
    //    }

    //    if (edesc.Text.Trim() == "")
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('步骤描述不能为空！');</script>");
    //        edesc.Focus();
    //        return;
    //    }
    //    if (edesc.Text.Length > 500)
    //    {
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('步骤描述不能超过500个字符！');</script>");
    //        edesc.Focus();
    //        return;
    //    }

    //    if (epic.HasFile)//判断控件是否有文件路径
    //    {
    //        dpicurl = Server.MapPath("~/foundation/image/step/" + pic);
    //        string filename = epic.FileName;//取得文件名
    //        string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
    //        if (filenametype.ToLower() != "jpg")//判断类型
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
    //            epic.Focus();
    //            return;
    //        }
    //        if (mg_StepBLL.CheckPicName(filename))
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
    //            epic.Focus();
    //            return;
    //        }
    //        pic = epic.FileName;
    //        picurl = Server.MapPath("~/foundation/image/step/" + pic);
    //    }


    //    string name = ename.Text.Trim();
    //    int id = NumericParse.StringToInt(stepid.Text);
    //    int clock = Convert.ToInt32(eclock.Text.Trim());
    //    string desc = edesc.Text.Trim();
    //    int stid = Convert.ToInt32(estid.SelectedValue);
    //    int bomid = Convert.ToInt32(ebomid.SelectedValue);
    //    int count = Convert.ToInt32(ecount.Text.Trim());

    //    bool IsExit = mg_StepBLL.CheckStepByName(2, id, name);
    //    if (IsExit)
    //    {
    //        bool flag = mg_StepBLL.UpdateStepByName(id, name, clock, desc, pic, stid, bomid, count);
    //        if (flag)
    //        {
    //            if (dpicurl!="")
    //            {
    //                //删除原有图片
    //                System.IO.File.Delete(dpicurl);
    //                //上传图片
    //                epic.SaveAs(picurl);
    //            }
    //            GridView1.EditIndex = -1;
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
    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称重复，保存失败！');</script>");
    //        return;
    //    }
    //}
    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    BindData();
    //}

}