using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_BOMTestGroupModel
    {
       public mg_BOMTestGroupModel() 
       {
       }
       private int _group_id;
       private string _groupname;
       public int group_id
       {
           get { return _group_id; }
           set { _group_id = value; }
       }
       public string groupname
       {
           get { return _groupname; }
           set { _groupname = value; }
       }
    }
}
