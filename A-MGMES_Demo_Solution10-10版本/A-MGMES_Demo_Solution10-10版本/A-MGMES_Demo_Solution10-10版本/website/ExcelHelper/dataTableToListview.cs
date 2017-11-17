using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using System.Web;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;

namespace website.excel
{
    public class dataTableToListview
    {
        static public ListView dataTableToListview1(ListView lv, DataTable dt)
        {
            if (dt != null)
            {
                lv.Items.Clear();
                lv.Columns.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    lv.Columns.Add(dt.Columns[i].Caption.ToString());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();


                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }


                    lv.Items.Add(lvi);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            return lv;
        }



        static public void ReportToExcel(ListView list, List<int> ColumnWidth, string ReportTitleName)
        {
            //获取用户选择的excel文件名称
            string path;
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "Excel files(*.xls)|*.xls";

            //获取保存路径
            path = HttpContext.Current.Request.MapPath("~/App_Data/excel2776.xlsx");
            Workbook wb = new Workbook();
            Worksheet ws = wb.Worksheets[0];
            Cells cell = ws.Cells;
            //定义并获取导出的数据源
            string[,] _ReportDt = new string[list.Items.Count, list.Columns.Count];
            for (int i = 0; i < list.Items.Count; i++)
            {
                for (int j = 0; j < list.Columns.Count; j++)
                {
                    _ReportDt[i, j] = list.Items[i].SubItems[j].Text.ToString();
                }
            }
            //合并第一行单元格
            Range range = cell.CreateRange(0, 0, 1, list.Columns.Count);
            range.Merge();
            cell["A1"].PutValue(ReportTitleName); //标题

            //设置行高
            cell.SetRowHeight(0, 20);

            //设置字体样式
            Style style1 = wb.Styles[wb.Styles.Add()];
            style1.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style1.Font.Name = "宋体";
            style1.Font.IsBold = true;//设置粗体
            style1.Font.Size = 12;//设置字体大小

            Style style2 = wb.Styles[wb.Styles.Add()];
            style2.HorizontalAlignment = TextAlignmentType.Center;
            style2.Font.Size = 10;

            //给单元格关联样式
            cell["A1"].SetStyle(style1); //报表名字 样式

            //设置Execl列名
            for (int i = 0; i < list.Columns.Count; i++)
            {
                cell[1, i].PutValue(list.Columns[i].Text);
                cell[1, i].SetStyle(style2);
            }

            //设置单元格内容
            int posStart = 2;
            for (int i = 0; i < list.Items.Count; i++)
            {
                for (int j = 0; j < list.Columns.Count; j++)
                {
                    cell[i + posStart, j].PutValue(_ReportDt[i, j].ToString());
                    cell[i + posStart, j].SetStyle(style2);
                }
            }

            //设置列宽
            for (int i = 0; i < list.Columns.Count; i++)
            {
                cell.SetColumnWidth(i, Convert.ToDouble(ColumnWidth[i].ToString()));
            }
            //保存excel表格
            wb.Save(path);

        }
    }
}