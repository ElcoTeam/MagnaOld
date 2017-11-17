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
        public int icdev;   // ͨѶ�豸��ʶ��
        public short st;    //��������ֵ
        

        //���������������
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


        private string ReadM1NO() //M1��:Ѱ�� ������֤ ������ 
        {
            string Data = "";           //����

            byte[] rlen = new byte[8]; //Ѱ������
            byte[] datarecv = new byte[1000]; //Ѱ������
            byte[] key = new byte[10]; //������֤����
            byte[] sRecData = new byte[20]; //�����ݲ���
            byte[] RecData = new byte[40]; //�����ݲ���

            st = mt_32dll.rf_card(icdev, 0, datarecv, rlen);
            if (st != 0)
            {
                //MessageBox.Show("M1��Ѱ��ʧ��!");
                return "";
            }
            else
            {
                //MessageBox.Show("M1��Ѱ���ɹ�!");
                key[0] = 0xff;
                key[1] = 0xff;
                key[2] = 0xff;
                key[3] = 0xff;
                key[4] = 0xff;
                key[5] = 0xff;
                //st = mt_32dll.rf_authentication_key(icdev, 0, 8, key); //ʹ������A ��2 ����0 ����֤
                st = mt_32dll.rf_authentication_key(icdev, 1, 8, key);
                if (st != 0)
                {
                    return "";
                }
                else
                {
                    //MessageBox.Show("M1��������֤�ɹ�!");
                    //st = mt_32dll.rf_read(icdev, 48, sRecData); 48Ϊ���ַ
                    st = mt_32dll.rf_read(icdev, 8, sRecData); //2 ����0 ��

                    if (st != 0)
                    {
                        return "";
                    }
                    else
                    {
                        mt_32dll.hex_asc(sRecData, RecData, 16);  //���Ϊ16 ��Ϊ16*2=32λ
                        //MessageBox.Show("M1�������ݳɹ�,����:" + Encoding.Default.GetString(RecData));
                        Data = Encoding.Default.GetString(RecData);

                    }
                }
            }
            return Data;
        }


        #region ���ص�ǰϵͳ�����õ��������ӵ���Ϣ
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

