using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Net;
using MGNMESApplication;
using System.Windows.Forms;
using Tools;

namespace MGMEApplication
{
    public class OPCHelper
    {
        #region OPC 服务
        string stationFlag;
        string flowlineFlag;

        public OPCServer ConnectedOPCServer;
        public OPCGroups ConnectedServerGroup;
        public OPCGroup ConnectedGroup;

        public OPCItems OPCItemCollection;
        public OPCItem[] KepItemArray = new OPCItem[1000];  //有时候索引会有问题，超出数据范围

        //int itemcount;
        public string[] OPCItemIDs = new string[1000];
        public int[] ItemServerHandles = new int[1000];
        public int[] ClientHandles = new int[1000];

        public void InitOPCServer(string stationFlag,string flowlineFlag)
        {
            this.flowlineFlag = flowlineFlag;
            this.stationFlag = stationFlag;
            ReadKepServerConfig();
            OPCServerConnect(OPCConfig.KepServerVersion, "");
            AddOPCGroup();
            AddOPCItem();
        }


        #region "读取OPC配置信息，源自app.config中, 数据将保存在OPCConfig全局对象中"
        private void ReadKepServerConfig()
        {
            //KepServerVersion WorkStationStartPoint WorkStationEndPoint TagStartPoint TagEndPoint Channel Device

            OPCConfig.KepServerVersion = AppConfigHelper.GetAppConfig("KepServerVersion");

            OPCConfig.WorkStationStartPoint = AppConfigHelper.GetAppConfig("WorkStationStartPoint");
            OPCConfig.WorkStationEndPoint = AppConfigHelper.GetAppConfig("WorkStationEndPoint");

            OPCConfig.Channel = AppConfigHelper.GetAppConfig("Channel");
            OPCConfig.Device = AppConfigHelper.GetAppConfig("Device");

            OPCConfig.TagStartPoint = AppConfigHelper.GetAppConfig("TagStartPoint");
            OPCConfig.TagEndPoint = AppConfigHelper.GetAppConfig("TagEndPoint");

            OPCConfig.TagCount = NumericParse.StringToInt(AppConfigHelper.GetAppConfig("TagCount"));



        }

        #endregion

        /// <summary>枚举本地OPC服务器
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetLocalServer()
        {
            string strHostIP;
            string strHostName;
            //获取本地计算机IP,计算机名称
            IPHostEntry IPHost = Dns.Resolve(Environment.MachineName);
            if (IPHost.AddressList.Length > 0)
            {
                strHostIP = IPHost.AddressList[0].ToString();
            }
            else
            {
                return null;
            }

            //通过IP来获取本地计算机名称
            IPHostEntry ipHostEntry = Dns.GetHostByAddress(strHostIP);
            strHostName = ipHostEntry.HostName.ToString();

            //获取本地计算机上的OPCServerName
            try
            {
                ConnectedOPCServer = new OPCServer();
                object serverList = ConnectedOPCServer.GetOPCServers(strHostName);
                return serverList;
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("枚举本地OPC服务器错误", exception.Message);
                return null;
            }
        }

        //OPC连接
        private void OPCServerConnect(string ServerName, object NoteName)
        {
            try
            {
                object serverList = GetLocalServer();
                if (!ConnectRemoteServer(NoteName.ToString(), ServerName))
                {
                    return;
                }
                RecurBrowse(ConnectedOPCServer.CreateBrowser());
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("连接OPC服务器错误", exception.Message);
            }
        }
        /// <summary>连接OPC服务器 
        ///连接OPC服务器 
        /// </summary>
        /// <param name="remoteServerIP"></param>
        /// <param name="remoteServerName"></param>
        /// <returns></returns>
        public bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                ConnectedOPCServer.Connect(remoteServerName, remoteServerIP);
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("连接OPC服务器错误", exception.Message);
                return false;
            }
            return true;
        }

        /// <summary>列出OPC服务器中所有节点
        /// 
        /// </summary>
        /// <param name="oPCBrowser"></param>
        public void RecurBrowse(OPCBrowser oPCBrowser)
        {
            //展开分支
            oPCBrowser.ShowBranches();
            //展开叶子
            oPCBrowser.ShowLeafs(true);
        }

        /// <summary>添加OPC组
        /// 创建组
        /// </summary>
        /// <param name="group_name"></param>
        /// <returns></returns>
        public bool AddOPCGroup()
        {
            try
            {
                ConnectedServerGroup = ConnectedOPCServer.OPCGroups;
                ConnectedOPCServer.OPCGroups.DefaultGroupIsActive = true;   //激活组
                ConnectedOPCServer.OPCGroups.DefaultGroupDeadband = 0;      //死区值，设为0时，服务器端该组内任何数据变化都通知组。
                ConnectedOPCServer.OPCGroups.DefaultGroupUpdateRate = 200;  //默认组群的刷新频率为200ms 
                ConnectedGroup = ConnectedServerGroup.Add(this.stationFlag);
                ConnectedGroup.UpdateRate = 10;                         //刷新频率为1秒。                       
                ConnectedGroup.IsActive = true;
                ConnectedGroup.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("组添加错误", exception.Message);
                return false;
            }
            return true;
        }

        public void AddOPCItem()
        {
            try
            {

                for (int i = NumericParse.StringToInt(OPCConfig.TagStartPoint); i <= NumericParse.StringToInt(OPCConfig.TagEndPoint); i++)
                {
                    // Channel + "." + Device + "." + WorkStation + "." + Tag_Alarm;
                    OPCItemIDs[i] = OPCConfig.Channel + "." + flowlineFlag + "." + stationFlag + "." + AppConfigHelper.GetAppConfig(i.ToString());
                    ClientHandles[i] = i;
                }
                OPCItemCollection = ConnectedGroup.OPCItems;
                OPCItemCollection.DefaultIsActive = true;

                for (int i = 1; i <= OPCConfig.TagCount; i++)
                {
                    KepItemArray[i] = OPCItemCollection.AddItem(OPCItemIDs[i], i);
                    ItemServerHandles[i] = KepItemArray[i].ServerHandle;        //在这里设置对应可写完成信号的变量在数组中对应的的itemHandleServer
                }
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("OPC Add Items", exception.Message);
            }
        }

        public void RemoveItems()
        {
            try
            {
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("OPC Remove Items", exception.Message);
            }
        }

        public void RemoveGroup()
        {
            try
            {
                if (ConnectedServerGroup != null)
                {
                    ConnectedServerGroup.Remove("Group1");
                    ConnectedServerGroup = null;
                    ConnectedGroup = null;
                }
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("Remove Group", exception.Message);
            }
        }


        /// <summary>
        /// 单个OPC string取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReadSingleValueFromOPC(OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            KepItemArray[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return ItemValues.ToString();
        }
        /// <summary>
        /// 单个OPC bool 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ReadSingleBoolValueFromOPC(OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            KepItemArray[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return Convert.ToBoolean(ItemValues);
        }

        public void DisconnectServer()
        {
            try
            {
                if (ConnectedOPCServer != null)
                {
                    ConnectedOPCServer.Disconnect();
                    ConnectedOPCServer = null;
                }
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("Disconnect", exception.Message);
            }
        }

        public void DisplayOPC_COM_ErrorValue(string OPC_function, string ErrorCode)
        {
            string ErrorDisplay = "The OPC function" + OPC_function + "has returned an error of " + ErrorCode.ToString() + "";
            MessageBox.Show(ErrorDisplay, "OPC Function Error", MessageBoxButtons.OK);
        }

       
        #endregion
    }
}
