using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace website
{
    public partial class Part_TimeCuter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dti = DateTime.Now;
                DateTime dti1 = new DateTime(dti.Year, dti.Month, 1);
                DateTime dti2 = new DateTime(dti.Year, dti.Month, 1).AddMonths(1).AddDays(-1);

                TextBox1.Text = dti1.ToShortDateString();
                TextBox2.Text = dti2.ToShortDateString();
            }
        }

        public string Date1
        {
            get
            {
                return TextBox1.Text;
            }
            set
            {

            }
        }

        public string Date2
        {
            get
            {
                return TextBox2.Text;
            }
            set
            {

            }
        }
    }
}