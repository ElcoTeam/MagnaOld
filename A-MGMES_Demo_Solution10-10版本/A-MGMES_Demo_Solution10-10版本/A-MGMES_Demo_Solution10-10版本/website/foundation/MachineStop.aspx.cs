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
/// 设备停机记录
/// lx 2017-07-07
/// </summary>
public partial class foundation_MachineStop : System.Web.UI.Page
{
    DataTable dt_st_no = new DataTable();
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
        #region 绑定工位列表
        BindStationNO(); 
        #endregion

        #region 格式化结果集
        DataTable dt = mg_Report_MachineStopBLL.GetAllData(); 
        DataTable dtRes = new DataTable();
        dtRes.Columns.Add("orderNO", typeof(string));
        dtRes.Columns.Add("machineStop_id", typeof(string));
        dtRes.Columns.Add("st_no", typeof(string));
        dtRes.Columns.Add("machineStop_reason", typeof(string));
        dtRes.Columns.Add("start_time", typeof(string));
        dtRes.Columns.Add("end_time", typeof(string));
        dtRes.Columns.Add("memo", typeof(string));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr_res = dtRes.NewRow();
            dr_res["orderNO"] = dt.Rows[i]["orderNO"];
            dr_res["machineStop_id"] = dt.Rows[i]["machineStop_id"];
            dr_res["st_no"] = dt.Rows[i]["st_no"];
            dr_res["machineStop_reason"] = dt.Rows[i]["machineStop_reason"];
            dr_res["memo"] = dt.Rows[i]["memo"];

