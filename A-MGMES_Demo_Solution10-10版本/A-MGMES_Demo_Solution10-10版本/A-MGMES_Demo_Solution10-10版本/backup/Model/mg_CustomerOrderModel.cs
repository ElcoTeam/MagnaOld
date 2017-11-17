
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_CustomerOrderModel
    {
        #region Model
        private decimal? _co_id;
        private string _co_no;
        private string _all_id;
        private int? _co_count = 0;
        private int? _co_cutomerid;
        private string _co_customer = "沃尔沃";
        private int? _co_state = 0;
        private int? _co_order;
        private int? _co_iscutted;
        private string _all_ids;
        private string _co_counts;
        private string _appPartdesc;
        private string _idcounts;

        public string idcounts
        {
            get { return _idcounts; }
            set { _idcounts = value; }
        }

        public string appPartdesc
        {
            get { return _appPartdesc; }
            set { _appPartdesc = value; }
        }

        public string co_counts
        {
            get { return _co_counts; }
            set { _co_counts = value; }
        }

        public string all_ids
        {
            get { return _all_ids; }
            set { _all_ids = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? co_id
        {
            set { _co_id = value; }
            get { return _co_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string co_no
        {
            set { _co_no = value; }
            get { return _co_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string all_id
        {
            set { _all_id = value; }
            get { return _all_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? co_count
        {
            set { _co_count = value; }
            get { return _co_count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? co_cutomerid
        {
            set { _co_cutomerid = value; }
            get { return _co_cutomerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string co_customer
        {
            set { _co_customer = value; }
            get { return _co_customer; }
        }
        /// <summary>
        /// 0：未生成；1：已生成
        /// </summary>
        public int? co_state
        {
            set { _co_state = value; }
            get { return _co_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? co_order
        {
            set { _co_order = value; }
            get { return _co_order; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? co_isCutted
        {
            set { _co_iscutted = value; }
            get { return _co_iscutted; }
        }
        #endregion Model

    }
}
