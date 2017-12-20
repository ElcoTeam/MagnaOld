using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Production_AlarmModel
    {
         public Production_AlarmModel()
        { }
       
        private int? _id;
        private string _product_date;
        private string _stationNo;
        private string _stationName;
        private decimal? _material_num;
        private decimal? _production_num;
        private decimal? _maintenance_num;
        private decimal? _quality_num;
        private decimal? _overcycle_num;
        private decimal? _total_num;
        public int? id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string product_date
        {
            set { _product_date = value; }
            get { return _product_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string stationNo
        {
            set { _stationNo = value; }
            get { return _stationNo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string stationName
        {
            set { _stationName = value; }
            get { return _stationName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? material_num
        {
            set { _material_num = value; }
            get { return _material_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? production_num
        {
            set { _production_num = value; }
            get { return _production_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? maintenance_num
        {
            set { _maintenance_num = value; }
            get { return _maintenance_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? quality_num
        {
            set { _quality_num = value; }
            get { return _quality_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? overcycle_num
        {
            set { _overcycle_num = value; }
            get { return _overcycle_num; }
        }
        public decimal? total_num
        {
            set { _total_num = value; }
            get { return _total_num; }
        }
    }
}
