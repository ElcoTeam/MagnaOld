using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_PropertyModel
    {
        public mg_PropertyModel()
        { }

        #region Model
        private int? _prop_id;
        private int? _prop_type;
        private string _prop_name;
        /// <summary>
        /// 
        /// </summary>
        public int? prop_id
        {
            set { _prop_id = value; }
            get { return _prop_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Prop_type
        {
            set { _prop_type = value; }
            get { return _prop_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string prop_name
        {
            set { _prop_name = value; }
            get { return _prop_name; }
        }
        #endregion Model
    }

    public enum mg_PropertyEnum
    {
        AllPartCarType = 1,         //整车高中低配类型
        AllPartColor = 2,           //整车颜色
        AllPartMaterial = 3,        //整车材质
        BOMCategory = 4,            //零件类型
        PartCategory = 5,           //部件类型
        BOMLevel = 6,               
        BOMColor = 7,
        BOMMaterial = 8,
        BOMSupplier = 9,
        BOMStore = 10,
        StationType = 11,
        Customer = 12

    }

    public enum mg_XLSEnum
    {
        FS_Drive = 19,
        FSB_Drive = 43,
        FSC_Drive = 44,
        RSB40 = 45,
        RSB60 = 46,
        RSC = 47,
        FS_Passenger = 48,
        FSB_Passenger = 49,
        FSC_Passenger = 50,

        //FS=51,
        //FSB=52,
        //FSC=53
        FS_Temp=54
    }
}
