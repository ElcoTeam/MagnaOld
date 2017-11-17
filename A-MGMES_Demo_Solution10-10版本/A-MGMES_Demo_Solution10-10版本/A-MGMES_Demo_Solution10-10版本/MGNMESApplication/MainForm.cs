using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using OPCAutomation;
using System.Threading;
using Bll;
using Tools;
using Model;

namespace MGNMESApplication
{
    public partial class MainForm : Form
    {
        mg_userModel mg_user;
        string vin;
        string productType;
        int Step_Num;
        string machineNO;

        //
        int DataType;
        string BOM;

        string ODS_Num;
        string ODS_Pic;
        string ODS_Txt;

        //工作时长
        int workDuration;
        bool finish;
        bool start;

        DataTable bomAllTB;
        DataTable odsAllTB;
        DataTable machineStepTB;
        //SQL mySql = new SQL();
        public MainForm(mg_userModel mg_user)
        {
            this.mg_user = mg_user;
            InitializeComponent();
            workDuration = 0;
            this.lab_time.Text = workDuration.ToString();
            this.grd_ODS.AutoGenerateColumns = false;
            this.grd_bom.AutoGenerateColumns = false;
            OPCServerConnect(OPCConfig.KepServerVersion, "");
            AddOPCGroup();
            AddOPCItem();
            ConnectedGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(ConnectedGroup_DataChange);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            SettingUserInfo();      //设置用户信息
            GetProductInfo();                            //获取VIN，然后获取产品基础信息 ： 生产序号、座椅件号、座椅件名
            //SettingWorkDuration();                      //工作时长
            //Step_Num = Convert.ToInt32(ReadSingleValueFromOPC(OPCConfigEnum.StepNumber));                  //获取工作步骤
            //GetTorqueInfo();                            //获取扭矩信息
            //GetScanCodeInfo();                          //获取零件扫描值
            //BindODS();                      //工作步骤列表
            //BindBOM();                      //零件列表
            //BindData();             //opc信号数据

            if (!string.IsNullOrEmpty(vin))
            {
                BindODS();                      //工作步骤列表
                BindBOM();                      //零件列表
            }
        }
        //OPC数据发生变化
        void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = KepItemArray[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;

                if (itemid == OPCConfig.Addr_Alarm)
                {

                }
                else if (itemid == OPCConfig.Addr_Finish)        //开始为true，结束为false，才累加时长 | 开始为false，结束为true，才关闭累计
                {
                    SettingWorkDuration();
                }
                else if (itemid == OPCConfig.Addr_Start)           //开始为true，结束为false，才累加时长 | 开始为false，结束为true，才关闭累计
                {
                    SettingWorkDuration();
                }
                else if (itemid == OPCConfig.Addr_StepNumber)        //工序步骤改变
                {
                    SelectedGRDRow();
                    SelectedbomGRDRow();
                }
                else if (itemid == OPCConfig.Addr_Product_VIN)        //生产订单号发生变化，则重新绑定相关信息
                {
                    this.lab_bom.Text = "";
                    this.lab_CurruntODS.Text = "";
                    this.lab_TorqueAngle.Text = "";
                    this.lab_TorqueValue.Text = "";
                    Step_Num = 0;                  //获取工作步骤
                   
                    GetProductInfo();                            //获取VIN，然后获取产品基础信息 ： 生产序号、座椅件号、座椅件名
                    //SettingWorkDuration();                      //工作时长
                    //GetTorqueInfo();                            //获取扭矩信息
                    //GetScanCodeInfo();                          //获取零件扫描值
                    if (!string.IsNullOrEmpty(vin))
                    {
                        BindODS();                      //工作步骤列表
                        BindBOM();                      //零件列表
                    }
                }
                else if (itemid == OPCConfig.Addr_Scan_Code_Gun)     //扫描值
                {
                    GetScanCodeInfo();
                }
                else if (itemid == OPCConfig.Addr_Torque_Angle)       //扭矩角度
                {
                    GetTorqueInfo();
                }
                else if (itemid == OPCConfig.Addr_Torque_Value)       //扭矩力度
                {
                    GetTorqueInfo();
                }
                else if (itemid == OPCConfig.Addr_MatchResult)       //特征码-扫描值-匹配
                {
                    bool isFlag = Convert.ToBoolean(ReadSingleValueFromOPC(OPCConfigEnum.MatchResult));
                    if (isFlag)
                    {
                        RecordBOMCode();
                        BindBOM();                      //零件列表
                    }
                }
                else if (itemid == OPCConfig.Addr_TraceCode)       //追溯码
                {
                    UpdateBOMCode();
                    BindBOM();                      //零件列表
                }
            }
        }

