using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class mg_TestRepairItemModel
    {
       public mg_TestRepairItemModel()
       {

       }
       private int? _tr_id;
       private string _ItemCaption;
       private int? _Sorting;
       private int? _IsUseing;
       private string _IsUseingName;
       private int? _ItemType;
       private string _ItemTypeName;

       public int? tr_id
       {
           get { return _tr_id; }
           set { _tr_id = value; }
       }

       public string ItemCaption
       {
           get { return _ItemCaption; }
           set { _ItemCaption = value; }
       }
       public int? Sorting
       {
           get { return _Sorting; }
           set { _Sorting = value; }
       }
       public int? IsUseing
       {
           get { return _IsUseing; }
           set { _IsUseing = value; }
       }
       public string IsUseingName
       {
           get { return _IsUseingName; }
           set { _IsUseingName = value; }
       }

       public int? ItemType
       {
           get { return _ItemType; }
           set { _ItemType = value; }
       }
       public string ItemTypeName
       {
           get { return _ItemTypeName; }
           set { _ItemTypeName = value; }
       }

    }
}
