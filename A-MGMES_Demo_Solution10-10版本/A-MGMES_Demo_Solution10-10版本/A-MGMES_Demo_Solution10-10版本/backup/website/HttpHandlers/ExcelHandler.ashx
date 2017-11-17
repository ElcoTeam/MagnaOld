<%@ WebHandler Language="C#" Class="ExcelHandler" %>

using System;
using System.Web;
using BLL;
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
            CreateCommonExcel(ds, xlsPath, filename[2], mg_XLSEnum.FSB_Drive, "Drive");
            CreateCommonExcel(ds, xlsPath, filename[3], mg_XLSEnum.FSB_Passenger, "Passenger");
            CreateCommonExcel(ds, xlsPath, filename[4], mg_XLSEnum.FSC_Drive, "Drive");
            CreateCommonExcel(ds, xlsPath, filename[5], mg_XLSEnum.FSC_Passenger, "Passenger");
            CreateCommonExcel(ds, xlsPath, filename[6], mg_XLSEnum.RSB40, "40%靠背");
            CreateCommonExcel(ds, xlsPath, filename[7], mg_XLSEnum.RSB60, "60%靠背");
            CreateCommonExcel(ds, xlsPath, filename[8], mg_XLSEnum.RSC, "坐垫");
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
    private static void CreateCommonExcel(DataSet ds, string xlsPath, string filename, mg_XLSEnum xlsenum,string title)
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
                string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
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
                //遍历工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    int rowindex = 0;
                    //工位号
                    rowindex = 34 * (i) + 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    worksheet.Cells[rowindex, 1].Value = stationNo;

                    rowindex++;
                    worksheet.Cells[rowindex, 1].Value = title;
                    worksheet.Cells[rowindex, 2].Value = "Steps";
                    worksheet.Cells[rowindex, 3].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 4].Value = "Barcode";
                    worksheet.Cells[rowindex, 5].Value = "Reserved1";
                    worksheet.Cells[rowindex, 6].Value = "Reserved2";

                    if (rowindex == 2)
                    {
                        worksheet.Cells[2, 7].Value = "Production Info";
                        worksheet.Cells[2, 8].Value = "Others";
                    }
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);

                    //步骤
                    for (int j = 0; j < 32; j++)
                    {
                        rowindex++;

                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_barcode = null;

                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            bom_barcode = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        }

                        worksheet.Cells[rowindex, 1].Value = "装配物料" + (j + 1);//left
                        worksheet.Cells[rowindex, 2].Value = step_plccode;
                        worksheet.Cells[rowindex, 3].Value = bom_storename;
                        worksheet.Cells[rowindex, 4].Value = bom_barcode;

                        if (rowindex > 2 && rowindex < 35)
                        {
                            worksheet.Cells[rowindex, 7].Value = "Reserved" + (j + 1);//product
                            worksheet.Cells[rowindex, 8].Value = "Reserved" + (j + 1);
                        }

                    }
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
                string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
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
                //遍历工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    //工位号
                    int rowindex = 0;
                    rowindex = 34 * (i) + 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    worksheet.Cells[rowindex, 1].Value = stationNo;

                    rowindex++;
                    worksheet.Cells[rowindex, 1].Value = "Drive";
                    worksheet.Cells[rowindex, 2].Value = "Steps";
                    worksheet.Cells[rowindex, 3].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 4].Value = "Barcode";
                    worksheet.Cells[rowindex, 5].Value = "Reserved1";
                    worksheet.Cells[rowindex, 6].Value = "Reserved2";

                    if (rowindex == 2)
                    {
                        worksheet.Cells[2, 7].Value = "Production Info";
                        worksheet.Cells[2, 8].Value = "Others";
                    }
                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);

                    //步骤
                    for (int j = 0; j < 32; j++)
                    {
                        rowindex++;

                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_barcode = null;

                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            bom_barcode = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        }

                        worksheet.Cells[rowindex, 1].Value = "装配物料" + (j + 1);//left
                        worksheet.Cells[rowindex, 2].Value = step_plccode;
                        worksheet.Cells[rowindex, 3].Value = bom_storename;
                        worksheet.Cells[rowindex, 4].Value = bom_barcode;

                        if (rowindex > 2 && rowindex < 35)
                        {
                            worksheet.Cells[rowindex, 7].Value = "Reserved" + (j + 1);//product
                            worksheet.Cells[rowindex, 8].Value = "Reserved" + (j + 1);
                        }

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
                string partSheet = DataHelper.GetCellDataToStr(row, "part_name");
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
                string st_no_firstRow = st_no_f.Substring(0, st_no_f.IndexOf('-'));
                worksheet.Cells[1, 1].Value = st_no_firstRow;
                //第一行表头
                //Drive（Left）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2 ||	Drive（Right）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2  Production Info		Others
                //左表头
                worksheet.Cells[2, 1].Value = "Passenger（Left）";
                worksheet.Cells[2, 2].Value = "Steps";
                worksheet.Cells[2, 3].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[2, 4].Value = "Barcode";
                worksheet.Cells[2, 5].Value = "Reserved1";
                worksheet.Cells[2, 6].Value = "Reserved2";
                //右表头
                worksheet.Cells[2, 7].Value = "Passenger（Right）";
                worksheet.Cells[2, 8].Value = "Steps";
                worksheet.Cells[2, 9].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[2, 10].Value = "Barcode";
                worksheet.Cells[2, 11].Value = "Reserved1";
                worksheet.Cells[2, 12].Value = "Reserved2";

                //product 表头
                worksheet.Cells[2, 13].Value = "Production Info";
                worksheet.Cells[2, 14].Value = "Others";

                for (int i = 0; i < 32; i++)
                {
                    worksheet.Cells[i + 3, 1].Value = "装配物料" + (i + 1);//left
                    if (i < leftStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(leftStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_storename");
                        string bom_barcode = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_barcode");

                        worksheet.Cells[i + 3, 2].Value = step_plccode;
                        worksheet.Cells[i + 3, 3].Value = bom_storename;
                        worksheet.Cells[i + 3, 4].Value = bom_barcode;

                    }

                    worksheet.Cells[i + 3, 7].Value = "装配物料" + (i + 1);//right
                    if (i < rightStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(rightStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_storename");
                        string bom_barcode = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_barcode");

                        worksheet.Cells[i + 3, 8].Value = step_plccode;
                        worksheet.Cells[i + 3, 9].Value = bom_storename;
                        worksheet.Cells[i + 3, 10].Value = bom_barcode;

                    }


                    worksheet.Cells[i + 3, 13].Value = "Reserved" + (i + 1);//product
                    worksheet.Cells[i + 3, 14].Value = "Reserved" + (i + 1);

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
                //遍历其他工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    //工位号
                    int rowindex = 0;
                    rowindex = 34 * (i + 1) + 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    worksheet.Cells[rowindex, 1].Value = stationNo;

                    //表头
                    rowindex++;
                    worksheet.Cells[rowindex, 1].Value = "Passenger（Left）";
                    worksheet.Cells[rowindex, 2].Value = "Steps";
                    worksheet.Cells[rowindex, 3].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 4].Value = "Barcode";
                    worksheet.Cells[rowindex, 5].Value = "Reserved1";
                    worksheet.Cells[rowindex, 6].Value = "Reserved2";
                    //右表头
                    worksheet.Cells[rowindex, 7].Value = "Passenger（Right）";
                    worksheet.Cells[rowindex, 8].Value = "Steps";
                    worksheet.Cells[rowindex, 9].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 10].Value = "Barcode";
                    worksheet.Cells[rowindex, 11].Value = "Reserved1";
                    worksheet.Cells[rowindex, 12].Value = "Reserved2";

                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);

                    //步骤
                    for (int j = 0; j < 32; j++)
                    {
                        rowindex++;

                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_barcode = null;

                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            bom_barcode = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        }

                        worksheet.Cells[rowindex, 1].Value = "装配物料" + (j + 1);//left

                        if (st_typeid == "40")
                        {
                            worksheet.Cells[rowindex, 2].Value = step_plccode;
                            worksheet.Cells[rowindex, 3].Value = bom_storename;
                            worksheet.Cells[rowindex, 4].Value = bom_barcode;
                        }

                        worksheet.Cells[rowindex, 7].Value = "装配物料" + (j + 1);//right

                        if (st_typeid == "41")
                        {
                            worksheet.Cells[rowindex, 8].Value = step_plccode;
                            worksheet.Cells[rowindex, 9].Value = bom_storename;
                            worksheet.Cells[rowindex, 10].Value = bom_barcode;
                        }
                    }
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

                //第一行工位号单元格
                string st_no_firstRow = st_no_f.Substring(0, st_no_f.IndexOf('-'));
                worksheet.Cells[1, 1].Value = st_no_firstRow;
                //第一行表头
                //Drive（Left）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2 ||	Drive（Right）	Steps	Work-bins(料箱料架号）	Barcode	Reserved1	Reserved2  Production Info		Others
                //左表头
                worksheet.Cells[2, 1].Value = "Drive（Left）";
                worksheet.Cells[2, 2].Value = "Steps";
                worksheet.Cells[2, 3].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[2, 4].Value = "Barcode";
                worksheet.Cells[2, 5].Value = "Reserved1";
                worksheet.Cells[2, 6].Value = "Reserved2";
                //右表头
                worksheet.Cells[2, 7].Value = "Drive（Right）";
                worksheet.Cells[2, 8].Value = "Steps";
                worksheet.Cells[2, 9].Value = "Work-bins(料箱料架号）";
                worksheet.Cells[2, 10].Value = "Barcode";
                worksheet.Cells[2, 11].Value = "Reserved1";
                worksheet.Cells[2, 12].Value = "Reserved2";

                //product 表头
                worksheet.Cells[2, 13].Value = "Production Info";
                worksheet.Cells[2, 14].Value = "Others";

                for (int i = 0; i < 32; i++)
                {
                    worksheet.Cells[i + 3, 1].Value = "装配物料" + (i + 1);//left
                    if (i < leftStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(leftStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_storename");
                        string bom_barcode = DataHelper.GetCellDataToStr(leftStepRows[i], "bom_barcode");

                        worksheet.Cells[i + 3, 2].Value = step_plccode;
                        worksheet.Cells[i + 3, 3].Value = bom_storename;
                        worksheet.Cells[i + 3, 4].Value = bom_barcode;

                    }

                    worksheet.Cells[i + 3, 7].Value = "装配物料" + (i + 1);//right
                    if (i < rightStepRows.Length)
                    {
                        string step_plccode = DataHelper.GetCellDataToStr(rightStepRows[i], "step_plccode");
                        string bom_storename = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_storename");
                        string bom_barcode = DataHelper.GetCellDataToStr(rightStepRows[i], "bom_barcode");

                        worksheet.Cells[i + 3, 8].Value = step_plccode;
                        worksheet.Cells[i + 3, 9].Value = bom_storename;
                        worksheet.Cells[i + 3, 10].Value = bom_barcode;

                    }


                    worksheet.Cells[i + 3, 13].Value = "Reserved" + (i + 1);//product
                    worksheet.Cells[i + 3, 14].Value = "Reserved" + (i + 1);

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
                //遍历其他工位
                for (int i = 0; i < fsdriveStationDT.Rows.Count; i++)
                {
                    //工位号
                    int rowindex = 0;
                    rowindex = i + 34 * (i + 1) + 1;
                    DataRow stationRow = fsdriveStationDT.Rows[i];
                    string stationNo = DataHelper.GetCellDataToStr(stationRow, "st_no");
                    string st_id = DataHelper.GetCellDataToStr(stationRow, "st_id");
                    worksheet.Cells[rowindex, 1].Value = stationNo;

                    //表头
                    rowindex++;
                    worksheet.Cells[rowindex, 1].Value = "Drive（Left）";
                    worksheet.Cells[rowindex, 2].Value = "Steps";
                    worksheet.Cells[rowindex, 3].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 4].Value = "Barcode";
                    worksheet.Cells[rowindex, 5].Value = "Reserved1";
                    worksheet.Cells[rowindex, 6].Value = "Reserved2";
                    //右表头
                    worksheet.Cells[rowindex, 7].Value = "Drive（Right）";
                    worksheet.Cells[rowindex, 8].Value = "Steps";
                    worksheet.Cells[rowindex, 9].Value = "Work-bins(料箱料架号）";
                    worksheet.Cells[rowindex, 10].Value = "Barcode";
                    worksheet.Cells[rowindex, 11].Value = "Reserved1";
                    worksheet.Cells[rowindex, 12].Value = "Reserved2";

                    //该工位下步骤数据
                    DataRow[] stepRows = stepDT.Select("part_id=" + part_id + " and st_id=" + st_id);

                    //步骤
                    for (int j = 0; j < 32; j++)
                    {
                        rowindex++;

                        string st_typeid = null;
                        string step_plccode = null;
                        string bom_storename = null;
                        string bom_barcode = null;

                        if (j < stepRows.Length)
                        {
                            DataRow stepRow = stepRows[j];
                            st_typeid = DataHelper.GetCellDataToStr(stepRow, "st_typeid");
                            step_plccode = DataHelper.GetCellDataToStr(stepRow, "step_plccode");
                            bom_storename = DataHelper.GetCellDataToStr(stepRow, "bom_storename");
                            bom_barcode = DataHelper.GetCellDataToStr(stepRow, "bom_barcode");
                        }

                        worksheet.Cells[rowindex, 1].Value = "装配物料" + (j + 1);//left

                        if (st_typeid == "37")
                        {
                            worksheet.Cells[rowindex, 2].Value = step_plccode;
                            worksheet.Cells[rowindex, 3].Value = bom_storename;
                            worksheet.Cells[rowindex, 4].Value = bom_barcode;
                        }

                        worksheet.Cells[rowindex, 7].Value = "装配物料" + (j + 1);//right

                        if (st_typeid == "38")
                        {
                            worksheet.Cells[rowindex, 8].Value = step_plccode;
                            worksheet.Cells[rowindex, 9].Value = bom_storename;
                            worksheet.Cells[rowindex, 10].Value = bom_barcode;
                        }
                    }
                }
            }
            package.Save();
        }
    }




    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}