        private void UpdateBOMCode()
        {
            if (!string.IsNullOrEmpty(BOM))
            {
                mg_BOM_MatchModel model = new mg_BOM_MatchModel();
                DataRow[] rows = bomAllTB.Select("indexNO=" + BOM);
                model.BOMNO = rows[0]["BOMNO"].ToString();
                model.VIN = vin;
                model.TraceCode = ReadSingleValueFromOPC(OPCConfigEnum.TraceCode);

                mg_BOM_MatchBLL bll = new mg_BOM_MatchBLL();
                bll.UpdateBOMCode(model);
            }
        }

        private void RecordBOMCode()
        {
            mg_BOM_MatchModel model = new mg_BOM_MatchModel();
            DataRow[] rows = bomAllTB.Select("indexNO=" + BOM);
            model.BOMNO = rows[0]["BOMNO"].ToString();
            model.ScanCode = ReadSingleValueFromOPC(OPCConfigEnum.Scan_Code_Gun);
            //model.UID = mg_user.UID;
            model.UID = mg_user.user_id.ToString();
            model.VIN = vin;

            mg_BOM_MatchBLL bll = new mg_BOM_MatchBLL();
            bll.RecordBOMCode(model);
        }




        #region 设置用户信息
        private void SettingUserInfo()
        {
            //lab_user.Text = mg_user.name;               //用户名
            lab_user.Text = mg_user.user_name;               //用户名
            //lab_userNO.Text = mg_user.UID;      //用户号
            lab_userNO.Text = mg_user.user_id.ToString();      //用户号
            machineNO = lab_machineNO.Text = mg_user.machineNO;            //工位
            //this.pic_user.Image = Image.FromFile(Application.StartupPath + "\\image\\user\\" + mg_user.photo);
            this.pic_user.Image = Image.FromFile(Application.StartupPath + "\\image\\user\\" + mg_user.user_pic);
        }
        #endregion

        #region 工作时长
        private void SettingWorkDuration()
        {
            finish = ReadSingleBoolValueFromOPC(OPCConfigEnum.Finish);                  //工作结束信号点
            start = ReadSingleBoolValueFromOPC(OPCConfigEnum.Start);                  //工作开始信号点

            if (start && !finish)//计时
            {
                if (workDuration == 0)
                {
                    this.timer1.Start();
                }
            }
            else if (!start && finish)//计时
            {
                workDuration = 0;
                this.lab_time.Text = workDuration.ToString();
                this.timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            workDuration++;
            this.lab_time.Text = workDuration.ToString();
        }
        #endregion

        #region 获取产品基础信息 ： 生产序号、座椅件号、座椅件名
        private void GetProductInfo()
        {
            vin = ReadProductVINFromOPC();                  //获取产品工作标识
            this.lab_pvin.Text = vin;

            DataTable dt = mg_workqueueBLL.GetProductInforForVIN(vin);
            if (DataHelper.HasData(dt))
            {
                this.lab_pNO.Text = DataHelper.GetCellDataToStr(dt.Rows[0], "PartNO");
                this.lab_pposition.Text = DataHelper.GetCellDataToStr(dt.Rows[0], "PartDesc");
                productType = DataHelper.GetCellDataToStr(dt.Rows[0], "ProductType");
            }
        }
        //读取product_vin
        private string ReadProductVINFromOPC()
        {
            return ReadSingleValueFromOPC(OPCConfigEnum.Product_VIN);
        }
        #endregion

        #region  获取扭矩信息
        private void GetTorqueInfo()
        {
            string torquevalue = ReadSingleValueFromOPC(OPCConfigEnum.Torque_Value);
            string torqueAngle = ReadSingleValueFromOPC(OPCConfigEnum.Torque_Angle);
            this.lab_TorqueAngle.Text = torqueAngle + " °";
            this.lab_TorqueValue.Text = torquevalue + " Nm";
        }
        #endregion

        #region 工序步骤
        private void BindODS()
        {
            if (!string.IsNullOrEmpty(vin) && vin.Length == 18)
            {
                odsAllTB = null;
                odsAllTB = mg_ODSBLL.Getmg_ODSByMachineNO(machineNO);

                this.grd_ODS.DataSource = odsAllTB;
            }

        }

        private void grd_ODS_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SelectedGRDRow();
        }

