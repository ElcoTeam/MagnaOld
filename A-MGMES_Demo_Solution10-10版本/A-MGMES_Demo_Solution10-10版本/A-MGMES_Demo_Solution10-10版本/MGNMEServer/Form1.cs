using OPCAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Bll;
using Tools;


namespace MGNMEServer
{
    public partial class Form1 : Form
    {
        OPCHelper opcHelper;

        DataTable fsdtable;
        DataTable fsdbtable;
        DataTable fsdctable;
        DataTable rsb40table;
        DataTable rsb60table;
        DataTable rsctable;

       

        bool isFSzhu = true;
        bool isFSBzhu = true;
        bool isFSCzhu = true;

        public Form1()
        {
            InitializeComponent();
        }



        //下发订单
        private void button1_Click(object sender, EventArgs e)
        {
            this.timer_fs1.Start();
            this.timer_fsb.Start();
            this.timer_fsc.Start();
            this.timer_rsb40.Start();
            this.timer_rsb60.Start();
            this.timer_rsc.Start();
        }
        //暂停下发
        private void button2_Click(object sender, EventArgs e)
        {
            this.timer_fs1.Stop();
            this.timer_fsb.Stop();
            this.timer_fsc.Stop();
            this.timer_rsb40.Stop();
            this.timer_rsb60.Stop();
            this.timer_rsc.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //开起kepserver
            opcHelper = new OPCHelper();
            opcHelper.InitOPCServer();
            opcHelper.group_fs1.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_fs1_DataChange);
            opcHelper.group_fsb.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_fsb_DataChange);
            opcHelper.group_fsc.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_fsc_DataChange);
            opcHelper.group_rsb40.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_rsb40_DataChange);
            opcHelper.group_rsb60.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_rsb60_DataChange);
            opcHelper.group_rsc.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(group_rsc_DataChange);

            isFSzhu = mg_CuttedOrderBLL.CheckFSzhu();
            isFSBzhu = mg_CuttedOrderBLL.CheckFSBzhu();
            isFSCzhu = mg_CuttedOrderBLL.CheckFSCzhu();
        }
        void group_rsc_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_rsc[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_rsc[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(rsctable))
                    {
                        UpdateThePLC_FLAG(ref rsctable, opcHelper.group_rsc, opcHelper.items_rsc, opcHelper.itemArr_rsc, opcHelper.ItemServerHandles_rsc);
                    }
                }
                else if (itemid == opcHelper.itemAddress_rsc[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    PLC_FLAGChangeHandles(opcHelper.itemArr_rsc, ref rsctable);
                }
            }
        }

        void group_rsb60_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_rsb60[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_rsb60[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(rsb60table))
                    {
                        UpdateThePLC_FLAG(ref rsb60table, opcHelper.group_rsb60, opcHelper.items_rsb60, opcHelper.itemArr_rsb60, opcHelper.ItemServerHandles_rsb60);
                    }
                }
                else if (itemid == opcHelper.itemAddress_rsb60[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    PLC_FLAGChangeHandles(opcHelper.itemArr_rsb60, ref rsb60table);
                }
            }
        }

        void group_rsb40_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_rsb40[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_rsb40[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(rsb40table))
                    {
                        UpdateThePLC_FLAG(ref rsb40table, opcHelper.group_rsb40, opcHelper.items_rsb40, opcHelper.itemArr_rsb40, opcHelper.ItemServerHandles_rsb40);
                    }
                }
                else if (itemid == opcHelper.itemAddress_rsb40[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    PLC_FLAGChangeHandles(opcHelper.itemArr_rsb40, ref rsb40table);
                }
            }
        }

        void group_fsc_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {

            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_fsc[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_fsc[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(fsdctable))
                    {
                        UpdateTheFS_PLC_FLAG(ref fsdctable, opcHelper.group_fsc, opcHelper.items_fsc, opcHelper.itemArr_fsc, opcHelper.ItemServerHandles_fsc);
                    }
                }
                else if (itemid == opcHelper.itemAddress_fsc[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    FS_PLC_FLAGChangeHandles(opcHelper.itemArr_fsc, ref fsdctable, ref isFSCzhu);
                }
            }

        }

        void group_fsb_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {

            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_fsb[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_fsb[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(fsdbtable))
                    {
                        UpdateTheFS_PLC_FLAG(ref fsdbtable, opcHelper.group_fsb, opcHelper.items_fsb, opcHelper.itemArr_fsb, opcHelper.ItemServerHandles_fsb);
                    }
                }
                else if (itemid == opcHelper.itemAddress_fsb[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    FS_PLC_FLAGChangeHandles(opcHelper.itemArr_fsb, ref fsdbtable, ref  isFSBzhu);
                }
            }

        }

        void group_fs1_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {

            for (int i = 1; i <= NumItems; i++)
            {
                string itemid = opcHelper.itemArr_fs1[Convert.ToInt32(ClientHandles.GetValue(i))].ItemID;
                if (itemid == opcHelper.itemAddress_fs1[(int)OPCConfigEnum.Product_VIN])        //订单号发生变化
                {
                    if (DataHelper.HasData(fsdtable))
                    {
                        UpdateTheFS_PLC_FLAG(ref fsdtable, opcHelper.group_fs1, opcHelper.items_fs1, opcHelper.itemArr_fs1, opcHelper.ItemServerHandles_fs1);
                    }
                }
                else if (itemid == opcHelper.itemAddress_fs1[(int)OPCConfigEnum.Flag])          //flag变化
                {
                    FS_PLC_FLAGChangeHandles(opcHelper.itemArr_fs1, ref fsdtable, ref isFSzhu);
                }
            }

        }




        //FS1
        private void timer_fs1_Tick(object sender, EventArgs e)
        {
            WriteTheFS_PLC_VIN(opcHelper.group_fs1, opcHelper.items_fs1, opcHelper.itemArr_fs1, opcHelper.ItemServerHandles_fs1, ref fsdtable);
        }

        //FSB
        private void timer_fsb_Tick(object sender, EventArgs e)
        {
            WriteTheFS_PLC_VIN(opcHelper.group_fsb, opcHelper.items_fsb, opcHelper.itemArr_fsb, opcHelper.ItemServerHandles_fsb, ref  fsdbtable);
        }

        //FSC
        private void timer_fsc_Tick(object sender, EventArgs e)
        {
            WriteTheFS_PLC_VIN(opcHelper.group_fsc, opcHelper.items_fsc, opcHelper.itemArr_fsc, opcHelper.ItemServerHandles_fsc, ref  fsdctable);
        }

        //RSB40
        private void timer_rsb40_Tick(object sender, EventArgs e)
        {
            WriteThePLC_VIN(opcHelper.group_rsb40, opcHelper.items_rsb40, opcHelper.itemArr_rsb40, opcHelper.ItemServerHandles_rsb40, ref  rsb40table);
        }

        //RSB60
        private void timer_rsb60_Tick(object sender, EventArgs e)
        {
            WriteThePLC_VIN(opcHelper.group_rsb60, opcHelper.items_rsb60, opcHelper.itemArr_rsb60, opcHelper.ItemServerHandles_rsb60, ref  rsb60table);
        }

        //RSC
        private void timer_rsc_Tick(object sender, EventArgs e)
        {
            WriteThePLC_VIN(opcHelper.group_rsc, opcHelper.items_rsc, opcHelper.itemArr_rsc, opcHelper.ItemServerHandles_rsc, ref  rsctable);
        }

        /*
         *
         * 
         * 
         * 
         * 
         */
        #region RSB\RSC

        private void PLC_FLAGChangeHandles(OPCItem[] itemArr, ref DataTable dt)
        {
            bool flag1 = opcHelper.ReadSingleBoolValueFromOPC(itemArr, OPCConfigEnum.Flag);
            if (flag1)
            {
                DataRow row = dt.Rows[0];
                string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
                string coid = DataHelper.GetCellDataToStr(row, "coid");
                string id = DataHelper.GetCellDataToStr(row, "id");
                if (itemArr == opcHelper.itemArr_rsb40)
                {
                    mg_CuttedOrderBLL.IsPublished(coid, id, "mg_Order_RSB40","rsb40");
                }
                else if (itemArr == opcHelper.itemArr_rsb60)
                {
                    mg_CuttedOrderBLL.IsPublished(coid, id, "mg_Order_RSB60", "rsb60");
                }
                else if (itemArr == opcHelper.itemArr_rsc)
                {
                    mg_CuttedOrderBLL.IsPublished(coid, id, "mg_Order_RSC", "rsc");
                }
                dt = null;
            }
        }
        private void UpdateThePLC_FLAG(ref DataTable dt, OPCGroup group, OPCItems items, OPCItem[] itemArr, int[] itemsHandles)
        {
            DataRow row = dt.Rows[0];
            string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
            string vin = opcHelper.ReadSingleValueFromOPC(itemArr, OPCConfigEnum.Product_VIN);
            if (orderVIN == vin)
            {
                opcHelper.OPCItemWrite((int)OPCConfigEnum.Flag, Convert.ToString("1"), items, itemsHandles, group);
            }
        }

        private void WriteThePLC_VIN(OPCGroup group, OPCItems items, OPCItem[] itemArr, int[] itemhandles, ref DataTable dt)
        {
            //如果plc flag 是 false, 则进行订单下发写值
            bool flag = opcHelper.ReadSingleBoolValueFromOPC(itemArr, OPCConfigEnum.Flag);
            if (!flag)
            {
                //是获取主驾表还是副驾表
                if (!DataHelper.HasData(dt))
                {
                    if (group == opcHelper.group_rsb40)
                    {
                        dt = mg_CuttedOrderBLL.GetData("mg_Order_RSB40", "rsb40");
                    }
                    else if (group == opcHelper.group_rsb60)
                    {
                        dt = mg_CuttedOrderBLL.GetData("mg_Order_RSB60", "rsb60");
                    }
                    else if (group == opcHelper.group_rsc)
                    {
                        dt = mg_CuttedOrderBLL.GetData("mg_Order_RSC", "rsc");
                    }
                }
                if (DataHelper.HasData(dt))
                {
                    //向20 个 PLC点写入
                    DataRow row = dt.Rows[0];
                    string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
                    byte[] temp = new byte[1];

                    for (int i = 1; i <= orderVIN.Length; i++)
                    {
                        temp = System.Text.Encoding.ASCII.GetBytes(orderVIN.Substring(i - 1, 1));
                        opcHelper.OPCItemWrite(i, Convert.ToString(temp[0]), items, itemhandles, group);
                    }
                }
            }
        }


        #endregion



        /*
         *
         * 
         * 
         * 
         * 
         */
        #region 主线

        private void FS_PLC_FLAGChangeHandles(OPCItem[] itemArr, ref DataTable dt, ref bool isZhu)
        {
            bool flag1 = opcHelper.ReadSingleBoolValueFromOPC(itemArr, OPCConfigEnum.Flag);
            if (flag1)
            {
                DataRow row = dt.Rows[0];
                string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
                string coid = DataHelper.GetCellDataToStr(row, "coid");
                string id = DataHelper.GetCellDataToStr(row, "id");
                if (itemArr == opcHelper.itemArr_fs1)
                {
                    mg_CuttedOrderBLL.IsFSPublished(coid, id, isZhu);
                }
                else if (itemArr == opcHelper.itemArr_fsb)
                {
                    mg_CuttedOrderBLL.IsFSBPublished(coid, id, isZhu);
                }
                else if (itemArr == opcHelper.itemArr_fsc)
                {
                    mg_CuttedOrderBLL.IsFSCPublished(coid, id, isZhu);
                }
                isZhu = isZhu ? false : true;
                dt = null;
            }
        }

        private void UpdateTheFS_PLC_FLAG(ref DataTable dt, OPCGroup group, OPCItems items, OPCItem[] itemArr, int[] itemsHandles)
        {
            DataRow row = dt.Rows[0];
            string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
            string vin = opcHelper.ReadSingleValueFromOPC(itemArr, OPCConfigEnum.Product_VIN);
            if (orderVIN == vin)
            {
                opcHelper.OPCItemWrite((int)OPCConfigEnum.Flag, Convert.ToString("1"), items, itemsHandles, group);
            }
        }


        private void WriteTheFS_PLC_VIN(OPCGroup group, OPCItems items, OPCItem[] itemArr, int[] itemhandles, ref DataTable dt)
        {
            //如果plc flag 是 false, 则进行订单下发写值
            bool flag = opcHelper.ReadSingleBoolValueFromOPC(itemArr, OPCConfigEnum.Flag);
            if (!flag)
            {
                //是获取主驾表还是副驾表
                //dt = DataHelper.HasData(dt) ? dt : mg_CuttedOrderBLL.GetFSDData(isFSzhu);
                if (!DataHelper.HasData(dt))
                {
                    if (group == opcHelper.group_fs1)
                    {
                        dt = mg_CuttedOrderBLL.GetFSDData(isFSzhu);
                    }
                    else if (group == opcHelper.group_fsb)
                    {
                        dt = mg_CuttedOrderBLL.GetFSBDData(isFSBzhu);
                    }
                    else if (group == opcHelper.group_fsc)
                    {
                        dt = mg_CuttedOrderBLL.GetFSCDData(isFSCzhu);
                    }
                }
                if (DataHelper.HasData(dt))
                {
                    //向20 个 PLC点写入
                    DataRow row = dt.Rows[0];
                    string orderVIN = DataHelper.GetCellDataToStr(row, "orderno");
                    byte[] temp = new byte[1];

                    for (int i = 1; i <= orderVIN.Length; i++)
                    {
                        temp = System.Text.Encoding.ASCII.GetBytes(orderVIN.Substring(i - 1, 1));
                        opcHelper.OPCItemWrite(i, Convert.ToString(temp[0]), items, itemhandles, group);
                    }
                }
            }
        }
        #endregion


    }
}
