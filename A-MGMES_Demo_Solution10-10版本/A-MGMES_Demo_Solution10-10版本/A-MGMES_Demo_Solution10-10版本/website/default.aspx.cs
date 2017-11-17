using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace website
{
    public partial class _default : System.Web.UI.Page
    {
        void InitUserConfig()
        {
            #region 列名
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                string ReakCfgPath = FunCommon.GetDir() + "/Config/GridViewColumnHeaders.cfg";

                fs = new FileStream(ReakCfgPath, FileMode.Open);
                sr = new StreamReader(fs, Encoding.Default);
                GlobalData.GridViewColumnHeaderTable = sr.ReadToEnd();
                GlobalData.GridViewColumnHeaderTable = GlobalData.GridViewColumnHeaderTable.Replace("\r\n", "");
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
            #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            FunSql.Init();
            InitUserConfig();

            Response.Redirect("AdminIndex.aspx");
        }
    }
}