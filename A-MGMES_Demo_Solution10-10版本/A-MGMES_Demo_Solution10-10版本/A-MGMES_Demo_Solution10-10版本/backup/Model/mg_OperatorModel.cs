using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_OperatorModel
    {
        public mg_OperatorModel()
		{}

        #region Model
        private int? _op_id;
        private int? _st_id;
        private string _op_name;
        private string _op_no;
        private string _op_pic;
        private int? _op_isoperator;
        private string _op_mac;
        private string _st_no;

        public string st_no
        {
            get { return _st_no; }
            set { _st_no = value; }
        }

        private string _st_name;

        public string st_name
        {
            get { return _st_name; }
            set { _st_name = value; }
        }
        private string _op_isoperator_name;

        public string op_isoperator_name
        {
            get { return _op_isoperator_name; }
            set { _op_isoperator_name = value; }
        }
        private string _op_sex_name;

        public string op_sex_name
        {
            get { return _op_sex_name; }
            set { _op_sex_name = value; }
        }

        //  


        private int? _op_sex;
        /// <summary>
        /// 
        /// </summary>
        public int? op_id
        {
            set { _op_id = value; }
            get { return _op_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? st_id
        {
            set { _st_id = value; }
            get { return _st_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string op_name
        {
            set { _op_name = value; }
            get { return _op_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string op_no
        {
            set { _op_no = value; }
            get { return _op_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string op_pic
        {
            set { _op_pic = value; }
            get { return _op_pic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? op_isoperator
        {
            set { _op_isoperator = value; }
            get { return _op_isoperator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string op_mac
        {
            set { _op_mac = value; }
            get { return _op_mac; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? op_sex
        {
            set { _op_sex = value; }
            get { return _op_sex; }
        }
        #endregion Model
    }
}
