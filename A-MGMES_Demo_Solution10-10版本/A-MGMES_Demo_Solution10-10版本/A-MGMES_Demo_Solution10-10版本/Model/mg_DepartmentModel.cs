using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_DepartmentModel
    {
        public mg_DepartmentModel()
		{}

        #region Model
        private int? _dep_id;
        private string _dep_name;
        /// <summary>
        /// 
        /// </summary>
        public int? dep_id
        {
            set { _dep_id = value; }
            get { return _dep_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dep_name
        {
            set { _dep_name = value; }
            get { return _dep_name; }
        }
        #endregion Model
    }
}
