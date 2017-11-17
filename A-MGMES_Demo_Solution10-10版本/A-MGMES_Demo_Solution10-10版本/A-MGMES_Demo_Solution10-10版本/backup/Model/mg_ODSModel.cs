using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_ODSModel
    {
        private string _ods_name;

        public string ods_name
        {
            get { return _ods_name; }
            set { _ods_name = value; }
        }
        private string _ods_keywords;

        public string ods_keywords
        {
            get { return _ods_keywords; }
            set { _ods_keywords = value; }
        }
    }
}
