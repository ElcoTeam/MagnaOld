using System;
using System.Runtime.InteropServices;
namespace MT3
{
    /// <summary>
    /// MT3��API�ӿ�˵����
    /// </summary>
    public class mt_32dll
    {
        public mt_32dll()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�򿪶�д��open_device
        //˵������ͨѶ�ӿ�
        public static extern int open_device(byte nPort,long ulBaud);

        [DllImport("mt_32.dll", EntryPoint = "close_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�رն�д��close_device
        //˵����    �ر�ͨѶ��
        public static extern Int16 close_device(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "hex_asc", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��16 ������ת��ΪASCII �ַ�hex_asc
        public static extern Int16 hex_asc([MarshalAs(UnmanagedType.LPArray)]byte[] sHex,[MarshalAs(UnmanagedType.LPArray)]byte[] sAsc, ulong ulLength);

        [DllImport("mt_32.dll", EntryPoint = "asc_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ASCII �ַ�ת��Ϊ16 ������asc_hex
        public static extern Int16 asc_hex([MarshalAs(UnmanagedType.LPArray)]byte[] sAsc,[MarshalAs(UnmanagedType.LPArray)]byte[] sHex,ulong ulLength);

        [DllImport("mt_32.dll", EntryPoint = "ICC_Reset", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]  //�Ӵ�ʽ��Ƭ�ϵ縴λICC_Reset
        public static extern Int16 ICC_Reset(int icdev, byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_PowerOn", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ��Ƭ�ϵ縴λICC_PowerOn
        public static extern Int16 ICC_PowerOn(int icdev, byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_CommandExchange", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ��ƬAPDU ICC_CommandExchange
        public static extern Int16 ICC_CommandExchange(int icdev,byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sCmd,short nCmdLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sResp, [MarshalAs(UnmanagedType.LPArray)]byte[] nRespLen);

        [DllImport("mt_32.dll", EntryPoint = "ICC_PowerOff", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ��Ƭ�µ�ICC_PowerOff
        public static extern Int16 ICC_PowerOff(int icdev, byte nCardSet);

        [DllImport("mt_32.dll", EntryPoint = "ICC_CommandExchange_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ��ƬAPDU ICC_CommandExchange_hex
        public static extern Int16 ICC_CommandExchange_hex(int icdev,  byte nCardSet, [MarshalAs(UnmanagedType.LPArray)]byte[] sCmd,  [MarshalAs(UnmanagedType.LPArray)]byte[] sResp);

        [DllImport("mt_32.dll", EntryPoint = "contact_select", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ�洢����������contact_select
        public static extern Int16 contact_select(int icdev,byte nCardType);

        [DllImport("mt_32.dll", EntryPoint = "contact_verify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�Ӵ�ʽ�洢������ʶ��contact_verify
        public static extern Int16 contact_verify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] nCardType);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_is42", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)] //���4442 ��sle4442_is42
        public static extern Int16 sle4442_is42(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sCardState);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��4442 ��sle4442_read
        public static extern Int16 sle4442_read(int icdev,byte nAddr,short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //д4442 ��sle4442_write
        public static extern Int16 sle4442_write(int icdev,byte nAddr,short nWLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 ������У��sle4442_pwd_check
        public static extern Int16 sle4442_pwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        //[DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_read", SetLastError = true,
        //     CharSet = CharSet.Auto, ExactSpelling = false,
        //     CallingConvention = CallingConvention.StdCall)] //4442 ����ȡ����sle4442_pwd_read���ݲ�֧�֣�
        //public static extern Int16 sle4442_pwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 ����������sle4442_pwd_modify
        public static extern Int16 sle4442_pwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_probit_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 ��������λsle4442_probit_read
        public static extern Int16 sle4442_probit_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sProBitData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_probit_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4442 ��д����λsle4442_probit_write
        public static extern Int16 sle4442_probit_write(int icdev,byte nAddr,short nWLen,[MarshalAs(UnmanagedType.LPArray)]byte[]sProBitData);

        [DllImport("mt_32.dll", EntryPoint = "sle4442_errcount_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���������sle4442_errcount_read
        public static extern Int16 sle4442_errcount_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrCount);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_is28", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���4428 ��sle4428_is28
        public static extern Int16 sle4428_is28(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sCardState);
	
        [DllImport("mt_32.dll", EntryPoint = "sle4428_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��4428 ��sle4428_read
        public static extern Int16 sle4428_read(int icdev, short nAddr, short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //д4428 ��sle4428_write
        public static extern Int16 sle4428_write(int icdev, short nAddr, short nWLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 ������У��sle4428_pwd_check
        public static extern Int16 sle4428_pwd_check(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 ����������sle4428_pwd_modify
        public static extern Int16 sle4428_pwd_modify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_probit_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 ��������λsle4428_probit_readdata
        public static extern Int16 sle4428_probit_readdata(int icdev, short nAddr, short nDLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "sle4428_probit_writedata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //4428 д����sle4428_probit_writedata
        public static extern Int16 sle4428_probit_writedata(int icdev, short nAddr, short nWLen, [MarshalAs(UnmanagedType.LPArray)]byte[] sWriteData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��Ƭ����У��at88sc102_pwd_check
        public static extern Int16 at88sc102_pwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���Ŀ�Ƭ����at88sc102_pwd_modify
        public static extern Int16 at88sc102_pwd_modify(int icdev, [MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ȡ��Ƭ����at88sc102_pwd_read
        public static extern Int16 at88sc102_pwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_check", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�û���1 ��������У��at88sc102_ua1_epwd_check
        public static extern Int16 at88sc102_ua1_epwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�����û���1 ��������at88sc102_ua1_epwd_modify
        public static extern Int16 at88sc102_ua1_epwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_epwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ȡ�û���1 ��������at88sc102_ua1_epwd_read
        public static extern Int16 at88sc102_ua1_epwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�û���2 ��������У��at88sc102_ua2_epwd_check
        public static extern Int16 at88sc102_ua2_epwd_check(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_epwd_modify", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�����û���2 ��������at88sc102_ua2_epwd_modify
        public static extern Int16 at88sc102_ua2_epwd_modify(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sNewKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_epwd_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ȡ�û���2 ��������at88sc102_ua2_epwd_read
        public static extern Int16 at88sc102_ua2_epwd_read(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sKey);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_pwd_errorcount", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ȡ������������ֵat88sc102_pwd_errorcount
        public static extern Int16 at88sc102_pwd_errorcount(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrorCount);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_fusecount", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //��ȡEZ2 ����������ֵat88sc102_ua2_fusecount
        public static extern Int16 at88sc102_ua2_fusecount(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nErrorCount);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_probit_clr", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //����λ����at88sc102_probit_clr
        public static extern Int16 at88sc102_probit_clr(int icdev,byte nProBitType);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���û���1 ����at88sc102_ua1_readdata
        public static extern Int16 at88sc102_ua1_readdata(int icdev,byte nAddr,byte nRLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_readdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���û���2 ����at88sc102_ua2_readdata
        public static extern Int16 at88sc102_ua2_readdata(int icdev,byte nAddr,byte nRLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sRecData);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_clrdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�����û���1 ����at88sc102_ua1_clrdata
        public static extern Int16 at88sc102_ua1_clrdata(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua2_clrdata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�����û���2 ����at88sc102_ua2_clrdata
        public static extern Int16 at88sc102_ua2_clrdata(int icdev,byte nECState);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_Anafuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //ģ���۶�at88sc102_Anafuse
        public static extern Int16 at88sc102_Anafuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //ȡ��ģ���۶�at88sc102_Anafuse_cancel
        public static extern Int16 at88sc102_Anafuse_cancel(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_ua1_modifydata", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�޸��û���1 ������at88sc102_ua1_modifydata
        public static extern Int16 at88sc102_ua1_modifydata(int icdev,byte nAddr,byte nMLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sModifyData);

        [DllImport("mt_32.dll", EntryPoint = "open_device", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�޸��û���2 ������at88sc102_ua2_modifydata
        public static extern Int16 at88sc102_ua2_modifydata(int icdev,byte nECState,byte nAddr,byte nMLen,[MarshalAs(UnmanagedType.LPArray)]byte[] sModifyData);
        
        [DllImport("mt_32.dll", EntryPoint = "at88sc102_fuse_issuerfuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�۶�ISSUER FUSE at88sc102_fuse_issuerfuse
        public static extern Int16 at88sc102_fuse_issuerfuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_fuse_ec2enfuse", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�۶�EC2EN FUSE at88sc102_fuse_ec2enfuse
        public static extern Int16 at88sc102_fuse_ec2enfuse(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "at88sc102_is102", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���102 ��at88sc102_is102
        public static extern Int16 at88sc102_is102(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] nCardState);

        [DllImport("mt_32.dll", EntryPoint = "OpenCard", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //����ǽӴ�ʽ��OpenCard
        public static extern Int16 OpenCard(int icdev, byte mode, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr,[MarshalAs(UnmanagedType.LPArray)]byte[] snr, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "ExchangePro", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //Ӧ�ò㴫������ExchangePro
        public static extern Int16 ExchangePro(int icdev,[MarshalAs(UnmanagedType.LPArray)]byte[] sAtr,short len, [MarshalAs(UnmanagedType.LPArray)]byte[] Rec, [MarshalAs(UnmanagedType.LPArray)]byte[] nAtrLen);

        [DllImport("mt_32.dll", EntryPoint = "CloseCard", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //���÷ǽӴ�ʽ��ƬΪhalt ״̬CloseCard
        public static extern Int16 CloseCard(int icdev);

        [DllImport("mt_32.dll", EntryPoint = "rf_card", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //����ǽӴ�ʽ�洢��rf_card
        public static extern Int16 rf_card(int icdev, byte mode, [MarshalAs(UnmanagedType.LPArray)]byte[] sAtr, [MarshalAs(UnmanagedType.LPArray)]byte[] len);

        [DllImport("mt_32.dll", EntryPoint = "rf_authentication_key", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢����֤����rf_authentication_key
        public static extern Int16 rf_authentication_key(int icdev, byte mode, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] key);

        [DllImport("mt_32.dll", EntryPoint = "rf_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢��������rf_read
        public static extern Int16 rf_read(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] read);

        [DllImport("mt_32.dll", EntryPoint = "rf_write", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢��д����rf_write
        public static extern Int16 rf_write(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]byte[] write);

        [DllImport("mt_32.dll", EntryPoint = "rf_initval", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢��дֵ��rf_initval
        public static extern Int16 rf_initval(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_increment",SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢����ֵrf_increment
        public static extern Int16 rf_increment(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_decrement", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢����ֵrf_decrement
        public static extern Int16 rf_decrement(int icdev,byte Add,long val);

        [DllImport("mt_32.dll", EntryPoint = "rf_readval", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)] //�ǽӴ�ʽ�洢����ֵ��rf_readval
        public static extern Int16 rf_readval(int icdev, byte Add, [MarshalAs(UnmanagedType.LPArray)]long[] val);
    }
}
