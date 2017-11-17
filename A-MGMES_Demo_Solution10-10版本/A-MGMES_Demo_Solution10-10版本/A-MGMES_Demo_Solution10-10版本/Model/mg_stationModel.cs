using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_stationModel
    {
        public mg_stationModel()
		{}

        #region Model
        private int? _st_id;
        private int? _fl_id;
        private string _st_no;
        private DateTime? _st_pretime;
        private int? _st_ispre;
        private string _st_name;
        private string _st_mac;
        private int? _st_typeid;
        private string _st_typename;
        private int? _st_order;
        private int? _st_isfirst;
        private int? _st_isend;

        private string _st_isfirstname;
        private string _st_isendname;

        public string st_isendname
        {
            get { return _st_isendname; }
            set { _st_isendname = value; }
        }

        public string st_isfirstname
        {
            get { return _st_isfirstname; }
            set { _st_isfirstname = value; }
        }

        public int? st_isend
        {
            get { return _st_isend; }
            set { _st_isend = value; }
        }

        public int? st_isfirst
        {
            get { return _st_isfirst; }
            set { _st_isfirst = value; }
        }
        
        private string _st_odsfile;
        private string _st_mushifile;
        private string _st_odsfilename;

        public string st_odsfilename
        {
            get { return _st_odsfilename; }
            set { _st_odsfilename = value; }
        }

        public string st_odsfile
        {
            get { return _st_odsfile; }
            set { _st_odsfile = value; }
        }

        public string st_mushifile
        {
            get { return _st_mushifile; }
            set { _st_mushifile = value; }
        }

        private string _fl_name;

        public string fl_name
        {
            get { return _fl_name; }
            set { _fl_name = value; }
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
        public int? fl_id
        {
            set { _fl_id = value; }
            get { return _fl_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string st_no
        {
            set { _st_no = value; }
            get { return _st_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? st_pretime
        {
            set { _st_pretime = value; }
            get { return _st_pretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? st_ispre
        {
            set { _st_ispre = value; }
            get { return _st_ispre; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string st_name
        {
            set { _st_name = value; }
            get { return _st_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string st_mac
        {
            set { _st_mac = value; }
            get { return _st_mac; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? st_typeid
        {
            set { _st_typeid = value; }
            get { return _st_typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string st_typename
        {
            set { _st_typename = value; }
            get { return _st_typename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? st_order
        {
            set { _st_order = value; }
            get { return _st_order; }
        }

        /// <summary>
        /// 计时开始
        /// </summary>
        public int? st_clock_Start
        {
            get;
            set;
        }

        /// <summary>
        /// 计时节拍
        /// </summary>
        public int? st_clock
        {
            get;
            set;
        }
        #endregion Model

    }
}
