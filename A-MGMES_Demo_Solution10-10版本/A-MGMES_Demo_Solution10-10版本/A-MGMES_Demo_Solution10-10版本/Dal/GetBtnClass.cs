using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using DAL;

namespace SortManagent.SortDao
{
    public class GetBtnClass
    {
       
        public static List<OrderList> BtnClassList;
        public static List<OrderList> olst;
        public GetBtnClass()
        {
            var listname = px_PanrameterDAL.QueryPanrameterListForPart().Select(s => s.Name).ToList();

            List<OrderList> list = new List<OrderList>();
            foreach (var item in listname)
            {
                if (item.Equals("靠背骨架") || item.Equals("坐垫骨架"))
                {
                    OrderList dic = new OrderList() { SortName = item, IsAutoPrint = false, IsAutoSend = false, Ascordesc = true, wlprintindex = 0 };
                    list.Add(dic);
                }
                else
                {
                    OrderList dic = new OrderList() { SortName = item, IsAutoPrint = false, IsAutoSend = false, Ascordesc = false, wlprintindex = 0 };
                    list.Add(dic);
                }
            }
            BtnClassList = list;


            List<OrderList> olstt = new List<OrderList>();
            foreach (var item in SortDao.GetBtnClass.BtnClassList)
            {
                OrderList o = new OrderList();
                o.SortName = Util.ToDataTable.getWLName(item.SortName);
                o.wlprintindex = item.wlprintindex;
                o.Ascordesc = item.Ascordesc;
                olstt.Add(o);
            }
            olst = olstt;
        }
    }
}