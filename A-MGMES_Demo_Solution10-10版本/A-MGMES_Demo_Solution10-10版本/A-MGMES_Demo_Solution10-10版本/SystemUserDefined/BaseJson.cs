using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SortManagent.SystemUserDefined
{
    public class BaseJson
    {
        //请求是否成功
        public bool isOk;
        //提示信息
        public string msg;
        //真实数据
        public object data;
    }
}