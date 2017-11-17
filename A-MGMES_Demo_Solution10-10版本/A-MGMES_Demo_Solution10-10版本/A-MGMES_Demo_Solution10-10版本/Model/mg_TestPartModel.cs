using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_TestPartModel
    {
        public mg_TestPartModel()
        {

        }
        private int? _p_id;
        private int? _partid;
        public string _partname; 
        private string _partno;
        private int? _sorting;
        private string _tIDS;
        private string _testcaption;
        private int? _sIDS;
        private string _stationno;
        private int? _t_id;


        public int? p_id
        {
            set { _p_id = value; }
            get { return _p_id; }
        }
        public int? partid
        {
            get { return _partid; }
            set { _partid = value; }
        }
        public string partname
        {
            get { return _partname; }
            set { _partname = value; }
        }
        public string partno
        {
            get { return _partno; }
            set { _partno = value; }
        }
        public int? sorting
        {
            get { return _sorting; }
            set { _sorting = value; }
        }
        public string tIDS
        {
            get { return _tIDS; }
            set { _tIDS = value; }
        }
        public string testcaption
        {
            get { return _testcaption; }
            set { _testcaption = value; }
        }
        public int? sIDS
        {
            get { return _sIDS; }
            set { _sIDS = value; }
        }
        public string stationno
        {
            get { return _stationno; }
            set { _stationno = value; }
        }
        public int? t_id
        {
            get { return _t_id; }
            set { _t_id = value; }
        }
    }
}
