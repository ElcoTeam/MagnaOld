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
using System
    ;
using OPCAutomation;
using System.Threading;
using Bll;
using Tools;
using Model;
using MGMEApplication;

namespace MGNMESApplication
{
    public partial class MainApplicationForm : Form
    {

        public mg_OperatorModel mg_operator;
        public mg_stationModel mg_station;
        mg_partModel partModel;

        OPCHelper opcHelper;
        //
        //FS 70107866 160823 0001
        string vin;                 //生产订单号
        string partNO;         //部件号
        //int step_Num;               //步骤号        //原step_num
        int step_Num;               //步骤号        //原step_num
        //int ods_Num;               //步骤号
        string machineNO;
        int bomrowIndex = 0;
        //
        bool hasInitVIN;
        //工作时长
        int workDuration;
        bool finish;
        bool start;

        DataTable stepAllTB;
        DataTable bomDT;
        DataRow[] bomRows;

        //SQL mySql = new SQL();
        public MainApplicationForm(mg_OperatorModel model, mg_stationModel stationModel)
        {
            hasInitVIN = false;
            this.mg_station = stationModel;
            this.mg_operator = model;
            InitializeComponent();
            workDuration = 0;
            this.lab_time.Text = workDuration.ToString();
            this.grd_step.AutoGenerateColumns = false;
            this.grd_bom.AutoGenerateColumns = false;

            //开起kepserver
            opcHelper = new OPCHelper();
            opcHelper.InitOPCServer(stationModel.st_no, stationModel.fl_name);
            opcHelper.ConnectedGroup.DataChange += ConnectedGroup_DataChange;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            SettingOperatorInfo();      //设置用户信息
            GetOrderInfo();                            //生产序号、座椅件号、座椅件名
            if (!string.IsNullOrEmpty(vin) && vin.Length == 20)
            {
                BindStep();
                BindBOM();
                step_Num = Convert.ToInt32(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.StepNumber));
                if (step_Num != 0)
                {
                    SelectedStepRow();
                    SelectedbomGRDRow();
                    SettingStepInfo();
                    //SettingBomInfo();
                    GetScanCodeInfo();
                    GetTorqueInfo();
                }

                //ods_Num = Convert.ToInt32(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.ODSNumber));
                //if (ods_Num != 0)
                //{
                //    SelectedStepRow();
                //    SettingStepInfo();
                //}

                SettingWorkDuration();
            }
            //GetProductInfo();                            //获取VIN，然后获取产品基础信息 ： 生产序号、座椅件号、座椅件名
            //SettingWorkDuration();                      //工作时长
            //Step_Num = Convert.ToInt32(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.StepNumber));                  //获取工作步骤
            //GetTorqueInfo();                            //获取扭矩信息
            //GetScanCodeInfo();                          //获取零件扫描值
            //BindODS();                      //工作步骤列表
            //BindBOM();                      //零件列表
            ////BindData();             //opc信号数据

