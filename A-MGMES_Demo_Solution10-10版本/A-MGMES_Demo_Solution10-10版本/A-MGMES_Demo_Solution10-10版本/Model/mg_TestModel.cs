using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_TestModel
    {
       public mg_TestModel()
       {

       }
       private int? _t_id;
       private string _testcaption;

       public int? t_id
       {
           get { return _t_id; }
           set { _t_id = value; }
       }

       public string testcaption
       {
           get { return _testcaption; }
           set { _testcaption = value; }
       }



    }
}
