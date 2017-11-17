using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_allpartModel
    {
        public mg_allpartModel()
		{}
        #region Model
        private int _all_id;
        private string _all_no;
        private int? _all_rateid;
        private string _all_ratename;
        private int? _all_colorid;
        private string _all_colorname;
        private int? _all_metaid;
        private string _all_metaname;
        private string _all_desc;
        /// <summary>
        /// 
        /// </summary>
        public int all_id
        {
            set { _all_id = value; }
            get { return _all_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_no
        {
            set { _all_no = value; }
            get { return _all_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? all_rateid
        {
            set { _all_rateid = value; }
            get { return _all_rateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_ratename
        {
            set { _all_ratename = value; }
            get { return _all_ratename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? all_colorid
        {
            set { _all_colorid = value; }
            get { return _all_colorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_colorname
        {
            set { _all_colorname = value; }
            get { return _all_colorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? all_metaid
        {
            set { _all_metaid = value; }
            get { return _all_metaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_metaname
        {
            set { _all_metaname = value; }
            get { return _all_metaname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_desc
        {
            set { _all_desc = value; }
            get { return _all_desc; }
        }
        #endregion Model

    }
}
