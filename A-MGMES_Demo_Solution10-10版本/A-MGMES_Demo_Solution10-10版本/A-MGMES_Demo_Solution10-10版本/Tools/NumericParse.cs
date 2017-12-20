using System;


namespace Tools
{
    public class NumericParse
    {
        // Methods
        public static object IsBoolNull(bool? num)
        {
            if (!num.HasValue)
            {
                return DBNull.Value;
            }
            return num;
        }

        public static object IsDateNull(DateTime? date)
        {
            if (!date.HasValue)
            {
                return DBNull.Value;
            }
            return date;
        }

        public static object IsDateNull2(DateTime? date)
        {
            DateTime? nullable;
            if (!(date.HasValue && !((nullable = date).HasValue && (nullable.GetValueOrDefault() == Convert.ToDateTime("0001-01-01 0:00:00")))))
            {
                return DBNull.Value;
            }
            return date;
        }

        public static bool IsDateTime(string str)
        {
            DateTime time;
            return DateTime.TryParse(str, out time);
        }

        public static object IsDecimalNull(decimal? num)
        {
            if (!num.HasValue)
            {
                return DBNull.Value;
            }
            return num;
        }

        public static object IsGuidNull(Guid? num)
        {
            if (!num.HasValue)
            {
                return DBNull.Value;
            }
            return num;
        }

        public static object IsIntNull(int? num)
        {
            if (!num.HasValue)
            {
                return DBNull.Value;
            }
            return num;
        }

        public static object IsStringNull(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return DBNull.Value;
            }
            return num;
        }

        public static bool? StringToBoolean(string str)
        {
            bool result = false;
            if (bool.TryParse(str, out result))
            {
                return new bool?(result);
            }
            return null;
        }

        public static DateTime? StringToDateTime(string str)
        {
            DateTime result = new DateTime();
            if (!string.IsNullOrEmpty(str) && DateTime.TryParse(str.Trim(), out result))
            {
                return new DateTime?(result);
            }
            return null;
        }
       

        public static DateTime StringToDateTime(string str, bool needCoverToNow)
        {
            DateTime result = new DateTime();
            if (!string.IsNullOrEmpty(str.Trim()) && !DateTime.TryParse(str.Trim(), out result))
            {
                return DateTime.Now;
            }
            return result;
        }

        public static string StringToDateTime(string str, string strForm)
        {
            DateTime result = new DateTime();
            if (!string.IsNullOrEmpty(str.Trim()) && DateTime.TryParse(str.Trim(), out result))
            {
                return result.ToString(strForm);
            }
            return null;
        }

        public static decimal StringToDecimal(string str)
        {
            decimal result = 0M;
            decimal.TryParse(str.Trim(), out result);
            return result;
        }
        public static decimal CutDecimalWithN(decimal? d, int n)
        {
            string strDecimal = d.ToString();
            int index = strDecimal.IndexOf(".");
            if (index == -1 || strDecimal.Length < index + n + 1)
            {
                strDecimal = string.Format("{0:F" + n + "}", d);
            }
            else
            {
                int length = index;
                if (n != 0)
                {
                    length = index + n + 1;
                }
                strDecimal = strDecimal.Substring(0, length);
            }
            return Decimal.Parse(strDecimal);
        }  
        public static decimal StringToDecimal(string str, decimal init)
        {
            decimal result = 0M;
            if (decimal.TryParse(str.Trim(), out result))
            {
                return result;
            }
            return init;
        }

        public static Guid? StringToGUID(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return new Guid(str);
            }
            return null;
        }

        public static int StringToInt(string str)
        {
            int result = 0;
            int.TryParse(str.Trim(), out result);
            return result;
        }

        public static int StringToInt(string str, int init)
        {
            int result = 0;
            if (string.IsNullOrEmpty(str))
            {
                return result;
            }
            if (int.TryParse(str.Trim(), out result))
            {
                return result;
            }
            return init;
        }

        public static int? StringToIntOrNull(string str)
        {
            int result = 0;
            if (int.TryParse(str.Trim(), out result))
            {
                return new int?(result);
            }
            return null;
        }
    }



}