using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class px_PanrameterModel
    {
        
        public px_PanrameterModel()
		{}
        #region Model
        private int _SerialID;
        private string _Name;
        private int _Number;
        private int _SortName;
        private bool _IsAutoSend;
        private bool _IsAutoPrint;
        private bool _Ascordesc;
        private int _wlprintindex;
        private string _IsAutoSend1;
        private string _IsAutoPrint1;
        private string _Ascordesc1;
        /// <summary>
        /// 
        /// </summary>
        public int SerialID
        {
            set { _SerialID = value; }
            get { return _SerialID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Number
        {
            set { _Number = value; }
            get { return _Number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SortName
        {
            set { _SortName = value; }
            get { return _SortName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsAutoSend
        {
            set { _IsAutoSend = value; }
            get { return _IsAutoSend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsAutoPrint
        {
            set { _IsAutoPrint = value; }
            get { return _IsAutoPrint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Ascordesc
        {
            set { _Ascordesc = value; }
            get { return _Ascordesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int wlprintindex
        {
            set { _wlprintindex = value; }
            get { return _wlprintindex; }
        }

        public string IsAutoSend1
        {
            set { _IsAutoSend1 = value; }
            get { return _IsAutoSend1; }
        }
        public string IsAutoPrint1
        {
            set { _IsAutoPrint1 = value; }
            get { return _IsAutoPrint1; }
        }
        public string Ascordesc1
        {
            set { _Ascordesc1 = value; }
            get { return _Ascordesc1; }
        }
        #endregion Model

    }

    public class OrderList
    {
        public string total;
        public List<px_PanrameterModel> rows;
        public string SortName { get; set; }
        public bool IsAutoSend { get; set; }
        public bool IsAutoPrint { get; set; }
        public bool Ascordesc { get; set; }
        public int wlprintindex { get; set; }
    }

    public class ToDataTable
    {
        public static string getWLName(string zfj)
        {
            if (zfj == "靠背面套")
            {
                return "前排靠背面套";
            }
            if (zfj == "坐垫面套")
            {
                return "前排坐垫面套";
            }
            if (zfj == "靠背骨架")
            {
                return "前排靠背骨架";
            }
            if (zfj == "坐垫骨架")
            {
                return "前排坐垫骨架";
            }
            if (zfj == "线束")
            {
                return "前排线束";
            }
            if (zfj == "大背板")
            {
                return "前排大背板";
            }
            if (zfj == "40靠背")
            {
                return "后40%靠背面套";
            }
            if (zfj == "后坐垫")
            {
                return "后排坐垫面套";
            }
            if (zfj == "60靠背")
            {
                return "后60%靠背面套";
            }
            if (zfj == "后排中央扶手")
            {
                return "后排中央扶手";
            }
            if (zfj == "后排中央头枕")
            {
                return "后排中央头枕";
            }
            if (zfj == "40侧头枕")
            {
                return "后40%侧头枕";
            }
            if (zfj == "60侧头枕")
            {
                return "后60%侧头枕";
            }
            return "";
        }
    }

  
}