        private void SelectedGRDRow()
        {
            foreach (DataGridViewRow row in this.grd_ODS.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            Step_Num = Convert.ToInt32(ReadSingleValueFromOPC(OPCConfigEnum.StepNumber));
            int index = (Step_Num == 0) ? 0 : Step_Num - 1;
            if (Step_Num != 0)
            {
                this.grd_ODS.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
                this.grd_ODS.FirstDisplayedScrollingRowIndex = index;

                DataRow datarow = odsAllTB.Rows[Step_Num - 1];

                if (datarow != null)
                {
                    ODS_Pic = DataHelper.GetCellDataToStr(datarow, "picture");
                    ODS_Txt = DataHelper.GetCellDataToStr(datarow, "explain");
                    this.lab_CurruntODS.Text = ODS_Txt;
                    this.pic_bom.Image = Image.FromFile(Application.StartupPath + "\\image\\program\\" + ODS_Pic);
                }
                else
                {
                    this.lab_CurruntODS.Text = "";
                    this.pic_bom.Image = null;
                }

                machineStepTB = null;
                machineStepTB = mg_machinestepBLL.GetMachineStepByMachineNOAndStepAndBomtype(machineNO, Step_Num, productType);
                if (DataHelper.HasData(machineStepTB))
                {
                    ODS_Num = DataHelper.GetCellDataToStr(machineStepTB.Rows[0], "ODS");
                    DataType = NumericParse.StringToInt(DataHelper.GetCellDataToStr(machineStepTB.Rows[0], "datatype"));
                    BOM = DataHelper.GetCellDataToStr(machineStepTB.Rows[0], "BOM");
                }
            }
            


        }
        #endregion

        #region 工序步骤中相应的零件
        private void BindBOM()
        {
            bomAllTB = null;
            //bomAllTB = mg_BOMBLL.GetBomByBOMtype(productType, vin, mg_user.UID);
            //bomAllTB = mg_BOMBLL.GetBomByBOMtype(productType, vin, mg_user.user_id);
            if (DataHelper.HasData(bomAllTB))
            {
                this.grd_bom.DataSource = bomAllTB;
            }
        }


        private void grd_bom_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SelectedbomGRDRow();
        }
        private void SelectedbomGRDRow()
        {
            foreach (DataGridViewRow row in this.grd_bom.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }

            if (!string.IsNullOrEmpty(BOM))
            {
                int index = Convert.ToInt32(BOM) - 1;
                this.grd_bom.Rows[index].DefaultCellStyle.BackColor = Color.LightBlue;
                this.grd_bom.FirstDisplayedScrollingRowIndex = (index != -1) ? index : 0;
                DataRow[] rows = bomAllTB.Select("indexNO=" + BOM);
                if (rows != null && rows.Length > 0)
                {
                    this.lab_bom.Text = rows[0]["name"].ToString();

                }
                else
                {
                    this.lab_bom.Text = "";
                }
            }
            else
            {
                this.lab_bom.Text = "";
            }

        }
        #endregion

        #region 获取零件扫描值
        private void GetScanCodeInfo()
        {
            string scancode = ReadSingleValueFromOPC(OPCConfigEnum.Scan_Code_Gun);
            this.lab_scancode.Text = scancode.ToString();
        }
        #endregion

