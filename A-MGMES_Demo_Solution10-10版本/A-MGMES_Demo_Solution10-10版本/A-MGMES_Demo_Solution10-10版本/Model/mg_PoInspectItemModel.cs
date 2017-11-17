using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_PoInspectItemModel
    {
       public mg_PoInspectItemModel() 
       {
           
       }
       private int _pi_id;
       private string _piitem;
       private string _piitemdescribe;
       public int pi_id
       {
           get { return _pi_id; }
           set { _pi_id = value; }
       }
       public string piitem
       {
           get { return _piitem; }
           set { _piitem = value; }
       }
       public string piitemdescribe
       {
           get { return _piitemdescribe; }
           set { _piitemdescribe = value; }
       }
    }
}
