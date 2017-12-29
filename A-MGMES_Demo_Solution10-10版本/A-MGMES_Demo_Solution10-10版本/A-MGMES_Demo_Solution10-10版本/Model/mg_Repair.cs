using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_Repair
    {
        public int rowid { get; set; }
        public string StationNo { get; set; }
        public string op_name { get; set; }
        public string ItemCaption { get; set; }
        public int IsQualified { get; set; }
        public string  IsQualifiedstr { get; set; }
        public string CreateTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string OrderNo { get; set; }
        public string TestCaption { get; set; }
        public string TestValue { get; set; }
        public string TestValueMin { get; set; }
        public string TestValueMax { get; set; }
    }
}
