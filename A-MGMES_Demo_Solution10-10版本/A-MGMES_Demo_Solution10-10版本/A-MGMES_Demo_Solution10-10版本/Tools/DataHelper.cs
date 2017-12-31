using System.Collections.Generic;
using System.Data;
using System;
using System.Reflection;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Text;
namespace Tools
{

    public class DataHelper
    {
        // Methods

        public static string ExportDatatableToHtml(DataTable dt,string title)
        {
            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append("<!doctype html><html><head><meta charset='utf-8'><title>");
            strHTMLBuilder.Append(title);
            strHTMLBuilder.Append("</title>");
            strHTMLBuilder.Append("</head>");
            strHTMLBuilder.Append("<body>");
            strHTMLBuilder.Append("<h3 style='text-align:center'>");
            strHTMLBuilder.Append(title);
            strHTMLBuilder.Append("</h3>");

            strHTMLBuilder.Append("<table align='center' border='1px' cellpadding='5' cellspacing='0' >");

            strHTMLBuilder.Append("<tr >");
            foreach (DataColumn myColumn in dt.Columns)
            {
                strHTMLBuilder.Append("<td >");
                strHTMLBuilder.Append(myColumn.ColumnName);
                strHTMLBuilder.Append("</td>");

            }
            strHTMLBuilder.Append("</tr>");


            foreach (DataRow myRow in dt.Rows)
            {

                strHTMLBuilder.Append("<tr >");
                foreach (DataColumn myColumn in dt.Columns)
                {
                    strHTMLBuilder.Append("<td >");
                    strHTMLBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    strHTMLBuilder.Append("</td>");

                }
                strHTMLBuilder.Append("</tr>");
            }

            //Close tags. 
            strHTMLBuilder.Append("</table>");
            strHTMLBuilder.Append("</body>");
            strHTMLBuilder.Append("</html>");

            string Htmltext = strHTMLBuilder.ToString();

            return Htmltext;

        } 
        public static List<T> ConvertIListToList<T>(IList<T> gbList) where T : class
        {
            if ((gbList != null) && (gbList.Count >= 1))
            {
                List<T> list = new List<T>();
                for (int i = 0; i < gbList.Count; i++)
                {
                    T item = gbList[i];
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            return null;
        }

        public static IList<T> DataSetToList<T>(DataSet ds, int index)
        {
            if ((ds == null) || (ds.Tables.Count < 0))
            {
                return null;
            }
            if (index > (ds.Tables.Count - 1))
            {
                return null;
            }
            if (index < 0)
            {
                index = 0;
            }
            DataTable table = ds.Tables[index];
            IList<T> list = new List<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                T local = Activator.CreateInstance<T>();
                PropertyInfo[] properties = local.GetType().GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (info.Name.Equals(table.Columns[j].ColumnName))
                        {
                            if (table.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(local, table.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(local, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(local);
            }
            return list;
        }

        public static IList<T> DataSetToList<T>(DataSet ds, string tn)
        {
            int index = 0;
            if ((ds == null) || (ds.Tables.Count < 0))
            {
                return null;
            }
            if (string.IsNullOrEmpty(tn))
            {
                return null;
            }
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].TableName.Equals(tn))
                {
                    index = i;
                    break;
                }
            }
            return DataSetToList<T>(ds, index);
        }

        public static string GetCellDataToStr(DataRow row, string columnKey)
        {
            if ((row != null) && !((row[columnKey] == null) || string.IsNullOrEmpty(row[columnKey].ToString())))
            {
                return row[columnKey].ToString();
            }
            return "";
        }
        
        public static string GetCellDataToStr(DataTable dt, int rowIndex, int colIndex)
        {
            if ((dt.Rows.Count > 0) && ((dt.Rows[rowIndex] != null) && (dt.Rows[rowIndex][colIndex] != null)))
            {
                return dt.Rows[rowIndex][colIndex].ToString();
            }
            return "";
        }

        public static string GetCellDataToStr(DataTable dt, int rowIndex, string columnKey)
        {
            if ((dt.Rows.Count > 0) && ((dt.Rows[rowIndex] != null) && (dt.Rows[rowIndex][columnKey] != null)))
            {
                return dt.Rows[rowIndex][columnKey].ToString();
            }
            return "";
        }

        public static string GetCellDataToStr(DataView dv, int rowIndex, string columnKey)
        {
            if ((dv.Count > 0) && ((dv[rowIndex] != null) && (dv[rowIndex].Row[columnKey] != null)))
            {
                return dv[rowIndex].Row[columnKey].ToString();
            }
            return "";
        }

        public static Type GetCoreType(Type t)
        {
            if ((t != null) && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                return Nullable.GetUnderlyingType(t);
            }
            return t;
        }

        public static string GetDataPagerString(string Fields, string TableName, string Condition, string CountField, string OrderField, string OrderType, string StartIndex, string EndIndex)
        {
            return ("with Res as (select " + Fields + " FROM " + TableName + " " + Condition + " )\tselect * into #TempT from Res;select COUNT(#TempT.[" + CountField + "] ) as RecordTotal\tfrom \t#TempT;select * from (select row_number()over(order by #TempT.[" + OrderField + "] " + OrderType + " )rownumber,* from #TempT)a where rownumber between " + StartIndex + " and " + EndIndex + "; drop table #TempT;");
        }

        public static string GetDataPagerStringByTop(string Fields, string TableName, string Condition, string CountFiled, string OrderField, string OrderType, string pagesize, string pageindex, string PagerCol)
        {
            if (string.IsNullOrEmpty(Condition))
            {
                return ("SELECT TOP " + pagesize + " " + Fields + " FROM " + TableName + " WHERE " + PagerCol + " NOT IN(SELECT TOP (" + pagesize + "*(" + pageindex + "-1)) " + PagerCol + " FROM " + TableName + " ORDER BY " + PagerCol + " " + OrderType + ")ORDER BY " + OrderField + " " + OrderType + ";select Count(" + CountFiled + ") as RecordTotal from " + TableName);
            }
            return (" SELECT TOP " + pagesize + " " + Fields + " FROM " + TableName + " WHERE " + PagerCol + " NOT IN(SELECT TOP (" + pagesize + "*(" + pageindex + "-1)) " + PagerCol + " FROM " + TableName + " where 1=1 " + Condition + " ORDER BY " + OrderField + " " + OrderType + ")" + Condition + " ORDER BY " + PagerCol + " " + OrderType + ";select Count(" + CountFiled + ") as RecordTotal from " + TableName + " where 1=1 " + Condition);
        }

        public static bool HasData(DataSet ds)
        {
            return (((ds != null) && (ds.Tables.Count > 0)) && (ds.Tables[0].Rows.Count > 0));
        }

        public static bool HasData(DataTable dt)
        {
            return ((dt != null) && (dt.Rows.Count > 0));
        }

        public string htmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str).Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
        }

