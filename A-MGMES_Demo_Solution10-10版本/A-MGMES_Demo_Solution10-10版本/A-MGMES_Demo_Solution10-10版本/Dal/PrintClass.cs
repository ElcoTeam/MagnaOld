using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Data;
using System;
using ZXing.Common;
using ZXing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Model;
using System.Linq;
using System.Collections;
using DAL;
using SortManagent.SortDao;
namespace SortManagent.Util
{
    public class PrintClass : System.Web.UI.Page
    {
        EncodingOptions options = null;
        BarcodeWriter writer = null;
        MemoryStream ms;
        Image img;
        Image img_abc;
        List<Image> img_abc_s = new List<Image>();
        //打印日期 
        string datatime = DateTime.Now.ToString("yyyy年MM月dd日");


        //以下用户可自定义
        //当前要打印文本的字体及字号
        private static Font TableColoumsFont = new Font("Verdana", 11, FontStyle.Bold);
        private static Font TableFont = new Font("Verdana", 10, FontStyle.Regular);
        //表头字体
        private Font HeadFont = new Font("Verdana", 20, FontStyle.Bold);
        private Font PaiXu = new Font("Verdana", 14);
        private Font RiQi = new Font("Verdana", 12);
        //表头文字
        private string HeadText = string.Empty;
        //表头高度
        private int HeadHeight = 40;
        //表的基本单位
        private float[] XUnit;
        private int YUnit = TableFont.Height * 2;
        //以下为模块内部使用
        private PrintDocument DataTablePrinter;
        private DataRow DataGridRow;
        private DataTable DataTablePrint;
        //当前要所要打印的记录行数,由计算得到
        private int PageRecordNumber;
        //正要打印的页号
        private int PrintingPageNumber = 1;
        //已经打印完的记录数
        private int PrintRecordComplete;
        private float PLeft;
        private int PTop;
        private int PRight;
        private int PBottom;
        private int PWidth;
        private int PHeigh;
        //当前画笔颜色
        private SolidBrush DrawBrush = new SolidBrush(Color.Black);
        //每页打印的记录条数
        private int PrintRecordNumber;
        //第一页打印的记录条数
        private int FirstPrintRecordNumber;
        //总共应该打印的页数
        private int TotalPage;
        //与列名无关的统计数据行的类目数（如，总计，小计......）
        public int TotalNum = 0;
        string[] chadan;
        Hashtable image_abcCartype_zhu = new Hashtable();
        Hashtable image_abcCartype_fu = new Hashtable();
        Hashtable image_abcCartype_houpai = new Hashtable();

