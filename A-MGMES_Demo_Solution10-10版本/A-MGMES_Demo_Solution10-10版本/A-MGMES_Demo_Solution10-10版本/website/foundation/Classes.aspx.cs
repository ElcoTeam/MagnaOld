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

/// <summary>
/// 班次维护
/// lx 2017-06-23
/// </summary>
public partial class foundation_Classes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void BindData()
    {
        //BindStationID();

        #region 绑定数据时发现超过两条则新增班次不可用-目前在保存时判断，暂时注销
        //DataTable dt = mg_ClassesBLL.GetAllData();
        //if (dt.Rows.Count >= 2)
        //{
        //    BtSave.Enabled = false;
        //}
        //else
        //{
        //    BtSave.Enabled = true;
        //} 
        #endregion

        #region 格式化结果集
        DataTable dtClass = mg_ClassesBLL.GetAllData();
        DataTable dtRes = new DataTable();
        dtRes.Columns.Add("cl_id", typeof(int));
        dtRes.Columns.Add("cl_name", typeof(string));
        dtRes.Columns.Add("cl_starttime", typeof(string));
        dtRes.Columns.Add("cl_endtime", typeof(string));

        for (int i = 0; i < dtClass.Rows.Count; i++)
        {
            DataRow dr_res = dtRes.NewRow();
            dr_res["cl_id"] = dtClass.Rows[i]["cl_id"];
            dr_res["cl_name"] = dtClass.Rows[i]["cl_name"];

            string s_time = Convert.ToDateTime(dtClass.Rows[i]["cl_starttime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["cl_starttime"] = s_time;

            string e_time = Convert.ToDateTime(dtClass.Rows[i]["cl_endtime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["cl_endtime"] = e_time;

            dtRes.Rows.Add(dr_res);
        } 
        #endregion

        this.GridView1.DataSource = dtRes;
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

    /// <summary>
    /// 验证控件中参数
    /// </summary>
    /// <returns></returns>
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
        if (FormatHelper.IsDateTime(this.txt_starttime.Text.Trim()) == false)
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
        if (FormatHelper.IsDateTime(this.txt_endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间格式不正确！');</script>");
            this.txt_endtime.Focus();
            return false;
        }
        return true;
    }

    /// <summary>
    /// 新增班次-保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtSave_Click(object sender, EventArgs e)
    {
        #region 只能排两班，新增则提示无法保存
        if (GridView1.Rows.Count >= 2)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "无法保存", "<script language='javascript'> alert('已有两条排班信息，如需改动，请删除一条再保存！');</script>");
            return;
        } 
        #endregion

        if (CkText_Box())
        {
            #region 新增排班时间与已有排班时间不能交叉

            DateTime s_time_add = Convert.ToDateTime(this.txt_starttime.Text.Trim());
            DateTime e_time_add = Convert.ToDateTime(this.txt_endtime.Text.Trim());

            DataTable dt = mg_ClassesBLL.GetAllData();
            DateTime s_time_gv = Convert.ToDateTime(dt.Rows[0]["cl_starttime"].ToString());
            DateTime e_time_gv = Convert.ToDateTime(dt.Rows[0]["cl_endtime"].ToString());

            if((s_time_add >= s_time_gv && s_time_add <= e_time_gv) || (e_time_add >= s_time_gv && e_time_add <= e_time_gv))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "时间交叉", "<script language='javascript'> alert('班次时间不能与已有班次时间重叠，保存失败！');</script>");
                return;
            }

            #endregion

            string classesname = this.txt_name.Text.Trim();
            string starttime = Convert.ToDateTime(this.txt_starttime.Text).ToString("yyyy-MM-dd HH:mm:ss");
            string endtime = Convert.ToDateTime(this.txt_endtime.Text).ToString("yyyy-MM-dd HH:mm:ss");
            

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

    /// <summary>
    /// 点击编辑按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
        this.GridView1.EditIndex = row.RowIndex;
        BindData();

        #region 编辑时改变日期格式，否则无法保存
        TextBox aa = (TextBox)GridView1.Rows[row.RowIndex].FindControl("TextBox2");
        string starttime = aa.Text.Trim();
        aa.Text = Convert.ToDateTime(starttime).ToString("yyyy-MM-dd HH:mm:ss");
        TextBox bb = (TextBox)GridView1.Rows[row.RowIndex].FindControl("TextBox3");
        string endtime = aa.Text.Trim();
        aa.Text = Convert.ToDateTime(endtime).ToString("yyyy-MM-dd HH:mm:ss"); 
        #endregion
    }

    /// <summary>
    /// 点击删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// 编辑后保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        #region 参数判断
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
        if (FormatHelper.IsDateTime(starttime.Text.Trim()) == false)
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
        if (FormatHelper.IsDateTime(endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "下班时间", "<script language='javascript'> alert('下班时间格式不正确！');</script>");
            endtime.Focus();
            return;
        } 
        #endregion

        #region 更新后的排班时间与已有排班时间不能交叉

        DateTime s_time_add = Convert.ToDateTime(starttime.Text.Trim());
        DateTime e_time_add = Convert.ToDateTime(endtime.Text.Trim());

        DataTable dt = mg_ClassesBLL.GetAllData();
        DateTime s_time_gv = Convert.ToDateTime(dt.Rows[0]["cl_starttime"].ToString());
        DateTime e_time_gv = Convert.ToDateTime(dt.Rows[0]["cl_endtime"].ToString());

        if ((s_time_add >= s_time_gv && s_time_add <= e_time_gv) || (e_time_add >= s_time_gv && e_time_add <= e_time_gv))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "时间交叉", "<script language='javascript'> alert('班次时间不能与已有班次时间重叠，保存失败！');</script>");
            return;
        }

        #endregion

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

    /// <summary>
    /// 取消编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }

}