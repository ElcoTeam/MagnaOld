using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;


namespace Bll
{
    public class mg_orderBLL
    {
        public static bool Addor(string cono,string or_no ,string allno, int orcount)
        {
            return mg_orderDAL.Addor( cono, or_no, allno,  orcount) > 0 ? true : false;
        }

        public static DataTable GetAllData()
        {
            return mg_orderDAL.GetAllData();
        }

        public static bool Updateor(int coid)
        {
            return mg_orderDAL.UpDateor(coid) > 0 ? true : false;
        }
    }
}
