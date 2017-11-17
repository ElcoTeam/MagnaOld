using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class mg_BOMTestModel
    {
        public mg_BOMTestModel()
        {
        }
        private int _test_id;
        private int? _testgroupid;
        private string _testgroupname;
        private int? _testpage;
        private int? _testtype;
        private int? _testcalculatetype;
        private string _testcaption;
        private float? _testvaluemin;
        private float? _testvaluemax;
        private int? _testvalueiscontain;
        private string _testvalueunit;
        private string _plcname;
        private string _plcvaluetype;
        private float? _plcoutmultiple;
        public int test_id
        {
            get { return _test_id; }
            set { _test_id = value; }
        }
        public int? testgroupid 
        {
            get { return _testgroupid; }
            set { _testgroupid = value; }
        }
        public string testgroupname 
        {
            get { return _testgroupname; }
            set { _testgroupname = value; }
        }
        public int? testpage
        {
            get { return _testpage; }
            set { _testpage = value; }
        }
        public int? testtype
        {
            get { return _testtype; }
            set { _testtype = value; }
        }
        public int? testcalculatetype
        {
            get { return _testcalculatetype; }
            set { _testcalculatetype = value; }
        }
        public string testcaption
        {
            get { return _testcaption; }
            set { _testcaption = value; }
        }
        public float? testvaluemin
        {
            get { return _testvaluemin; }
            set { _testvaluemin = value; }
        }
        public float? testvaluemax
        {   get { return _testvaluemax; }
            set { _testvaluemax = value; }
        }
        public int? testvalueiscontain
        {
            get { return _testvalueiscontain; }
            set { _testvalueiscontain = value; }
        }
        public string testvalueunit 
        {
            get { return _testvalueunit; }
            set { _testvalueunit = value; }
        }
        private string _testtypename;
        public string testtypename 
        {
            get { return _testtypename; }
            set { _testtypename = value; }
        }
        private string _testcalculatetypename;
        public string testcalculatetypename
        {
            get { return _testcalculatetypename; }
            set { _testcalculatetypename = value; }
        }
        private string _testvalueiscontainname;
        public string testvalueiscontainname
        {
            get { return _testvalueiscontainname; }
            set { _testvalueiscontainname = value; }
        }
        public string plcname 
        {
            get { return _plcname; }
            set { _plcname = value; }
        }
        public string plcvaluetype
        {
            get { return _plcvaluetype; }
            set { _plcvaluetype = value; }
        }
        public float? plcoutmultiple
        {
            get { return _plcoutmultiple; }
            set { _plcoutmultiple = value; }
        }
    }
}
