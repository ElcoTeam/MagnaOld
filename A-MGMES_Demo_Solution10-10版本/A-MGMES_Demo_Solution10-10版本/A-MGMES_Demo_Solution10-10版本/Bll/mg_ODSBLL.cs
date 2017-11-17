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
    public class mg_ODSBLL
    {

        public static DataTable Getmg_ODSByMachineNO(string machineNO)
        {
            return mg_ODSDAL.Getmg_ODSByMachineNO(machineNO);
        }
    }
}
