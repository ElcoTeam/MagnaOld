using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
namespace SortManagent.Util
{
    public class CustomerJsonResult : JsonResult
    {
        public object Data { get; set; }
       
        public CustomerJsonResult()
        {
        }
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }

        public static CustomerJsonResult ReturnValue(bool isOK, string msg, object data)
        {
            var ttt = new CustomerJsonResult()
            {
                Data = (new SystemUserDefined.BaseJson()
                {
                    isOk = isOK,
                    msg = msg,
                    data = data
                })
            };
            return ttt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOK"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CustomerJsonResult ReturnArrayValue<T>(bool isOK, string msg, List<T> list)
        {
            JObject json = new JObject();
            Type resultType = list[0].GetType();
            PropertyInfo[] pros = resultType.GetProperties();
            foreach (var pro in pros)
            {
                json.Add(new JProperty(pro.Name, GetObjectPropertyValue(list, pro.Name)));
            }
            return new CustomerJsonResult()
            {
                Data = (new SystemUserDefined.BaseJson()
                {
                    isOk = isOK,
                    msg = msg,
                    data = json
                })
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOK"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CustomerJsonResult ReturnArrayValue<T>(bool isOK, string msg, List<T> list, int totalCount)
        {
            JObject json = new JObject();
            Type resultType = list[0].GetType();
            PropertyInfo[] pros = resultType.GetProperties();
            foreach (var pro in pros)
            {
                json.Add(new JProperty(pro.Name, GetObjectPropertyValue(list, pro.Name)));
            }
            return new CustomerJsonResult()
            {
                Data = (new SystemUserDefined.BaseJson()
                {
                    isOk = isOK,
                    msg = msg,
                    data = new
                    {
                        totalCount = totalCount,
                        data = json
                    }
                })
            };
        }
        static List<object> GetObjectPropertyValue<T>(IList<T> list, string propertyname)
        {
            List<object> objs = new List<object>();
            foreach (T t in list)
            {
                Type type = typeof(T);

                PropertyInfo property = type.GetProperty(propertyname);

                if (property == null) return objs;

                object o = property.GetValue(t, null);

                if (o == null) return objs;

                objs.Add(o.ToString());
            }
            return objs;
        }
    }
}