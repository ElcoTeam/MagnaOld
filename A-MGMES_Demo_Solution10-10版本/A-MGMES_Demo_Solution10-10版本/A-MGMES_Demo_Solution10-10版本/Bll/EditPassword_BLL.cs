using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Model;
namespace Bll
{
   public class EditPassword_BLL
    {
        public static ResultMsg_User EditPsw(mg_userModel dataEntity, ResultMsg_User result)
       {
           return EditPassword_DAL.EditPsw(dataEntity, result);
       }
    }
}
