using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MT3;
using System.Threading;
using Bll;
using Model;
using System.Net.NetworkInformation;


namespace MGMEApplication
{
    public partial class RFID_LoginFrom : Form
    {

        string cardNO;
        public int icdev;   // 通讯设备标识符
        public short st;    //函数返回值


        //bool isNeedAdmin;

        public RFID_LoginFrom()
        {
            InitializeComponent();
        }


        private void RFID_LoginFrom_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        //读卡器连接测试
        private void button1_Click(object sender, EventArgs e)
        {
            if (BTConnect1())
            {
                this.lab_title.Text = "读卡器连接正常，等待中...";
                MessageBox.Show("与读卡器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.timer1.Start();
            }
            else
            {
                this.lab_title.Text = "读卡器连接异常，请检查！";
                MessageBox.Show("与读卡器连接失败，请检查！\r\n请确保工作站处于联网状态！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.timer1.Stop();
                this.timer2.Stop();
            }
        }


        //周期性监测刷卡
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool isContected = BTConnect1();
            if (isContected)
            {
                Thread.Sleep(1000);
                this.lab_title.Text = "读卡器连接正常，等待中...";
                cardNO = ReadM1NO();
                if (!string.IsNullOrEmpty(cardNO))
                {
                    //判断是否为该工位
                    mg_OperatorModel model = mg_OperatorBLL.GetOperaterForCardNO(cardNO);
                    if (model != null)
                    {
                        string mac = GetMacString();
                        if (mac != model.op_mac.ToUpper() && 1 != (int)model.op_isoperator)
                        {
                            this.lab_title.Text = "您在此工位未授权，请联系管理员...";
                            cardNO = "";
                            this.timer1.Start();
                        }
                        else if (1 == (int)model.op_isoperator)
                        {
                            this.lab_title.Text = "管理员已授权，请员工二次刷卡...";
                            this.timer1.Stop();
                            this.timer2.Start();
                        }
                        else
                        {
                            this.timer2.Stop();
                            this.timer1.Stop();
                            //this.Close();
                            mg_stationModel stationModel = mg_StationBLL.GetStationByMac(mac);

                            MGNMESApplication.MainApplicationForm form = new MGNMESApplication.MainApplicationForm(model, stationModel);
                            form.Show();
                            this.Hide();
                        }
                    }
                }
            }
            else
            {
                this.lab_title.Text = "读卡器连接异常，请检查！";
                MessageBox.Show("读卡器连接失败，请检查！\r\n请确保工作站处于联网状态！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //二次授权
        private void timer2_Tick(object sender, EventArgs e)
        {
            string cardnum = ReadM1NO();
            if (cardNO != cardnum)
            {
                if (!string.IsNullOrEmpty(cardnum))
                {
                    this.timer2.Stop();
                    this.timer1.Stop();
                    string mac = GetMacString();
                    mg_stationModel stationModel = mg_StationBLL.GetStationByMac(mac);
                    mg_OperatorModel model = mg_OperatorBLL.GetOperaterForCardNO(cardnum);
                    MGNMESApplication.MainApplicationForm form = new MGNMESApplication.MainApplicationForm(model, stationModel);
                    form.Show();

                    this.Hide();
                }
            }

        }

        //与读卡器建立连接
        public bool BTConnect1()
        {

            bool BTConnect1 = false;
            if (icdev > 1)
            {
                st = mt_32dll.close_device(icdev);
            }
            icdev = mt_32dll.open_device(0, 115200);
            if (icdev > 1)
            {
                BTConnect1 = true;
            }
            else
            {
                this.timer1.Stop();
            }

            return BTConnect1;
        }


        private string ReadM1NO() //M1卡:寻卡 密码认证 读数据 
        {
            string Data = "";           //卡号

            byte[] rlen = new byte[8]; //寻卡参数
            byte[] datarecv = new byte[1000]; //寻卡参数
            byte[] key = new byte[10]; //密码认证参数
            byte[] sRecData = new byte[20]; //读数据参数
            byte[] RecData = new byte[40]; //读数据参数

            st = mt_32dll.rf_card(icdev, 0, datarecv, rlen);
            if (st != 0)
            {
                //MessageBox.Show("M1卡寻卡失败!");
                return "";
            }
            else
            {
                //MessageBox.Show("M1卡寻卡成功!");
                key[0] = 0xff;
                key[1] = 0xff;
                key[2] = 0xff;
                key[3] = 0xff;
                key[4] = 0xff;
                key[5] = 0xff;
                //st = mt_32dll.rf_authentication_key(icdev, 0, 8, key); //使用密码A 对2 扇区0 块认证
                st = mt_32dll.rf_authentication_key(icdev, 1, 8, key);
                if (st != 0)
                {
                    MessageBox.Show("M1卡密码认证失败!");
                    return "";
                }
                else
                {
                    //MessageBox.Show("M1卡密码认证成功!");
                    //st = mt_32dll.rf_read(icdev, 48, sRecData); 48为块地址
                    st = mt_32dll.rf_read(icdev, 8, sRecData); //2 扇区0 块

                    if (st != 0)
                    {
                        MessageBox.Show("M1卡读数据失败!");
                        return "";
                    }
                    else
                    {
                        mt_32dll.hex_asc(sRecData, RecData, 16);  //最大为16 即为16*2=32位
                        //MessageBox.Show("M1卡读数据成功,数据:" + Encoding.Default.GetString(RecData));
                        Data = Encoding.Default.GetString(RecData);

                    }
                }
            }
            return Data;
        }


        #region 返回当前系统所启用的网络连接的信息
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }
        #endregion



        public static string GetMacString()
        {
            string strMac = "";
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    strMac = ni.GetPhysicalAddress().ToString();
                    return strMac.ToUpper();
                }
            }
            return strMac.ToUpper();
        }


    }
}
