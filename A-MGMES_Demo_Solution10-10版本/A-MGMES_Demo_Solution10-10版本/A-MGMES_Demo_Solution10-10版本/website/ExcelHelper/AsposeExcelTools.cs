using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using Aspose.Cells;
namespace website
{
    public class AsposeExcelTools
    {
        public static int EXCEL13_MaxRow = 800000;

        public static bool DataTableToExcel(DataTable datatable, string filepath, out string error)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
                Aspose.Cells.Cells cells = sheet.Cells;

                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {                           
                            if (row[i].GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                //------插入图片数据-------
                                System.Drawing.Image image = (System.Drawing.Image)row[i];
                                MemoryStream mstream = new MemoryStream();
                                image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                sheet.Pictures.Add(nRow, i, mstream);
                            }
                            else
                            {
                                cells[nRow, i].PutValue(row[i]);
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                workbook.Save(filepath);
                return true;
            }
            catch (System.Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }


        public static bool DataTableToExcel2_sheet0(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();

            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Aspose.Cells.Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
               // style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    DataColumn col = datatable.Columns[i];
                    string columnName = col.Caption ?? col.ColumnName;
                    wb.Worksheets[0].Cells[rowIndex, i].PutValue(columnName);
                    wb.Worksheets[0].Cells[rowIndex, i].SetStyle(style);
                }
                rowIndex++;

                foreach (DataRow row in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        wb.Worksheets[0].Cells[rowIndex, i].PutValue(row[i].ToString());
                    }
                    rowIndex++;
                }

                for (int k = 0; k < datatable.Columns.Count; k++)
                {
                    wb.Worksheets[0].AutoFitColumn(k, 0, 150);
                }
                wb.Worksheets[0].FreezePanes(1, 0, 1, datatable.Columns.Count);
                wb.Save(filepath);
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }

        }

        public static bool DataTableToExcel2_test(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            wb.Worksheets.Clear();
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Aspose.Cells.Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                //style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                //新建立sheet页
                int count = datatable.Rows.Count;
                int totalNum = datatable.Rows.Count / EXCEL13_MaxRow + 1;//总共生成的sheet页
                string sheetName = "";
                int beginIndex = 0;
                int endIndex = 0;
                for (int n = 0; n < totalNum; n++)
                {
                    sheetName = "sheet" + n.ToString();
                    wb.Worksheets.Add(sheetName);
                    rowIndex = 0;
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        DataColumn col = datatable.Columns[i];
                        string columnName = col.Caption ?? col.ColumnName;
                        wb.Worksheets[n].Cells[rowIndex, i].PutValue(columnName);
                        wb.Worksheets[n].Cells[rowIndex, i].SetStyle(style);
                    }
                    rowIndex++;
                    beginIndex = n * EXCEL13_MaxRow;
                    endIndex = beginIndex + EXCEL13_MaxRow;
                    if (endIndex >= count)
                    {
                        endIndex = count;
                    }
                    int rownum = endIndex - beginIndex;
                    if (beginIndex < count && endIndex <= count)
                    {

                        wb.Worksheets[n].Cells.ImportDataTable(datatable,true,beginIndex,0,rownum,datatable.Columns.Count,false);
                        //for (int j = beginIndex; j < endIndex; j++)
                        //{
                        //    DataRow row = datatable.Rows[j];
                        //    for (int i = 0; i < datatable.Columns.Count; i++)
                        //    {
                        //        wb.Worksheets[n].Cells[rowIndex, i].PutValue(row[i].ToString());
                        //    }
                        //    rowIndex++;
                        //}
                    }

                    //for (int k = 0; k < datatable.Columns.Count; k++)
                    //{
                    //    wb.Worksheets[n].AutoFitColumn(k, 0, 150);
                    //}
                    //wb.Worksheets[n].FreezePanes(1, 0, 1, datatable.Columns.Count);
                }
                //创建文件
                FileStream file = new FileStream(filepath, FileMode.Create);

                //关闭释放流，不然没办法写入数据
                file.Close();
                file.Dispose();
               
                wb.Save(filepath);
                wb = null;

                return true;
            }
            catch (Exception e)
            {
                for (int i = 0; i < datatable.Rows.Count / EXCEL13_MaxRow + 1; i++)
                {
                    wb.Worksheets[i].Cells.Clear();
                }
                wb.Worksheets.Clear();
                wb = null;
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }

        }

        public static bool DataTableToExcel2(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook();
            wb.Worksheets.Clear();
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Aspose.Cells.Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
                //style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                //新建立sheet页
                int count = datatable.Rows.Count;
                int totalNum = datatable.Rows.Count / EXCEL13_MaxRow + 1;//总共生成的sheet页
               string sheetName = "";
               int beginIndex = 0;
               int endIndex = 0;
               for (int n = 0; n < totalNum; n++)
               {
                   sheetName = "sheet" + n.ToString();
                   wb.Worksheets.Add(sheetName);
                   rowIndex = 0;
                   for (int i = 0; i < datatable.Columns.Count; i++)
                   {
                       DataColumn col = datatable.Columns[i];
                       string columnName = col.Caption ?? col.ColumnName;
                       wb.Worksheets[n].Cells[rowIndex, i].PutValue(columnName);
                       wb.Worksheets[n].Cells[rowIndex, i].SetStyle(style);
                   }
                   rowIndex++;
                   beginIndex = n * EXCEL13_MaxRow;
                   endIndex = beginIndex + EXCEL13_MaxRow;
                   if(endIndex >=count)
                   {
                       endIndex = count;
                   }
                   if (beginIndex < count && endIndex <= count)
                   {
                       for (int j =beginIndex; j <endIndex; j++)
                       {
                           DataRow row = datatable.Rows[j];
                           for (int i = 0; i < datatable.Columns.Count; i++)
                           {
                               wb.Worksheets[n].Cells[rowIndex, i].PutValue(row[i].ToString());
                           }
                           rowIndex++;
                       }
                   }

                   for (int k = 0; k < datatable.Columns.Count; k++)
                   {
                       wb.Worksheets[n].AutoFitColumn(k, 0, 150);
                   }
                   wb.Worksheets[n].FreezePanes(1, 0, 1, datatable.Columns.Count);
               }
               //创建文件
               FileStream file = new FileStream(filepath, FileMode.Create);

               //关闭释放流，不然没办法写入数据
               file.Close();
               file.Dispose();
               wb.Save(filepath);
                wb = null;
              
                return true;
            }
            catch (Exception e)
            {
                for (int i = 0; i < datatable.Rows.Count / EXCEL13_MaxRow + 1; i++)
                {
                    wb.Worksheets[i].Cells.Clear();
                }
                wb.Worksheets.Clear();
                wb = null;
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }

        }

        /// <summary>
        /// Excel文件转换为DataTable.
        /// </summary>
        /// <param name="filepath">Excel文件的全路径</param>
        /// <param name="datatable">DataTable:返回值</param>
        /// <param name="error">错误信息:返回错误信息，没有错误返回""</param>
        /// <returns>true:函数正确执行 false:函数执行错误</returns>
        public static bool ExcelFileToDataTable(string filepath, out DataTable datatable, out string error)
        {
            error = "";
            datatable = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatable = null;
                    return false;
                }
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(filepath);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1);
          
               
                return true;
            }
            catch (System.Exception e)
            {
                error = e.Message;
                return false;
            }

        }



 

      
    }
}