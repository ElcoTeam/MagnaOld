/*******************************************************************
 * 版权所有： 
 * 类 名 称：ExcelHelper
 * 作    者：zk
 * 电子邮箱：77148918@QQ.com
 * 创建日期：2012/2/25 10:17:21 
 * 修改描述：从excel导入datatable时，可以导入日期类型。
 *           但对excel中的日期类型有一定要求，要求至少是yyyy/mm/dd类型日期；           
 * 修改描述：将datatable导入excel中，对类型为字符串的数字进行处理，
 *           导出数字为double类型；
 * 
 * 
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.SS;
using NPOI.DDF;
using NPOI.SS.Util;
using System.Collections;
using System.Text.RegularExpressions;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;

namespace website
{
    public class ExcelHelper
    {
        public static int EXCEL03_MaxRow = 60000;
        private static WriteLog wl = new WriteLog();
        #region 从datatable中将数据导出到excel
        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream ExportDT(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = null ;

            #region 右击文件 属性信息

            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "http://www.yongfa365.com/";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "柳永法"; //填加xls文件作者信息
            //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
            //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
            //    si.Comments = "说明信息"; //填加xls文件作者信息
            //    si.Title = "NPOI测试"; //填加xls文件标题信息
            //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            
            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            //空表只有表头和列头
            #region 新建表，填充表头，填充列头，样式

            //if (rowIndex % EXCEL03_MaxRow == 0)
            if(rowIndex==0)
            {

                sheet = workbook.CreateSheet() as HSSFSheet;


                #region 表头及样式

                {
                    HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                    headerRow.HeightInPoints = 25;
                    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    HSSFFont font = workbook.CreateFont() as HSSFFont;
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 70;
                    headStyle.SetFont(font);

                    headerRow.GetCell(0).CellStyle = headStyle;
                    sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                    //headerRow.Dispose();
                }
                rowIndex++;
                #endregion


                #region 列头及样式

                {
                    HSSFRow headerRow = sheet.CreateRow(1) as HSSFRow;


                    HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    HSSFFont font = workbook.CreateFont() as HSSFFont;
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);


                    foreach (DataColumn column in dtSource.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                        //设置列宽，这里我多加了10个字符的长度
                        sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 10) * 256);


                    }
                    //headerRow.Dispose();
                }

                #endregion

                rowIndex ++;
            }
            //空表导出完毕
            #endregion
            foreach (DataRow row in dtSource.Rows)
            {
                //#region 新建表，填充表头，填充列头，样式

                //if (rowIndex % EXCEL03_MaxRow == 0)
                //{
                    
                //    sheet = workbook.CreateSheet() as HSSFSheet;
                    

                //    #region 表头及样式

                //    {
                //        HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                //        headerRow.HeightInPoints = 25;
                //        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                //        HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                //        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //        HSSFFont font = workbook.CreateFont() as HSSFFont;
                //        font.FontHeightInPoints = 10;
                //        font.Boldweight = 70;
                //        headStyle.SetFont(font);

                //        headerRow.GetCell(0).CellStyle = headStyle;
                //        sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                //        //headerRow.Dispose();
                //    }

                //    #endregion


                //    #region 列头及样式

                //    {
                //        HSSFRow headerRow = sheet.CreateRow(1) as HSSFRow;


                //        HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                //        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //        HSSFFont font = workbook.CreateFont() as HSSFFont;
                //        font.FontHeightInPoints = 10;
                //        font.Boldweight = 700;
                //        headStyle.SetFont(font);


                //        foreach (DataColumn column in dtSource.Columns)
                //        {
                //            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                //            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                //            //设置列宽，这里我多加了10个字符的长度
                //            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 10) * 256);


                //        }
                //        //headerRow.Dispose();
                //    }

                //    #endregion

                //    rowIndex = 2;
                //}

                //#endregion

                #region 填充内容

                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                    newCell.SetCellType(CellType.String);
                    
                    //新增的四句话，设置CELL格式为文本格式   
                    //HSSFCellStyle dateStyle2 = workbook.CreateCellStyle() as HSSFCellStyle;
                    //HSSFDataFormat format2 = workbook.CreateDataFormat() as HSSFDataFormat;
                    //dateStyle2.DataFormat = format.GetFormat("@");
                    //newCell.CellStyle = dateStyle2;
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            double result;
                            if (isNumeric(drValue, out result))
                            {

                                double.TryParse(drValue, out result);
                                newCell.SetCellValue(drValue);
                                //newCell.SetCellValue(result);
                                break;
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                                break;
                            }

                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle; //格式化显示
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }

                #endregion

                rowIndex++;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;

                    //sheet.Dispose();
                    //workbook.Dispose();
                    workbook.Close();
                    return ms;
                }
            }
            catch(Exception e)
            {
                string s = e.Message;
               using( MemoryStream ms = new MemoryStream(0))
               {

                   return ms;
               }
            }
            finally
            {
                workbook.Close();

            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        static void ExportDTI(DataTable dtSource, string strHeaderText, FileStream fs)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = null;

            #region 右击文件 属性信息

            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "http://www.yongfa365.com/";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "柳永法"; //填加xls文件作者信息
            //    si.ApplicationName = "NPOI测试程序"; //填加xls文件创建程序信息
            //    si.LastAuthor = "柳永法2"; //填加xls文件最后保存者信息
            //    si.Comments = "说明信息"; //填加xls文件作者信息
            //    si.Title = "NPOI测试"; //填加xls文件标题信息
            //    si.Subject = "NPOI测试Demo"; //填加文件主题信息
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            XSSFCellStyle dateStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            XSSFDataFormat format = workbook.CreateDataFormat() as XSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            //以前空表就不导出，增加空表导出
            #region 新建表，填充表头，填充列头，样式
            //if (rowIndex % EXCEL03_MaxRow == 0)
            if(rowIndex==0)
            {
                sheet = workbook.CreateSheet() as XSSFSheet;
                #region 表头及样式
                {
                    XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
                    headerRow.HeightInPoints = 25;
                    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    XSSFFont font = workbook.CreateFont() as XSSFFont;
                    font.FontHeightInPoints = 20;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);

                    headerRow.GetCell(0).CellStyle = headStyle;

                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                    //headerRow.Dispose();
                }
                rowIndex++;
                #endregion


                #region 列头及样式

                {
                    XSSFRow headerRow = sheet.CreateRow(1) as XSSFRow;


                    XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    XSSFFont font = workbook.CreateFont() as XSSFFont;
                    font.FontHeightInPoints = 10;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);


                    foreach (DataColumn column in dtSource.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                        //设置列宽
                        sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                    }
                    //headerRow.Dispose();
                }
                rowIndex++;
            }
                    #endregion
               #endregion
            //空表导出end

          
            foreach (DataRow row in dtSource.Rows)
            {
                //#region 新建表，填充表头，填充列头，样式
                //if (rowIndex % EXCEL03_MaxRow== 0)
                //{
                //    sheet = workbook.CreateSheet() as XSSFSheet;
                //    #region 表头及样式
                //    {
                //        XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
                //        headerRow.HeightInPoints = 25;
                //        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                //        XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                //        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //        XSSFFont font = workbook.CreateFont() as XSSFFont;
                //        font.FontHeightInPoints = 20;
                //        font.Boldweight = 700;
                //        headStyle.SetFont(font);

                //        headerRow.GetCell(0).CellStyle = headStyle;

                //        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                //        //headerRow.Dispose();
                //    }

                //    #endregion


                //    #region 列头及样式

                //    {
                //        XSSFRow headerRow = sheet.CreateRow(1) as XSSFRow;


                //        XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                //        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //        XSSFFont font = workbook.CreateFont() as XSSFFont;
                //        font.FontHeightInPoints = 10;
                //        font.Boldweight = 700;
                //        headStyle.SetFont(font);


                //        foreach (DataColumn column in dtSource.Columns)
                //        {
                //            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                //            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                //            //设置列宽
                //            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                //        }
                //        //headerRow.Dispose();
                //    }

                //    #endregion

                //    rowIndex = 2;
                //}

                //#endregion

                #region 填充内容

                XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    XSSFCell newCell = dataRow.CreateCell(column.Ordinal) as XSSFCell;

                    newCell.SetCellType(CellType.String);

                    ////新增的四句话，设置CELL格式为文本格式   
                    //XSSFCellStyle dateStyle2 = workbook.CreateCellStyle() as XSSFCellStyle;
                    //XSSFDataFormat format2 = workbook.CreateDataFormat() as XSSFDataFormat;
                    //dateStyle2.DataFormat = format.GetFormat("@");
                    //newCell.CellStyle = dateStyle2;
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            double result;
                            if (isNumeric(drValue, out result))
                            {

                                double.TryParse(drValue, out result);
                                newCell.SetCellValue(drValue);
                                //newCell.SetCellValue(result);
                                break;
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                                break;
                            }
                        case "System.float": //空
                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            if (drValue == null || drValue == "" || drValue == "-1")
                            {
                                newCell.SetCellValue(" ");
                            }
                            else
                            {
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                            }
                            newCell.CellStyle = dateStyle; //格式化显示
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":

                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV = 0;
                            if (drValue == null || drValue == "")
                            {
                                newCell.SetCellValue(" ");
                            }
                            else
                            {
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                            }

                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }

                #endregion

                rowIndex++;
            }
            try
            {


                workbook.Write(fs);
                fs.Close();
                workbook.Close();
            }
            catch(Exception e )
            {
                throw new Exception();
                string s = e.Message;
            }
            finally
            {
                fs.Close();
                workbook.Close();
            }
        }
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void ExportDTtoExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            string result = "";
            if (dtSource == null )
            {
                result = "数据源为空";
            }
            else
            {
                string[] temp = strFileName.Split('.');

                if (temp[temp.Length - 1] == "xls" && dtSource.Columns.Count < 256 && dtSource.Rows.Count < 65536)
                {
                    try
                    {
                        using (MemoryStream ms = ExportDT(dtSource, strHeaderText))
                        {
                            using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                            {
                                byte[] data = ms.ToArray();
                                fs.Write(data, 0, data.Length);
                                fs.Flush();
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    if (temp[temp.Length - 1] == "xls")
                        strFileName = strFileName + "x";
                    try
                    {
                        using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                        {
                            ExportDTI(dtSource, strHeaderText, fs);

                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception();
                    }
                   

                }
            }
        }
        #endregion
        #region test
        //test begin
       /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static byte[] ExportDTtoExcelTest(DataTable dtSource, string strHeaderText, string strFileName)
        {
            if (dtSource == null || dtSource.Rows.Count <1) 
            {
                byte[] b = new byte[1];
                 b[0]= 1;
                return b;
            }
            else
            {
               return  DataTable2Excel(dtSource,strFileName);
             }
        }
        public static byte[] ExportDTtoExcelTest(DataTable dtSource, string strHeaderText, string strFileName,int i)
        {
            if (dtSource == null || dtSource.Rows.Count < 1)
            {
                byte[] b = new byte[1];
                b[0] = 1;
                return b;
            }
            else
            {
                IWorkbook book = null;
                string extension = System.IO.Path.GetExtension(strFileName);
                if (extension.Equals(".xls"))
                {
                    //把xls文件中的数据写入wk中
                    book = new HSSFWorkbook();
                }
                else
                {
                    //把xlsx文件中的数据写入wk中
                    book = new XSSFWorkbook();
                }
                
                return DataTable2Excel(dtSource, strFileName,i,book);
            }
        }

        public static byte[] DataTable2Excel(DataTable dt, string strFileName,int inum,IWorkbook book)
        {
            
            
            string sheetName = "sheet"+inum.ToString();
            try
            {
                
                int rowNum = dt.Rows.Count;
                if (rowNum <= EXCEL03_MaxRow)
                    DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
                else
                {
                    int page = rowNum / EXCEL03_MaxRow;
                    if (page * EXCEL03_MaxRow < rowNum)//当总行数不被sheetRows整除时，经过四舍五入可能页数不准
                    {
                        page = page + 1;
                    }
                    for (int i = 0; i < page; i++)
                    {
                        int start = i * EXCEL03_MaxRow;
                        int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                        DataWrite2Sheet(dt, start, end, book, sheetName);
                    }
                    int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                    DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    book.Write(ms);
                    book.Close();
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                string s;
                byte[] b = new byte[1];
                b[0] = 2;
                s = e.Message.ToString();
                return b;
            }
            finally
            {
                book.Close();
            }
        }
        public static byte[] DataTable2Excel(DataTable dt,   string strFileName)
        {     
              IWorkbook book = null;
              string extension = System.IO.Path.GetExtension(strFileName);
              string sheetName = "sheet";
            try
            {
               
                if (extension.Equals(".xls"))
                {
                    //把xls文件中的数据写入wk中
                    book = new HSSFWorkbook();
                }
                else
                {
                    //把xlsx文件中的数据写入wk中
                    book = new XSSFWorkbook();
                }
           int rowNum = dt.Rows.Count;
            if (rowNum < EXCEL03_MaxRow)
                DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
            else
            {
                int page = rowNum / EXCEL03_MaxRow;
                if (page * EXCEL03_MaxRow < rowNum)//当总行数不被sheetRows整除时，经过四舍五入可能页数不准
                {
                    page = page + 1;
                 }
                for (int i = 0; i < page; i++)
                {
                    int start = i * EXCEL03_MaxRow;
                    int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                    DataWrite2Sheet(dt, start, end, book, sheetName + i.ToString());
                }
                int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName + page.ToString());
            }
           using( MemoryStream ms = new MemoryStream())
           {
            book.Write(ms);
            book.Close();
            return ms.ToArray();
           }
            }
            catch(Exception e)
            {
                string s;
                 byte[] b = new byte[1];
                 b[0]= 2;
                 s=e.Message.ToString();
                return b;
            }
            finally
            {
                book.Close();
            }
        }

         private static void DataWrite2Sheet(DataTable dt, int startRow, int endRow, IWorkbook book,string sheetName)
        {
            ISheet sheet = book.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                string val = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                cell.SetCellValue(val);
            }
            int rowIndex = 1;
            for (int i = startRow; i <= endRow; i++)
            {
                DataRow dtRow = dt.Rows[i];
                IRow excelRow = sheet.CreateRow(rowIndex++);
                for (int j = 0; j < dtRow.ItemArray.Length; j++)
                {
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                }
            }
 
        }
        #endregion  //test end

     

        #region 导入Excel到DataTable
        //从excel中将数据导出到DataSet
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataSet ImportExceltoDs(string strFileName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            int sheetNum = wb.NumberOfSheets;
            for (int i = 0; i < sheetNum; i++)
            {
                ISheet sheet = wb.GetSheetAt(i);
                dt = ImportDt(sheet, 0, true);
                ds.Tables.Add(dt);
            }
            return ds;
        }

       
        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName)
        {
            DataTable dt = null;
            DataSet ds = new DataSet();
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            int sheetNum = wb.NumberOfSheets;
            for (int i = 0; i < sheetNum; i++)
            {
                DataTable dtTemp = new DataTable();
                ISheet sheet = wb.GetSheetAt(i);
                dtTemp = ImportDt(sheet, 1, true);
                ds.Tables.Add(dtTemp);
            }

            int tableNum = ds.Tables.Count;
            dt = ds.Tables[0].Clone();
            if (dt != null)
            {
                for (int i = 0; i < tableNum; i++)
                {
                    dt.Merge(ds.Tables[i]);
                }
            }
                return dt;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheet(SheetName);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet isheet = wb.GetSheetAt(SheetIndex);
            DataTable table = new DataTable();
            table = ImportDt(isheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            isheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheet(SheetName);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet序号</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheetAt(SheetIndex);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

      

        /// <summary>
        /// 将制定sheet中的数据导出到datatable中
        /// </summary>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        static DataTable ImportDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
             DataTable table = new DataTable();
            IRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                //DataColumn column = new DataColumn(Convert.ToString(i));
                                //table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        IRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i);
                        }
                        else
                        {
                            row = sheet.GetRow(i);
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.Error:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                wl.WriteLogs(exception.ToString());
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        wl.WriteLogs(exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            return table;
        }

        #endregion


        public static void InsertSheet(string outputFile, string sheetname, DataTable dt)
        {

            using (FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read))
            {
                IWorkbook hssfworkbook = WorkbookFactory.Create(readfile);
                //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                int num = hssfworkbook.GetSheetIndex(sheetname);
                ISheet sheet1;
                if (num >= 0)
                    sheet1 = hssfworkbook.GetSheet(sheetname);
                else
                {
                    sheet1 = hssfworkbook.CreateSheet(sheetname);
                }


                try
                {
                    if (sheet1.GetRow(0) == null)
                    {
                        sheet1.CreateRow(0);
                    }
                    for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                    {
                        if (sheet1.GetRow(0).GetCell(coluid) == null)
                        {
                            sheet1.GetRow(0).CreateCell(coluid);
                        }

                        sheet1.GetRow(0).GetCell(coluid).SetCellValue(dt.Columns[coluid].ColumnName);
                    }
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    throw;
                }


                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i) == null)
                        {
                            sheet1.CreateRow(i);
                        }
                        for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                        {
                            if (sheet1.GetRow(i).GetCell(coluid) == null)
                            {
                                sheet1.GetRow(i).CreateCell(coluid);
                            }

                            sheet1.GetRow(i).GetCell(coluid).SetCellValue(dt.Rows[i - 1][coluid].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        wl.WriteLogs(ex.ToString());
                        //throw;
                    }
                }
                try
                {
                    readfile.Close();

                    using (FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        hssfworkbook.Write(writefile);
                        writefile.Close();
                        hssfworkbook.Close();
                    }
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                }
                finally
                {
                    readfile.Close();
                    hssfworkbook.Close();
                }
            }
        }
        #region 更新excel中的数据
        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[] updateData, int coluid, int rowid)
        {
            //FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = WorkbookFactory.Create(outputFile);
            //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    throw;
                }
            }
            try
            {
                //readfile.Close();
                using (FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    hssfworkbook.Write(writefile);
                    hssfworkbook.Close();
                    writefile.Close();
                }
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
            finally
            {
                hssfworkbook.Close();
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        wl.WriteLogs(ex.ToString());
                    }
                }
            }
            try
            {
                using (FileStream writefile = new FileStream(outputFile, FileMode.Create))
                {
                    hssfworkbook.Write(writefile);
                    hssfworkbook.Close();
                    writefile.Close();
                }
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
            finally
            {
                hssfworkbook.Close();
            }
        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluid">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[] updateData, int coluid, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    throw;
                }
            }
            try
            {
                readfile.Close();
                using (FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    hssfworkbook.Write(writefile);
                    writefile.Close();
                    hssfworkbook.Close();
                }
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
            finally
            {
                hssfworkbook.Close();
            }

        }

        /// <summary>
        /// 更新Excel表格
        /// </summary>
        /// <param name="outputFile">需更新的excel表格路径</param>
        /// <param name="sheetname">sheet名</param>
        /// <param name="updateData">需更新的数据</param>
        /// <param name="coluids">需更新的列号</param>
        /// <param name="rowid">需更新的开始行号</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        wl.WriteLogs(ex.ToString());
                    }
                }
            }
            try
            {
                using (FileStream writefile = new FileStream(outputFile, FileMode.Create))
                {
                    hssfworkbook.Write(writefile);
                    hssfworkbook.Close();
                    writefile.Close();
                }
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
            finally
            {
                hssfworkbook.Close();
            }
        }

        #endregion

        public static int GetSheetNumber(string outputFile)
        {
            int number = -1;
            try
            {
                using (FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read))
                {

                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                    number = hssfworkbook.NumberOfSheets;
                    hssfworkbook.Close();
                }

            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            
            return number;
        }

        public static ArrayList GetSheetName(string outputFile)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
                {
                    arrayList.Add(hssfworkbook.GetSheetName(i));
                }
            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            return arrayList;
        }

        public static bool isNumeric(String message, out double result)
        {
            Regex rex = new Regex(@"^[-]?\d+[.]?\d*$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = double.Parse(message);
                return true;
            }
            else
                return false;

        }



    }
}