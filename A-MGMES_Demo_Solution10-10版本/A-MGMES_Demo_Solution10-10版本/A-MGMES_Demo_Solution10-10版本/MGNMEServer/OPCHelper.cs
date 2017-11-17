using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Net;
using System.Windows.Forms;
using Tools;
using System.Threading;


namespace MGNMEServer
{
    public class OPCHelper
    {
        #region OPC 服务

        public OPCServer ConnectedOPCServer;
        public OPCGroups ConnectedServerGroup;

        public OPCGroup group_fs1;
       // public OPCGroup group_fs2;
        public OPCGroup group_fsb;
        public OPCGroup group_fsc;
        public OPCGroup group_rsb40;
        public OPCGroup group_rsb60;
        public OPCGroup group_rsc;

        public OPCItems items_fs1;
       // public OPCItems items_fs2;
        public OPCItems items_fsb;
        public OPCItems items_fsc;
        public OPCItems items_rsb40;
        public OPCItems items_rsb60;
        public OPCItems items_rsc;

        public OPCItem[] itemArr_fs1 = new OPCItem[100];
       // public OPCItem[] itemArr_fs2 = new OPCItem[100];
        public OPCItem[] itemArr_fsb = new OPCItem[100];
        public OPCItem[] itemArr_fsc = new OPCItem[100];
        public OPCItem[] itemArr_rsb40 = new OPCItem[100];
        public OPCItem[] itemArr_rsb60 = new OPCItem[100];
        public OPCItem[] itemArr_rsc = new OPCItem[100];
        public int[] ItemServerHandles_fs1 = new int[100];
      //  public int[] ItemServerHandles_fs2 = new int[100];
        public int[] ItemServerHandles_fsb = new int[100];
        public int[] ItemServerHandles_fsc = new int[100];
        public int[] ItemServerHandles_rsb40 = new int[100];
        public int[] ItemServerHandles_rsb60 = new int[100];
        public int[] ItemServerHandles_rsc = new int[100];

        public string[] itemAddress_fs1 = new string[100];
      //  public string[] itemAddress_fs2 = new string[100];
        public string[] itemAddress_fsb = new string[100];
        public string[] itemAddress_fsc = new string[100];
        public string[] itemAddress_rsb40 = new string[100];
        public string[] itemAddress_rsb60 = new string[100];
        public string[] itemAddress_rsc = new string[100];

        public void InitOPCServer()
        {
            ReadKepServerConfig();
            OPCServerConnect(OPCConfig.KepServerVersion, "");
            AddOPCGroup();
            AddOPCItem();
        }
        

