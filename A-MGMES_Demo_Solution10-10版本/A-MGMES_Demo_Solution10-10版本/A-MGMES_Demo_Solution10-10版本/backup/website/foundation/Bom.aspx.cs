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


public partial class foundation_Bom : System.Web.UI.Page
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
        //this.GridView1.DataSource = mg_BomBLL.GetAllData();
        //this.GridView1.DataBind();
        //this.GridView1.SelectedIndex = -1;
        //this.txt_name.Text = "";
        //this.txt_no.Text = "";
        //this.txt_level.Text = "";
        //this.txt_desc.Text = "";
        //this.txt_material.Text = "";
        //this.txt_profile.Text = "";
        //this.txt_weight.Text = "";
        //this.txt_supplier.Text = "";
        //this.txt_category.Text = "";
        //this.txt_comments.Text = "";
    }
    //protected bool CkText_Box()
    //{
    //    //if (this.txt_name.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部件名称不能为空！');</script>");
    //    //    this.txt_name.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.CheckPunctuation(this.txt_name.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部件名称不能包含单引号！');</script>");
    //    //    this.txt_name.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_no.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "编号", "<script language='javascript'> alert('部件编号不能为空！');</script>");
    //    //    this.txt_no.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.CheckPunctuation(this.txt_no.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "编号", "<script language='javascript'> alert('部件编号不能包含单引号！');</script>");
    //    //    this.txt_no.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_level.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "等级", "<script language='javascript'> alert('部件等级不能为空！');</script>");
    //    //    this.txt_level.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.IsInteger(this.txt_level.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "等级", "<script language='javascript'> alert('部件等级必须是整数！');</script>");
    //    //    this.txt_level.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_desc.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('部件描述不能为空！');</script>");
    //    //    this.txt_desc.Focus();
    //    //    return false;
    //    //}
    //    //if (this.txt_desc.Text.Length > 500)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('部件描述不能超过500个字符！');</script>");
    //    //    this.txt_desc.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_material.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "材质", "<script language='javascript'> alert('部件材质不能为空！');</script>");
    //    //    this.txt_material.Focus();
    //    //    return false;
    //    //}
    //    //if (this.txt_material.Text.Length > 500)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "材质", "<script language='javascript'> alert('部件材质不能超过500个字符！');</script>");
    //    //    this.txt_material.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_profile.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "说明", "<script language='javascript'> alert('部件说明不能为空！');</script>");
    //    //    this.txt_profile.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.CheckPunctuation(this.txt_profile.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "说明", "<script language='javascript'> alert('部件说明不能包含单引号！');</script>");
    //    //    this.txt_profile.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_weight.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "重量", "<script language='javascript'> alert('部件重量不能为空！');</script>");
    //    //    this.txt_weight.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.IsInteger(this.txt_weight.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "重量", "<script language='javascript'> alert('部件重量必须是整数！');</script>");
    //    //    this.txt_weight.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_supplier.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "供应商", "<script language='javascript'> alert('供应商不能为空！');</script>");
    //    //    this.txt_supplier.Focus();
    //    //    return false;
    //    //}
    //    //if (this.txt_supplier.Text.Length > 500)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "供应商", "<script language='javascript'> alert('供应商不能超过500个字符！');</script>");
    //    //    this.txt_supplier.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_category.Text.Trim() == "")
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "品类", "<script language='javascript'> alert('部件品类不能为空！');</script>");
    //    //    this.txt_category.Focus();
    //    //    return false;
    //    //}
    //    //if (FormatHelper.CheckPunctuation(this.txt_category.Text.Trim()) == false)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "品类", "<script language='javascript'> alert('部件品类不能包含单引号！');</script>");
    //    //    this.txt_category.Focus();
    //    //    return false;
    //    //}

    //    //if (this.txt_comments.Text.Length > 500)
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "备注", "<script language='javascript'> alert('备注不能超过500个字符！');</script>");
    //    //    this.txt_supplier.Focus();
    //    //    return false;
    //    //}

    //    //if (Fld_pic.HasFile)//判断控件是否有文件路径
    //    //{
    //    //    string filename = Fld_pic.FileName;//取得文件名
    //    //    string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
    //    //    if (filenametype.ToLower() != "jpg")//判断类型
    //    //    {
    //    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
    //    //        this.Fld_pic.Focus();
    //    //        return false;
    //    //    }
    //    //    if (mg_BomBLL.CheckPicName(filename))
    //    //    {
    //    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
    //    //        this.Fld_pic.Focus();
    //    //        return false;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('请选择图片！');</script>");
    //    //    this.Fld_pic.Focus();
    //    //    return false;
    //    //}
    //    //return true;
    //}
    protected void BtSave_Click(object sender, EventArgs e)
    {
        //if (CkText_Box())
        //{
        //    string name = this.txt_name.Text.Trim();
        //    string no = this.txt_no.Text.Trim();
        //    int level =Convert.ToInt32(this.txt_level.Text.Trim());
        //    string desc = this.txt_desc.Text.Trim();

        //    string pic = this.Fld_pic.FileName;
        //    string picurl = Server.MapPath("~/foundation/image/bom/" + pic);
        //    //传到根目录的images文件夹+重命名的文件名，也可以用原来的图片的名称，自己定。上传成功；
            

        //    string material = this.txt_material.Text.Trim();
        //    string profile = this.txt_profile.Text.Trim();
        //    int weight = Convert.ToInt32(this.txt_weight.Text.Trim());
        //    string supplier = this.txt_supplier.Text.Trim();
        //    string category = this.txt_category.Text.Trim();
        //    string comments = this.txt_comments.Text.Trim();

        //    bool IsExit = mg_BomBLL.CheckBomByName(1, 0, name);
        //    if (IsExit)
        //    {
        //        bool flag = mg_BomBLL.AddBomByName(name, no, level, desc, pic, material, profile, weight, supplier, category, comments);
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
        //BindData();
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
        //string dpicurl = Server.MapPath("~/foundation/image/bom/" + dpic);

        //bool flag = mg_BomBLL.DelBomByName(id);
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
        //TextBox eno = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_eno");
        //TextBox elevel = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_elevel");
        //TextBox edesc = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_edesc");
        //TextBox ematerial = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ematerial");
        //TextBox eprofile = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_eprofile");
        //TextBox eweight = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_eweight");
        //TextBox esupplier = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_esuppller");
        //TextBox ecategory = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ecategory");
        //TextBox ecomments = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_ecomments");

        

        //FileUpload epic = (FileUpload)GridView1.Rows[GridView1.EditIndex].FindControl("upd_pic");

        //string pic = ((Label)GridView1.Rows[GridView1.EditIndex].FindControl("lbl_opic")).Text.Trim();
        //string picurl = "";
        //string dpicurl = "";

        //if (ename.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部件名称不能为空！');</script>");
        //    ename.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(ename.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('部件名称不能包含单引号！');</script>");
        //    ename.Focus();
        //    return;
        //}

        //if (eno.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "编号", "<script language='javascript'> alert('部件编号不能为空！');</script>");
        //    eno.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(eno.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "编号", "<script language='javascript'> alert('部件编号不能包含单引号！');</script>");
        //    eno.Focus();
        //    return;
        //}

        //if (elevel.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "等级", "<script language='javascript'> alert('部件等级不能为空！');</script>");
        //    elevel.Focus();
        //    return;
        //}
        //if (FormatHelper.IsInteger(elevel.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "等级", "<script language='javascript'> alert('部件等级必须是整数！');</script>");
        //    elevel.Focus();
        //    return;
        //}

        //if (edesc.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('部件描述不能为空！');</script>");
        //    edesc.Focus();
        //    return;
        //}
        //if (edesc.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "描述", "<script language='javascript'> alert('部件描述不能超过500个字符！');</script>");
        //    edesc.Focus();
        //    return;
        //}

        //if (ematerial.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "材质", "<script language='javascript'> alert('部件材质不能为空！');</script>");
        //    ematerial.Focus();
        //    return;
        //}
        //if (ematerial.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "材质", "<script language='javascript'> alert('部件材质不能超过500个字符！');</script>");
        //    ematerial.Focus();
        //    return;
        //}

        //if (eprofile.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "说明", "<script language='javascript'> alert('部件说明不能为空！');</script>");
        //    eprofile.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(eprofile.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "说明", "<script language='javascript'> alert('部件说明不能包含单引号！');</script>");
        //    eprofile.Focus();
        //    return;
        //}

        //if (eweight.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "重量", "<script language='javascript'> alert('部件重量不能为空！');</script>");
        //    eweight.Focus();
        //    return;
        //}
        //if (FormatHelper.IsInteger(eweight.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "重量", "<script language='javascript'> alert('部件重量必须是整数！');</script>");
        //    eweight.Focus();
        //    return;
        //}

        //if (esupplier.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "供应商", "<script language='javascript'> alert('供应商不能为空！');</script>");
        //    esupplier.Focus();
        //    return;
        //}
        //if (esupplier.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "供应商", "<script language='javascript'> alert('供应商不能超过500个字符！');</script>");
        //    esupplier.Focus();
        //    return;
        //}

        //if (ecategory.Text.Trim() == "")
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "品类", "<script language='javascript'> alert('部件品类不能为空！');</script>");
        //    ecategory.Focus();
        //    return;
        //}
        //if (FormatHelper.CheckPunctuation(ecategory.Text.Trim()) == false)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "品类", "<script language='javascript'> alert('部件品类不能包含单引号！');</script>");
        //    ecategory.Focus();
        //    return;
        //}

        //if (ecomments.Text.Length > 500)
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "备注", "<script language='javascript'> alert('备注不能超过500个字符！');</script>");
        //    ecomments.Focus();
        //    return;
        //}
        //if (epic.HasFile)//判断控件是否有文件路径
        //{
        //    dpicurl = Server.MapPath("~/foundation/image/bom/" + pic);
        //    string filename = epic.FileName;//取得文件名
        //    string filenametype = filename.Substring(filename.LastIndexOf(".") + 1);//取得后缀
        //    if (filenametype.ToLower() != "jpg")//判断类型
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片格式只支持jpg！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    if (mg_StepBLL.CheckPicName(filename))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "图片", "<script language='javascript'> alert('图片名称重复！');</script>");
        //        epic.Focus();
        //        return;
        //    }
        //    pic = epic.FileName;
        //    picurl = Server.MapPath("~/foundation/image/bom/" + pic);
        //}

        //int id = NumericParse.StringToInt(bomid.Text);
        //string name = ename.Text.Trim();
        //string no = eno.Text.Trim();
        //int level = Convert.ToInt32(elevel.Text.Trim());
        //string desc = edesc.Text.Trim();
        //string material = ematerial.Text.Trim();
        //string profile = eprofile.Text.Trim();
        //int weight = Convert.ToInt32(eweight.Text.Trim());
        //string supplier = esupplier.Text.Trim();
        //string category = ecategory.Text.Trim();
        //string comments = ecomments.Text.Trim();

        //bool IsExit = mg_BomBLL.CheckBomByName(2, id, name);
        //if (IsExit)
        //{
        //    bool flag = mg_BomBLL.UpdateBomByName(id, name, no, level, desc, material, profile, weight, supplier, category, comments,pic);
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
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('保存失败！');</script>");
        //        return;
        //    }
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('步骤名称重复，保存失败！');</script>");
        //    return;
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //BindData();
    }

}