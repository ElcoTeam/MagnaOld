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
    public class mg_BOM_MatchBLL
    {

        public bool RecordBOMCode(mg_BOM_MatchModel model)
        {
            return (mg_BOM_MatchDAL.RecordBOMCode(model) > 0) ? true : false;
        }

        public bool UpdateBOMCode(mg_BOM_MatchModel model)
        {
            return (mg_BOM_MatchDAL.UpdateBOMCode(model) != -1) ? true : false;
        }
    }


}
