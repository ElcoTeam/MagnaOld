using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MT3;
using System.Threading;
using BLL;
using Model;
using System.Net.NetworkInformation;


    public  class M32Helper 
    {

        string cardNO;
        public int icdev;   // 通讯设备标识符
        public short st;    //函数返回值
        

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
                    return "";
                }
                else
                {
                    //MessageBox.Show("M1卡密码认证成功!");
                    //st = mt_32dll.rf_read(icdev, 48, sRecData); 48为块地址
                    st = mt_32dll.rf_read(icdev, 8, sRecData); //2 扇区0 块

                    if (st != 0)
                    {
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

