using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model;
using Tools;

namespace Bll
{
    public class px_PrintBLL
    {
        public static List<px_PrintModel> Querypx_PrintList()
        {
            return px_PrintDAL.Querypx_PrintList();
        }
    }

}
