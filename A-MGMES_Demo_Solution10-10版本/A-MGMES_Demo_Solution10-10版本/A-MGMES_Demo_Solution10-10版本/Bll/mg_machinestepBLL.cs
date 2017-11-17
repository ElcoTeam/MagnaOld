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
    public class mg_machinestepBLL
    {

        public static DataTable GetMachineStepByMachineNOAndStepAndBomtype(string machineNO, int workstep, string productType)
        {
            return mg_machinestepDAL.GetMachineStepByMachineNOAndStepAndBomtype(machineNO, workstep, productType);
        }
    }

    
}
