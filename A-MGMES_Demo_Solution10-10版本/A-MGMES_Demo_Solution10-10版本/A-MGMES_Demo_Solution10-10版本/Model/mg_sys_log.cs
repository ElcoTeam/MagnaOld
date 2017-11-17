using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_sys_log
    {
        public string sys_id { get; set; }
        public string op_id { get; set; }
        public string op_name { get; set; }
        public string fl_id { get; set; }
        public string fl_name { get; set; }
        public string st_id { get; set; }
        public string st_no { get; set; }
        public string or_no { get; set; }
        public string part_no { get; set; }
        public int step_order { get; set; }
        public string step_startTime { get; set; }
        public string step_endTime { get; set; }
        public string step_duringtime { get; set; }
        public decimal AngleResult { get; set; }
        public decimal TorqueResult { get; set; }
        public string scanCode { get; set; }
        public string scanResult { get; set; }
        public string sys_desc { get; set; }
    }
}
