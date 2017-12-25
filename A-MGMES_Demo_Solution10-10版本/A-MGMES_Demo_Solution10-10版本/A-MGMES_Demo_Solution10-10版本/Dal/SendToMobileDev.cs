using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using SortManagent.Util;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Model;
using DAL;
using SortManagent.SortDao;
namespace SortManagent.Util
{
    public class SendToMobileDev
    {

        public static bool istherd = true;
      //  private MagnaDBEntities db;
        private DataTable dddd = new DataTable();
        GetModel getmodel = new GetModel();
        List<WlFlag> boolflag = new List<WlFlag>();

        public class WlFlag {
            public string name { get; set; }
            public bool flag { get; set; }

        }
        private int processtimesse = 28;
        public SendToMobileDev() {
            boolflag.Add(new WlFlag() { flag = true, name = "坐垫面套" });
            boolflag.Add(new WlFlag() { flag = true, name = "靠背面套" });
            boolflag.Add(new WlFlag() { flag = true, name = "坐垫骨架" });
            boolflag.Add(new WlFlag() { flag = true, name = "靠背骨架" });
            boolflag.Add(new WlFlag() { flag = true, name = "线束" });
            boolflag.Add(new WlFlag() { flag = true, name = "大背板" });
            boolflag.Add(new WlFlag() { flag = true, name = "40靠背" });
            boolflag.Add(new WlFlag() { flag = true, name = "40侧头枕" });
            boolflag.Add(new WlFlag() { flag = true, name = "后排中央扶手" });
            boolflag.Add(new WlFlag() { flag = true, name = "后排中央头枕" });
            boolflag.Add(new WlFlag() { flag = true, name = "60靠背" });
            boolflag.Add(new WlFlag() { flag = true, name = "60侧头枕" });
            boolflag.Add(new WlFlag() { flag = true, name = "后坐垫" });
            string webroot = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            StreamReader sr = new StreamReader(webroot+"\\settime.txt", System.Text.Encoding.Default);
            string line = sr.ReadLine();
            if(line!=null && line.Length>0)
            {
                try
                {
                    processtimesse = int.Parse(line.Trim());
                }catch{}
            }

        }
        public void AutoDo_HistoryData()
        {
            bool isHistroyCar = true;
            string carID, resultljh, abb, carID_NoScan = "";
            List<string> sqlUp = new List<string>();
            List<string> carID_His = new List<string>();
            List<string> car_abb_IsHis = new List<string>();
            DateTime dt_DBDateTime, newDateTime;
            dt_DBDateTime = DateTime.Now;
         
            List<View_px_AllList_DoHis> classes = px_SendToMobileDevDAL.QueryView_px_AllList_DoHisList().ToList();
            string carID_Temp="";
            foreach (View_px_AllList_DoHis vaHis in classes)
            {
                carID = vaHis.车身号;
                //未扫描车
                if (carID_NoScan.Equals(carID))
                    continue;

                abb = this.GetStr(vaHis.abb);
                resultljh = this.GetStr(vaHis.resultljh);
                //换车了
                if (!carID_Temp.Equals(carID) && !carID_Temp.Equals(""))
                { 
                    if (isHistroyCar)
                        carID_His.Add(carID_Temp);
                    isHistroyCar = true;
                    carID_NoScan = "";
                    car_abb_IsHis.Clear();
                }
                carID_Temp = carID;

                if (resultljh.Length > 5)
                {
                    if (!car_abb_IsHis.Contains(abb))
                        car_abb_IsHis.Add(abb);
                }
                else if (car_abb_IsHis.Contains(abb))
                    ;
                else
                {
                    isHistroyCar = false;
                    carID_NoScan = carID;
                }
            }
            if (!carID_Temp.Equals("") && isHistroyCar && !carID_His.Contains(carID_Temp))
                carID_His.Add(carID_Temp);

            foreach (string caridStr in carID_His)
            {
                px_SendToMobileDevDAL.update_mg_CustomerOrder_3_OrderIsHistory(caridStr);                
            }

            newDateTime = DateTime.Now;
            TimeSpan ts = newDateTime.Subtract(dt_DBDateTime);//日期差计算(C#)
        }

