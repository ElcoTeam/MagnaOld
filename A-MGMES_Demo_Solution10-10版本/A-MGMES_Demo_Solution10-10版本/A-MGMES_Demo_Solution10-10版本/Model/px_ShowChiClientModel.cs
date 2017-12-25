using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class px_ShowChiClientModel
    {
        public px_ShowChiClientModel()
		{}
        #region Model
        private int _SID;
        private string _SName;
        private string _ClientIP;
        private string _SAddTime;
        private string _SRole;
        private string _SRamark;      
      
        /// <summary>
        /// 
        /// </summary>
        public int SID
        {
            set { _SID = value; }
            get { return _SID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SName
        {
            set { _SName = value; }
            get { return _SName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientIP
        {
            set { _ClientIP = value; }
            get { return _ClientIP; }
        }
        public string SAddTime
        {
            set { _SAddTime = value; }
            get { return _SAddTime; }
        }
        public string SRole
        {
            set { _SRole = value; }
            get { return _SRole; }
        }
        public string SRamark
        {
            set { _SRamark = value; }
            get { return _SRamark; }
        }
        #endregion Model

    }

  
}