        public static bool IsNullable(Type t)
        {
            return (!t.IsValueType || (t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(Nullable<>))));
        }

        public static void JoinSqlPar(List<SqlParameter> list, string key, object value, SqlDbType sqltype, string size)
        {
            SqlParameter parameter;
            if (!string.IsNullOrEmpty(size))
            {
                parameter = new SqlParameter(key, sqltype, Convert.ToInt32(size));
                if ((sqltype == SqlDbType.DateTime) && string.IsNullOrEmpty(value.ToString()))
                {
                    parameter.Value = NumericParse.IsDateNull2(null);
                }
                else
                {
                    parameter.Value = value;
                }
                list.Add(parameter);
            }
            else
            {
                parameter = new SqlParameter(key, sqltype);
                parameter.Value = value;
                list.Add(parameter);
            }
        }

        public static string NoHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        public static DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
            {
                return dt;
            }
            DataTable table = dt.Clone();
            int num = (PageIndex - 1) * PageSize;
            int count = PageIndex * PageSize;
            if (num < dt.Rows.Count)
            {
                if (count > dt.Rows.Count)
                {
                    count = dt.Rows.Count;
                }
                for (int i = num; i <= (count - 1); i++)
                {
                    DataRow row = table.NewRow();
                    DataRow row2 = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        row[column.ColumnName] = row2[column.ColumnName];
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable table = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                Type coreType = GetCoreType(info.PropertyType);
                table.Columns.Add(info.Name, coreType);
            }
            foreach (T local in items)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(local, null);
                }
                table.Rows.Add(values);
            }
            return table;
        }


    }

}