        private string GetStr(object str)
        {
            string st = "";
            if (str != null)
                st = str.ToString();
            return st;
        }
        public void AutoPrintSent_autoSendPrinted()
        {
            string CreateTime = "";
            DateTime dt_DBDateTime,newDateTime;
            //离现在最远 未打印OrderIsPrint = 0 整个订单未打印
            string sql = @"SELECT     TOP (1) CONVERT(int, CustomerOrder.OrderID) AS ID, CONVERT(int, p.ID) AS PartOrderID, CONVERT(nvarchar, CustomerOrder.OrderID) AS 订单号, 
                      CustomerOrder.SerialNumber AS 车身号, CONVERT(nvarchar, p.OrderIsPrint) AS IsPrint, CONVERT(nvarchar, CustomerOrder.CreateTime) AS CreateTime
FROM         dbo.mg_CustomerOrder_3 AS CustomerOrder INNER JOIN
                      dbo.mg_Customer_Product AS Customer_Product ON CustomerOrder.OrderID = Customer_Product.CustomerOrderID INNER JOIN
                      dbo.mg_PartOrder AS p ON Customer_Product.ID = p.CustomerProductID
WHERE     (CustomerOrder.OrderType = 1 OR
                      CustomerOrder.OrderType = 2) AND (p.OrderIsPrint = '0' or p.OrderIsPrint is null)
ORDER BY CustomerOrder.CreateTime ";//DESC


            List<mg_CustomerOrder_3_CreateTime> classes = px_SendToMobileDevDAL.AutoPrintSent_autoSendPrintedList().ToList();
            if (classes != null && classes.Count>0)
            {
                CreateTime = classes[0].CreateTime;
                //比较差半小时
                dt_DBDateTime = DateTime.Parse(CreateTime);
                newDateTime= DateTime.Now;
                TimeSpan ts= newDateTime.Subtract(dt_DBDateTime);//日期差计算(C#)
                if (ts.Hours > 0 || ts.Days > 0 || ts.Minutes > processtimesse)
                    this.AutoPrintSent_All();
            }

        }
        /// <summary>
        /// px_Print获取所有未打印集合  进行打印
        /// </summary>
        private void AutoPrintSent_autoSendPrintedDo()
        {
            List<string> pxID_List = new List<string>();
            //获取所有未打印集合
            string strKey = null;
            var pp = px_PrintDAL.Querypx_PrintList().Where(s => s.isauto == strKey).ToList();
            //var pp = db.px_Print.Where(s => s.PXID == "").ToList();
            foreach (var item in pp)
            {
                string pxID = item.PXID;
                string ordername = item.ordername;

                if (pxID_List.Contains(pxID))
                    continue;
                switch (ordername)
                {
                    case "坐垫面套":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫面套").IsAutoSend == true)
                            Send("前排坐垫面套", pxID);
                        break;

                    case "靠背面套":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背面套").IsAutoSend == true)
                            Send("前排靠背面套", pxID);
                        break;

                    case "坐垫骨架":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫骨架").IsAutoSend == true)
                            Send("前排坐垫骨架", pxID);
                        break;

                    case "靠背骨架":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背骨架").IsAutoSend == true)
                            Send("前排靠背骨架", pxID);
                        break;

                    case "线束":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "线束").IsAutoSend == true)
                            Send("前排线束", pxID);
                        break;

                    case "大背板":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "大背板").IsAutoSend == true)
                            Send("前排大背板", pxID);
                        break;

                    case "40靠背":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40靠背").IsAutoSend == true)
                            Send("后40靠背面套", pxID);
                        break;

                    case "40侧头枕":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40侧头枕").IsAutoSend == true)
                            Send("后40侧头枕", pxID);
                        break;

