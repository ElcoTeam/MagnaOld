using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Andon_Taglines
    {

        private int _id;
        /// <summary>
        /// ID
        /// </summary>	
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _taglinestype;
        /// <summary>
        /// 口号宣传类别，1生产大屏，2运营大屏1位置，3运营大屏2位置
        /// </summary>	
        public string TaglinesType
        {
            get { return _taglinestype; }
            set { _taglinestype = value; }
        }

        private string _taglinestext;
        /// <summary>
        /// 宣传口号内容
        /// </summary>	
        public string TaglinesText
        {
            get { return _taglinestext; }
            set { _taglinestext = value; }
        }

    }
}
