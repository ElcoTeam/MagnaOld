using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class px_TargetModel
    {
        public px_TargetModel()
		{}
        #region Model
        private int _pxtarget_id;
        private string _pxtarget_time;
        private int _pxtarget_target;
         
      
        /// <summary>
        /// 
        /// </summary>
        public int pxtarget_id
        {
            set { _pxtarget_id = value; }
            get { return _pxtarget_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pxtarget_time
        {
            set { _pxtarget_time = value; }
            get { return _pxtarget_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int pxtarget_target
        {
            set { _pxtarget_target = value; }
            get { return _pxtarget_target; }
        }
      
        #endregion Model

    }

  
}
