using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Script.Serialization;

    public class JSONTools
    {
        public static List<T> JSONStringToList<T>(string JsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 0x7fffffff;
            return serializer.Deserialize<List<T>>(JsonStr);
        }

        public static DataSet JsonToDataSet(string Json)
        {
            try
            {
                DataSet set = new DataSet();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 0x7fffffff;
                Dictionary<string, object> dictionary = (Dictionary<string, object>)serializer.DeserializeObject(Json);
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    DataTable table = new DataTable(pair.Key);
                    object[] objArray = (object[])pair.Value;
                    foreach (object obj3 in objArray)
                    {
                        Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj3;
                        DataRow row = table.NewRow();
                        foreach (KeyValuePair<string, object> pair2 in dictionary2)
                        {
                            if (!table.Columns.Contains(pair2.Key))
                            {
                                table.Columns.Add(pair2.Key.ToString());
                                row[pair2.Key] = pair2.Value;
                            }
                            else
                            {
                                row[pair2.Key] = pair2.Value;
                            }
                        }
                        table.Rows.Add(row);
                    }
                    set.Tables.Add(table);
                }
                return set;
            }
            catch
            {
                return null;
            }
        }

        public static string ModifyInfoJSON(string people, string date)
        {
            return ("{\"People\":\"" + people + "\",\"Date\":\"" + date + "\"}");
        }

        public static T ScriptDeserialize<T>(string json) where T : new()
        {
            T local = new T();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 0x7fffffff;
            return serializer.Deserialize<T>(json);
        }

        public static string ScriptSerialize<T>(T input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 0x7fffffff;
            return serializer.Serialize(input);
        }
    }
}