                    case "后排中央扶手":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央扶手").IsAutoSend == true)
                            Send("后60扶手", pxID);
                        break;

                    case "后排中央头枕":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央头枕").IsAutoSend == true)
                            Send("后60中头枕", pxID);
                        break;

                    case "60靠背":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60靠背").IsAutoSend == true)
                            Send("后60靠背面套", pxID);
                        break;

                    case "60侧头枕":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60侧头枕").IsAutoSend == true)
                            Send("后60侧头枕", pxID);
                        break;

                    case "后坐垫":
                        if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后坐垫").IsAutoSend == true)
                            Send("后坐垫坐垫面套", pxID);
                        break;

                }


                pxID_List.Add(pxID);
            }
        }

        private List<GetIndex> Getmg_partorder_ordertype_ProcessData(List<GetIndex> list, int number)
        {
            List<GetIndex> searchlist = new List<GetIndex>();
            searchlist = list.Where(s => s.mg_partorder_ordertype == 4).ToList();
            if (number == -1)
                searchlist.AddRange(list.ToList());
            else
            {
                if (searchlist != null && searchlist.Count > 0)
                    searchlist.AddRange(list.Take(number - searchlist.Count).ToList());
                else
                    searchlist.AddRange(list.Take(number).ToList());
            }
            return searchlist;

        }

        public void AutoPrintSent()
        {
            //获取所有未打印集合
            //var listindex = getmodel.GetListModels().Where(s => s.IsPrint == 0).ToList();

            List<px_PanrameterModel> panrameter = null;
            try
            {
                panrameter = px_PanrameterDAL.QueryPanrameterListForPart().ToList();
            }
            catch { Thread.Sleep(200); panrameter = px_PanrameterDAL.QueryPanrameterListForPart().ToList(); }
            List<GetIndex> list = null;
            var internetprintnamelist = px_InternetPrinterDAL.QueryInternetPrinterListForPart().ToList();
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫面套").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "坐垫面套");
                if (wf.flag)
                {
                    wf.flag = false;
                    int number = panrameter.FirstOrDefault(s => s.Name == "坐垫面套").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫面套", "前排").ToList();
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排坐垫面套", ToDataTable.getWLName("坐垫面套", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫面套").IsAutoSend)
                            {
                                Send("前排坐垫面套", erweima);
                            }

                        }
                    }
                }


            }

            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背面套").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "靠背面套");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "靠背面套").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "前排");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排靠背面套", ToDataTable.getWLName("靠背面套", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背面套").IsAutoSend)
                            {
                                Send("前排靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫骨架").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "坐垫骨架");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "坐垫骨架").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫骨架", "前排");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);
                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排坐垫骨架", ToDataTable.getWLName("坐垫骨架", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫骨架").IsAutoSend)
                            {
                                Send("前排坐垫骨架", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背骨架").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "靠背骨架");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "靠背骨架").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背骨架", "前排");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);
                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排靠背骨架", ToDataTable.getWLName("靠背骨架", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背骨架").IsAutoSend)
                            {
                                Send("前排靠背骨架", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "线束").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "线束");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "线束").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("线束", "前排");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排线束", ToDataTable.getWLName("线束", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "线束").IsAutoSend)
                            {
                                Send("前排线束", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "大背板").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "大背板");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "大背板").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("大背板", "前排");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排大背板", ToDataTable.getWLName("大背板", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "大背板").IsAutoSend)
                            {
                                Send("前排大背板", erweima);
                            }
                        }
                    }
                }
            }

            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40靠背").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "40靠背");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "40靠背").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "后40");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后40靠背面套", ToDataTable.getWLName("靠背面套", "后40"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40靠背").IsAutoSend)
                            {
                                Send("后40靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40侧头枕").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "40侧头枕");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "40侧头枕").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("侧头枕", "后40");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后40侧头枕", ToDataTable.getWLName("侧头枕", "后40"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40侧头枕").IsAutoSend)
                            {
                                Send("后40侧头枕", erweima);
                            }
                        }
                    }
                }
            }



            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央扶手").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "后排中央扶手");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "后排中央扶手").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("扶手", "后60");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后60扶手", ToDataTable.getWLName("扶手", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央扶手").IsAutoSend)
                            {
                                Send("后60扶手", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央头枕").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "后排中央头枕");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "后排中央头枕").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("中头枕", "后60");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "后60中头枕", ToDataTable.getWLName("中头枕", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央头枕").IsAutoSend)
                            {
                                Send("后60中头枕", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60靠背").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "60靠背");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "60靠背").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "后60");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后60靠背面套", ToDataTable.getWLName("靠背面套", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60靠背").IsAutoSend)
                            {
                                Send("后60靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60侧头枕").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "60侧头枕");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "60侧头枕").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("侧头枕", "后60");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "后60侧头枕", ToDataTable.getWLName("侧头枕", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60侧头枕").IsAutoSend)
                            {
                                Send("后60侧头枕", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后坐垫").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "后坐垫");
                if (wf.flag)
                {
                    wf.flag = false;


                    int number = panrameter.FirstOrDefault(s => s.Name == "后坐垫").Number;
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫面套", "后坐垫");
                    if (list != null && list.Count >= number)
                    {
                        list = Getmg_partorder_ordertype_ProcessData(list, number);

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后坐垫坐垫面套", ToDataTable.getWLName("坐垫面套", "后坐垫"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后坐垫").IsAutoSend)
                            {
                                Send("后坐垫坐垫面套", erweima);
                            }
                        }
                    }
                }
            }


            int autocount=  GetBtnClass.BtnClassList.Where(s => s.IsAutoPrint == true).ToList().Count();
            int wfcount = boolflag.Where(s => s.flag == false).ToList().Count();
            if (autocount == wfcount)  
            {
                foreach (var item in boolflag)
                {
                    item.flag = true;
                }
            }


        }

        public void AutoPrintSent_All()
        {
          
            //获取所有未打印集合
           // var listindex = getmodel.GetListModels().Where(s => s.IsPrint == 0).ToList();

            List<px_PanrameterModel> panrameter = null;
            try
            {
                panrameter =px_PanrameterDAL.QueryPanrameterListForPart().ToList();
            }
            catch { Thread.Sleep(200); panrameter = px_PanrameterDAL.QueryPanrameterListForPart().ToList(); }
            List<GetIndex> list = null;
            var internetprintnamelist = px_InternetPrinterDAL.QueryInternetPrinterListForPart().ToList();
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫面套").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "坐垫面套");
                if (true)
                {
                    wf.flag = false;




                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫面套", "前排").ToList();
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {
                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排坐垫面套", ToDataTable.getWLName("坐垫面套", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫面套").IsAutoSend)
                            {
                                Send("前排坐垫面套", erweima);
                            }

                        }
                    }
                }


            }

            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背面套").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "靠背面套");
                if (true)
                {
                    wf.flag = false;


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "前排");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {
                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排靠背面套", ToDataTable.getWLName("靠背面套", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背面套").IsAutoSend)
                            {
                                Send("前排靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫骨架").IsAutoPrint == true)
            {
                WlFlag wf = boolflag.FirstOrDefault(s => s.name == "坐垫骨架");
                if (true)
                {
                    wf.flag = false;


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫骨架", "前排");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {
                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排坐垫骨架", ToDataTable.getWLName("坐垫骨架", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "坐垫骨架").IsAutoSend)
                            {
                                Send("前排坐垫骨架", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背骨架").IsAutoPrint == true)
            {
                if (true)
                {

                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背骨架", "前排");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {
                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排靠背骨架", ToDataTable.getWLName("靠背骨架", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "靠背骨架").IsAutoSend)
                            {
                                Send("前排靠背骨架", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "线束").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("线束", "前排");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "前排线束", ToDataTable.getWLName("线束", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "线束").IsAutoSend)
                            {
                                Send("前排线束", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "大背板").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("大背板", "前排");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "前排大背板", ToDataTable.getWLName("大背板", "前排"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "大背板").IsAutoSend)
                            {
                                Send("前排大背板", erweima);
                            }
                        }
                    }
                }
            }

            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40靠背").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "后40");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后40靠背面套", ToDataTable.getWLName("靠背面套", "后40"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40靠背").IsAutoSend)
                            {
                                Send("后40靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40侧头枕").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("侧头枕", "后40");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后40侧头枕", ToDataTable.getWLName("侧头枕", "后40"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "40侧头枕").IsAutoSend)
                            {
                                Send("后40侧头枕", erweima);
                            }
                        }
                    }
                }
            }



            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央扶手").IsAutoPrint == true)
            {
                if (true)
                {
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("扶手", "后60");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后60扶手", ToDataTable.getWLName("扶手", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央扶手").IsAutoSend)
                            {
                                Send("后60扶手", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央头枕").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("中头枕", "后60");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "后60中头枕", ToDataTable.getWLName("中头枕", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后排中央头枕").IsAutoSend)
                            {
                                Send("后60中头枕", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60靠背").IsAutoPrint == true)
            {
                if (true)
                {


                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("靠背面套", "后60");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后60靠背面套", ToDataTable.getWLName("靠背面套", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60靠背").IsAutoSend)
                            {
                                Send("后60靠背面套", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60侧头枕").IsAutoPrint == true)
            {
                if (true)
                {
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("侧头枕", "后60");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {

                            bool flag = CallPrint.PrintM(list, erweima, "后60侧头枕", ToDataTable.getWLName("侧头枕", "后60"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "60侧头枕").IsAutoSend)
                            {
                                Send("后60侧头枕", erweima);
                            }
                        }
                    }
                }
            }
            if (GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后坐垫").IsAutoPrint == true)
            {
                if (true)
                {
                    string erweima = getpara();
                    list = getmodel.GetIndexWJ("坐垫面套", "后坐垫");
                    list = Getmg_partorder_ordertype_ProcessData(list, -1);
                    if (list != null && list.Count >= 0)
                    {

                        if (list.Count > 0)
                        {
                            bool flag = CallPrint.PrintM(list, erweima, "后坐垫坐垫面套", ToDataTable.getWLName("坐垫面套", "后坐垫"));
                            if (flag && GetBtnClass.BtnClassList.FirstOrDefault(s => s.SortName == "后坐垫").IsAutoSend)
                            {
                                Send("后坐垫坐垫面套", erweima);
                            }
                        }
                    }
                }
            }


            //int autocount = GetBtnClass.BtnClassList.Where(s => s.IsAutoPrint == true).ToList().Count();
            //int wfcount = boolflag.Where(s => s.flag == false).ToList().Count();
            //if (autocount == wfcount)
            //{
            //    foreach (var item in boolflag)
            //    {
            //        item.flag = true;
            //    }
            //}


        }

        
        string getpara()
        {

            Random r = new Random();
            Random r1 = new Random();
            return r.Next(100000000, 999999999).ToString() + r1.Next(100, 999).ToString();

        }

        public bool Send(String TableName, string id)
        {
            bool flag = false;
            List<px_ShowChiClientModel> ChiClient = new List<px_ShowChiClientModel>(50);
            List<px_ShowChiClientModel> SelectClient = new List<px_ShowChiClientModel>(50);
            foreach (var Send in TcpStart.userList)
            {

                String[] Ip = Send.client.Client.RemoteEndPoint.ToString().Split(':');
                var ip = Ip[0];

                var ClientIP = px_ShowChiClientDAL.Querypx_ShowChiClientListForPart().FirstOrDefault(s => s.ClientIP.Equals(ip));
                if (ClientIP!=null)
                {
                    ChiClient.Add(new px_ShowChiClientModel
                    {    SID = ClientIP.SID,
                        SName = ClientIP.SName.ToString(),
                        SRole = ClientIP.SRole.ToString(),
                        ClientIP = Ip[0]
                    });
                }
                else
                {
                    flag = false;
                    return flag;
                }
              


            }
            for (int i = 0; i < ChiClient.Count; i++)
            {
                String[] Role = ChiClient[i].SRole.Split(';');
                for (int u = 0; u < Role.Count(); u++)
                {
                    if (Role[u] == TableName)
                    {
                        SelectClient.Add(ChiClient[i]);
                        continue;
                    }
                }

            }

            if (SelectClient != null)
            {
                for (int c = 0; c < SelectClient.Count; c++)
                {
                    foreach (var Send in TcpStart.userList)
                    {
                        String[] Ip = Send.client.Client.RemoteEndPoint.ToString().Split(':');


                        if (SelectClient[c].ClientIP == Ip[0])
                        {
                            if (TcpStart.SendToClient(Send, id).Contains("发送信息成功"))
                                flag = true;
                            else
                                flag = false;
                        }
                    }
                }
            }


            if (flag)
            {
                var pp = px_PrintDAL.Querypx_PrintList().Where(s => s.PXID == id).ToList();
                foreach (var item in pp)
                {
                    string shouchiyiid = "";
                    foreach (var chiyi in SelectClient)
                    {
                        shouchiyiid += chiyi.SID + ";";
                    }
                    item.isauto = shouchiyiid;
                    item.resultljh = "✖";
                    item.SFlag = "✖ ";
                }
                px_PrintDAL.updateIsSendOkbyPXID("1",id);               
            }
            else//后天见
            {
                string SRole="";
                var pp = px_PrintDAL.Querypx_PrintList().Where(s => s.PXID == id).ToList();
                foreach (var item in pp)
                {
                    string shouchiyiid = "";
                    switch(item.XF+"-"+item.ordername)
                    {
                        case "主驾-坐垫骨架":
                        case "副驾-坐垫骨架":
                            SRole = "前排坐垫骨架";
                         break;
                        case "主驾-靠背骨架":
                        case "副驾-靠背骨架":
                         SRole = "前排靠背骨架";
                         break;
                        case "主驾-坐垫面套":
                        case "副驾-坐垫面套":
                         SRole = "前排坐垫面套";
                         break;
                        case "主驾-靠背面套":
                        case "副驾-靠背面套":
                         SRole = "前排靠背面套";
                         break;
                        case "主驾-线束":
                        case "副驾-线束":
                         SRole = "前排线束";
                         break;
                        case "主驾-大背板":
                        case "副驾-大背板":
                         SRole = "前排大背板";
                         break;
                        case "后40-靠背面套":
                         SRole = "后40靠背面套";
                         break;
                        case "后60-靠背面套":
                         SRole = "后60靠背面套";
                         break;
                        case "后坐垫-坐垫面套":
                         SRole = "后坐垫坐垫面套";
                         break;
                        case"后60-扶手":
                         SRole="后60扶手";
                         break;
                        case"后60-中头枕":
                         SRole="后60中头枕";
                         break;
                        case"后40-侧头枕":
                         SRole="后40侧头枕";
                         break;
                        case"后60-侧头枕":
                         SRole="后60侧头枕";
                         break;
                    }
                    var vaa = px_ShowChiClientDAL.Querypx_ShowChiClientListForPart().Where(s => s.SRole.IndexOf(SRole) != -1).ToList();
                    foreach (var chiyi in vaa)
                    {
                        shouchiyiid += chiyi.SID + ";";
                    }
                    item.isauto = shouchiyiid;
                    item.resultljh = "✖";
                    item.SFlag = "✖ ";
                }
                px_PrintDAL.updateIsSendOkbyPXID("0", id);             

            }
            return flag;
        }
    }
}






