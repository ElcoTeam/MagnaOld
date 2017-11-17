using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Tools;

namespace Dal
{
    public class AndonTaglinesDAL
    {
        /// <summary>
        /// 查询全部宣传口号
        /// </summary>
        /// <returns></returns>
        public static List<Andon_Taglines> QueryAndonTaglinesList()
        {
            string sql = "SELECT ID,TaglinesType,TaglinesText FROM Andon_Taglines";

            DataTable dt = SqlHelper.GetDataDataTable(SqlHelper.SqlConnString, CommandType.Text, sql, null);

            List<Andon_Taglines> list = new List<Andon_Taglines>();
            foreach (DataRow row in dt.Rows)
            {
                Andon_Taglines model = new Andon_Taglines();

                model.ID = NumericParse.StringToInt(DataHelper.GetCellDataToStr(row, "ID"));
                model.TaglinesType = DataHelper.GetCellDataToStr(row, "TaglinesType");
                model.TaglinesText = DataHelper.GetCellDataToStr(row, "TaglinesText");

                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 保存功能
        /// </summary>
        /// <param name="taglines"></param>
        /// <returns></returns>
        public static int SaveTaglines(Andon_Taglines taglines)
        {
            if (taglines != null && taglines.ID > 0)
            {
                string sql =
                @"UPDATE [Andon_Taglines]
                   SET [TaglinesType] = @TaglinesType
                      ,[TaglinesText] = @TaglinesText
                 WHERE [ID] = @ID";
                SqlParameter[] paras =
                {
                    new SqlParameter("@ID",taglines.ID),
                    new SqlParameter("@TaglinesType",taglines.TaglinesType),
                    new SqlParameter("@TaglinesText",taglines.TaglinesText), 

                };
                int resultNum = SqlHelper.ExecuteNonQuery(SqlHelper.SqlConnString, CommandType.Text, sql, paras);
                return resultNum;
            }
            return 0;
        }
    }
}
