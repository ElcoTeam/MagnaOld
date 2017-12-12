using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbUtility
{
   public class DataListModel<T>
    {
        public string total{get;set;}
       // public string pages { get; set; }
       // public string from { get; set; }
       // public string to { get; set; }
        public List<T> rows;
        public List<T> footer;
    }
}
