using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_PositionModel
    {
        public mg_PositionModel()
		{}

        #region Model
        private int? _posi_id;
        private string _posi_name;
        /// <summary>
        /// 
        /// </summary>
        public int? posi_id
        {
            set { _posi_id = value; }
            get { return _posi_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string posi_name
        {
            set { _posi_name = value; }
            get { return _posi_name; }
        }
        #endregion Model

    }
}
