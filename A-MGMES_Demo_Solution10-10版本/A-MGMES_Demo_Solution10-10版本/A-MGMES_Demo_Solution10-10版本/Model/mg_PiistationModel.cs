using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_PiistationModel
    {
        public mg_PiistationModel()
        {
        }
        private int? _ps_id;
        private int? _pi_id;
        private string _station_no;
        private int? _sorting;
        private string _piitem;

        private string _piIDs;

        public int? ps_id
        {
            get { return _ps_id; }
            set { _ps_id = value; }
        }
        public int? pi_id
        {
            get { return _pi_id; }
            set { _pi_id = value; }
        }
        public string station_no
        {
            get { return _station_no; }
            set { _station_no = value; }
        }
        public int? sorting
        {
            get { return _sorting; }
            set { _sorting = value; }
        }
        public string piitem
        {
            get { return _piitem; }
            set { _piitem = value; }
        }
        public string piIDs
        {
            get { return _piIDs; }
            set { _piIDs = value; }
        }
    }
}
