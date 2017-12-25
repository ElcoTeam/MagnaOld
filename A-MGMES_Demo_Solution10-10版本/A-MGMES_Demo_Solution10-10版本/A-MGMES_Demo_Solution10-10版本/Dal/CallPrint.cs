using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Model;
using DAL;
using DbUtility;
using SortManagent.SortDao;
namespace SortManagent.Util
{
    public class CallPrint
    {

      public  static List<classess> classes;
      //  static MagnaDBEntities db = new MagnaDBEntities();
        /// <summary>
        /// 用于打印普通打印单
        /// </summary>
        /// <param name="dtlist">要打印的list</param>
        /// <param name="erweima">二维码</param>
        /// <param name="id">名称</param>
        /// <returns></returns>
        public static bool PrintM(List<GetIndex> dtlist, string erweima, string id,string title,bool isdou=true)
        {
            string DateTimeNow;
           // db = new MagnaDBEntities();         
            classes = px_MaterialsortprintingDAL.Querymg_classes().ToList();
            List<PrintModel> list = new List<PrintModel>();
            PrintClass t = new PrintClass();
            bool flag = false;
            OrderList oo = GetBtnClass.olst.FirstOrDefault(s => s.SortName == title);
            foreach (var item in dtlist)
            {
                oo.wlprintindex = oo.wlprintindex + 1;
                PrintModel pm = new PrintModel();
                pm.序号 = oo.wlprintindex;
                pm.车身号 = item.车身号.ToString();
                pm.车型 = item.等级.ToString();
                pm.主副驾 = item.主副驾.ToString();
                pm.零件号 = item.零件号;
                pm.数量 = "1";
                list.Add(pm);
            }
            DataTable dt=null;
          

            List<OrderList> ol = new List<OrderList>();
            foreach (var item in GetBtnClass.BtnClassList)
            {
                OrderList o = new OrderList();
                o.SortName = ToDataTable.getWLName(item.SortName);
                o.Ascordesc = item.Ascordesc;
                ol.Add(o);
            }
            bool flagAsc =  ol.FirstOrDefault(s => s.SortName.Equals(title)).Ascordesc;
            if (!flagAsc)
         
            {
                dt = ToDataTable.ListToDataTable(list.OrderBy(s => s.序号).ToList());
            }
            else
            {
                dt = ToDataTable.ListToDataTable(list.OrderByDescending(s => s.序号).ToList());
            }
           
          
            //if (id.IndexOf("前排")==-1)
            //{
            //    dt.Columns.Remove("主副驾");
            //}
            var InternetPrinter = px_InternetPrinterDAL.QueryInternetPrinterListForPart().ToList();
            for (int i = 0; i < InternetPrinter.Count; i++)
            {
                string name = InternetPrinter[i].IName.ToString();
                string Role = InternetPrinter[i].IRole.ToString();//此打印机名称要与控制面板中打印名称一致
                string[] Role2 = Role.Split(';');

                foreach (string j in Role2)
                {
                    string text = id;
                    if (j.Equals(text))
                    {
                        for (int k = 0; k < int.Parse(InternetPrinter[i].IRamark); k++)
                        {
                            flag = t.Print(dt, title, erweima, name);
                        }
                       

                    }
                }

            }
            if (flag && isdou)
            {
                string isprint_toStr = "";
                List<mg_PartOrde> mg_PartOrde = px_MaterialsortprintingDAL.mg_PartOrde().ToList();
                string aaa = "";
                foreach (var item in dtlist)
                {
                    px_PrintModel px = new px_PrintModel();
                    px.PXID = erweima;
                    px.orderid = item.车身号;
                    px.cartype = item.等级;
                    px.XF = item.主副驾;
                    px.LingjianHao = item.零件号;
                    px.sum = "1";
                    px.ordername = item.零件号描述;
                    px.dayintime = DateTime.Now;
                    px.printpxid = list.FirstOrDefault(s => s.主副驾 == item.主副驾 && s.车身号 == item.车身号 && s.零件号 == item.零件号).序号;                   
                    px_PrintDAL.Insertpx_Print(px);
                    //增加 相应生产线的物料打印后给mg_PartOrder表对应生产线数据行打印字段OrderIsPrint置1
                    //
                    System.Threading.Thread.Sleep(1);
                    DateTimeNow = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, 12);
                                  //DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "." + DateTime.Now.Millisecond;

                    isprint_toStr = mg_PartOrde.FirstOrDefault(s => s.id == item.PartOrderID.ToString()).OrderIsPrintSYS;
                    isprint_toStr = isprint_toStr + ";" + item.主副驾 + item.零件号描述;
                    string sql = "update mg_PartOrder set OrderIsPrintSYS='" + isprint_toStr
                        + "' where ID ='" + item.PartOrderID + "' ";                  

                    SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

                    aaa +=item.车身号+"    "+ item.主副驾 + item.零件号描述 + " "+DateTimeNow+"\r\n";
                    if (!item.零件号描述.Equals("坐垫骨架") && !item.零件号描述.Equals("靠背骨架"))
                    {
                        string sql1 = "update mg_PartOrder set OrderIsPrint='1', OrderPrintTime='" + DateTimeNow
                            + "' where ID ='" + item.PartOrderID + "' and OrderPrintTime is null ";
                        SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql1, null);                        
                    }
                    else
                    {//琉璃小屋
                       // db.Database.ExecuteSqlCommand("update mg_PartOrder set OrderIsPrint='1'  where ID ='" + item.PartOrderID + "' and OrderPrintTime is null ");
                    }
                }            
               
            }
            return flag;
        }
        /// <summary>
        /// 用于打印已打印存在订单
        /// </summary>
        /// <param name="dtlist"></param>
        /// <param name="erweima"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool PrintM(List<px_PrintModel> dtlist, string erweima, string id)
        {
            classes = px_MaterialsortprintingDAL.Querymg_classes();           
            DataTable dt = new DataTable();
            List<PrintModel> list = new List<PrintModel>();

            foreach (var item in dtlist)
            {
                PrintModel pm = new PrintModel();
                pm.序号 = dtlist.IndexOf(item);
                pm.车身号 = item.orderid.ToString();
                pm.车型 = item.cartype.ToString();
                pm.主副驾 = item.XF.ToString();
                pm.零件号 = item.LingjianHao;
                pm.数量 = item.sum;
                list.Add(pm);
            }
            dt = ToDataTable.ListToDataTable<PrintModel>(list);
            string re = "";

            PrintClass t = new PrintClass();
            var InternetPrinter = px_InternetPrinterDAL.QueryInternetPrinterListForPart().ToList();
            for (int i = 0; i < InternetPrinter.Count; i++)
            {
                string name = InternetPrinter[i].IName.ToString();
                string Role = InternetPrinter[i].IRole.ToString();//此打印机名称要与控制面板中打印名称一致
                string[] Role2 = Role.Split(';');
                foreach (string j in Role2)
                {
                    string text = id;
                    if (j.Equals(text))
                    {
                        t.Print(dt, text, erweima, name);
                    }
                }
            }
            if (re.Equals("s"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PrintCD(List<CDDYPRINTTABLE> pxt, List<GetIndex> pxlist, string erweima, string id,bool isdu=true)
        {
            classes = px_MaterialsortprintingDAL.Querymg_classes();          
            List<PrintModel> list = new List<PrintModel>();
            PrintClass t = new PrintClass();
            bool flag = false;
           
            DataTable dt = ToDataTable.ListToDataTable(pxt);
            var InternetPrinter = px_InternetPrinterDAL.QueryInternetPrinterListForPart().ToList();
            for (int i = 0; i < InternetPrinter.Count; i++)
            {
                string name = InternetPrinter[i].IName.ToString();
                string Role = InternetPrinter[i].IRole.ToString();//此打印机名称要与控制面板中打印名称一致
                string[] Role2 = Role.Split(';');

                foreach (string j in Role2)
                {
                    string text = id;
                    if (j.Equals(text))
                    {
                        flag = t.Print(dt, id, erweima, name);
                    }
                }

            }
            if (flag&& isdu)
            {
                foreach (var item in pxlist)
                {
                    px_PrintModel px = new px_PrintModel();
                    px.PXID = erweima;
                    px.orderid = item.订单号;
                    px.cartype = item.等级;
                    px.XF = item.主副驾;
                    px.LingjianHao = item.零件号;
                    px.sum = "1";
                    px.ordername = item.零件号描述+"(插单)";
                    px.dayintime = DateTime.Now;
                    px.printpxid = (pxlist.IndexOf(item)+1);
                    px_PrintDAL.Insertpx_Print(px);
                   
                }
              

            }
            return flag;
        }
    }
}