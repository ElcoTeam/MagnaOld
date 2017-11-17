using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Tools
{

    public class FormatHelper
    {
        public static string FormatJSONSerDate(string str)
        {
            string str2 = str;
            MatchCollection matchs = Regex.Matches(str, @"\\/Date\((\d+)\)\\/", RegexOptions.Multiline);
            foreach (Match match in matchs)
            {
                DateTime time = new DateTime(0x7b2, 1, 1);
                str2 = str2.Replace(match.Value, time.AddMilliseconds((double)long.Parse(match.Groups[1].Value)).ToLocalTime().ToString());
            }
            return str2;
        }

        public static string FormatJSONSerDate(string str, string formate)
        {
            string newValue = "";
            string str3 = str;
            MatchCollection matchs = Regex.Matches(str, @"\\/Date\((\d+)\)\\/", RegexOptions.Multiline);
            foreach (Match match in matchs)
            {
                DateTime time = new DateTime(0x7b2, 1, 1);
                time = time.AddMilliseconds((double)long.Parse(match.Groups[1].Value)).ToLocalTime();
                newValue = string.Format("{0:" + formate + "}", time);
                str3 = str3.Replace(match.Value, newValue);
            }
            return str3;
        }

        public static string FormatToDate(object obj, string formatValue)
        {
            if (obj != null)
            {
                DateTime result = new DateTime();
                if (DateTime.TryParse(obj.ToString(), out result))
                {
                    return result.ToString(formatValue);
                }
                return null;
            }
            return null;
        }

        public static string FormatToDate(string str, string formatValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime result = new DateTime();
                if (DateTime.TryParse(str, out result))
                {
                    return result.ToString(formatValue);
                }
                return null;
            }
            return null;
        }


        public static string ToUnicodeString(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }

        public static string FromUnicodeString(string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException )
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }


        public static string FiltSymbol(string str)
        {
            string l_strResult = str.Replace("\n", "").Replace("\t", "").Replace("\r", "");
            return l_strResult;
        }

        public static string ReplaceShuangYinHao(string str, string newChar)
        {
            string l_strResult = str.Replace("\"", newChar);
            return l_strResult;
        }

        public static string ReplaceSYHFilterSymbol(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string res1 = FiltSymbol(str);
                string res2 = ReplaceShuangYinHao(res1, "'");
                return res2;
            }
            return str;
        }


        public static string FilterSpecial(string str)
        {
            if (str == "")
            {
                return str;
            }
            else
            {
                str = str.Replace("'", "");
                str = str.Replace("<", "");
                str = str.Replace(">", "");
                str = str.Replace("%", "");
                str = str.Replace("'delete", "");
                str = str.Replace("''", "");
                str = str.Replace("\"\"", "");
                str = str.Replace(",", "");
                str = str.Replace(".", "");
                str = str.Replace(">=", "");
                str = str.Replace("=<", "");
                str = str.Replace("-", "");
                str = str.Replace("_", "");
                str = str.Replace(";", "");
                str = str.Replace("||", "");
                str = str.Replace("[", "");
                str = str.Replace("]", "");
                str = str.Replace("&", "");
                str = str.Replace("#", "");
                str = str.Replace("/", "");
                str = str.Replace("-", "");
                str = str.Replace("|", "");
                str = str.Replace("?", "");
                str = str.Replace(">?", "");
                str = str.Replace("?<", "");
                str = str.Replace(" ", "");

                str = str.Replace("·", "");
                str = str.Replace("~", "");
                str = str.Replace("！", "");
                str = str.Replace("#", "");
                str = str.Replace("@", "");
                str = str.Replace("￥", "");
                str = str.Replace("%", "");
                str = str.Replace("……", "");
                str = str.Replace("&", "");
                str = str.Replace("*", "");
                str = str.Replace("（", "");
                str = str.Replace("）", "");
                str = str.Replace("——", "");
                str = str.Replace("-", "");
                str = str.Replace("+", "");
                str = str.Replace("=", "");
                str = str.Replace("，", "");
                str = str.Replace("。", "");
                str = str.Replace("/", "");
                str = str.Replace("？", "");
                str = str.Replace("【", "");
                str = str.Replace("】", "");
                str = str.Replace("{", "");
                str = str.Replace("}", "");
                str = str.Replace("”", "");
                str = str.Replace("‘", "");
                str = str.Replace("：", "");
                str = str.Replace("；", "");

                return str;
            }
        }


        public static string xPsw_Str(string xPassWord) //加密，将特殊符号转换为口令的数字或字母形式
        {
            string xPsw_Str = "";
            int n;
            string xStr1 = "";

            int xLens = xPassWord.Length;

            int xBt;
            int xCs = 0;
            int xAsc;
            Random i = new Random();
            int xTmp;
            char[] xStr;
            xBt = (int)(i.NextDouble() * 255);
            xStr1 = xBt.ToString("X4").Substring(2, 2);//转换为16进制，并截取后两位
            xTmp = xBt ^ xLens;
            xStr1 = xStr1 + xTmp.ToString("X4").Substring(2, 2);
            for (n = 0; n < xLens; n++)
            {
                xStr = xPassWord.Substring(n, 1).ToCharArray();
                xAsc = (int)xStr[0];
                xCs = xCs + xAsc;
                xTmp = xBt ^ xAsc;

                xStr1 = xStr1 + xTmp.ToString("X4").Substring(2, 2);
                xCs = xCs % 200;

            }
            xTmp = xBt ^ xCs;
            xStr1 = xStr1 + xTmp.ToString("X4").Substring(2, 2);
            xPsw_Str = xStr1;
            return xPsw_Str;
        }

        public static string xPsw_Back(string xPassWord) //解密，口令还原
        {
            string xPsw_Back = "";
            int n;
            string xStr1 = "";
            int xLens;
            int xBt;
            int xCs;
            int xAsc;
            int xCC = 0;
            xBt = Convert.ToInt32(xPassWord.Substring(0, 2), 16); //16进制转换为10进制
            xLens = Convert.ToInt32(xPassWord.Substring(2, 2), 16) ^ xBt;
            xCs = Convert.ToInt32(xPassWord.Substring(xPassWord.Length - 2, 2), 16) ^ xBt;
            for (n = 0; n < xLens; n++)
            {
                xAsc = Convert.ToInt32(xPassWord.Substring(3 + (n + 1) * 2 - 1, 2), 16) ^ xBt;
                xCC = xCC + xAsc;
                xStr1 = xStr1 + (char)xAsc;
                xCC = xCC % 200;

            }
            if (xCC == xCs)
                xPsw_Back = xStr1;
            else
                xPsw_Back = "";
            return xPsw_Back;
        }

        public static bool CheckPunctuation(string xstr)
        {
            //验证输入字符串是否包含单引号
            bool flag = true;
            if (xstr.Length == 0)
            {
                flag = true;
            }
            else
            {
                char[] x = xstr.ToCharArray();
                for (int i = 0; i < xstr.Length; i++)
                {
                    if (x[i] == (char)39)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
        public static bool IsEmail(string xstr)
        {
            //验证是否是整数的字符串
            return Regex.IsMatch(xstr, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        public static bool IsInteger(string xstr)
        {
            //验证是否是整数的字符串
            return Regex.IsMatch(xstr, @"^\d*$");
        }
        public static bool IsTime(string xstr)
        {
            //验证是否是时间格式字符串
            return Regex.IsMatch(xstr, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// lx 2017-06-22
        /// </summary>
        /// <param name="xstr"></param>
        /// <returns></returns>
        public static bool IsDateTime(string xstr)
        {
            //验证是否是时间格式字符串
            return Regex.IsMatch(xstr, @"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)\s+([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$");
        }

        /// <summary>
        /// 用于GridView行合并
        /// </summary>
        /// <param name="gvw"></param>
        /// <param name="col"></param>
        /// <param name="controlName"></param>
        public static void MergeRows(GridView gvw, int col, string controlName)
        {
            for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gvw.Rows[rowIndex];

                GridViewRow previousRow = gvw.Rows[rowIndex + 1];

                Label row_lbl = row.Cells[col].FindControl(controlName) as Label;
                Label previousRow_lbl = previousRow.Cells[col].FindControl(controlName) as Label;

                if (row_lbl != null && previousRow_lbl != null)
                {
                    if (row_lbl.Text == previousRow_lbl.Text)
                    {
                        row.Cells[col].RowSpan = previousRow.Cells[col].RowSpan < 1 ? 2 : previousRow.Cells[col].RowSpan + 1;

                        previousRow.Cells[col].Visible = false;
                    }
                }
            }
        }
    }
}

