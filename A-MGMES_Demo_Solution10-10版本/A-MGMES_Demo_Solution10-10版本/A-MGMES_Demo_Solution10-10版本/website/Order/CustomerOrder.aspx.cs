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
using DbUtility;
using Microsoft.SqlServer.Server;
using Tools;

public partial class Order_CustomerOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    DDBind(txt_info);
        //    BindData();
        //}
    }

//    private void BindData()
//    {
//        this.GridView1.DataSource = mg_CustomerOrderBLL.GetAllData("");
//        this.GridView1.DataBind();
//        this.GridView1.SelectedIndex = -1;
//        this.txt_no.Text = "";
//        this.txt_no.Text = "";
//        this.txt_info.Text = "";
//        this.ddlcustom.Text ="";
//        txt_info.SelectedIndex = -1;
//    }

//    private void DDBind(DropDownList dr)
//    {
//        dr.Items.Clear();
//        dr.Items.Add("");
//        string sql = @"select [all_no] from [mg_allpart] order by all_id asc";
//        DataTable tb = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

//        if (tb.Rows.Count != 0)
//        {
//            for (int i = 0; i < tb.Rows.Count; i++)
//            {
//                dr.Items.Add(tb.Rows[i]["all_no"].ToString());
//            }
//        }
//        tb.Dispose();
//    }

//    private  int  getID(string no)
//    {
//        int ID = 0;

//        DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, "select [all_id] from mg_allpart where all_no='" + no + "'", null);
//        if (dt.Rows.Count > 0)
//        {
//            foreach (DataRow dr in dt.Rows)
//            {
//                ID = NumericParse.StringToInt(dr["all_id"].ToString());
//            }
//        }
//        else
//        {
//            ID = 0;
//        }
//        dt.Dispose();
//        return ID;
//    }

//    protected void Button1_Click(object sender, EventArgs e)
//    {
//        string sql = "",partno="",parttype="",or_no="",partSql="";
//        bool orFlag;
//        int insertInfo;
//        #region
//        if (this.txt_no.Text == "")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('订单号不能为空！');</script>");
//            this.txt_no.Focus();
//            return;
//        }

//        if (FormatHelper.CheckPunctuation(this.txt_no.Text.Trim()) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('订单号不能包含单引号！');</script>");
//            this.txt_no.Focus();
//            return;
//        }

//        if (this.txt_info.SelectedValue == "")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅型号不能为空！');</script>");
//            this.txt_info.Focus();
//            return;
//        }

//        if (FormatHelper.CheckPunctuation(this.txt_info.SelectedValue.Trim()) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅型号不能包含单引号！');</script>");
//            this.txt_info.Focus();
//            return;
//        }

//        if (txt_count.Text.Trim() == "")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('数量不能为空！');</script>");
//            this.txt_count.Focus();
//            return;
//        }
//        if (FormatHelper.CheckPunctuation(this.txt_count.Text.Trim()) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('数量不能包含单引号！');</script>");
//            this.txt_count.Focus();
//            return;
//        }

//        if (FormatHelper.IsInteger(txt_count.Text.Trim()) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('数量必须为整数！');</script>");
//            this.txt_count.Focus();
//            return;
//        }

//        if (!string.IsNullOrEmpty(this.ddlcustom.Text.Trim()))
//        {
//            if (FormatHelper.CheckPunctuation(this.ddlcustom.Text.Trim()) == false)
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('客户不能包含单引号！');</script>");
//                this.ddlcustom.Focus();
//                return;
//            }
//        }
//        #endregion


//        bool flag = mg_CustomerOrderBLL.CheckdOrder(1, 0, this.txt_no.Text);
//        if (flag)
//        {
//            bool inflag = mg_CustomerOrderBLL.AdddOrder(txt_no.Text.Trim(), getID(txt_info.SelectedValue), NumericParse.StringToInt(txt_count.Text.Trim()), ddlcustom.Text.Trim());
//            if (inflag)
//            {
                
