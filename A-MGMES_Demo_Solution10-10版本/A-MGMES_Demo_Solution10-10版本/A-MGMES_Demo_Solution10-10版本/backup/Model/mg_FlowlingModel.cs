using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_FlowlingModel
    {
        public mg_FlowlingModel()
		{}

        #region Model
        private int? _fl_id;
        private string _fl_name;
        /// <summary>
        /// 
        /// </summary>
        public int? fl_id
        {
            set { _fl_id = value; }
            get { return _fl_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fl_name
        {
            set { _fl_name = value; }
            get { return _fl_name; }
        }
        #endregion Model

    }
}
