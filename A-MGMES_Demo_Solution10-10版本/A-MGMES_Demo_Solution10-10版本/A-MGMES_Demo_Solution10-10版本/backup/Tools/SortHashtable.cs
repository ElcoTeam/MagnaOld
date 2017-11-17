/**************************************************
******** 
******** 为其SqlHelper中 事务方法ExecuteSqlTran()所用   将Hashtable自动排序功能取消，顺序成为  ADD 添加顺序
******** 姜任鹏******** 
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Tools
{
    /// <summary>
    /// 为其SqlHelper中 事务方法ExecuteSqlTran()所用   将Hashtable自动排序功能取消，顺序成为  ADD 添加顺序
    /// </summary>
    public class SortHashtable : Hashtable
    {
        private ArrayList keys = new ArrayList();
        public SortHashtable()
        {
        }
        public override void Add(object key, object value)
        {
            base.Add(key, value);
            keys.Add(key);
        }
        public override ICollection Keys
        {
            get
            {
                return keys;
            }
        }
        public override void Clear()
        {
            base.Clear();
            keys.Clear();
        }
        public override void Remove(object key)
        {
            base.Remove(key);
            keys.Remove(key);
        }
        public override IDictionaryEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}


