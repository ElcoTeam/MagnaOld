using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bll;
using Tools;

namespace Bll
{
    public class AndonTaglinesBLL
    {

        /// <summary>
        /// 查询全部宣传口号
        /// </summary>
        /// <returns></returns>
        public static string QueryAndonTaglinesList()
        {
            List<Andon_Taglines> tempList = AndonTaglinesDAL.QueryAndonTaglinesList();
            String jsonStr = JSONTools.ScriptSerialize<List<Andon_Taglines>>(tempList);
            return jsonStr;
        }

        /// <summary>
        /// 保存功能
        /// </summary>
        /// <param name="taglines"></param>
        /// <returns></returns>
        public static string SaveTaglines(Andon_Taglines taglines)
        {
            int resultNum = AndonTaglinesDAL.SaveTaglines(taglines);
            return (resultNum > 0) ? true.ToString() : false.ToString();
        }
    }
}