//                or_no = CreateNo();
//                orFlag = mg_orderBLL.Addor(txt_no.Text.Trim(),or_no, txt_info.SelectedValue,
//                    NumericParse.StringToInt(txt_count.Text.Trim()));
//                #region   生成生产通知单，并进行拆分
//                sql = @"SELECT   dbo.mg_allpart.all_no, dbo.mg_part.part_no, dbo.mg_part.part_tpye, dbo.mg_part.part_name FROM  dbo.mg_allpart_part_rel INNER JOIN
//                dbo.mg_part ON dbo.mg_allpart_part_rel.partid_id = dbo.mg_part.part_id INNER JOIN
//                dbo.mg_allpart ON dbo.mg_allpart_part_rel.all_id = dbo.mg_allpart.all_id where dbo.mg_allpart.all_no='" + txt_info.SelectedValue + "'";
//                DataTable dtpart = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text,sql, null);
//                if (dtpart.Rows.Count > 0)
//                {
//                    partSql = "";
//                    for (int i = 0; i < NumericParse.StringToInt(txt_count.Text.Trim()); i++)
//                    {
//                        foreach (DataRow row in dtpart.Rows)
//                        {
//                            partno = row["part_no"].ToString();
//                            parttype = row["part_tpye"].ToString();
//                            if (parttype == "FSL" )
//                            {
//                                partSql += @"insert into [mg_Order_FS] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','" + parttype + "','" + txt_no.Text.Trim() + @"')";
//                                partSql += @"insert into [mg_Order_FSB] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','FSBL','" + txt_no.Text.Trim() + @"')";
//                                partSql += @"insert into [mg_Order_FSC] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','FSCL','" + txt_no.Text.Trim() + @"')";
//                            }
//                            if (parttype == "FSR")
//                            {
//                                partSql += @"insert into [mg_Order_FS] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','" + parttype + "','" + txt_no.Text.Trim() + @"')";
//                                partSql += @"insert into [mg_Order_FSB] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','FSBR','" + txt_no.Text.Trim() + @"')";
//                                partSql += @"insert into [mg_Order_FSC] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','FSCR','" + txt_no.Text.Trim() + @"')";
//                            }
//                            if (parttype == "RSB40" || parttype == "RSB60")
//                            {
//                                partSql += @"insert into [mg_Order_RSB] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','" + parttype + "','" + txt_no.Text.Trim() + @"')";
//                            }
//                            if (parttype == "RSC" )
//                            {
//                                partSql += @"insert into [mg_Order_RSC] ([or_no],[part_no],[part_type],[co_no]) values ('" + or_no + "','" + partno + "','" + parttype + "','" + txt_no.Text.Trim() + @"')";
//                            }
//                        }
//                    }
//                    if (!string.IsNullOrEmpty(partSql))
//                    {
//                        insertInfo = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, partSql, null);
//                    }
//                }
//                #endregion
//                BindData();
//            }
//            else
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称",
//                    "<script language='javascript'> alert('保存失败！');</script>");
//                return;
//            }
//        }
//        else
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称",
//                    "<script language='javascript'> alert('订单号重复，保存失败！');</script>");
//            return;
//        }
//    }

//    /// <summary>
//    /// 生产生产通知单NO
//    /// </summary>
//    private string CreateNo()
//    {
//        string or_no = "";
//        string year = DateTime.Now.Year.ToString();
//        string month = NumericParse.StringToInt(DateTime.Now.Month.ToString()) > 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
//        string day = NumericParse.StringToInt(DateTime.Now.Day.ToString()) > 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString();
//        string date = year + month + day;
//        string sql = "select top 1 [or_no] from [mg_Order] where or_no like '" + date + "%' order by [or_id] desc";
//        DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);
//        if (dt.Rows.Count > 0)
//        {
//            foreach (DataRow row in dt.Rows)
//            {
//                or_no = (NumericParse.StringToInt(row["or_no"].ToString().Substring(8, 4))+1).ToString();
//            }
//        }
//        else
//        {
//            or_no = date + "0001";
//        }
//        return or_no;
//    }

//    protected void BtEdit_Click(object sender, ImageClickEventArgs e)
//    {
//        ImageButton imgBtn = sender as ImageButton;
//        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
//        this.GridView1.EditIndex = row.RowIndex;


