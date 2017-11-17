using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Model;
using System.Reflection;
namespace website
{
    /// <summary>
    /// Services1002_WaringList 的摘要说明
    /// </summary>
    public class Services1002_WaringList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string StartTime = request["StartTime"];
            string EndTime = request["EndTime"];
            //2017-08-01 11:00:00
            //:00:00
            if (StartTime == ":00:00")
            {
                StartTime = DateTime.Now.ToString();
                EndTime = DateTime.Now.ToString();
            }
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(6, 6);
            dic.Add(1, 5);
            dic.Add(2, 4);
            dic.Add(3, 3);
            dic.Add(5, 2);
            dic.Add(4, 1);
            //报警级别现在是 612354 分别代表急停 物料 质量 维修 生产 超时 

             List<mg_alarm> allalarm ;
            //List<mg_alarm> excel;
            DataTable excel;

            //List<mg_alarm> allalarm = DataReader.getfsa(StartTime.ToString(), EndTime.ToString(),Convert.ToDateTime( StartTime));
            if (StartTime == null || EndTime == null)   //没什么用
            {
                allalarm = DataReader.getfsa();
                //excel = DataReader.getfsaexcel();
                excel = null;

            }
            else
            {
                string dt1 = Convert.ToDateTime(StartTime).ToString("yyyy-MM-dd")+" 00:00:00";
                string dt2 = Convert.ToDateTime(EndTime).ToString("yyyy-MM-dd") + " 23:59:59";
                allalarm = DataReader.getfsabydate(dt1, dt2);
                excel = DataReader.getfsabydateexcel(dt1, dt2);

            }
            foreach (mg_alarm item in allalarm)
            {
                item.AlarmType = dic[item.AlarmType];
            }
            //var q = from m in allalarm
            //        orderby m.AlarmType
            //        group m by m.AlarmStation into g
            //        select new mg_alarm{AlarmStation=g.Key,AlarmType=g.Max().AlarmType};
            //List<mg_alarm> alls = q.ToList<mg_alarm>();

            List<mg_alarm> alls = allalarm
                .GroupBy(t => t.AlarmStation)
                //.Select(t => new mg_alarm { AlarmStation = t.Key, AlarmType = t.Max(e => e.AlarmType) }).Where(t => t.AlarmType != 4) //原先是4
                .Select(t => new mg_alarm { AlarmStation = t.Key, AlarmType = t.Max(e => e.AlarmType) }) //上一层已经过滤掉4的了，所以...
                .ToList();

            List<mg_alarm> fsa = alls.Where(t => t.AlarmStation.Contains("FSA")).ToList();
            List<mg_alarm> fsb = alls.Where(t => t.AlarmStation.Contains("FSB")).ToList();
            List<mg_alarm> fsc = alls.Where(t => t.AlarmStation.Contains("FSC")).ToList();
            List<mg_alarm> rsb = alls.Where(t => t.AlarmStation.Contains("RSB")).ToList();
            List<mg_alarm> rsc = alls.Where(t => t.AlarmStation.Contains("RSC")).ToList();

            int fsacount = fsa.Count;
            int fsbcount = fsb.Count;
            int fsccount = fsc.Count;
            int rsbcount = rsb.Count;
            int rsccount = rsc.Count;

            int[] allal = new int[5] { fsacount, fsbcount, fsccount, rsbcount, rsccount };

            int max = allal.Max();


            List<mg_alarm> afteralarm = new List<mg_alarm>();
            mg_alarm alarm = new mg_alarm();
            alarm.AlarmStation = " ";
            alarm.AlarmType = 0;
            for (int i = 0; i < max; i++)
            {
                if (fsacount-1 < i)
                {
                    fsa.Add(alarm);
                }
                if (fsbcount-1 < i)
                {
                    fsb.Add(alarm);
                }
                if (fsccount-1 < i)
                {
                    fsc.Add(alarm);
                }
                if (rsbcount-1 < i)
                {
                   rsb.Add(alarm);
                }
                if (rsccount-1 < i)
                {
                    rsc.Add(alarm);
                }
                afteralarm.Add(fsa.ElementAt(i));
                afteralarm.Add(fsb.ElementAt(i));
                afteralarm.Add(fsc.ElementAt(i));
                afteralarm.Add(rsb.ElementAt(i));
                afteralarm.Add(rsc.ElementAt(i));
            }
            //ExcelHelper.ExportDTtoExcel(  hehe.ToDataTable<mg_alarm>(excel), "", HttpContext.Current.Request.MapPath("~/App_Data/excel2012.xlsx"));
            ExcelHelper.ExportDTtoExcel(excel, "", HttpContext.Current.Request.MapPath("~/App_Data/excel2012.xlsx"));

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(afteralarm);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}