using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using Tools;
using System.Data;
using Dal;
using Bll;

namespace Bll
{
    public class mg_TestBLL
    {
        public static string QueryTestList()
       {
           string jsonStr = "[]";
           List<mg_TestModel> list = mg_TestDAL.QueryTestList();
           jsonStr = JSONTools.ScriptSerialize<List<mg_TestModel>>(list);
           return jsonStr;
       }
        
    }
       
    }

