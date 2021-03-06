﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{   
    public class Sys_Menu
    {
        public int? ID { get; set; }
        public string MenuNo { get; set; }
        public string MenuName { get; set; }
        public string MenuAddr { get; set; }
        public string ParentNo { get; set; }
        public string MenuTag { get; set; }
        public string Image { get; set; }
    }

    public class MenuTree
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        //public string check { get; set; }
        public List<MenuTree> children { get; set; }
       
    }

}