        private string GetImage_abc(DataTable dt)
        {
            string image_abc = "";          
            List<GetIndex> pxlist = new List<GetIndex>();
            List<CDDYPRINTTABLE> listddytable = new List<CDDYPRINTTABLE>();
            //正常打印
            if (image_abcCartype_zhu == null || image_abcCartype_zhu.Count == 0)
            {

                var AllModelList = px_MaterialsortprintingDAL.GetProductPrintID().ToList();
                foreach (var item in AllModelList)
                {
                    if (item.ProductType == 1 && !image_abcCartype_zhu.ContainsKey(item.ProductName))
                        image_abcCartype_zhu.Add(item.ProductName, item.ProductPrintID);
                    else if (item.ProductType == 2 && !image_abcCartype_fu.ContainsKey(item.ProductName))
                        image_abcCartype_fu.Add(item.ProductName, item.ProductPrintID);
                    else if (!image_abcCartype_houpai.ContainsKey(item.ProductName))
                        image_abcCartype_houpai.Add(item.ProductName, item.ProductPrintID);

                }
            }
            string xf, cartype;
            if (dt.Rows!=null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    xf = item["主副驾"].ToString().Trim();
                    cartype = item["车型"].ToString().Trim();
                    if (xf.Equals("主驾"))
                        image_abc += image_abcCartype_zhu[cartype].ToString().Trim();
                    else if (xf.Equals("副驾"))
                        image_abc += image_abcCartype_fu[cartype].ToString().Trim();
                    else
                        image_abc += image_abcCartype_houpai[cartype].ToString().Trim();
                }
            }
            return image_abc;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="Title">打印文件的标题</param>
        public bool Print(DataTable dt, string Title, string paixudanhao, string dayinjimingcheng, params string[] chadan)
        {
            options = new EncodingOptions
            {
                Width = 300,
                Height = 60
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options = options;
            Bitmap bitmap = writer.Write(paixudanhao);
            img = Image.FromHbitmap(bitmap.GetHbitmap());

            string paixudanhao_abc = GetImage_abc( dt);
            if (false && paixudanhao_abc.Length > 78)
            //paixudanhao_abc = paixudanhao_abc.Substring(0, 78);
            {
                options = new EncodingOptions
                {
                    Width = 100,
                    Height = 100
                };
                string paixudanhao_abc_s = ""; int abci = 0;
                for (int i = 0; i <= paixudanhao_abc.Length / 78; i++)
                {
                    if (i != 0)
                        abci = i * 78 - 1;
                    if (i == paixudanhao_abc.Length / 78)
                        paixudanhao_abc_s = paixudanhao_abc.Substring(abci);
                    else
                        paixudanhao_abc_s = paixudanhao_abc.Substring(abci, 78);

                    writer = new BarcodeWriter();
                    writer.Format = BarcodeFormat.QR_CODE;
                    writer.Options = options;
                    Bitmap bitmap_abc = writer.Write(paixudanhao_abc_s);
                    Image img_abcS = Image.FromHbitmap(bitmap_abc.GetHbitmap());

                    img_abc_s.Add(img_abcS);
                }
            }
            else
            {
                options = new EncodingOptions
                {
                    Width = 100,
                    Height = 100
                };
                writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                writer.Options = options;
                Bitmap bitmap_abc = writer.Write(paixudanhao_abc);
                img_abc = Image.FromHbitmap(bitmap_abc.GetHbitmap());
            }
            var re = CreatePrintDocument(dt, Title, dayinjimingcheng, chadan);
            if (re != null)
            {
                re.Print();
                return true;

            }
            else
            { return false; }



        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="dt">要打印的DataTable</param>
        /// <param name="Title">打印文件的标题</param>
        public string PrintPriview(DataTable dt, string Title, string paixudanhao, string dayinjimingcheng, params string[] chada)
        {

            var re = "";
            try
            {

                options = new EncodingOptions
                {
                    //DisableECI = true,  
                    //CharacterSet = "UTF-8",  
                    Width = 300,
                    Height = 60
                };
                writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.CODE_128;
                writer.Options = options;
                Bitmap bitmap = writer.Write(paixudanhao.ToString());
                img = Image.FromHbitmap(bitmap.GetHbitmap());

                string paixudanhao_abc = GetImage_abc(dt);
                if (paixudanhao_abc.Length > 78)
                    paixudanhao_abc = paixudanhao_abc.Substring(0, 78);
                options = new EncodingOptions
                {
                    Width = 100,
                    Height = 100
                };
                writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                writer.Options = options;
                Bitmap bitmap_abc = writer.Write(paixudanhao_abc);
                img_abc = Image.FromHbitmap(bitmap_abc.GetHbitmap());

            }
            catch (Exception a)
            {

                re = "f";
            }
            try
            {

                PrintPreviewDialog PrintPriview = new PrintPreviewDialog();
                PrintPriview.Document = CreatePrintDocument(dt, Title, dayinjimingcheng, chada);
                PrintPriview.WindowState = FormWindowState.Maximized;
                PrintPriview.ShowDialog();

                //在这里将数据插入到数据里 
                //拼写sql语句 入库就ok
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                System.Web.HttpContext.Current.Response.Redirect("../Edit/" + paixudanhao);
                re = "f";
            }
            if (re.Equals("f"))
            {
                return "f";
            }
            else
            {
                return "s";
            }
        }
        string dayinji;
        /// <summary>
        /// 创建打印文件
        /// </summary>
        private PrintDocument CreatePrintDocument(DataTable dt, string Title, string dayinjimingcheng, params string[] chadan)
        {
            try
            {
                this.dayinji = dayinjimingcheng;
                this.chadan = chadan;
                DataTablePrint = dt;
                HeadText = Title;
                DataTablePrinter = new PrintDocument();
                DataTablePrinter.PrinterSettings.PrinterName = dayinjimingcheng;
                PageSetupDialog PageSetup = new PageSetupDialog();
                PageSetup.Document = DataTablePrinter;
                DataTablePrinter.DefaultPageSettings = PageSetup.PageSettings;
                DataTablePrinter.DefaultPageSettings.Landscape = false;//设置打印横向还是纵向
                                                                       // DataTablePrinter.DefaultPageSettings.Landscape = true;//设置打印横向还是纵向
                                                                       //PLeft = 30; //DataTablePrinter.DefaultPageSettings.Margins.Left;
                PTop = DataTablePrinter.DefaultPageSettings.Margins.Top;
                //PRight = DataTablePrinter.DefaultPageSettings.Margins.Right;
                PBottom = DataTablePrinter.DefaultPageSettings.Margins.Bottom;
                PWidth = DataTablePrinter.DefaultPageSettings.Bounds.Height;
                PHeigh = DataTablePrinter.DefaultPageSettings.Bounds.Width;
                XUnit = new float[DataTablePrint.Columns.Count];

                PrintRecordNumber = Convert.ToInt32((PWidth - PTop - PBottom - YUnit) / YUnit);
                FirstPrintRecordNumber = Convert.ToInt32((PWidth - PTop - 150 - PBottom - HeadHeight - YUnit) / YUnit);

                //if (PrintRecordNumber > 1)
                //    PrintRecordNumber = PrintRecordNumber - 1;
                if (FirstPrintRecordNumber >1 )
                    FirstPrintRecordNumber = FirstPrintRecordNumber - 1;

                if (DataTablePrint.Rows.Count > FirstPrintRecordNumber)
                {
                    if ((DataTablePrint.Rows.Count - FirstPrintRecordNumber) % PrintRecordNumber == 0)
                    {
                        TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 1;
                    }
                    else
                    {
                        TotalPage = (DataTablePrint.Rows.Count - FirstPrintRecordNumber) / PrintRecordNumber + 2;
                    }
                }
                else
                {
                    TotalPage = 1;
                    FirstPrintRecordNumber = DataTablePrint.Rows.Count;
                }
               
                DataTablePrinter.PrintPage += new PrintPageEventHandler(DataTablePrinter_PrintPage);
                DataTablePrinter.DocumentName = HeadText;

                return DataTablePrinter;
            }
            catch (Exception a)
            {
                return null;
            }

        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        private void DataTablePrinter_PrintPage(object sende, PrintPageEventArgs Ev)
        {


            float tableWith = 0;
            string ColumnText;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            //打印表格线格式
            Pen Pen = new Pen(Brushes.Black, 1);

            #region 设置列宽

            foreach (DataRow dr in DataTablePrint.Rows)
            {
                for (int i = 0; i < DataTablePrint.Columns.Count; i++)
                {
                    float colwidth = Ev.Graphics.MeasureString(dr[i].ToString().Trim(), TableFont).Width + 70;
                    if (colwidth > XUnit[i])
                    {
                        XUnit[i] = colwidth;
                    }
                }
            }

            if (PrintingPageNumber == 1)
            {
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();
                    int colwidth = Convert.ToInt32(Ev.Graphics.MeasureString(ColumnText, TableFont).Width);
                    if (colwidth > XUnit[Cols])
                    {
                        XUnit[Cols] = colwidth;
                    }
                }
            }
            for (int i = 0; i < XUnit.Length; i++)
            {
                tableWith += XUnit[i];
            }
            #endregion

            PLeft = (Ev.PageBounds.Width - tableWith) / 2;
            float x = PLeft;
            int y = PTop;
            int stringY = PTop + (YUnit - TableFont.Height) / 2;
            int rowOfTop = PTop;

            //第一页
            if (PrintingPageNumber == 1)
            {
                //打印表头
                Ev.Graphics.DrawString(HeadText + "--" + dayinji, HeadFont, DrawBrush, new Point(Ev.PageBounds.Width / 2, PTop), sf);
                Ev.Graphics.DrawString("排序单号：", PaiXu, DrawBrush, new Point(Ev.PageBounds.Width / 8 - 30, PTop + 100), sf);
                Ev.Graphics.DrawString("打印日期：" + datatime + "", RiQi, DrawBrush, new Point(Ev.PageBounds.Width - 160, PTop + 100), sf);
                string dt = DateTime.Now.ToString("yyyy-MM-dd ");
                string banci = "";


                foreach (var item in CallPrint.classes)
                {
                    DateTime statime = DateTime.Parse(item.cl_starttime.ToString("HH:mm:ss"));
                    DateTime endtime = DateTime.Parse(item.cl_endtime.ToString("HH:mm:ss"));
                    DateTime nowtime = DateTime.Parse(DateTime.Now.ToString("HH:mm:ss"));
                    if (item.cl_starttime<item.cl_endtime)
                    {
                        if (nowtime < endtime && nowtime > statime)
                        {
                            banci = item.cl_name;
                        }
                       
                    }
                    else if(item.cl_starttime > item.cl_endtime )
                    {
                        endtime= endtime.AddDays(1);
                        if (nowtime < endtime && nowtime > statime)
                        {
                            banci = item.cl_name;
                        }
                    }

                }
               
                
                Ev.Graphics.DrawString("班次：" + banci, RiQi, DrawBrush, new Point(Ev.PageBounds.Width - 160, PTop + 120), sf);
                Ev.Graphics.DrawImage(img, new Point(Ev.PageBounds.Width / 7 - 10, PTop + 85));
                if (img_abc_s.Count > 0)
                {
                    int i_s = 0;
                    foreach(Image img_s in img_abc_s)
                        Ev.Graphics.DrawImage(img_s, new Point(Ev.PageBounds.Width / 4 - 10 + i_s*108, PTop + 85));
                }
                else
                    Ev.Graphics.DrawImage(img_abc, new Point(Ev.PageBounds.Width / 2 - 10, PTop + 85));


                //try
                //{
                int i = chadan.Length;
                if (chadan.Length > 0)
                {
                    Ev.Graphics.DrawString("车身号：" + chadan[0] + "", RiQi, DrawBrush, new Point(Ev.PageBounds.Width / 8 - 30, PTop + 150), sf);
                    Ev.Graphics.DrawString("车  型：" + chadan[1] + "", RiQi, DrawBrush, new Point(Ev.PageBounds.Width - 160, PTop + 150), sf);
                }
                //}
                //catch (Exception )
                //{

                //    throw;
                //}



                //   PTop = PTop + 150;
                //设置为第一页时行数
                PageRecordNumber = FirstPrintRecordNumber;
                rowOfTop = y = PTop + 150 + HeadFont.Height + 10;
                stringY = PTop + 150 + HeadFont.Height + 10 + (YUnit - TableFont.Height) / 2;
            }
            else
            {
                //计算,余下的记录条数是否还可以在一页打印,不满一页时为假
                if (DataTablePrint.Rows.Count - PrintRecordComplete >= PrintRecordNumber)
                {
                    PageRecordNumber = PrintRecordNumber;
                }
                else
                {
                    PageRecordNumber = DataTablePrint.Rows.Count - PrintRecordComplete;
                }
            }

            #region 列名
            if (PrintingPageNumber == 1 || PageRecordNumber > TotalNum)//最后一页只打印统计行时不打印列名
            {
                //得到datatable的所有列名
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    ColumnText = DataTablePrint.Columns[Cols].ColumnName.ToString().Trim();

                    int colwidth = Convert.ToInt32(Ev.Graphics.MeasureString(ColumnText, TableColoumsFont).Width);
                    Ev.Graphics.DrawString(ColumnText, TableColoumsFont, DrawBrush, x + (XUnit[Cols] - colwidth) / 2, stringY);
                    x += XUnit[Cols];
                }
            }
            #endregion



            Ev.Graphics.DrawLine(Pen, PLeft, rowOfTop, x, rowOfTop);
            stringY += YUnit;
            y += YUnit;
            Ev.Graphics.DrawLine(Pen, PLeft, y, x, y);

            //当前页面已经打印的记录行数
            int PrintingLine = 0;
            while (PrintingLine < PageRecordNumber)
            {
                x = PLeft;
                //确定要当前要打印的记录的行号
                DataGridRow = DataTablePrint.Rows[PrintRecordComplete];
                for (int Cols = 0; Cols <= DataTablePrint.Columns.Count - 1; Cols++)
                {
                    float coloumswidth = Convert.ToInt32(Ev.Graphics.MeasureString(DataGridRow[Cols].ToString().Trim(), TableFont).Width);
                    Ev.Graphics.DrawString(DataGridRow[Cols].ToString().Trim(), TableFont, DrawBrush, x + (XUnit[Cols] - coloumswidth) / 2, stringY);
                    x += XUnit[Cols];
                }
                stringY += YUnit;
                y += YUnit;
                Ev.Graphics.DrawLine(Pen, PLeft, y, x, y);

                PrintingLine += 1;
                PrintRecordComplete += 1;
                if (PrintRecordComplete >= DataTablePrint.Rows.Count)
                {
                    Ev.HasMorePages = false;
                    PrintRecordComplete = 0;
                }
            }

            Ev.Graphics.DrawLine(Pen, PLeft, rowOfTop, PLeft, y);
            x = PLeft;

            for (int Cols = 0; Cols < DataTablePrint.Columns.Count; Cols++)
            {
                x += XUnit[Cols];
                Ev.Graphics.DrawLine(Pen, x, rowOfTop, x, y);
            }



            PrintingPageNumber += 1;

            if (PrintingPageNumber > TotalPage)
            {
                Ev.HasMorePages = false;
                PrintingPageNumber = 1;
                PrintRecordComplete = 0;
            }
            else
            {
                Ev.HasMorePages = true;
            }


        }

    }
}