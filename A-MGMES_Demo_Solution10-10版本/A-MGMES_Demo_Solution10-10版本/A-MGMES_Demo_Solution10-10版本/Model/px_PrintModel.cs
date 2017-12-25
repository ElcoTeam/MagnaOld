using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class px_PrintModel
    {
        public px_PrintModel()
		{}
        #region Model
        public int id { get; set; }
        public string PXID { get; set; }
        public string orderid { get; set; }
        public string cartype { get; set; }
        public string XF { get; set; }
        public string LingjianHao { get; set; }
        public string sum { get; set; }
        public string ordername { get; set; }
        public Nullable<System.DateTime> dayintime { get; set; }
        public Nullable<int> printpxid { get; set; }
        public string resultljh { get; set; }
        public string isauto { get; set; }
        public string SFlag { get; set; }
        #endregion Model

    }

  
}
