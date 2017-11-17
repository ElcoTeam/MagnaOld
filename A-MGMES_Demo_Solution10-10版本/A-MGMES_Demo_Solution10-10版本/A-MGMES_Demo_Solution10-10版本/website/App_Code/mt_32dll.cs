using System;
using System.Runtime.InteropServices;
namespace MT3
{
    /// <summary>
    /// MT3的API接口说明。
    /// </summary>
    public class mt_32dll
    {
        public mt_32dll()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //打开读写器open_device
        //说明：打开通讯接口
        public static extern int open_device(byte nPort,long ulBaud);

        [DllImport("mt_32.dll", EntryPoint = "close_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //关闭读写器close_device
        //说明：    关闭通讯口
        public static extern Int16 close_device(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "hex_asc", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //将16 进制数转换为ASCII 字符hex_asc
        public static extern Int16 hex_asc([MarshalAs(UnmanagedType.LPArray)]byte[] sHex,[MarshalAs(UnmanagedType.LPArray)]byte[] sAsc, ulong ulLength);

        [DllImport("mt_32.dll", EntryPoint = "asc_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //将ASCII 字符转换为16 进制数asc_hex
        public static extern Int16 asc_hex([MarshalAs(UnmanagedType.LPArray)]byte[] sAsc,[MarshalAs(UnmanagedType.LPArray)]byte[] sHex,ulong ulLength);

        [DllImport("mt_32.dll", EntryPoint = "ICC_Reset", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]  //接触式卡片上电复位ICC_Reset
        public static extern Int16 ICC_Reset(int icdev, byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_PowerOn", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式卡片上电复位ICC_PowerOn
        public static extern Int16 ICC_PowerOn(int icdev, byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_CommandExchange", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式卡片APDU ICC_CommandExchange
        public static extern Int16 ICC_CommandExchange(int icdev,byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sCmd,short nCmdLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sResp, [MarshalAs(UnmanagedType.LPArray)]byte[] nRespLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_PowerOff", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式卡片下电ICC_PowerOff
        public static extern Int16 ICC_PowerOff(int icdev, byte nCardSet);

        [DllImport("mt_32.dll", EntryPoint = "ICC_CommandExchange_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式卡片APDU ICC_CommandExchange_hex
        public static extern Int16 ICC_CommandExchange_hex(int icdev,  byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sCmd,  [MarshalAs(UnmanagedType.LPArray)]byte[] sResp);

        [DllImport("mt_32.dll", EntryPoint = "contact_select", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式存储卡类型设置contact_select
        public static extern Int16 contact_select(int icdev,byte nCardType);

        [DllImport("mt_32.dll", EntryPoint = "contact_verify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //接触式存储卡类型识别contact_verify
        public static extern Int16 contact_verify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] nCardType);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_is42", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)] //检测4442 卡sle4442_is42
        public static extern Int16 sle4442_is42(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sCardState);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读4442 卡sle4442_read
        public static extern Int16 sle4442_read(int icdev,byte nAddr,short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //写4442 卡sle4442_write
        public static extern Int16 sle4442_write(int icdev,byte nAddr,short nWLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 卡密码校验sle4442_pwd_check
        public static extern Int16 sle4442_pwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        //[DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_read", SetLastError = true,
        //     CharSet = CharSet.Auto, ExactSpelling = false,
        //     CallingConvention = CallingConvention.StdCall)] //4442 卡读取密码sle4442_pwd_read（暂不支持）
        //public static extern Int16 sle4442_pwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 卡更改密码sle4442_pwd_modify
        public static extern Int16 sle4442_pwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_probit_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 卡读保护位sle4442_probit_read
        public static extern Int16 sle4442_probit_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sProBitData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_probit_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 卡写保护位sle4442_probit_write
        public static extern Int16 sle4442_probit_write(int icdev,byte nAddr,short nWLen,[MarshalAs(UnmanagedType.LPArray)]byte[]sProBitData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_errcount_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读错误次数sle4442_errcount_read
        public static extern Int16 sle4442_errcount_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrCount);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_is28", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //检测4428 卡sle4428_is28
        public static extern Int16 sle4428_is28(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sCardState);
	
        [DllImport("mt_32.dll", EntryPoint = "sle4428_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读4428 卡sle4428_read
        public static extern Int16 sle4428_read(int icdev, short nAddr, short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //写4428 卡sle4428_write
        public static extern Int16 sle4428_write(int icdev, short nAddr, short nWLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 卡密码校验sle4428_pwd_check
        public static extern Int16 sle4428_pwd_check(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 卡更改密码sle4428_pwd_modify
        public static extern Int16 sle4428_pwd_modify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_probit_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 卡读保护位sle4428_probit_readdata
        public static extern Int16 sle4428_probit_readdata(int icdev, short nAddr, short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_probit_writedata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 写保护sle4428_probit_writedata
        public static extern Int16 sle4428_probit_writedata(int icdev, short nAddr, short nWLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //卡片密码校验at88sc102_pwd_check
        public static extern Int16 at88sc102_pwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //更改卡片密码at88sc102_pwd_modify
        public static extern Int16 at88sc102_pwd_modify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读取卡片密码at88sc102_pwd_read
        public static extern Int16 at88sc102_pwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //用户区1 擦除密码校验at88sc102_ua1_epwd_check
        public static extern Int16 at88sc102_ua1_epwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //更改用户区1 擦除密码at88sc102_ua1_epwd_modify
        public static extern Int16 at88sc102_ua1_epwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读取用户区1 擦除密码at88sc102_ua1_epwd_read
        public static extern Int16 at88sc102_ua1_epwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //用户区2 擦除密码校验at88sc102_ua2_epwd_check
        public static extern Int16 at88sc102_ua2_epwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_epwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //更改用户区2 擦除密码at88sc102_ua2_epwd_modify
        public static extern Int16 at88sc102_ua2_epwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_epwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读取用户区2 擦除密码at88sc102_ua2_epwd_read
        public static extern Int16 at88sc102_ua2_epwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_errorcount", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读取密码错误计数器值at88sc102_pwd_errorcount
        public static extern Int16 at88sc102_pwd_errorcount(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrorCount);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_fusecount", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读取EZ2 擦除计数器值at88sc102_ua2_fusecount
        public static extern Int16 at88sc102_ua2_fusecount(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrorCount);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_probit_clr", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //保护位清零at88sc102_probit_clr
        public static extern Int16 at88sc102_probit_clr(int icdev,byte nProBitType);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读用户区1 数据at88sc102_ua1_readdata
        public static extern Int16 at88sc102_ua1_readdata(int icdev,byte nAddr,byte nRLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //读用户区2 数据at88sc102_ua2_readdata
        public static extern Int16 at88sc102_ua2_readdata(int icdev,byte nAddr,byte nRLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_clrdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //擦除用户区1 数据at88sc102_ua1_clrdata
        public static extern Int16 at88sc102_ua1_clrdata(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_clrdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //擦除用户区2 数据at88sc102_ua2_clrdata
        public static extern Int16 at88sc102_ua2_clrdata(int icdev,byte nECState);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_Anafuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //模拟熔断at88sc102_Anafuse
        public static extern Int16 at88sc102_Anafuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //取消模拟熔断at88sc102_Anafuse_cancel
        public static extern Int16 at88sc102_Anafuse_cancel(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_modifydata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //修改用户区1 的数据at88sc102_ua1_modifydata
        public static extern Int16 at88sc102_ua1_modifydata(int icdev,byte nAddr,byte nMLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sModifyData);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //修改用户区2 的数据at88sc102_ua2_modifydata
        public static extern Int16 at88sc102_ua2_modifydata(int icdev,byte nECState,byte nAddr,byte nMLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sModifyData);
        
        [DllImport("mt_32.dll", EntryPoint = "at88sc102_fuse_issuerfuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //熔断ISSUER FUSE at88sc102_fuse_issuerfuse
        public static extern Int16 at88sc102_fuse_issuerfuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_fuse_ec2enfuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //熔断EC2EN FUSE at88sc102_fuse_ec2enfuse
        public static extern Int16 at88sc102_fuse_ec2enfuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_is102", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //检测102 卡at88sc102_is102
        public static extern Int16 at88sc102_is102(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nCardState);

        [DllImport("mt_32.dll", EntryPoint = "OpenCard", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //激活非接触式卡OpenCard
        public static extern Int16 OpenCard(int icdev, byte mode, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr,[MarshalAs(UnmanagedType.LPArray)]byte[] snr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ExchangePro", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //应用层传输命令ExchangePro
        public static extern Int16 ExchangePro(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sAtr,short len, [MarshalAs(UnmanagedType.LPArray)]byte[] Rec, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "CloseCard", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //设置非接触式卡片为halt 状态CloseCard
        public static extern Int16 CloseCard(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "rf_card", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //激活非接触式存储卡rf_card
        public static extern Int16 rf_card(int icdev, byte mode, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] len);

        [DllImport("mt_32.dll", EntryPoint = "rf_authentication_key", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡认证扇区rf_authentication_key
        public static extern Int16 rf_authentication_key(int icdev, byte mode, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] key);

        [DllImport("mt_32.dll", EntryPoint = "rf_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡读数据rf_read
        public static extern Int16 rf_read(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] read);

        [DllImport("mt_32.dll", EntryPoint = "rf_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡写数据rf_write
        public static extern Int16 rf_write(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] write);

        [DllImport("mt_32.dll", EntryPoint = "rf_initval", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡写值块rf_initval
        public static extern Int16 rf_initval(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_increment",SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡加值rf_increment
        public static extern Int16 rf_increment(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_decrement", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡减值rf_decrement
        public static extern Int16 rf_decrement(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_readval", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //非接触式存储卡读值块rf_readval
        public static extern Int16 rf_readval(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]long[] val);
    }
}
