using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGNMEServer
{
    public static class OPCConfig
    {
        public static string KepServerVersion;

        public static string Channel;
        public static string Device_FS;
        public static string Device_FSB;
        public static string Device_FSC;
        public static string Device_RSB;
        public static string Device_RSC;

        public static string TagStartPoint;
        public static string TagEndPoint;

        public static string TagCount;

        public static string st_fs1;
        public static string st_fs2;
        public static string st_fsb1;
        public static string st_fsc1;
        public static string st_rsb40;
        public static string st_rsb60;
        public static string st_rsc1;


    }

    public enum OPCConfigEnum : int
    {
        VIN_1 = 1,
        VIN_2,
        VIN_3,
        VIN_4,
        VIN_5,
        VIN_6,
        VIN_7,
        VIN_8,
        VIN_9,
        VIN_10,
        VIN_11,
        VIN_12,
        VIN_13,
        VIN_14,
        VIN_15,
        VIN_16,
        VIN_17,
        VIN_18,
        VIN_19,
        VIN_20,
        Product_VIN,
        Flag
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
