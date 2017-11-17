using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_partModel
    {
        public mg_partModel()
		{}
        #region Model
        private int? _part_id;
        private string _part_no;
        private string _part_name;
        private string _part_desc;
        private int? _part_type;
        private int? _pflowlineid;
        private string _pflowlinename;
        private int? _pproductid;
        private string _pproductname;
        private int? _part_categoryid;
        private string _part_category;
        private int? _propid;
        private string _propname;
        private string _parttypename;

        private string _allpartIDs;
        private string _allpartNOs;

        public string allpartNOs
        {
            get { return _allpartNOs; }
            set { _allpartNOs = value; }
        }

        public string allpartIDs
        {
            get { return _allpartIDs; }
            set { _allpartIDs = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? part_id
        {
            set { _part_id = value; }
            get { return _part_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string part_no
        {
            set { _part_no = value; }
            get { return _part_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string part_name
        {
            set { _part_name = value; }
            get { return _part_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string part_desc
        {
            set { _part_desc = value; }
            get { return _part_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? part_categoryid
        {
            set { _part_categoryid = value; }
            get { return _part_categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string part_Category
        {
            set { _part_category = value; }
            get { return _part_category; }
        }
        public int? part_type
        {
            set { _part_type = value; }
            get { return _part_type; }
        }
        public int? pflowlineid
        {
            set { _pflowlineid = value; }
            get { return _pflowlineid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pflowlinename
        {
            set { _pflowlinename = value; }
            get { return _pflowlinename; }
        }
        public int? pproductid
        {
            set { _pproductid = value; }
            get { return _pproductid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pproductname
        {
            set { _pproductname = value; }
            get { return _pproductname; }
        }
        public int? propid
        {
            set { _propid = value; }
            get { return _propid; }
        }
        public string propname
        {
            set { _propname = value; }
            get { return _propname; }
        }
        public string parttypename
        {
            set { _parttypename = value; }
            get { return _parttypename; }
        }
        #endregion Model


    }
}
