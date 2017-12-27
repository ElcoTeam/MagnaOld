using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SortManagent.Util
{
    public class ToDataTable
    {
        public static DataTable ListToDataTable<TEntity>(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("转换的集合为空");
            }
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties();
            DataTable dt = new DataTable(type.Name);
            foreach (var item in properties)
            {
                try
                {
                    if (!item.Name.ToString().Equals("IsPrint"))
                    {
                        dt.Columns.Add(new DataColumn(item.Name) { DataType = item.PropertyType });
                    }
                }
                catch (Exception a)
                { 
                }
            }
            foreach (var item in entities)
            {
                DataRow row = dt.NewRow();
                foreach (var property in properties)
                {
                    try
                    {

                        row[property.Name] = property.GetValue(item, null);
                      
                    }
                    catch (Exception a)
                    {

                        
                    }
                 
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static string getWLName(string id, string zfj)
        {
            if (zfj == "前排")
            {
                return zfj + id;
            }
            if (zfj == "后40" && id.IndexOf("靠背面套") != -1)
            {
                return "后40%靠背面套";
            }
            if (zfj == "后40" && id.IndexOf("侧头枕") != -1)
            {
                return "后40%侧头枕";
            }
            if (zfj == "后60" && id.IndexOf("靠背面套") != -1)
            {
                return "后60%靠背面套";
            }
            if (zfj == "后60" && id.IndexOf("扶手") != -1)
            {
                return "后排中央扶手";
            }
            if (zfj == "后60" && id.IndexOf("中头枕") != -1)
            {
                return "后排中央头枕";
            }
            if (zfj == "后60" && id.IndexOf("侧头枕") != -1)
            {
                return "后60%侧头枕";
            }
            if (zfj == "后坐垫" && id.IndexOf("坐垫面套") != -1)
            {
                return "后排坐垫面套";
            }
            return "";
        }

        public static string getWLName(string zfj)
        {
            if (zfj == "靠背面套")
            {
                return "前排靠背面套";
            }
            if (zfj == "坐垫面套")
            {
                return "前排坐垫面套";
            }
            if (zfj == "靠背骨架")
            {
                return "前排靠背骨架";
            }
            if (zfj == "坐垫骨架")
            {
                return "前排坐垫骨架";
            }
            if (zfj == "线束")
            {
                return "前排线束";
            }
            if (zfj == "大背板") { 
                return "前排大背板";
            }
            if (zfj == "40靠背") 
            {
                return "后40%靠背面套";
            }
            if (zfj == "后坐垫")
            {
                return "后排坐垫面套";
            }
            if (zfj == "60靠背")
            {
                return "后60%靠背面套";
            }
            if (zfj == "后排中央扶手")
            {
                return "后排中央扶手";
            }
            if (zfj == "后排中央头枕")
            {
                return "后排中央头枕";
            }
            if (zfj == "40侧头枕")
            {
                return "后40%侧头枕";
            }
            if (zfj == "60侧头枕")
            {
                return "后60%侧头枕";
            }
            return "";
        }
        public static string getWLNamesforwebservice(string zfj)
        {
            if (zfj == "靠背面套")
            {
                return "前排靠背面套";
            }
            if (zfj == "坐垫面套")
            {
                return "前排坐垫面套";
            }
            if (zfj == "靠背骨架")
            {
                return "前排靠背骨架";
            }
            if (zfj == "坐垫骨架")
            {
                return "前排坐垫骨架";
            }
            if (zfj == "线束")
            {
                return "前排线束";
            }
            if (zfj == "大背板")
            {
                return "前排大背板";
            }
            if (zfj == "40靠背")
            {
                return "后40靠背面套";
            }
            if (zfj == "后坐垫")
            {
                return "后坐垫坐垫面套";
            }
            if (zfj == "60靠背")
            {
                return "后60靠背面套";
            }
            if (zfj == "后排中央扶手")
            {
                return "后60扶手";
            }
            if (zfj == "后排中央头枕")
            {
                return "后60中头枕";
            }
            if (zfj == "40侧头枕")
            {
                return "后40侧头枕";
            }
            if (zfj == "60侧头枕")
            {
                return "后60侧头枕";
            }
            return "";
        }
        public static string getWLNameforPrint(string zfj, string id)
        {
            if (zfj == "主驾"|| zfj=="副驾")
            {
                return "前排"+ id;
            }
            else
            {
                return zfj + id;
            }
          
        }

        //static DataTable List2DataTable<TEntity>(List<TEntity> entities)
        //{
        //    if (entities == null)
        //    {
        //        throw new ArgumentNullException("转换的集合为空");
        //    }
        //    Type type = typeof(TEntity);
        //    PropertyInfo[] properties = type.GetProperties();
        //    DataTable dt = new DataTable(type.Name);
        //    foreach (var item in properties)
        //    {
        //        dt.Columns.Add(new DataColumn(item.Name) { DataType = item.PropertyType });
        //    }
        //    foreach (var item in entities)
        //    {
        //        DataRow row = dt.NewRow();
        //        foreach (var property in properties)
        //        {
        //            row[property.Name] = property.GetValue(item);
        //        }
        //        dt.Rows.Add(row);
        //    }
        //    return dt;
        //}

    }
}