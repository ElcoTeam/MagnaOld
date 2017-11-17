using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace website
{
    class FunCommon
    {
        #region 确认框
        public static bool MessageBoxWithResult(string mes, string head = "提示")
        {
            System.Media.SystemSounds.Beep.Play();
            if (System.Windows.Forms.MessageBox.Show(mes, head, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly) == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        public static bool MessageBoxShow(string mes)
        {
            System.Media.SystemSounds.Beep.Play();
            if (System.Windows.Forms.MessageBox.Show(mes, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly) == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 字符串
        public static bool Isnull(object item)
        {
            if (item.ToString().Trim().Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string[] CutToArry(string str, char key)
        {
            string[] sArray = str.Split(key);
            return sArray;
        }
        #endregion

        #region 路径
        public static string GetDir()
        {
            string Dir = System.Web.HttpContext.Current.Server.MapPath("Default.aspx");
            Dir = Dir.Replace("Default.aspx", "");

            return Dir;
        }

        public static void SetColumnHeader(ref DataTable source)
        {
            for (int i = 0; i < source.Columns.Count; i++)
            {
                string OldColumnName = source.Columns[i].ColumnName;
                string NewColumnName = FunCommon.GetJsonValue(GlobalData.GridViewColumnHeaderTable, OldColumnName);
                if (!String.IsNullOrEmpty(NewColumnName))
                {
                    source.Columns[i].ColumnName = NewColumnName;
                }

            }
        }
        #endregion

        #region Json
        public static string CreateJson(string columns, object[] values)
        {
            try
            {
                string JsonStr = "{";
                string[] c = FunCommon.CutToArry(columns, ',');
                for (int i = 0; i < c.Length; i++)
                {
                    if (i < values.Length)
                    {
                        JsonStr += "\"" + c[i] + "\":\"" + values[i].ToString() + "\",";
                    }
                    else
                    {
                        JsonStr += "\"" + c[i] + "\":\"" + "" + "\",";
                    }
                }

                if (JsonStr.Length > 1)
                {
                    JsonStr = JsonStr.Remove(JsonStr.Length - 1);
                }

                JsonStr += "}";

                return JsonStr;
            }
            catch
            {
                return null;
            }
        }
        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }
        #endregion

        #region 赋值
        public static void SetValues(object[] Items, object[] Values)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].GetType() == typeof(TextBox))
                {
                    TextBox p = (TextBox)Items[i];
                    if (Values[i] == null)
                    {
                        p.Text = null;
                    }
                    else
                    {
                        p.Text = Values[i].ToString();
                    }
                }
                else if (Items[i].GetType() == typeof(DropDownList))
                {
                    DropDownList p = (DropDownList)Items[i];
                    p.Text = Values[i].ToString();
                }
            }
        }
        #endregion

        #region 取值
        public static object[] GetValues(object[] items)
        {
            object[] values = new object[items.Length];
            int index = 0;
            foreach (object t in items)
            {
                if (t.GetType() == typeof(TextBox))
                {
                    TextBox p = (TextBox)t;
                    values[index] = p.Text;
                }
                else if (t.GetType() == typeof(DropDownList))
                {
                    DropDownList p = (DropDownList)t;
                    values[index] = p.Text;
                }

                index++;
            }

            return values;
        }
        #endregion

        #region GirdView
        public static string GetValue(GridView view, int RowIndex, string Column)
        {
            try
            {
                int ColumnIndex = 0;
                bool Res = false;
                while (ColumnIndex < view.Columns.Count)
                {
                    if (view.Columns[ColumnIndex].HeaderText == Column)
                    {
                        Res = true;
                        break;
                    }
                    ColumnIndex++;
                }

                if (Res)
                {
                    return view.Rows[RowIndex].Cells[ColumnIndex].Text;
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region 多字段筛选

        public static int match_check(string str, string key)
        {//查询在一串字符串中某字符串被包含了多少次
            if (str.Contains(key))
            {
                string strReplaced = str.Replace(key, "");
                return (str.Length - strReplaced.Length) / key.Length;
            }

            return 0;
        }

        public static string cut_out(string res, char key)
        {//1截取某符号前边的字符 2截取某符号中间的字符  
            int mat = match_check(res, key.ToString());
            if (mat < 1)
            {
                return null;
            }

            //截取数据
            char[] ch = new char[res.Length];
            ch = res.ToCharArray();
            int strseek = 0;

            string str = null;
            while (ch[strseek] != key)
            {
                str = str + ch[strseek];
                strseek++;
            }

            return str;
        }

        public static DataTable Senior_search(DataTable Source, string SearchColumns, string PriColumn, string SearchKey)
        {
            string[] columns = CutToArry(SearchColumns, ',');

            try
            {
                if (SearchKey.Trim().Length != 0)
                {
                    SearchKey = SearchKey.Trim();
                    DataTable Senior_Senior_search_keys = new DataTable();
                    Senior_Senior_search_keys.Columns.Add("key", System.Type.GetType("System.String"));
                    int keysnum = match_check(SearchKey.Trim(), " ") + 1;
                    for (int i = 0; i < keysnum - 1; i++)
                    {
                        DataRow dr = Senior_Senior_search_keys.NewRow();
                        dr["key"] = cut_out(SearchKey, ' ');
                        SearchKey = SearchKey.Remove(0, dr["key"].ToString().Length + 1);
                        Senior_Senior_search_keys.Rows.Add(dr);
                    }
                    DataRow dr1 = Senior_Senior_search_keys.NewRow();
                    dr1["key"] = SearchKey;
                    Senior_Senior_search_keys.Rows.Add(dr1);
                    DataTable dt = Source;

                    DataTable newdt = dt.Clone();
                    newdt.Clear();

                    for (int i = 0; i < Source.Rows.Count; i++)
                    {
                        DataTable Senior_Senior_searchkeysbuf = Senior_Senior_search_keys.Copy();
                        for (int index = 0; index < columns.Length; index++)
                        {
                            string buffer = Source.Rows[i][columns[index]].ToString();
                            for (int Senior_Senior_searchitemindex = 0; Senior_Senior_searchitemindex < Senior_Senior_searchkeysbuf.Rows.Count; Senior_Senior_searchitemindex++)
                            {
                                // Console.WriteLine(Senior_Senior_searchkeysbuf.Rows[Senior_Senior_searchitemindex]["key"].ToString());
                                if (buffer.Contains(Senior_Senior_searchkeysbuf.Rows[Senior_Senior_searchitemindex]["key"].ToString()))
                                {
                                    Senior_Senior_searchkeysbuf.Rows.Remove(Senior_Senior_searchkeysbuf.Rows[Senior_Senior_searchitemindex]);
                                    Senior_Senior_searchitemindex--;
                                }
                            }
                        }

                        if (Senior_Senior_searchkeysbuf.Rows.Count == 0)
                        {
                            DataRow[] found = newdt.Select(PriColumn + " = '" + Source.Rows[i][PriColumn] + "'");
                            if (found.Length == 0)
                            {
                                newdt.ImportRow(Source.Rows[i]);
                            }
                        }
                    }

                    return newdt;
                }
                else
                {
                    return Source;
                }
            }
            catch
            {
                return Source;
            }
        }
        #endregion

        #region Datatable/Json
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            //jsonBuilder.Append("{\"");
            //jsonBuilder.Append(dt.TableName);
            //jsonBuilder.Append("[");
            
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }
            jsonBuilder.Append("]");
            // jsonBuilder.Append("}");
            return jsonBuilder.ToString();    //生成json
            //return jsonBuilder.ToString().Substring(0, 2);
        }

        public static string DataTableToJson2(int TotalCount, DataTable dt)
        {
            try
            {
                string JsonStr = null;
                JsonStr += "{\"total\":\"" + TotalCount + "\",\r\n";
                JsonStr += "\"rows\":[\r\n";
                foreach (DataRow row in dt.Rows)
                {
                    JsonStr += "{\r\n";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        JsonStr += "\"" + dt.Columns[i].ColumnName + "\":\"" + row[i].ToString().Trim() + "\",\r\n";
                    }
                    JsonStr = JsonStr.Remove(JsonStr.Length - 3);
                    JsonStr += "}, ";
                }
                JsonStr = JsonStr.Remove(JsonStr.Length - 2);
                JsonStr += "]}";

                return JsonStr;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}