

/*必要库*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace Tools
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileOperator
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="filename">文件</param>
        /// <param name="extension">判断的扩展名组</param>
        /// <param name="flag">是否返回</param>
        /// <param name="contentLength">文件长度  n * 1KB</param>
        /// <param name="fileupload">上传控件  System.Web.UI.WebControls.FileUpload</param>
        /// <returns>bool</returns>
        public static bool UploadFile(string dir, string filename, string[] extension, bool flag, int contentLength, System.Web.UI.WebControls.FileUpload fileupload)
        {
            if (!Directory.Exists(dir))                 //判断目录是否存在
            {
                Directory.CreateDirectory(dir);
            }

            string[] ext = extension;
            string file = dir + filename;

            if (string.IsNullOrEmpty(file))             //上传文件是否存在
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('请上传相关文件');</script>");
                if (!flag)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>history.back();</script>");
                    System.Web.HttpContext.Current.Response.End();
                }
            }
            else
            {
                CheckFileExtension(ext, file);           //检测文件扩展名

                CheckCapacity(contentLength, fileupload);//检测文件大小

                string imgUri = dir + filename;

                FileUpload(fileupload, file, imgUri);   //上传

            }
            return true;
        }

        //上传
        private static void FileUpload(System.Web.UI.WebControls.FileUpload fileupload, string file, string imgUri)
        {
            try
            {
                if (System.IO.File.Exists(imgUri))
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('此文件已存在，建议重命名后继续上传');if(!confirm('若继续将覆盖原文件,确定继续吗？')){history.back();}</script>");
                    System.IO.File.Delete(imgUri);
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }

                fileupload.SaveAs(imgUri);
            }
            catch (Exception)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('上传文件操作失败，请刷新页面重试!');history.back();</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            finally
            {
                fileupload.Dispose();
            }
        }

        //检测文件大小
        private static void CheckCapacity(int contentLength, System.Web.UI.WebControls.FileUpload fileupload)
        {
            long bytes = contentLength * 1000;
            byte[] b = fileupload.FileBytes;
            if (b.Length >= bytes)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('对不起，为了网络的流畅，本网站只能上传容量为小于 " + contentLength + "KB的文件,请上传正确文件');history.back();</script>");
                System.Web.HttpContext.Current.Response.End();
            }
        }

        //检测文件扩展名
        private static void CheckFileExtension(string[] ext, string file)
        {
            if (ext != null)
            {
                string extensionImg = Path.GetExtension(file).ToLower();
                int count = 0;
                for (int i = 0; i < ext.Length; i++)
                {
                    if (ext[i].Equals(extensionImg))
                    {
                        ++count;
                        break;
                    }
                }
                if (count == 0)
                {
                    string s = " ";
                    for (int i = 0; i < ext.Length; i++)
                    {
                        s += ext[0].ToString() + " ";
                    }
                    System.Web.HttpContext.Current.Response.Write("<script>alert('对不起，为了网络的流畅，本网站只能上传后缀名为:  " + s + "  的文件,请上传正确文件');history.back();</script>");
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 删除服务器文件
        /// </summary>
        /// <param name="uri">文件资源uri</param>
        public static void DeleteFile(string uri)
        {
            try
            {
                if (File.Exists(uri))
                {
                    File.Delete(uri);
                }
            }
            catch (Exception)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('由于网络或者文件权限问题，此文件无法删除，请刷新重试或与相关人员联系');history.back();</script>");
                System.Web.HttpContext.Current.Response.End();
            }
        }

        public static long GetFileSize(string uri)
        {
            try
            {
                if (File.Exists(uri))
                {
                    FileInfo info = new FileInfo(uri);
                    return info.Length/1000;
                }
            }
            catch (Exception)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('读取文件失败');history.back();</script>");
                System.Web.HttpContext.Current.Response.End();
            }
            return 0;
        }



        public static string MapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用             
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {                     //strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');                     
                    strPath = strPath.TrimStart('\\');
                } return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
    }
}
