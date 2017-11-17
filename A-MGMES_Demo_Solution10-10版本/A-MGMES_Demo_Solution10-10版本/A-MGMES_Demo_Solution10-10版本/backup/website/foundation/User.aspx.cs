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


public partial class foundation_User : System.Web.UI.Page
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
        //BindDepartmentName();
        //BindPositionName();
        //this.GridView1.DataSource = mg_UserBLL.GetAllData();
        //this.GridView1.DataBind();
        //this.GridView1.SelectedIndex = -1;
        //this.txt_name.Text = "";
        //this.txt_pwd.Text = "";
        //this.txt_rfid.Text = "";
        //this.txt_email.Text = "";
        //this.txt_menuids.Text = "";
    }

    private void BindDepartmentName()
    {
        //this.drp_depname.DataSource = mg_UserBLL.GetDepName();
        //this.drp_depname.DataValueField = "dep_id";
        //this.drp_depname.DataTextField = "dep_name";
        //this.drp_depname.DataBind();
    }

    private void BindPositionName()
    {
        //this.drp_posiname.DataSource = mg_UserBLL.GetPosiName();
        //this.drp_posiname.DataValueField = "posi_id";
        //this.drp_posiname.DataTextField = "posi_name";
        //this.drp_posiname.DataBind();
    }

    protected void CkText_Box()
    {
        //if (this.txt_name.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('员工姓名不能为空！');</script>");
        //    this.txt_name.Focus();
        //    return false;
        //}
        //if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('员工姓名不能包含单引号！');</script>");
        //    this.txt_name.Focus();
        //    return false;
        //}

        //if (this.txt_pwd.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "密码", "<script language='javascript'> alert('登录密码不能为空！');</script>");
        //    this.txt_pwd.Focus();
        //    return false;
        //}

        //if (this.txt_rfid.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RFID", "<script language='javascript'> alert('员工RFID不能为空！');</script>");
        //    this.txt_rfid.Focus();
        //    return false;
        //}


        //if (this.txt_email.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Email", "<script language='javascript'> alert('员工Email不能为空！');</script>");
        //    this.txt_email.Focus();
        //    return false;
        //}
        //if (FormatHelper.IsEmail(this.txt_email.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Email", "<script language='javascript'> alert('Email格式不正确！');</script>");
        //    this.txt_email.Focus();
        //    return false;
        //}

        //if (this.txt_menuids.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "可见", "<script language='javascript'> alert('用户可见不能超过500个字符！');</script>");
        //    this.txt_menuids.Focus();
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
        //    if (mg_UserBLL.CheckPicName(filename))
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
        //return true;
    }
    protected void BtSave_Click(object sender, EventArgs e)
    {
        //if (CkText_Box())
        //{
        //    string name = this.txt_name.Text.Trim();
        //    string pwd = this.txt_pwd.Text.Trim();
        //    string rfid = this.txt_rfid.Text.Trim();
        //    string email = this.txt_email.Text.Trim();
        //    int depid = Convert.ToInt32(this.drp_depname.SelectedItem.Value);
        //    int posiid = Convert.ToInt32(this.drp_posiname.SelectedItem.Value);
        //    string menuids = this.txt_menuids.Text.Trim();
        //    string pic = this.Fld_pic.FileName;
        //    string picurl = Server.MapPath("~/foundation/image/user/" + pic);
        //    //传到根目录的images文件夹+重命名的文件名，也可以用原来的图片的名称，自己定。上传成功；

        //    bool IsExit = mg_UserBLL.CheckUserByName(1, 0, name);
        //    if (IsExit)
        //    {
        //        bool flag = mg_UserBLL.AddUserByName(name, pwd, rfid, email, depid, posiid, pic, menuids);
        //        if (flag)
        //        {
        //            Fld_pic.SaveAs(picurl);
        //            BindData();
        //        }
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部件名称重复，保存失败！');</script>");
        //        return;
        //    }
        //}
    }
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton imgBtn = sender as ImageButton;
        //GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        //this.GridView1.EditIndex = row.RowIndex;

        //Label depname = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_depno");
        //string old_depname = depname.Text;
        //Label posiname = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_posino");
        //string old_posiname = posiname.Text;

        //BindData();
        //DropDownList edepname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_edepname");
        //edepname.DataSource = mg_UserBLL.GetDepName();
        //edepname.DataValueField = "dep_id";
        //edepname.DataTextField = "dep_name";
        //edepname.DataBind();
        //edepname.SelectedValue = old_depname;

        //DropDownList eposiname = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_eposiname");
        //eposiname.DataSource = mg_UserBLL.GetPosiName();
        //eposiname.DataValueField = "posi_id";
        //eposiname.DataTextField = "posi_name";
        //eposiname.DataBind();
        //eposiname.SelectedValue = old_posiname;
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
        //string dpicurl = Server.MapPath("~/foundation/image/user/" + dpic);

        //bool flag = mg_UserBLL.DelUserByName(id);
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
        //Label bomid = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lb_eid");
        //TextBox ename = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ename");
        //TextBox epwd = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_epwd");
        //TextBox erfid = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_erfid");
        //TextBox eemail = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_eemail");
        //TextBox emenuids = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_emenuids");
        //DropDownList edepid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_edepname");
        //DropDownList eposiid = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("drp_eposiname");
        //FileUpload epic = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("upd_pic");
        //string pic = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lbl_opic")).Text.Trim();
        //string picurl = "";
        //string dpicurl = "";

        //if (ename.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('员工姓名不能为空！');</script>");
        //    ename.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(ename.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('员工姓名不能包含单引号！');</script>");
        //    ename.Focus();
        //    return;
        //}

        //if (epwd.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "密码", "<script language='javascript'> alert('登录密码不能为空！');</script>");
        //    epwd.Focus();
        //    return;
        //}

        //if (erfid.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RFID", "<script language='javascript'> alert('员工RFID不能为空！');</script>");
        //    erfid.Focus();
        //    return;
        //}


        //if (eemail.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Email", "<script language='javascript'> alert('员工Email不能为空！');</script>");
        //    eemail.Focus();
        //    return;
        //}
        //if (FormatHelper.IsEmail(eemail.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Email", "<script language='javascript'> alert('Email格式不正确！');</script>");
        //    eemail.Focus();
        //    return;
        //}

        //if (emenuids.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "可见", "<script language='javascript'> alert('用户可见不能超过500个字符！');</script>");
        //    emenuids.Focus();
        //    return;
        //}

        //if (epic.HasFile)//判断控件是否有文件路径
        //{
        //    dpicurl = Server.MapPath("~/foundation/image/user/" + pic);
        //    string filename = epic.FileName;//取得文件名
        //    string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
        //    if (filenametype.ToLower() != "jpg")//判断类型
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    if (mg_UserBLL.CheckPicName(filename))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    pic = epic.FileName;
        //    picurl = Server.MapPath("~/foundation/image/user/" + pic);
        //}

        //int id = NumericParse.StringToInt(bomid.Text);
        //string name = ename.Text.Trim();
        //string pwd = epwd.Text.Trim();
        //string rfid = erfid.Text.Trim();
        //string email = eemail.Text.Trim();
        //string menuids = emenuids.Text.Trim();
        //int depid = Convert.ToInt32(edepid.SelectedValue);
        //int posiid = Convert.ToInt32(eposiid.SelectedValue);


        //bool IsExit = mg_UserBLL.CheckUserByName(2, id, name);
        //if (IsExit)
        //{
        //    bool flag = mg_UserBLL.UpdateUserByName(id, name, pwd, rfid, email, menuids, depid, posiid,pic);
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