        #region 控件事件
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button9_Click(object sender, EventArgs e)
        {

        }
        private void button14_Click(object sender, EventArgs e)
        {

        }
        private void button13_Click(object sender, EventArgs e)
        {

        }
        private void button12_Click(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {

        }
        private void button11_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        #region OPC 服务

        OPCServer ConnectedOPCServer;
        OPCGroups ConnectedServerGroup;
        OPCGroup ConnectedGroup;

        OPCItems OPCItemCollection;
        OPCItem[] KepItemArray = new OPCItem[100];  //有时候索引会有问题，超出数据范围

        //int itemcount;
        string[] OPCItemIDs = new string[70];
        int[] ItemServerHandles = new int[70];
        int[] ClientHandles = new int[70];
        string[] OPCITEM = new string[70];



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
                ConnectedGroup = ConnectedServerGroup.Add("Group1");
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

                //OPCConfig.SettingTagAddress((mg_user.gongwei == 1) ? OPCConfig.WorkStationStartPoint : OPCConfig.WorkStationEndPoint);

                //Type opcSignal = typeof(OPCConfigEnum);
                //Array Arrays = Enum.GetValues(opcSignal);
                //for (int i = 0; i <= Arrays.LongLength; i++)
                //{
                //    OPCItemIDs[i] = OPCConfig.Addr_Alarm;
                //    ClientHandles[i] = (int)OPCConfigEnum.Alarm;
                //}

                OPCItemIDs[1] = OPCConfig.Addr_Alarm;
                ClientHandles[1] = (int)OPCConfigEnum.Alarm;
                OPCItemIDs[2] = OPCConfig.Addr_Finish;
                ClientHandles[2] = (int)OPCConfigEnum.Finish;

                OPCItemIDs[3] = OPCConfig.Addr_Start;
                ClientHandles[3] = (int)OPCConfigEnum.Start;

                OPCItemIDs[4] = OPCConfig.Addr_StepNumber;
                ClientHandles[4] = (int)OPCConfigEnum.StepNumber;

                OPCItemIDs[5] = OPCConfig.Addr_Product_VIN;
                ClientHandles[5] = (int)OPCConfigEnum.Product_VIN;

                OPCItemIDs[6] = OPCConfig.Addr_Scan_Code_Gun;
                ClientHandles[6] = (int)OPCConfigEnum.Scan_Code_Gun;

                OPCItemIDs[7] = OPCConfig.Addr_Torque_Angle;
                ClientHandles[7] = (int)OPCConfigEnum.Torque_Angle;

                OPCItemIDs[8] = OPCConfig.Addr_Torque_Value;
                ClientHandles[8] = (int)OPCConfigEnum.Torque_Value;

                OPCItemIDs[9] = OPCConfig.Addr_MatchResult;
                ClientHandles[9] = (int)OPCConfigEnum.MatchResult;

                OPCItemIDs[10] = OPCConfig.Addr_TraceCode;
                ClientHandles[10] = (int)OPCConfigEnum.TraceCode;

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

        private void RemoveItems()
        {
            try
            {
                //Array Errors;
                //for (int i = 0; i < Program.OpcStaticCount + Program.Opc_TagCount; i++)
                //{
                //    int[] temp = new int[] { 0, KepItemArray[i + 1].ServerHandle };
                //    Array serverHandle = (Array)temp;
                //    OPCItemCollection.Remove(1, ref serverHandle, out Errors);
                //}
            }
            catch (Exception exception)
            {
                DisplayOPC_COM_ErrorValue("OPC Remove Items", exception.Message);
            }
        }

        private void RemoveGroup()
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
        /// 单个OPC取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string ReadSingleValueFromOPC(OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            KepItemArray[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return ItemValues.ToString();
        }
        /// <summary>
        /// 单个OPC取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ReadSingleBoolValueFromOPC(OPCConfigEnum opcEnum)
        {
            object ItemValues;
            object Qualities;
            object TimeStamps;
            KepItemArray[(int)opcEnum].Read(1, out ItemValues, out Qualities, out TimeStamps);
            return Convert.ToBoolean(ItemValues);
        }

        private void DisconnectServer()
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

        private void DisplayOPC_COM_ErrorValue(string OPC_function, string ErrorCode)
        {
            string ErrorDisplay = "The OPC function" + OPC_function + "has returned an error of " + ErrorCode.ToString() + "";
            MessageBox.Show(ErrorDisplay, "OPC Function Error", MessageBoxButtons.OK);
        }
        #endregion


    }
}
