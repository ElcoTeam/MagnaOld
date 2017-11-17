using System;
using System.Web;
using Bll;
using Model;
using Tools;
using System.IO;
using OfficeOpenXml;
using System.Data;
using System.Drawing;

public class ExcelHandler : IHttpHandler
{

    HttpRequest Request = null;
    HttpResponse Response = null;
    static readonly System.Text.RegularExpressions.Regex numberMatcher = new System.Text.RegularExpressions.Regex("[0-9]+");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        Request = context.Request;
        Response = context.Response;

        string method = context.Request.Params["method"];
        switch (method)
        {
            case "createCX11":
                CreateCX11();
                break;

        }
    }

    void CreateCX11()
    {
        //获取整个数据
        DataSet ds = mg_StepBLL.QeuryALLData();
        if (DataHelper.HasData(ds))
        {
            //检查excel目录
            string xlsPath = HttpContext.Current.Server.MapPath("\\excel\\");
            if (!Directory.Exists(xlsPath))
            {
                Directory.CreateDirectory(xlsPath);
            }
            //9个excel文件名
            string[] filename = {
                                @"CX11 FS Drive Configuration.xlsx",
                                @"CX11 FS Passenger Configuration.xlsx",
                                @"CX11 FSB Drive Configuration.xlsx",
                                @"CX11 FSB Passenger Configuration.xlsx",
                                @"CX11 FSC Drive Configuration.xlsx",
                                @"CX11 FSC Passenger Configuration.xlsx",
                                @"CX11 RSB40 Configuration.xlsx",
                                @"CX11 RSB60 Configuration.xlsx",
                                @"CX11 RSC Configuration.xlsx"
                            };

            //创建FS Drive EXCEL
            CreateFSDriveExcel(ds, xlsPath, filename[0]);
            CreateFSPassengerExcel(ds, xlsPath, filename[1]);
            //CreateFSBDriveExcel(ds, xlsPath, filename[2]);
            CreateCommonExcel(ds, xlsPath, filename[2], 1, 7, mg_XLSEnum.FSB_Drive, "FSB", "Drive");
            CreateCommonExcel(ds, xlsPath, filename[3], 1, 7, mg_XLSEnum.FSB_Passenger, "FSB", "Passenger");
            CreateCommonExcel(ds, xlsPath, filename[4], 1, 4, mg_XLSEnum.FSC_Drive, "FSC", "Drive");
            CreateCommonExcel(ds, xlsPath, filename[5], 1, 4, mg_XLSEnum.FSC_Passenger, "FSC", "Passenger");
            CreateCommonExcel(ds, xlsPath, filename[6], 2, 6, mg_XLSEnum.RSB40, "RSB", "40%靠背");
            CreateCommonExcel(ds, xlsPath, filename[7], 1, 6, mg_XLSEnum.RSB60, "RSB", "60%靠背");
            CreateCommonExcel(ds, xlsPath, filename[8], 1, 4, mg_XLSEnum.RSC, "RSC", "坐垫");
            Response.Write("true");
            Response.End();

            //FS  FSB  FSC  RSB40   RSB60  RSC
        }
        else
        {
            Response.Write("false");
            Response.End();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="xlsPath"></param>
    /// <param name="filename"></param>
    /// <param name="startSite">开始站点</param>
    /// <param name="endSite">结束站点</param>
    /// <param name="xlsenum"></param>
    /// <param name="type"></param>
    /// <param name="title"></param>
    private static void CreateCommonExcel(DataSet ds, string xlsPath, string filename, int startSite, int endSite, mg_XLSEnum xlsenum, string type, string title)
    {
        //获取部件数据
        DataTable fsdriveDT = new DataTable();
        DataTable partDT = ds.Tables["part"];
        fsdriveDT = partDT.Copy();
        fsdriveDT.Rows.Clear();
        DataRow[] rows = partDT.Select("part_categoryid=" + (int)xlsenum);
        foreach (DataRow row in rows)
        {
            fsdriveDT.ImportRow(row);
        }

        //如果有文件，则重新生成
        string xlswebfile = (xlsPath + filename);
        if (File.Exists(xlswebfile))
        {
            File.Delete(xlswebfile);
        }
        string xlsFile = xlswebfile.Replace("\\", "/");
        using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsFile)))
        {
            //遍历每个部件
            foreach (DataRow row in fsdriveDT.Rows)
            {
                string part_id = DataHelper.GetCellDataToStr(row, "part_id");
                //string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
                string partSheet = DataHelper.GetCellDataToStr(row, "part_no");
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(partSheet);      //添加 FS Drive sheet
                worksheet.Cells.Style.ShrinkToFit = true;

                DataTable stationDT = ds.Tables["station"];     //该部件下的工位
                DataTable stepDT = ds.Tables["step"];     //该部件下的工位下的步骤

                //获取工位数据
                DataTable fsdriveStationDT = new DataTable();
                fsdriveStationDT = stationDT.Copy();
                fsdriveStationDT.Rows.Clear();
                DataRow[] stationrows = stationDT.Select("part_categoryid=" + (int)xlsenum + " and part_id=" + part_id);
                foreach (DataRow stationrow in stationrows)
                {
                    fsdriveStationDT.ImportRow(stationrow);
                }
                int rowindex = 1;
                int currentSite = startSite;
                //遍历工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    //工位号
                    int colIndex = 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    if (stationNo=="")
                    {
                        stationNo= "0";
                    }
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    stationNo = GetStationNo(stationNo);
                    worksheet.Cells[rowindex, 1].Value = stationNo;
                    rowindex++;

                    worksheet.Cells[rowindex, colIndex++].Value = title;
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                    rowindex++;

                    int station = int.Parse(numberMatcher.Match(stationNo).Value) / 10;
                    // 补齐站点（***010-***200）
                    if (station > currentSite)
                    {
                        FillEmptyStation(worksheet, type, ref rowindex, currentSite, station, false);
                        currentSite = station;
                    }
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);
                    
                    //步骤
                    for (int j = 0; j < 30; j++)
                    {
                        colIndex = 1;
                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_customerPN = null;
                        string time = null;
                        string count = null;
                        string barcode_start = null;
                        string barcode_number = null;

                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//left
                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            worksheet.Cells[rowindex, colIndex++].Value = j + 1;
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_customerPN");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        }
                        rowindex++;
                    }
                    currentSite++;
                }
                currentSite++;
                // 补齐站点（***010-***200）
                for (; currentSite <= endSite; currentSite++)
                {
                    SingleFillEmptyStation(worksheet, type, ref rowindex, rowindex - 31, currentSite, false);
                }
            }
            package.Save();
        }
    }




    private static void CreateFSBDriveExcel(DataSet ds, string xlsPath, string filename)
    {
        //获取部件数据
        DataTable fsdriveDT = new DataTable();
        DataTable partDT = ds.Tables["part"];
        fsdriveDT = partDT.Copy();
        fsdriveDT.Rows.Clear();
        DataRow[] rows = partDT.Select("part_categoryid=" + (int)mg_XLSEnum.FSB_Drive);
        foreach (DataRow row in rows)
        {
            fsdriveDT.ImportRow(row);
        }

        //如果有文件，则重新生成
        string xlswebfile = (xlsPath + filename);
        if (File.Exists(xlswebfile))
        {
            File.Delete(xlswebfile);
        }
        string xlsFile = xlswebfile.Replace("\\", "/");
        using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsFile)))
        {
            //遍历每个部件
            foreach (DataRow row in fsdriveDT.Rows)
            {
                string part_id = DataHelper.GetCellDataToStr(row, "part_id");
                //string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
                string partSheet = DataHelper.GetCellDataToStr(row, "part_no");
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(partSheet);      //添加 FS Drive sheet
                worksheet.Cells.Style.ShrinkToFit = true;

                DataTable stationDT = ds.Tables["station"];     //该部件下的工位
                DataTable stepDT = ds.Tables["step"];     //该部件下的工位下的步骤

                //获取工位数据
                DataTable fsdriveStationDT = new DataTable();
                fsdriveStationDT = stationDT.Copy();
                fsdriveStationDT.Rows.Clear();
                DataRow[] stationrows = stationDT.Select("part_categoryid=" + (int)mg_XLSEnum.FSB_Drive + " and part_id=" + part_id);
                foreach (DataRow stationrow in stationrows)
                {
                    fsdriveStationDT.ImportRow(stationrow);
                }
                int rowindex = 1;
                //遍历工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    int colIndex = 1;
                    //工位号
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    worksheet.Cells[rowindex, 1].Value = GetStationNo(stationNo);
                    rowindex++;

                    worksheet.Cells[rowindex, colIndex++].Value = "Drive";
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";

                    rowindex++;
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);

                    //步骤
                    for (int j = 0; j < 30; j++)
                    {
                        colIndex = 1;
                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_customerPN = null;
                        string time = null;
                        string count = null;
                        string barcode_start = null;
                        string barcode_number = null;

                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            bom_customerPN = DataHelper.GetCellDataToStr(stepRow, "bom_customerPN");
                            time = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                            count = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                            barcode_start = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                            barcode_number = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        }

                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//left
                        worksheet.Cells[rowindex, colIndex++].Value = j + 1;
                        worksheet.Cells[rowindex, colIndex++].Value = step_plccode;
                        worksheet.Cells[rowindex, colIndex++].Value = bom_customerPN;
                        worksheet.Cells[rowindex, colIndex++].Value = time;
                        worksheet.Cells[rowindex, colIndex++].Value = count;
                        worksheet.Cells[rowindex, colIndex++].Value = barcode_start;
                        worksheet.Cells[rowindex, colIndex++].Value = barcode_number;

                        if (rowindex > 2 && rowindex < 33)
                        {
                            worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (j + 1);//product
                            worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (j + 1);
                        }
                        rowindex++;
                    }
                }
            }
            package.Save();
        }
    }

    //
    private static void CreateFSPassengerExcel(DataSet ds, string xlsPath, string filename)
    {
        //获取部件数据
        DataTable fsdriveDT = new DataTable();
        DataTable partDT = ds.Tables["part"];
        fsdriveDT = partDT.Copy();
        fsdriveDT.Rows.Clear();
        DataRow[] rows = partDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Passenger);
        foreach (DataRow row in rows)
        {
            fsdriveDT.ImportRow(row);
        }

        //如果有文件，则重新生成
        string xlswebfile = (xlsPath + filename);
        if (File.Exists(xlswebfile))
        {
            File.Delete(xlswebfile);
        }
        string xlsFile = xlswebfile.Replace("\\", "/");
        using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsFile)))
        {
            //遍历每个部件
            foreach (DataRow row in fsdriveDT.Rows)
            {
                string part_id = DataHelper.GetCellDataToStr(row, "part_id");
                //string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
                string partSheet = DataHelper.GetCellDataToStr(row, "part_no");
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(partSheet);      //添加 FS Drive sheet
                worksheet.Cells.Style.ShrinkToFit = true;

                DataTable stationDT = ds.Tables["station"];     //该部件下的工位
                DataTable stepDT = ds.Tables["step"];     //该部件下的工位下的步骤

                //获取前两个工位
                DataRow[] twostationrows = stationDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Passenger + " and part_id=" + part_id);
                DataRow firstRow = twostationrows[0];
                string st_id_f = DataHelper.GetCellDataToStr(firstRow, "st_id");
                string st_no_f = DataHelper.GetCellDataToStr(firstRow, "st_no");
                string st_typeid_f = DataHelper.GetCellDataToStr(firstRow, "st_typeid");
                string st_order_f = DataHelper.GetCellDataToStr(firstRow, "st_order");
                DataRow secondRow = twostationrows[1];
                string st_id_s = DataHelper.GetCellDataToStr(secondRow, "st_id");
                string st_no_s = DataHelper.GetCellDataToStr(secondRow, "st_no");
                string st_typeid_s = DataHelper.GetCellDataToStr(secondRow, "st_typeid");
                string st_order_s = DataHelper.GetCellDataToStr(secondRow, "st_order");

                string st_order = NumericParse.StringToInt(st_order_f) > NumericParse.StringToInt(st_order_s) ? st_order_f : st_order_s; //最大工位序号

                int rowindex = 1;
                //获取工位数据

                DataRow[] leftStepRows = null;
                DataRow[] rightStepRows = null;
                if (st_typeid_f == "40" && st_typeid_s == "41")
                {
                    leftStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_f);
                    rightStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_s);
                }
                else
                {
                    leftStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_s);
                    rightStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_f);
                }

                //第一行工位号单元格
                //string st_no_firstRow = st_no_f.Substring(0, st_no_f.IndexOf('-'));
                string st_no_firstRow = st_no_f;
                worksheet.Cells[rowindex, 1].Value = GetStationNo(st_no_firstRow);
                rowindex++;
                //第一行表头
                //Drive（Left）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2 ||	Drive（Right）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2  Production Info		Others
                //左表头
                int colIndex = 1;
                worksheet.Cells[rowindex, colIndex++].Value = "Passenger（Left）";
                worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                worksheet.Cells[rowindex, colIndex++].Value = "time";
                worksheet.Cells[rowindex, colIndex++].Value = "count";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode_start";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode_number";
                //右表头
                worksheet.Cells[rowindex, colIndex++].Value = "Passenger（Right）";
                worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                worksheet.Cells[rowindex, colIndex++].Value = "time";
                worksheet.Cells[rowindex, colIndex++].Value = "count";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                rowindex++;

                for (int i = 0; i < 30; i++)
                {
                    colIndex = 1;
                    worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (i + 1);//left
                    if (i < leftStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(leftStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_storename");
                        string bom_customerPN = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_customerPN");

                        worksheet.Cells[rowindex, colIndex++].Value = i + 1;
                        worksheet.Cells[rowindex, colIndex++].Value = step_plccode;
                        worksheet.Cells[rowindex, colIndex++].Value = bom_customerPN;

                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "step_clock");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_count");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "barcode_start");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "barcode_number");
                    }
                    colIndex = 9;
                    worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (i + 1);//right
                    if (i < rightStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(rightStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_storename");
                        string bom_customerPN = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_customerPN");

                        worksheet.Cells[rowindex, colIndex++].Value = i + 1;
                        worksheet.Cells[rowindex, colIndex++].Value = step_plccode;
                        worksheet.Cells[rowindex, colIndex++].Value = bom_customerPN;

                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "step_clock");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_count");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "barcode_start");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "barcode_number");
                    }


                    //worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (i + 1);//product
                    //worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (i + 1);
                    rowindex++;
                }

                //获取除第一个工位的其他工位数据
                DataTable fsdriveStationDT = new DataTable();
                fsdriveStationDT = stationDT.Copy();
                fsdriveStationDT.Rows.Clear();
                DataRow[] stationrows = stationDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Passenger + " and part_id=" + part_id + " and st_order>" + st_order);
                foreach (DataRow stationrow in stationrows)
                {
                    fsdriveStationDT.ImportRow(stationrow);
                }
                int currentStation = 1;
                //遍历其他工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    currentStation++;
                    //工位号
                    colIndex = 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    stationNo = GetStationNo(stationNo);
                    worksheet.Cells[rowindex, 1].Value = stationNo;
                    rowindex++;
                    //表头
                    worksheet.Cells[rowindex, colIndex++].Value = "Passenger（Left）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                    //右表头
                    worksheet.Cells[rowindex, colIndex++].Value = "Passenger（Right）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                    rowindex++;
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);
                    DataRow stepRow = null;


                    int station = int.Parse(numberMatcher.Match(stationNo).Value) / 10;
                    if (station > currentStation)
                    {
                        FillEmptyStation(worksheet, "FSA", ref rowindex, currentStation, station, true);
                        currentStation = station;
                    }

                    //步骤
                    for (int j = 0; j < 30; j++)
                    {
                        stepRow = null;
                        colIndex = 1;
                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_customerPN = null;

                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//left
                        if (j < stepRows.Length)
                        {
                            stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            //step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            //bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            //bom_barcode = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        }


                        //if (stepRow != null && st_typeid == "40")
                        if (stepRow != null)
                        {
                            worksheet.Cells[rowindex, colIndex++].Value = j + 1;

                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_customerPN");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        }
                        colIndex = 9;
                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//right

                        //if (stepRow != null && st_typeid == "41")
                        //{
                        //    worksheet.Cells[rowindex, colIndex++].Value = j + 1;
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        //}
                        rowindex++;
                    }
                }

                currentStation++;
                for (; currentStation <= 20; currentStation++)
                {
                    SingleFillEmptyStation(worksheet, "FSA", ref rowindex, rowindex - 31, currentStation, true);
                }
            }
            package.Save();
        }
    }




    private static void CreateFSDriveExcel(DataSet ds, string xlsPath, string filename)
    {
        //获取部件数据
        DataTable fsdriveDT = new DataTable();
        DataTable partDT = ds.Tables["part"];
        fsdriveDT = partDT.Copy();
        fsdriveDT.Rows.Clear();
        DataRow[] rows = partDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Drive);
        foreach (DataRow row in rows)
        {
            fsdriveDT.ImportRow(row);
        }

        //如果有文件，则重新生成
        string xlswebfile = (xlsPath + filename);
        if (File.Exists(xlswebfile))
        {
            File.Delete(xlswebfile);
        }
        string xlsFile = xlswebfile.Replace("\\", "/");
        using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsFile)))
        {
            //遍历每个部件
            foreach (DataRow row in fsdriveDT.Rows)
            {
                string part_id = DataHelper.GetCellDataToStr(row, "part_id");
                //string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
                string partSheet = DataHelper.GetCellDataToStr(row, "part_no");
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(partSheet);      //添加 FS Drive sheet
                worksheet.Cells.Style.ShrinkToFit = true;

                DataTable stationDT = ds.Tables["station"];     //该部件下的工位
                DataTable stepDT = ds.Tables["step"];     //该部件下的工位下的步骤

                //获取前两个工位
                DataRow[] twostationrows = stationDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Drive + " and part_id=" + part_id);
                DataRow firstRow = twostationrows[0];
                string st_id_f = DataHelper.GetCellDataToStr(firstRow, "st_id");
                string st_no_f = DataHelper.GetCellDataToStr(firstRow, "st_no");
                string st_typeid_f = DataHelper.GetCellDataToStr(firstRow, "st_typeid");
                string st_order_f = DataHelper.GetCellDataToStr(firstRow, "st_order");
                DataRow secondRow = twostationrows[1];
                string st_id_s = DataHelper.GetCellDataToStr(secondRow, "st_id");
                string st_no_s = DataHelper.GetCellDataToStr(secondRow, "st_no");
                string st_typeid_s = DataHelper.GetCellDataToStr(secondRow, "st_typeid");
                string st_order_s = DataHelper.GetCellDataToStr(secondRow, "st_order");

                string st_order = NumericParse.StringToInt(st_order_f) > NumericParse.StringToInt(st_order_s) ? st_order_f : st_order_s; //最大工位序号
                //获取工位数据

                DataRow[] leftStepRows = null;
                DataRow[] rightStepRows = null;
                if (st_typeid_f == "37" && st_typeid_s == "38")
                {
                    leftStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_f);
                    rightStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_s);
                }
                else
                {
                    leftStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_s);
                    rightStepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id_f);
                }

                int rowindex = 1;
                //第一行工位号单元格
                string st_no_firstRow = GetStationNo(st_no_f);

                worksheet.Cells[rowindex++, 1].Value = st_no_firstRow;
                int colIndex = 1;
                //第一行表头
                //Drive（Left）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2 ||	Drive（Right）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2  Production Info		Others
                //左表头
                worksheet.Cells[rowindex, colIndex++].Value = "Drive（Left）";
                worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                worksheet.Cells[rowindex, colIndex++].Value = "time";
                worksheet.Cells[rowindex, colIndex++].Value = "count";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                //右表头
                worksheet.Cells[rowindex, colIndex++].Value = "Drive（Right）";
                worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                worksheet.Cells[rowindex, colIndex++].Value = "time";
                worksheet.Cells[rowindex, colIndex++].Value = "count";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";

                rowindex++;

                for (int i = 0; i < 30; i++)
                {
                    colIndex = 1;
                    worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (i + 1);//left
                    if (i < leftStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(leftStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_storename");
                        string bom_customerPN = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_customerPN");

                        worksheet.Cells[rowindex, colIndex++].Value = i + 1;
                        worksheet.Cells[rowindex, colIndex++].Value = step_plccode;
                        //worksheet.Cells[i + 3, colIndex++].Value = bom_storename;
                        worksheet.Cells[rowindex, colIndex++].Value = bom_customerPN;
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "step_clock");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_count");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "barcode_start");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(leftStepRows[i], "barcode_number");

                    }
                    colIndex = 9;
                    worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (i + 1);//right
                    if (i < rightStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(rightStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_storename");
                        string bom_customerPN = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_customerPN");

                        worksheet.Cells[rowindex, colIndex++].Value = i + 1;
                        worksheet.Cells[rowindex, colIndex++].Value = step_plccode;
                        //worksheet.Cells[i + 3, colIndex++].Value = bom_storename;
                        worksheet.Cells[rowindex, colIndex++].Value = bom_customerPN;
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "step_clock");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_count");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "barcode_start");
                        worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(rightStepRows[i], "barcode_number");
                    }

                    //worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (i + 1);//product
                    //worksheet.Cells[rowindex, colIndex++].Value = "Reserved" + (i + 1);
                    rowindex++;
                }

                //获取除第一个工位的其他工位数据
                DataTable fsdriveStationDT = new DataTable();
                fsdriveStationDT = stationDT.Copy();
                fsdriveStationDT.Rows.Clear();
                DataRow[] stationrows = stationDT.Select("part_categoryid=" + (int)mg_XLSEnum.FS_Drive + " and part_id=" + part_id + " and st_order>" + st_order);
                foreach (DataRow stationrow in stationrows)
                {
                    fsdriveStationDT.ImportRow(stationrow);
                }

                int currentStation = 1;
                //遍历其他工
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    currentStation++;
                    //工位号
                    //rowindex = i + 32 * (i + 1) + 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    stationNo = GetStationNo(stationNo);
                    worksheet.Cells[rowindex, 1].Value = stationNo;
                    rowindex++;

                    //表头
                    colIndex = 1;
                    worksheet.Cells[rowindex, colIndex++].Value = "Drive（Left）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                    //右表头
                    worksheet.Cells[rowindex, colIndex++].Value = "Drive（Right）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Steps";
                    worksheet.Cells[rowindex, colIndex++].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, colIndex++].Value = "Barcode";
                    worksheet.Cells[rowindex, colIndex++].Value = "time";
                    worksheet.Cells[rowindex, colIndex++].Value = "count";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-start";
                    worksheet.Cells[rowindex, colIndex++].Value = "barcode-number";
                    rowindex++;


                    int station = int.Parse(numberMatcher.Match(stationNo).Value) / 10;
                    if (station > currentStation)
                    {
                        FillEmptyStation(worksheet, "FSA", ref rowindex, currentStation, station, true);
                        currentStation = station;
                    }
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);
                    DataRow stepRow = null;

                    //步骤
                    for (int j = 0; j < 30; j++)
                    {
                        stepRow = null;
                        colIndex = 1;
                        string st_typeid = null;

                        if (j < stepRows.Length)
                        {
                            stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                        }
                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//left
                        //if (stepRow != null && st_typeid == "37")
                        if (stepRow != null)
                        {
                            worksheet.Cells[rowindex, colIndex++].Value = j + 1;
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_customerPN");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                            worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        }
                        colIndex = 9;
                        worksheet.Cells[rowindex, colIndex++].Value = "装配物料" + (j + 1);//right

                        //if (stepRow != null && st_typeid == "38")
                        //{
                        //    worksheet.Cells[rowindex, colIndex++].Value = j + 1;
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "step_clock");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "bom_count");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_start");
                        //    worksheet.Cells[rowindex, colIndex++].Value = DataHelper.GetCellDataToStr(stepRow, "barcode_number");
                        //}
                        rowindex++;
                    }
                }
                currentStation++;
                for (; currentStation < 20; currentStation++)
                {
                    SingleFillEmptyStation(worksheet, "FSA", ref rowindex, rowindex - 31, currentStation, true);
                }
            }
            package.Save();
        }
    }

    private static void FillEmptyStation(ExcelWorksheet worksheet, string type, ref int rowindex, int station, int endStation, bool createRight)
    {
        rowindex -= 2;
        while (station < endStation)
        {
            worksheet.InsertRow(rowindex, 32);
            int copyFromRow = rowindex + 33;
            SingleFillEmptyStation(worksheet, type, ref rowindex, copyFromRow, station, createRight);
            station++;
        }
        rowindex += 2;
    }

    private static void SingleFillEmptyStation(ExcelWorksheet worksheet, string type, ref int rowindex, int copyFrom, int station, bool createRight)
    {
        int colIndex = 1;
        worksheet.Cells[rowindex, 1].Value = string.Format("{0}{1:d3}", type, station * 10);
        rowindex++;
        ExcelRow row = worksheet.Row(copyFrom);
        for (int i = 1; i < 17; i++)
        {
            worksheet.Cells[rowindex, i].Value = worksheet.Cells[copyFrom, i].Value;
        }
        rowindex++;
        //该工位下步骤数据
        //步骤
        for (int j = 0; j < 30; j++)
        {
            colIndex = 1;
            worksheet.Cells[rowindex, colIndex].Value = "装配物料" + (j + 1);//left
            if (createRight)
            {
                colIndex = 9;
                worksheet.Cells[rowindex, colIndex].Value = "装配物料" + (j + 1);//left
            }
            rowindex++;
        }
    }

    private static string GetStationNo(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }
        int index = str.IndexOf('-');
        if (index == -1)
            return str;
        return str.Substring(0, str.IndexOf('-'));
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}