            //if (!string.IsNullOrEmpty(vin))
            //{
            //    BindODS();                      //工作步骤列表
            //    BindBOM();                      //零件列表
            //}
        }
        //OPC数据发生变化
        void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.KepItemArray[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;

                if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Alarm])
                {

                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Finish])        //开始为true，结束为false，才累加时长 | 开始为false，结束为true，才关闭累计
                {
                    SettingWorkDuration();
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Start])           //开始为true，结束为false，才累加时长 | 开始为false，结束为true，才关闭累计
                {
                    SettingWorkDuration();
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.StepNumber])        //工序步骤改变
                {
                    step_Num = Convert.ToInt32(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.StepNumber));
                    SelectedStepRow();
                    SelectedbomGRDRow();

                    SettingStepInfo();

                }
                //else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.ODSNumber])        //ODS改变
                //{
                //    ods_Num = Convert.ToInt32(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.ODSNumber));
                //    SelectedStepRow();
                //    SettingStepInfo();
                //    //SelectedbomGRDRow();

                //    //SettingStepAndBomInfo();

                //}
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Product_VIN])        //生产订单号发生变化，则重新绑定相关信息
                {
                    //this.grd_bom.Rows.Clear();
                    //this.grd_step.Rows.Clear();
                    this.lab_bom.Text = "";
                    this.lab_CurruntStep.Text = "";
                    this.lab_TorqueAngle.Text = "";
                    this.lab_TorqueValue.Text = "";
                    step_Num = 1;                  //获取工作步骤
                    //ods_Num = 1;                  //获取工作步骤
                    this.lab_partNO.Text = "";
                    this.lab_partDesc.Text = "";

                    GetOrderInfo();                            //生产序号、座椅件号、座椅件名
                    BindStep();
                    BindBOM();
                    //SelectedStepRow();
                    //SelectedbomGRDRow();

                    if (step_Num != 0)
                    {
                        SelectedStepRow();
                        SelectedbomGRDRow();
                        SettingStepInfo();
                    }
                    //SettingWorkDuration();                      //工作时长
                    //GetTorqueInfo();                            //获取扭矩信息
                    //GetScanCodeInfo();                          //获取零件扫描值
                    //if (!string.IsNullOrEmpty(vin))
                    //{
                    //    BindODS();                      //工作步骤列表
                    //    BindBOM();                      //零件列表
                    //}
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Scan_Code_Gun])     //扫描值
                {
                    GetScanCodeInfo();
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Torque_Angle])       //扭矩角度
                {
                    GetTorqueInfo();
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.Torque_Value])       //扭矩力度
                {
                    GetTorqueInfo();
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.MatchResult])       //特征码-扫描值-匹配
                {
                    bool isFlag = Convert.ToBoolean(opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.MatchResult));
                    if (this.grd_bom.Rows.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        this.grd_bom.Rows[step_Num - 1].Cells[4].Value = isFlag ? "匹配" : "不匹配";
                    }

                    //if (isFlag)
                    //{
                    //    RecordBOMCode();
                    //    BindBOM();                      //零件列表
                    //}
                }
                else if (itemid == opcHelper.OPCItemIDs[(int)OPCConfigEnum.TraceCode])       //追溯码
                {
                    //UpdateStepBomCode();
                    //BindBOM();                      //零件列表
                }
            }
        }



        private void UpdateStepBomCode()
        {



            ////step_scanCode
            //    mg_BOM_MatchModel model = new mg_BOM_MatchModel();
            //    DataRow[] rows = bomAllTB.Select("indexNO=" + BOM);
            //    model.BOMNO = rows[0]["BOMNO"].ToString();
            //    model.VIN = vin;
            //    model.TraceCode =

            //    mg_BOM_MatchBLL bll = new mg_BOM_MatchBLL();
            //    bll.UpdateBOMCode(model);
        }

        private void RecordBOMCode()
        {
            //mg_BOM_MatchModel model = new mg_BOM_MatchModel();
            //DataRow[] rows = bomAllTB.Select("indexNO=" + BOM);
            //model.BOMNO = rows[0]["BOMNO"].ToString();
            //model.ScanCode = opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.Scan_Code_Gun);
            ////model.UID = mg_user.UID;
            //model.UID = mg_operator.op_id.ToString();
            //model.VIN = vin;

            //mg_BOM_MatchBLL bll = new mg_BOM_MatchBLL();
            //bll.RecordBOMCode(model);
        }

        #region 设置用户信息
        private void SettingOperatorInfo()
        {
            lab_operatorName.Text = mg_operator.op_name;               //用户名
            lab_operatorNO.Text = mg_operator.op_no.Substring(mg_operator.op_no.Length - 7);      //用户号
            machineNO = lab_stno.Text = mg_station.st_no;           //工位
            string filename = AppConfigHelper.GetAppConfig("OperatorImgPath") + "\\" + mg_operator.op_pic.Substring(mg_operator.op_pic.LastIndexOf('/') + 1);
            this.pic_operator.Image = Image.FromFile(filename);
        }
        #endregion

        #region 工作时长
        private void SettingWorkDuration()
        {
            finish = opcHelper.ReadSingleBoolValueFromOPC(OPCConfigEnum.Finish);                  //工作结束信号点
            start = opcHelper.ReadSingleBoolValueFromOPC(OPCConfigEnum.Start);                  //工作开始信号点

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

        #region 获取订单信息 ： 生产序号、座椅件号、座椅件名
        private void GetOrderInfo()
        {
            vin = ReadProductVINFromOPC();                  //获取产品工作标识
            //vin = "FS701078661608230001";
            if (!string.IsNullOrEmpty(vin) && vin.Length == 20)
            {
                if (!hasInitVIN)
                {
                    hasInitVIN = true;
                }
                this.lab_pvin.Text = vin;

                //截取部件号
                //FS70107866 160823 0001
                partNO = vin.Substring(2, 8);
                partModel = mg_PartBLL.GetPartModelByPartNO(partNO);

                this.lab_partNO.Text = partModel.part_no;
                this.lab_partDesc.Text = partModel.part_desc;
            }

            //productType = DataHelper.GetCellDataToStr(dt.Rows[0], "ProductType");
        }
        //读取product_vin
        private string ReadProductVINFromOPC()
        {
            return opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.Product_VIN);
        }
        #endregion





        #region 工序步骤

        private void SettingStepInfo()
        {
            DataRow row = stepAllTB.Rows[step_Num - 1];
            this.lab_CurruntStep.Text = DataHelper.GetCellDataToStr(row, "step_desc");

            //string bomimg = DataHelper.GetCellDataToStr(row, "bom_picture");
            //string filename = AppConfigHelper.GetAppConfig("BomImgPath") + "\\" + bomimg.Substring(bomimg.LastIndexOf('/') + 1);
            //this.pic_bom.Image = Image.FromFile(filename);

            string stepimg = DataHelper.GetCellDataToStr(row, "step_pic");
            string filename = AppConfigHelper.GetAppConfig("StepImgPath") + "\\" + stepimg.Substring(stepimg.LastIndexOf('/') + 1);
            this.pic_bom.Image = Image.FromFile(filename);


          //  this.lab_CurruntStep.Text = DataHelper.GetCellDataToStr(row, "step_desc");

        }


        //private void SettingStepAndBomInfo()
        //{
        //    DataRow row = stepAllTB.Rows[step_Num - 1];
        //    //string bomimg = DataHelper.GetCellDataToStr(row, "bom_picture");
        //    //string filename = AppConfigHelper.GetAppConfig("BomImgPath") + "\\" + bomimg.Substring(bomimg.LastIndexOf('/') + 1);
        //    //this.pic_bom.Image = Image.FromFile(filename);

        //    string stepimg = DataHelper.GetCellDataToStr(row, "step_pic");
        //    string filename = AppConfigHelper.GetAppConfig("StepImgPath") + "\\" + stepimg.Substring(stepimg.LastIndexOf('/') + 1);
        //    this.pic_bom.Image = Image.FromFile(filename);


        //    this.lab_CurruntStep.Text = DataHelper.GetCellDataToStr(row, "step_desc");
        //    this.lab_bom.Text = DataHelper.GetCellDataToStr(row, "bom_desc");
        //}

        private void BindStep()
        {
            stepAllTB = null;
            //stepAllTB = mg_StepBLL.GetAllStepForPartAndStation((int)mg_station.fl_id, (int)mg_station.st_id, (int)partModel.part_id);
            string st_ids = mg_StepBLL.GetIDsByMac(mg_station.st_mac);
            stepAllTB = mg_StepBLL.GetAllStepForPartAndStation(st_ids, partNO);
            this.grd_step.DataSource = stepAllTB;
        }

        private void grd_step_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.grd_step.Rows[0].Cells[0].Selected = false;
            //SelectedGRDRow();
        }

        private void SelectedStepRow()
        {
            foreach (DataGridViewRow row in this.grd_step.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            int rowIndex = step_Num - 1;
            this.grd_step.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
            this.grd_step.FirstDisplayedScrollingRowIndex = rowIndex;

        }
        #endregion

        #region 工序步骤中相应的零件
        private void BindBOM()
        {
            //DataView dv = stepAllTB.DefaultView;
            //bomDT = null;
            //bomDT = dv.ToTable(true, "bom_desc", "bom_barcode", "step_scanCode", "Step_matchResult", "bom_id", "bom_picture");
            //this.grd_bom.DataSource = bomDT;
            bomDT = null;
            bomDT = stepAllTB.Clone();
            bomRows = stepAllTB.Select("bom_id<>0");
            foreach (DataRow row in bomRows)
            {
                bomDT.ImportRow(row);
            }

            DataView dv = bomDT.DefaultView;
            dv.Sort = "step_order asc";
            bomDT = dv.ToTable();

            this.grd_bom.DataSource = bomDT;
        }

        private void SettingBomInfo()
        {
            DataRow row = stepAllTB.Rows[step_Num - 1];
            //string bomimg = DataHelper.GetCellDataToStr(row, "bom_picture");
            //string filename = AppConfigHelper.GetAppConfig("BomImgPath") + "\\" + bomimg.Substring(bomimg.LastIndexOf('/') + 1);
            //this.pic_bom.Image = Image.FromFile(filename);
            this.lab_bom.Text = DataHelper.GetCellDataToStr(row, "bom_desc");
        }


        private void grd_bom_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.grd_bom.Rows[0].Cells[0].Selected = false;
            //SelectedbomGRDRow();
        }
        private void SelectedbomGRDRow()
        {
            Image image = this.pic_bom.Image;
            this.pic_bom.Image = null;
            if (image != null)
            {
                image.Dispose();
            }
            this.lab_bom.Text = "";
            this.grd_bom.Rows[0].Cells[0].Selected = false;

            foreach (DataGridViewRow row in this.grd_bom.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            DataRow currentStepRow = stepAllTB.Rows[step_Num - 1];

            //for (int j = 0; j < bomDT.Rows.Count; j++)
            //{
            //    if (bomDT.Rows[j]["step_order"].ToString() == currentStepRow["step_order"].ToString())
            //    {
            //        this.grd_bom.Rows[bomrowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            //        this.grd_bom.FirstDisplayedScrollingRowIndex = bomrowIndex;
            //        SettingBomInfo();
            //        bomrowIndex = j;
            //        break;
            //    }
            //}

            foreach (DataGridViewRow row in this.grd_bom.Rows)
            {
                if (row.Cells["step_order"].Value.ToString() == currentStepRow["step_order"].ToString())
                {
                    row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    this.grd_bom.FirstDisplayedScrollingRowIndex = row.Index;
                    SettingBomInfo();
                    bomrowIndex = row.Index;
                    break;
                }
            }





        }
        #endregion

        #region 获取零件扫描值
        private void GetScanCodeInfo()
        {
            string scancode = opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.Scan_Code_Gun);
            this.lab_scancode.Text = scancode;
            //this.grd_bom.Rows[step_Num - 1].Cells[3].ValueType = typeof(string);
            //this.grd_bom.Rows[step_Num - 1].Cells[3].Value = scancode.ToString();
            this.grd_bom.Rows[bomrowIndex].Cells[2].ValueType = typeof(string);
            this.grd_bom.Rows[bomrowIndex].Cells[2].Value = scancode.ToString();

        }
        #endregion

        #region  获取扭矩信息
        private void GetTorqueInfo()
        {
            string torquevalue = opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.Torque_Value);
            string torqueAngle = opcHelper.ReadSingleValueFromOPC(OPCConfigEnum.Torque_Angle);
            this.lab_TorqueAngle.Text = (!string.IsNullOrEmpty(torqueAngle)) ? torqueAngle + " °" : "";
            this.lab_TorqueValue.Text = (!string.IsNullOrEmpty(torquevalue)) ? torquevalue + " Nm" : "";
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

        private void grd_step_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}
