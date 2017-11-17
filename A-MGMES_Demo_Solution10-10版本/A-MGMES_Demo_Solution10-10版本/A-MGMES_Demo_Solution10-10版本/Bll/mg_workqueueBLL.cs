using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Tools;
using System.Data;

namespace Bll
{
    public class mg_workqueueBLL
    {
        public static DataTable GetProductInforForVIN(string vin)
        {

            return mg_workqueueDAL.GetProductInforForVIN(vin);
        }
    }

    
}