        /// <summary>枚举本地OPC服务器
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetLocalServer()
        {
            //////////////////////////////////////////////////////////////////////

            string strHostName;
            //获取本地计算机IP,计算机名称
            strHostName = Dns.GetHostName();
            //获取本地计算机上的OPCServerName
            try
            {
                ConnectedOPCServer = new OPCServer();
                object serverList = ConnectedOPCServer.GetOPCServers(strHostName);

                foreach (string server in (Array)serverList)
                {
                    Console.WriteLine("本地OPC服务器：{0}", server);
                }
                return serverList;
            }
            catch (Exception err)
            {
                MessageBox.Show("枚举本地OPC服务器出错：" + err.Message + "");
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
            int intervalFresh = 10;
            try
            {
                ConnectedServerGroup = ConnectedOPCServer.OPCGroups;
                ConnectedOPCServer.OPCGroups.DefaultGroupIsActive = true;//激活组。
                ConnectedOPCServer.OPCGroups.DefaultGroupDeadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组。
                ConnectedOPCServer.OPCGroups.DefaultGroupUpdateRate = 200;//默认组群的刷新频率为200ms

                group_fs1 = ConnectedServerGroup.Add("FS1");
                group_fs1.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_fs1.IsActive = true;
                group_fs1.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false

                //group_fs2 = ConnectedServerGroup.Add("FS2");
                //group_fs2.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                //group_fs2.IsActive = true;
                //group_fs2.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false

                group_fsb = ConnectedServerGroup.Add("FSB");
                group_fsb.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_fsb.IsActive = true;
                group_fsb.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false

                group_fsc = ConnectedServerGroup.Add("FSC");
                group_fsc.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_fsc.IsActive = true;
                group_fsc.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false

                group_rsb40 = ConnectedServerGroup.Add("RSB40");
                group_rsb40.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_rsb40.IsActive = true;
                group_rsb40.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false

                Thread.Sleep(500);
                group_rsb60 = ConnectedServerGroup.Add("RSB60");
                group_rsb60.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_rsb60.IsActive = true;
                group_rsb60.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false
                Thread.Sleep(500);

                group_rsc = ConnectedServerGroup.Add("RSC");
                group_rsc.UpdateRate = intervalFresh;                         //刷新频率为1秒。                       
                group_rsc.IsActive = true;
                group_rsc.IsSubscribed = true;                      //使用订阅功能，即可以异步，默认false
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
                AssemblingOPCItem(ref items_fs1, group_fs1, itemArr_fs1, ItemServerHandles_fs1, OPCConfig.Device_FS, OPCConfig.st_fs1,itemAddress_fs1);
               // AssemblingOPCItem(ref items_fs2, group_fs2, itemArr_fs2, ItemServerHandles_fs2, OPCConfig.Device_FS, OPCConfig.st_fs2, itemAddress_fs2);
                AssemblingOPCItem(ref items_fsb, group_fsb, itemArr_fsb, ItemServerHandles_fsb, OPCConfig.Device_FSB, OPCConfig.st_fsb1, itemAddress_fsb);
                AssemblingOPCItem(ref items_fsc, group_fsc, itemArr_fsc, ItemServerHandles_fsc, OPCConfig.Device_FSC, OPCConfig.st_fsc1, itemAddress_fsc);
                AssemblingOPCItem(ref items_rsb40, group_rsb40, itemArr_rsb40, ItemServerHandles_rsb40, OPCConfig.Device_RSB, OPCConfig.st_rsb40, itemAddress_rsb40);
                AssemblingOPCItem(ref items_rsb60, group_rsb60, itemArr_rsb60, ItemServerHandles_rsb60, OPCConfig.Device_RSB, OPCConfig.st_rsb60, itemAddress_rsb60);
                AssemblingOPCItem(ref items_rsc, group_rsc, itemArr_rsc, ItemServerHandles_rsc, OPCConfig.Device_RSC, OPCConfig.st_rsc1, itemAddress_rsc);

                //for (int i = NumericParse.StringToInt(OPCConfig.TagStartPoint); i <= NumericParse.StringToInt(OPCConfig.TagEndPoint); i++)
                //{
                //    // Channel + "." + Device + "." + WorkStation + "." + Tag_Alarm;
                //    OPCItemIDs[i] = OPCConfig.Channel + "." + flowlineFlag + "." + stationFlag + "." + AppConfigHelper.GetAppConfig(i.ToString());
                //    ClientHandles[i] = i;
                //}
                //OPCItemCollection = ConnectedGroup.OPCItems;
                //OPCItemCollection.DefaultIsActive = true;

                //for (int i = 1; i <= OPCConfig.TagCount; i++)
                //{
                //    KepItemArray[i] = OPCItemCollection.AddItem(OPCItemIDs[i], i);
                //    ItemServerHandles[i] = KepItemArray[i].ServerHandle;        //在这里设置对应可写完成信号的变量在数组中对应的的itemHandleServer
                //}
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("OPC Add Items", exception.Message);
            }
        }

        private void AssemblingOPCItem(ref OPCItems items,OPCGroup group,OPCItem[] itemArr,int[] itemhandlesArr,string PLCDevice,string station,string[] itemAddrArr)
        {
            int startPointIndex = NumericParse.StringToInt(OPCConfig.TagStartPoint);
            int endPointIndex = NumericParse.StringToInt(OPCConfig.TagEndPoint);

            items = group.OPCItems;
            items.DefaultIsActive = true;
            for (int i = startPointIndex; i <= endPointIndex; i++)
            {
                string addr=OPCConfig.Channel + "." + PLCDevice + "." + station + "." + AppConfigHelper.GetAppConfig(i.ToString());
                itemAddrArr[i] = addr;
                itemArr[i] = items.AddItem(addr, i);
                itemhandlesArr[i] = itemArr[i].ServerHandle;
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

        //public void RemoveGroup()
        //{
        //    try
        //    {
        //        if (ConnectedServerGroup != null)
        //        {
        //            ConnectedServerGroup.Remove("Group1");
        //            ConnectedServerGroup = null;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        DisplayOPC_COM_ErrorValue("Remove Group", exception.Message);
        //    }
        //}


        /// <summary>
        /// 单个OPC string取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReadSingleValueFromOPC(OPCItem[] itemArr, OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            itemArr[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return ItemValues.ToString();
        }
        /// <summary>
        /// 单个OPC bool 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ReadSingleBoolValueFromOPC(OPCItem[] itemArr, OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            itemArr[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return Convert.ToBoolean(ItemValues);
        }

        public void OPCItemWrite(int Index, string VL,OPCItems items,int[] ItemHandles,OPCGroup group)
        {
            //AsyncWrite方法第二个参数
            //OPCItem bItem = OPCItemCollection.GetOPCItem(itemHandleServer);
            OPCItem bItem = items.GetOPCItem(ItemHandles[Index]);
            int[] SyncItemServerHandlestemp = new int[2] { 0, bItem.ServerHandle };   //SyncItemServerHandlestemp[1] = ItemServerHandles[Index];
            Array SyncItemServerHandles = (Array)SyncItemServerHandlestemp;
            //AsyncWrite方法第三个参数
            object[] SyncItemValuestemp = new object[2] { "", VL.ToString() };      //改变数值在此处，以后这里可以进行写完成信号的下发
            Array SyncItemValues = (Array)SyncItemValuestemp;
            //AsyncWrite方法第四个参数
            Array SyncItemServerErrors;
            //AsyncWrite方法第五个参数
            int TransactionID = 2009;
            //AsyncWrite方法第六个参数
            int cancelID;

            group.AsyncWrite(1, ref SyncItemServerHandles, ref SyncItemValues, out SyncItemServerErrors, TransactionID, out cancelID);

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



        #region "读取OPC配置信息，源自app.config中, 数据将保存在OPCConfig全局对象中"
        private void ReadKepServerConfig()
        {
            //KepServerVersion WorkStationStartPoint WorkStationEndPoint TagStartPoint TagEndPoint Channel Device

            OPCConfig.KepServerVersion = AppConfigHelper.GetAppConfig("KepServerVersion");

            OPCConfig.Channel = AppConfigHelper.GetAppConfig("Channel");
            OPCConfig.Device_FS = AppConfigHelper.GetAppConfig("Device_FS");
            OPCConfig.Device_FSB = AppConfigHelper.GetAppConfig("Device_FSB");
            OPCConfig.Device_FSC = AppConfigHelper.GetAppConfig("Device_FSC");
            OPCConfig.Device_RSB = AppConfigHelper.GetAppConfig("Device_RSB");
            OPCConfig.Device_RSC = AppConfigHelper.GetAppConfig("Device_RSC");

            OPCConfig.st_fs1 = AppConfigHelper.GetAppConfig("st_fs1");
           // OPCConfig.st_fs2 = AppConfigHelper.GetAppConfig("st_fs2");
            OPCConfig.st_fsb1 = AppConfigHelper.GetAppConfig("st_fsb1");
            OPCConfig.st_fsc1 = AppConfigHelper.GetAppConfig("st_fsc1");
            OPCConfig.st_rsb40 = AppConfigHelper.GetAppConfig("st_rsb40");
            OPCConfig.st_rsb60 = AppConfigHelper.GetAppConfig("st_rsb60");
            OPCConfig.st_rsc1 = AppConfigHelper.GetAppConfig("st_rsc1");


            OPCConfig.TagStartPoint = AppConfigHelper.GetAppConfig("TagStartPoint");
            OPCConfig.TagEndPoint = AppConfigHelper.GetAppConfig("TagEndPoint");

            OPCConfig.TagCount = AppConfigHelper.GetAppConfig("TagCount");
        }

        #endregion

        #endregion
    }
}
