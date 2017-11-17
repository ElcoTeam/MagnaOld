using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_StepModel
    {
        public mg_StepModel()
		{}

        #region Model
        private int? _step_id;
        private string _step_name;
        private int? _fl_id;
        private int? _st_id;
        private int? _bom_id;
        private int? _bom_count;
        private int? _step_clock;
        private string _step_desc;
        private string _step_pic;
        private int? _step_plccode;
        private int? _step_order;

        private string _fl_name;
        private string _st_name;
        private string _bom_PN;
        private string _bom_desc;
        private string _odsName;
        private string _odsKeyword;

        public string odsKeyword
        {
            get { return _odsKeyword; }
            set { _odsKeyword = value; }
        }

        public string odsName
        {
            get { return _odsName; }
            set { _odsName = value; }
        }

        private int _part_id;
        private string _part_no;

        public string part_no
        {
            get { return _part_no; }
            set { _part_no = value; }
        }

        public int part_id
        {
            get { return _part_id; }
            set { _part_id = value; }
        }

        public string bom_desc
        {
            get { return _bom_desc; }
            set { _bom_desc = value; }
        }

        public string bom_PN
        {
            get { return _bom_PN; }
            set { _bom_PN = value; }
        }

        public string st_name
        {
            get { return _st_name; }
            set { _st_name = value; }
        }

        public string fl_name
        {
            get { return _fl_name; }
            set { _fl_name = value; }
        }

        public int? step_order
        {
            get { return _step_order; }
            set { _step_order = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        public int? step_id
        {
            set { _step_id = value; }
            get { return _step_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string step_name
        {
            set { _step_name = value; }
            get { return _step_name; }
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
        public int? st_id
        {
            set { _st_id = value; }
            get { return _st_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_id
        {
            set { _bom_id = value; }
            get { return _bom_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bom_count
        {
            set { _bom_count = value; }
            get { return _bom_count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? step_clock
        {
            set { _step_clock = value; }
            get { return _step_clock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string step_desc
        {
            set { _step_desc = value; }
            get { return _step_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string step_pic
        {
            set { _step_pic = value; }
            get { return _step_pic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? step_plccode
        {
            set { _step_plccode = value; }
            get { return _step_plccode; }
        }
        /// <summary>
        /// 条码匹配开始位
        /// </summary>
        public int? barcode_start { get; set; }
        /// <summary>
        /// 条码匹配长度
        /// </summary>
        public int? barcode_number { get; set; }

        #endregion Model

    }
}
