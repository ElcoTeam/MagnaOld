using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGNMESApplication
{
    public static class OPCConfig
    {
        public static string KepServerVersion;
        public static string WorkStationStartPoint;
        public static string WorkStationEndPoint;

        public static string Channel;
        public static string Device;

        public static string TagStartPoint;
        public static string TagEndPoint;

        #region 遗留代码
        public static int TagCount;

        public static string Tag_Alarm;
        public static string Tag_Finish;
        public static string Tag_Start;
        public static string Tag_StepNumber;
        public static string Tag_Product_VIN;
        public static string Tag_Scan_Code_Gun;
        public static string Tag_Torque_Angle;
        public static string Tag_Torque_Value;
        public static string Tag_MatchResult;
        public static string Tag_TraceCode;
        //public static string Tag_ODSNumber;

        public static string Addr_Alarm;
        public static string Addr_Finish;
        public static string Addr_Start;
        public static string Addr_StepNumber;
        public static string Addr_Product_VIN;
        public static string Addr_Scan_Code_Gun;
        public static string Addr_Torque_Angle;
        public static string Addr_Torque_Value;
        public static string Addr_MatchResult;
        public static string Addr_TraceCode;
        //public static string Addr_ODSNumber;

        #endregion

        public static void SettingTagAddress(string Device, string WorkStation)
        {


            Addr_Alarm = Channel + "." + Device + "." + WorkStation + "." + Tag_Alarm;
            Addr_Finish = Channel + "." + Device + "." + WorkStation + "." + Tag_Finish;
            Addr_Start = Channel + "." + Device + "." + WorkStation + "." + Tag_Start;
            Addr_StepNumber = Channel + "." + Device + "." + WorkStation + "." + Tag_StepNumber;
            Addr_Product_VIN = Channel + "." + Device + "." + WorkStation + "." + Tag_Product_VIN;
            Addr_Scan_Code_Gun = Channel + "." + Device + "." + WorkStation + "." + Tag_Scan_Code_Gun;
            Addr_Torque_Angle = Channel + "." + Device + "." + WorkStation + "." + Tag_Torque_Angle;
            Addr_Torque_Value = Channel + "." + Device + "." + WorkStation + "." + Tag_Torque_Value;
            Addr_MatchResult = Channel + "." + Device + "." + WorkStation + "." + Tag_MatchResult;
            Addr_TraceCode = Channel + "." + Device + "." + WorkStation + "." + Tag_TraceCode;
            //Addr_ODSNumber = Channel + "." + Device + "." + WorkStation + "." + Tag_ODSNumber;
        }
    }

    public enum OPCConfigEnum : int
    {
        Alarm = 1,
        Finish,
        Start,
        StepNumber,
        Product_VIN,
        Scan_Code_Gun,
        Torque_Angle,
        Torque_Value,
        MatchResult,
        TraceCode
        //ODSNumber
    }

    // <add key="1" value="Alarm"/>
    //<add key="2" value="Finish"/>
    //<add key="3" value="Start"/>
    //<add key="4" value="StepNumber"/>
    //<add key="5" value="Product_VIN"/>
    //<add key="6" value="Scan_Code_Gun"/>
    //<add key="7" value="Torque_Angle"/>
    //<add key="8" value="Torque_Value"/>
    //<add key="9" value="MatchResult"/>
    //<add key="10" value="TraceCode"/>
}
