using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Net;
using Tools;
using Model;
using Bll;

namespace MGNMESApplication
{
    public partial class Frm_Login : Form
    {

        public Frm_Login()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            string strPsw = "";

            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能为空！", "警告！", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("用户密码不能为空！", "警告！", MessageBoxButtons.OK);
                txtPassword.Focus();
                return;
            }

            if (FormatHelper.CheckPunctuation(txtUserName.Text.Trim()) == false)
            {
                MessageBox.Show("用户名不能包含单引号！", "警告！", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
            if (FormatHelper.CheckPunctuation(txtPassword.Text.Trim()) == false)
            {
                MessageBox.Show("用户密码不能包含单引号！", "警告！", MessageBoxButtons.OK);
                txtPassword.Focus();
                return;
            }

            mg_userModel mg_user = mg_UserBLL.GetUserForUID(txtUserName.Text.Trim());
            if (mg_user != null)
            {
                //strPsw = FormatHelper.xPsw_Back(mg_user.PWD);
                //strPsw = FormatHelper.xPsw_Back(mg_user.user_pwd);
                strPsw = mg_user.user_pwd;
                if (txtPassword.Text.Trim() == strPsw)
                {
                    if (OPCConfig.WorkStationStartPoint != mg_user.machineNO && OPCConfig.WorkStationEndPoint != mg_user.machineNO)
                    {
                        MessageBox.Show("用户无权在此工位工作！", "警告！", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                       
                        MainForm fr = new MainForm(mg_user);
                        fr.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("您输入的密码有误，请重新输入！", "警告！", MessageBoxButtons.OK);
                    txtPassword.Text = "";
                    txtUserName.Text = "";
                    txtPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("用户名不存在！", "警告！", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            #region "读取OPC配置信息，源自app.config中, 数据将保存在OPCConfig全局对象中"

            OPCConfig.KepServerVersion = AppConfigHelper.GetAppConfig("Server");
            OPCConfig.WorkStationStartPoint = AppConfigHelper.GetAppConfig("WorkStation1");
            OPCConfig.WorkStationEndPoint = AppConfigHelper.GetAppConfig("WorkStation2");

            OPCConfig.Channel = AppConfigHelper.GetAppConfig("Channel");
            OPCConfig.Device = AppConfigHelper.GetAppConfig("Device");
            OPCConfig.TagCount = NumericParse.StringToInt(AppConfigHelper.GetAppConfig("TagCount"));

            OPCConfig.Tag_Alarm = AppConfigHelper.GetAppConfig("Tag_Alarm");
            OPCConfig.Tag_Finish = AppConfigHelper.GetAppConfig("Tag_Finish");
            OPCConfig.Tag_Start = AppConfigHelper.GetAppConfig("Tag_Start");
            OPCConfig.Tag_StepNumber = AppConfigHelper.GetAppConfig("Tag_StepNumber");
            OPCConfig.Tag_Product_VIN = AppConfigHelper.GetAppConfig("Tag_Product_VIN");
            OPCConfig.Tag_Scan_Code_Gun = AppConfigHelper.GetAppConfig("Tag_Scan_Code_Gun");
            OPCConfig.Tag_Torque_Angle = AppConfigHelper.GetAppConfig("Tag_Torque_Angle");
            OPCConfig.Tag_Torque_Value = AppConfigHelper.GetAppConfig("Tag_Torque_Value");
            OPCConfig.Tag_MatchResult = AppConfigHelper.GetAppConfig("Tag_MatchResult");
            OPCConfig.Tag_TraceCode = AppConfigHelper.GetAppConfig("Tag_TraceCode");

            #endregion


            this.Location = WinformHelper.GetCentrePoint();

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