//        Label lbinfo = (Label)GridView1.Rows[GridView1.EditIndex].FindControl("lbinfo");
//        string oldinfo = lbinfo.Text;
//        BindData();
//        DropDownList drinfo = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList1");
//        DDBind(drinfo);
//        drinfo.SelectedValue = oldinfo;
//    }
//    protected void BtSave_Click(object sender, ImageClickEventArgs e)
//    {
//        Label id = (Label) GridView1.Rows[GridView1.EditIndex].FindControl("elbID");
//        int coid = NumericParse.StringToInt(id.Text);
//        TextBox tbno = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("tbno");
//        string cono = tbno.Text;
//        DropDownList tbinfo = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("DropDownList1");
//        string coinfo = tbinfo.SelectedValue;
//        TextBox tbcount = (TextBox) GridView1.Rows[GridView1.EditIndex].FindControl("TextBox1");
//        string cocount =tbcount.Text;
//        TextBox tbcustom = (TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("edrcustomer");
//        string cocustom = tbcustom.Text;
//        //DropDownList ddl = (DropDownList)GridView1.Rows[GridView1.EditIndex].FindControl("edrcustomer");
//        //string cocustomer = ddl.SelectedValue;

//        #region
//        if (cono == "")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('订单号不能为空！');</script>");
//            tbno.Focus();
//            return;
//        }

//        if (FormatHelper.CheckPunctuation(cono) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('订单号不能包含单引号！');</script>");
//            tbno.Focus();
//            return;
//        }

//        if (coinfo == "")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('座椅类型不能为空！');</script>");
//            tbinfo.Focus();
//            return;
//        }

//        if (FormatHelper.IsInteger(cocount) == false)
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('数量必须为整数！');</script>");
//            tbinfo.Focus();
//            return;
//        }

//        if (!string.IsNullOrEmpty(cocustom))
//        {
//            if (FormatHelper.CheckPunctuation(cocustom) == false)
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('客户不能包含单引号！');</script>");
//                tbcustom.Focus();
//                return;
//            }
//        }
//        #endregion

//        bool flag = mg_CustomerOrderBLL.CheckdOrder(2, coid, cono);
//        if (flag)
//        {
//            bool upflag = mg_CustomerOrderBLL.UpdateOrder(coid, cono, getID(coinfo), NumericParse.StringToInt(cocount), cocustom);
//            if (upflag)
//            {
//                GridView1.EditIndex = -1;
//                BindData();
//            }
//            else
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称",
//                    "<script language='javascript'> alert('保存失败！');</script>");
//                return;
//            }
//        }
//        else
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称",
//                    "<script language='javascript'> alert('订单号重复，保存失败！');</script>");
//            return;
//        }
//    }
//    protected void BtCancel_Click(object sender, ImageClickEventArgs e)
//    {
//        GridView1.EditIndex = -1;
//        BindData();
//    }
//    protected void BtDel_Click(object sender, ImageClickEventArgs e)
//    {
//        ImageButton imgBtn = sender as ImageButton;
//        GridViewRow row = imgBtn.Parent.Parent as GridViewRow;
//        this.GridView1.EditIndex = row.RowIndex;
//        Label aa = (Label)GridView1.Rows[row.RowIndex].FindControl("lbID");
//        int id = NumericParse.StringToInt(aa.Text);
//        Label bb = (Label) GridView1.Rows[row.RowIndex].FindControl("lbstate");
//        string state = bb.Text;
//        if (state == "已生成")
//        {
//            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('订单已下达，不能删除！');</script>");
//            return;
//        }
//        else
//        {
//            bool flag = mg_CustomerOrderBLL.DelOrder(id);
//            if (flag)
//            {
//                GridView1.EditIndex = -1;
//                BindData();
//            }
//            else
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script language='javascript'> alert('删除失败！');</script>");
//                return;
//            }
//        }
//    }
//    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
//    {
//        GridView1.PageIndex = e.NewPageIndex;
//        BindData();
//    }
}