using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using System.Data;
namespace Bll
{
   public class Failure_BLL
    {
       public static int AddFailure(string name, string code)
       {
           return Failure_DAL.AddFailure(name,code) ;
       }
       public static int EditFailure(string name, string code,string ID)
       {
           return Failure_DAL.EditFailure(name, code,ID);
       }
       public static int DeleteFailure(string ID)
       {
           return Failure_DAL.DeleteFailure(ID);
       }
       public static DataTable GetTable()
       {
           return Failure_DAL.GetTable();
       }
    }
}
