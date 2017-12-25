using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class px_InternetPrinterModel
    {
        public px_InternetPrinterModel()
		{}
        #region Model
        private int _IID;
        private string _IName;
        private string _PrintIP;
        private string _IAddTime;
        private string _IRole;
        private string _IRamark;
        /// <summary>
        /// 
        /// </summary>
        public int IID
        {
            set { _IID = value; }
            get { return _IID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IName
        {
            set { _IName = value; }
            get { return _IName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PrintIP
        {
            set { _PrintIP = value; }
            get { return _PrintIP; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IAddTime
        {
            set { _IAddTime = value; }
            get { return _IAddTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IRole
        {
            set { _IRole = value; }
            get { return _IRole; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IRamark
        {
            set { _IRamark = value; }
            get { return _IRamark; }
        }
        #endregion Model

    }

   
}
