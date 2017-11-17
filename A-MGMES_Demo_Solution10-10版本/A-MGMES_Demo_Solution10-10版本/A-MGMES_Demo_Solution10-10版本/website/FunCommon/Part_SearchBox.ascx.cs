using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

namespace website
{
    public partial class Part_SearchBox : System.Web.UI.UserControl
    {
        #region 功能函数
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
        public static void Senior_search(ref GridView dg, string[] columns, string pri_columns, string Senior_search_key, DataTable scoure)
        {
            try
            {
                if (Senior_search_key.Trim().Length != 0)
                {
                    Senior_search_key = Senior_search_key.Trim();
                    DataTable Senior_Senior_search_keys = new DataTable();
                    Senior_Senior_search_keys.Columns.Add("key", System.Type.GetType("System.String"));
                    int keysnum = match_check(Senior_search_key.Trim(), " ") + 1;
                    for (int i = 0; i < keysnum - 1; i++)
                    {
                        DataRow dr = Senior_Senior_search_keys.NewRow();
                        dr["key"] = cut_out(Senior_search_key, ' ');
                        Senior_search_key = Senior_search_key.Remove(0, dr["key"].ToString().Length + 1);
                        Senior_Senior_search_keys.Rows.Add(dr);
                    }
                    DataRow dr1 = Senior_Senior_search_keys.NewRow();
                    dr1["key"] = Senior_search_key;
                    Senior_Senior_search_keys.Rows.Add(dr1);
                    DataTable dt = scoure;

                    DataTable newdt = dt.Clone();
                    newdt.Clear();

                    for (int i = 0; i < scoure.Rows.Count; i++)
                    {
                        DataTable Senior_Senior_searchkeysbuf = Senior_Senior_search_keys.Copy();
                        for (int index = 0; index < columns.Length; index++)
                        {
                            string buffer = scoure.Rows[i][columns[index]].ToString();
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
                            DataRow[] found = newdt.Select(pri_columns + " = '" + scoure.Rows[i][pri_columns] + "'");
                            if (found.Length == 0)
                            {
                                newdt.ImportRow(scoure.Rows[i]);
                            }
                        }
                    }

                    dg.DataSource = newdt;
                    dg.DataBind();
                }
                else
                {
                    dg.DataSource = scoure;
                }
            }
            catch
            {

            }
        }
        #endregion

        DataTable Source;
        GridView View;
        string[] SearchColumns;
        string PriColumns;

        void ReadCfg(string CfgPath)
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                string ReakCfgPath = FunCommon.GetDir() + "/Config/SearchBoxCtrler/" + CfgPath + ".cfg";
            
                fs = new FileStream(ReakCfgPath, FileMode.Open);
                sr = new  StreamReader(fs, Encoding.Default);
                string Realte = sr.ReadLine();

                SearchColumns = FunCommon.CutToArry(Realte, ',');

                sr.Close();
                fs.Close();
            }
            catch
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        public void Bind(DataTable source,ref GridView view,string CfgPath,string pri = "ID")
        {
            Source = source.Copy();
            View = view;
            ReadCfg(CfgPath);
            PriColumns = pri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetText(string text)
        {
            TextBox1.Text = text;
        }

        public string GetText()
        {
            return TextBox1.Text;
        }

        public void SearchExec()
        {
            Senior_search(ref View, SearchColumns, PriColumns, TextBox1.Text, Source);
        }

        public void SearchExec(string text)
        {
            GlobalData.NeedRefresh = true;
            Senior_search(ref View, SearchColumns, PriColumns, text, Source);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SearchExec(TextBox1.Text);
        }
    }
}