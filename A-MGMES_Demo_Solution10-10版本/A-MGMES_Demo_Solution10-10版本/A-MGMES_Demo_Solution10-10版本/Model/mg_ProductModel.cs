using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_ProductModel
    {
        public mg_ProductModel()
		{}

        #region Model
        private int? _p_id;
        private string _ProductNo;
        private string _ProductName;
        private string _ProductDesc;
        private int? _ProductType;
        private string _ProductTypeName;
        private int? _IsUseing;
        private string _IsUseingName;
        /// <summary>
        /// 
        /// </summary>
        public int? p_id
        {
            set { _p_id = value; }
            get { return _p_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductNo
        {
            set { _ProductNo = value; }
            get { return _ProductNo; }
        }
        public string ProductName
        {
            set { _ProductName = value; }
            get { return _ProductName; }
        }
        public string ProductDesc
        {
            set { _ProductDesc = value; }
            get { return _ProductDesc; }
        }
        public int? ProductType
        {
            set { _ProductType = value; }
            get { return _ProductType; }
        }
        public string ProductTypeName
        {
            set { _ProductTypeName = value; }
            get { return _ProductTypeName; }
        }
        public int? IsUseing
        {
            set { _IsUseing = value; }
            get { return _IsUseing; }
        }
        public string IsUseingName
        {
            set { _IsUseingName = value; }
            get { return _IsUseingName; }
        }
        #endregion Model

    }
}
