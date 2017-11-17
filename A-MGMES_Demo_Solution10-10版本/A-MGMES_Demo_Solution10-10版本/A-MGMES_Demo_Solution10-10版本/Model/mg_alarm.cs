using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_alarm
    {
       public int AlarmType { get; set; }
       public string AlarmText { get; set; }
       public string AlarmStation { get; set; }
       public DateTime AlarmStartTime { get; set; }
       public DateTime AlarmEndTime { get; set; }
       public string StartOrderNo { get; set; }
       public string EndOrderNo { get; set; }
       public int IsSolve { get; set; }
    }
}
