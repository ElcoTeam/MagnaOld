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
   public class mg_ProductBLL
    {
       
        public static string QueryProductList(string page, string pagesize)
        {
            string jsonStr = "[]";
            List<mg_ProductModel> list = null;
            string total = "0";
            if (page == "1")
            {
                list = QueryListForFirstPage(pagesize, out total);
            }
            else
            {
                list = QueryListForPaging(page, pagesize, out total);
            }

            mg_ProductPageModel model = new mg_ProductPageModel();
            model.total = total;
            model.rows = list;
            jsonStr = JSONTools.ScriptSerialize<mg_ProductPageModel>(model);
            return jsonStr;
        }
       private static List<mg_ProductModel> QueryListForPaging(string page, string pagesize, out string total)
        {
            List<mg_ProductModel> list = mg_ProductDAL.QueryListForPaging(page, pagesize, out total);
            return list;
        }

        private static List<mg_ProductModel> QueryListForFirstPage(string pagesize, out string total)
        {
            List<mg_ProductModel> list = mg_ProductDAL.QueryListForFirstPage(pagesize, out total);
            return list;
        }
        public static string queryProductidForPart()
        {
            string jsonStr = "[]";
            List<mg_ProductModel> list = mg_ProductDAL.queryProductidForPart();
            jsonStr = JSONTools.ScriptSerialize<List<mg_ProductModel>>(list);
            return jsonStr;
        }
        public static string SaveProduct(mg_ProductModel model)
        {
            return model.p_id == 0 ? AddProduct(model) : UpdateProduct(model);
        }
        private static string AddProduct(mg_ProductModel model)
        {
            int count = mg_ProductDAL.AddProduct(model);
            return count > 0 ?"true" : "false";
        }
        private static string UpdateProduct(mg_ProductModel model)
        {
           int count = mg_ProductDAL.UpdateProduct(model);
           return count > 0 ? "true" : "false";
        }
        public static string DeleteProduct(string fl_id)
        {
            int count = mg_ProductDAL.DeleteProduct(fl_id);
            return count > 0 ? "true" : "false";
        }
    }
       class mg_ProductPageModel
        {
          public string total;
          public List<mg_ProductModel> rows;
        }
    }

