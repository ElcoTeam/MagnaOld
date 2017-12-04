using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dal;
namespace Bll
{
   public  class DelJetSEQNR_BLL
    {
       public static string select()
       {
           return DelJetSEQNR_Dal.select();
       }
       public static string edit(string seqnr)
       {
           return DelJetSEQNR_Dal.edit(seqnr);
       }
    }
}