            string s_time = Convert.ToDateTime(dt.Rows[i]["start_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["start_time"] = s_time;

            string e_time = Convert.ToDateTime(dt.Rows[i]["end_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["end_time"] = e_time;

            dtRes.Rows.Add(dr_res);
        } 
        #endregion

        this.GridView1.DataSource = dtRes;
        this.GridView1.DataBind();
        this.GridView1.SelectedIndex = -1;
        this.ddl_st_no.SelectedIndex = -1;
        this.txt_reason.Text = "";
        this.txt_starttime.Text = "";
        this.txt_endtime.Text = "";
        this.txt_memo.Text = "";
        
    }

    /// <summary>
    /// 绑定工位下拉列表
    /// </summary>
    private void BindStationNO()
    {
        dt_st_no = mg_StationBLL.GetStationNO();

        DataTable res = dt_st_no.Clone();
        DataRow dr = res.NewRow();
        res.Rows.Add(dr);

        for (int i = 0; i < dt_st_no.Rows.Count; i++)
        {
            for (int j = 0; j < dt_st_no.Columns.Count; j++)
            {
                DataRow drr = res.NewRow();
                drr[j] = dt_st_no.Rows[i][j].ToString();
                res.Rows.Add(drr);
            }
        }

        this.ddl_st_no.DataSource = res;
        this.ddl_st_no.DataValueField = "st_no";
        this.ddl_st_no.DataBind();

    }

    /// <summary>
    /// 验证控件中参数
    /// </summary>
    /// <returns></returns>
    protected bool CkText_Box()
    {
        if (string.IsNullOrWhiteSpace(this.ddl_st_no.Text.Trim()))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('请选择工站！');</script>");
            this.ddl_st_no.Focus();
            return false;
        }

        if (this.txt_starttime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机开始时间", "<script language='javascript'> alert('停机开始时间不能为空！');</script>");
            this.txt_starttime.Focus();
            return false;
        }
        if (FormatHelper.IsDateTime(this.txt_starttime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机开始时间", "<script language='javascript'> alert('停机开始时间格式不正确！');</script>");
            this.txt_starttime.Focus();
            return false;
        }

        if (this.txt_endtime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机结束时间", "<script language='javascript'> alert('停机结束时间不能为空！');</script>");
            this.txt_endtime.Focus();
            return false;
        }
        if (FormatHelper.IsDateTime(this.txt_endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机结束时间", "<script language='javascript'> alert('停机结束时间格式不正确！');</script>");
            this.txt_endtime.Focus();
            return false;
        }
        return true;
    }

    /// <summary>
    /// 新增-保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtSave_Click(object sender, EventArgs e)
    {

        if (CkText_Box())
        {
            string st_no = this.ddl_st_no.Text.Trim();
            string reason = this.txt_reason.Text.Trim();
            string memo = this.txt_memo.Text.Trim();
            string starttime = Convert.ToDateTime(this.txt_starttime.Text).ToString("yyyy-MM-dd HH:mm:ss");
            string endtime = Convert.ToDateTime(this.txt_endtime.Text).ToString("yyyy-MM-dd HH:mm:ss");

            bool flag = mg_Report_MachineStopBLL.AddMachineStop(st_no, reason, starttime, endtime, memo);
            if (flag)
            {
                BindData();
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
        //TextBox aa = (TextBox)GridView1.Rows[row.RowIndex].FindControl("TextBox2");
        //string starttime = aa.Text.Trim();
        //aa.Text = Convert.ToDateTime(starttime).ToString("yyyy-MM-dd HH:mm:ss");
        //TextBox bb = (TextBox)GridView1.Rows[row.RowIndex].FindControl("TextBox3");
        //string endtime = aa.Text.Trim();
        //aa.Text = Convert.ToDateTime(endtime).ToString("yyyy-MM-dd HH:mm:ss"); 
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
        Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("lab_id");
        string ms_id = aa.Text.ToString();
        bool flag = mg_Report_MachineStopBLL.DelByID(ms_id);
        if (flag)
        {
            GridView1.EditIndex = -1;
            BindData();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "错误", "<script language='javascript'> alert('删除失败！');</script>");
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
        DropDownList control_st_no = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("gv_st_no");
        string st_no = control_st_no.Text.Trim();
        TextBox control_reason = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_reason");
        string reason = control_reason.Text.Trim();
        TextBox control_starttime = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_starttime");
        string stime = control_starttime.Text.Trim();
        TextBox control_endtime = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_endtime");
        string etime = control_endtime.Text.Trim();
        TextBox control_memo = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txt_memo");
        string memo = control_memo.Text.Trim();
        Label control_id = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lab_id");
        string id = control_id.Text.Trim();


        #region 参数判断
        if (string.IsNullOrWhiteSpace(st_no))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "工站", "<script language='javascript'> alert('工站不能为空！');</script>");
            control_st_no.Focus();
            return;
        }

        if (control_starttime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机开始时间", "<script language='javascript'> alert('停机开始时间不能为空！');</script>");
            control_starttime.Focus();
            return;
        }
        if (FormatHelper.IsDateTime(control_starttime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机开始时间", "<script language='javascript'> alert('停机开始时间格式不正确！');</script>");
            control_starttime.Focus();
            return;
        }

        if (control_endtime.Text.Trim() == "")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机结束时间", "<script language='javascript'> alert('停机结束时间不能为空！');</script>");
            control_endtime.Focus();
            return;
        }
        if (FormatHelper.IsDateTime(control_endtime.Text.Trim()) == false)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "停机结束时间", "<script language='javascript'> alert('停机结束时间格式不正确！');</script>");
            control_endtime.Focus();
            return;
        }
        #endregion

        bool flag = mg_Report_MachineStopBLL.UpdateByID(id,st_no,reason,stime,etime,memo);
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

    /// <summary>
    /// 绑定GridView中下拉列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString().Equals("Alternate, Edit"))
        {
            DropDownList ddl = e.Row.FindControl("gv_st_no") as DropDownList;
            if (ddl != null)
            {
                ddl.DataSource = dt_st_no;
                ddl.DataValueField = "st_no";
                ddl.DataBind();
            }
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        #region 判断参数
        string st_no = null;
        if (!string.IsNullOrWhiteSpace(ddl_st_no.SelectedValue.Trim()))
        {
            st_no = ddl_st_no.SelectedValue.Trim();
        }

        string start_time = null;
        if (!string.IsNullOrWhiteSpace(txt_starttime.Text.Trim()))
        {
            start_time = txt_starttime.Text.Trim();
        }

        string end_time = null;
        if (!string.IsNullOrWhiteSpace(txt_endtime.Text.Trim()))
        {
            end_time = txt_endtime.Text.Trim();
        }

        string reason = null;
        if (!string.IsNullOrWhiteSpace(txt_reason.Text.Trim()))
        {
            reason = txt_reason.Text.Trim();
        }

        string memo = null;
        if (!string.IsNullOrWhiteSpace(txt_memo.Text.Trim()))
        {
            memo = txt_memo.Text.Trim();
        }
        
        if ((string.IsNullOrWhiteSpace(start_time) && !string.IsNullOrWhiteSpace(end_time)) 
            || (!string.IsNullOrWhiteSpace(start_time) && string.IsNullOrWhiteSpace(end_time)))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "错误", "<script language='javascript'> alert('请填写开始日期与结束日期！');</script>");
            return;
        }
        #endregion

        #region 格式化结果集
        DataTable dt = mg_Report_MachineStopBLL.GetDataByCondition(st_no, start_time, end_time, reason, memo);
        DataTable dtRes = new DataTable();
        dtRes.Columns.Add("orderNO", typeof(string));
        dtRes.Columns.Add("machineStop_id", typeof(string));
        dtRes.Columns.Add("st_no", typeof(string));
        dtRes.Columns.Add("machineStop_reason", typeof(string));
        dtRes.Columns.Add("start_time", typeof(string));
        dtRes.Columns.Add("end_time", typeof(string));
        dtRes.Columns.Add("memo", typeof(string));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr_res = dtRes.NewRow();
            dr_res["orderNO"] = dt.Rows[i]["orderNO"];
            dr_res["machineStop_id"] = dt.Rows[i]["machineStop_id"];
            dr_res["st_no"] = dt.Rows[i]["st_no"];
            dr_res["machineStop_reason"] = dt.Rows[i]["machineStop_reason"];
            dr_res["memo"] = dt.Rows[i]["memo"];

            string s_time = Convert.ToDateTime(dt.Rows[i]["start_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["start_time"] = s_time;

            string e_time = Convert.ToDateTime(dt.Rows[i]["end_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            dr_res["end_time"] = e_time;

            dtRes.Rows.Add(dr_res);
        }
        #endregion

        this.GridView1.DataSource = dtRes;
        this.GridView1.DataBind();
        this.GridView1.SelectedIndex = -1;
        this.ddl_st_no.SelectedIndex = -1;
        this.txt_reason.Text = "";
        this.txt_starttime.Text = "";
        this.txt_endtime.Text = "";
        this.txt_memo.Text = "";
    }

}