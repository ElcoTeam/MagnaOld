using System;
using System.IO;

namespace website
{
    public class WriteLog
    {


        public void WriteLogs(string strmessage)
        {
            try
            {
                string strFilePathName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.ToString() + "\\log";
                if (!Directory.Exists(strFilePathName))
                {
                    Directory.CreateDirectory(strFilePathName);
                }
                string strFileName = strFilePathName + @"\record.log";
                FileStream objFS;
                if (File.Exists(strFileName))
                {
                    FileInfo objFileInfo = new FileInfo(strFileName);
                    objFS = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    objFS.Seek(0, SeekOrigin.End);
                }
                else
                {
                    objFS = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    objFS.Seek(0, SeekOrigin.End);
                }
                StreamWriter objSW = new StreamWriter(objFS);
                objSW.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.") +
                                System.DateTime.Now.Millisecond.ToString());
                objSW.Write(strmessage);
                objSW.WriteLine();
                objSW.Close();
                objFS.Close();
            }
            catch (Exception ex)
            {
                //string err = ex.Message;
                //EventLog.WriteEntry("PiWatchInterFace WriteLog Error:" + ex.Message.ToString());
            }
        }

        public void WriteLogs(string strmessage, string pathname)
        {
            try
            {
                string strFilePathName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.ToString() + "\\log";
                if (!Directory.Exists(strFilePathName))
                {
                    Directory.CreateDirectory(strFilePathName);
                }
                string strFileName = strFilePathName + @"\" + pathname + ".log";
                FileStream objFS;
                if (File.Exists(strFileName))
                {
                    FileInfo objFileInfo = new FileInfo(strFileName);
                    objFS = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    objFS.Seek(0, SeekOrigin.End);
                }
                else
                {
                    objFS = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    objFS.Seek(0, SeekOrigin.End);
                }
                StreamWriter objSW = new StreamWriter(objFS);
                objSW.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.") +
                                System.DateTime.Now.Millisecond.ToString());
                objSW.Write(strmessage);
                objSW.WriteLine();
                objSW.Close();
                objFS.Close();
            }
            catch (Exception ex)
            {
                //string err = ex.Message;
                //EventLog.WriteEntry("PiWatchInterFace WriteLog Error:" + ex.Message.ToString());
            }
        }


        
        
        /// <summary>
        /// 读文件操作
        /// </summary>
        /// <returns></returns>
        public static string ReadLogs(string FilePath)
        {
            StreamReader SR;
            string TempStr, RtnStr = "";
            if (File.Exists(FilePath))
            {
                SR = File.OpenText(FilePath);
                TempStr = SR.ReadLine();
                while (TempStr != null)
                {
                    RtnStr += TempStr + "\r\n";
                    TempStr = SR.ReadLine();

                }
                SR.Close();
            }
            return RtnStr;
        }
    }
}