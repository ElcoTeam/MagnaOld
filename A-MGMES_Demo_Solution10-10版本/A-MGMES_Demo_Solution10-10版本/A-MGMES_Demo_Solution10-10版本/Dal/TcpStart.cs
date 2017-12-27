using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Model;
using System.Web;
using SortManagent.SortDao;
using DAL;
namespace SortManagent.Util
{
    public class TcpStart:HttpApplication
    {
        public static Thread t;
        public static Thread autoSendPrinted;
        public static Thread threadReceive;
        public static Thread myThread;
        SendToMobileDev mp = new SendToMobileDev();
        public static List<Users> userList = new List<Users>();
       // MagnaDBEntities db = new MagnaDBEntities();
        /// <summary>
        /// 服务器IP地址
        /// </summary>;
        private IPAddress ServerIP;
        
        /// <summary>
        /// 监听端口
        /// </summary>
        private int port;
        private TcpListener myListener;
        /// <summary>
        /// 是否正常退出所有接收线程
        /// </summary>
        bool isNormalExit = false;

        public TcpStart()
        {

        }
        public void StartListen()
        {
            try
            {
            if(myListener==null)
            {
                Random rand = new Random();
                 int port = rand.Next(1000, 9999);
                 ServerIP = IPAddress.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "ServerIPAndPort.txt", Encoding.Default));
                    //string name = Dns.GetHostName();
                    //IPAddress[] ips = Dns.GetHostAddresses(name);
                  
                    //foreach (var item in ips)
                    //{
                    //    if (item.AddressFamily==AddressFamily.InterNetwork)
                    //    {
                    //        ServerIP = item;
                    //    }
                    //}
                myListener = new TcpListener(ServerIP, port);
                List<px_IPAddress_Port> list = px_MaterialsortprintingDAL.Getpx_IPAddress_Port();
                px_IPAddress_Port model = new px_IPAddress_Port();
                int id = list[0].ID;
                model.IP = ServerIP.ToString();
                model.Port = port;
              int rows=px_MaterialsortprintingDAL.Updatepx_IPAddress_Port(model);
                
                myListener.Start();
            }
                //创建一个线程监客户端连接请求
            myThread = new Thread(ListenClientConnect);
            myThread.Name = "tcplistener";
            myThread.Start();
        }
            catch (Exception a)
            {
                HttpResponse hh = new HttpResponse(TextWriter.Null);
        hh.Write("<script>alert('" + a.Message + "');</script>");
                //HttpResponse.Write("<script>alert('"+a.Message+"');</script>");
            }

}
        public void autoSendPrinted_targer()
        {
            bool flag = true;
            while (true)
            {
                Thread.Sleep(300000);
                if (flag)
                {
                    flag = false;
                    mp.AutoPrintSent_autoSendPrinted();
                    flag = true;
                }

            }
        }
        public void targerThread()
        {
            t = new Thread(new ThreadStart(targer), 1024);
            t.Start();
        }
        public void targer()
        {
            
            DateTime lastDateTime = DateTime.Now;
            DateTime nowDateTime = DateTime.Now;

            bool flag = true;
            bool flag_History = true;
            while (true)
            {
                if (flag)
                {
                    flag = false;
                    mp.AutoPrintSent();
                    Thread.Sleep(300);
                    nowDateTime = DateTime.Now;
                    TimeSpan ts = nowDateTime - lastDateTime;//日期差计算(C#)
                    if (ts.TotalMinutes > 5)
                    {
                        mp.AutoPrintSent_autoSendPrinted();
                        lastDateTime = DateTime.Now;
                    }
                    flag = true;
                }
                if (nowDateTime.Hour == 1 && flag_History)
                {
                    flag_History = false;
                    mp.AutoDo_HistoryData();
                }
                if (nowDateTime.Hour == 3)
                    flag_History = true;

                if (DateTime.Now.ToLongTimeString() == "00:00:00")
                {
                    foreach (var item in GetBtnClass.olst)
                    {
                        item.wlprintindex = 0;
                    }
                }
                Thread.Sleep(100);
            }
        }
        private void ListenClientConnect()
        {
            TcpClient newClient = null;
            string ipport="";
            while (true)
            {
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    
                    //当单击‘停止监听’或者退出此窗体时 AcceptTcpClient() 会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；
                Users user = new Users(newClient);
                threadReceive = new Thread(ReceiveData);
                threadReceive.Name = "Receive";
                threadReceive.Start(user);
                String[] Ip = user.client.Client.RemoteEndPoint.ToString().Split(':');
                ipport += Ip[0] + ":" + Ip[1] + "\n";
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "curip.txt", ipport);
                userList.Add(user);
            }

        }
        private void ReceiveData(object userState)
        {
            Users user = (Users)userState;
            TcpClient client = user.client;
            while (isNormalExit == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串，此方法会自动判断字符串长度前缀
                    receiveString = user.br.ReadString();
                }
                catch (Exception)
                {
                    if (isNormalExit == false)
                    {
                        RemoveUser(user);
                    }
                    break;
                }
                Thread.Sleep(100);//haole
            }
        }
        /// <summary>
        /// 发送消息给所有客户
        /// </summary>
        /// <param name="user">指定发给哪个用户</param>
        /// <param name="message">信息内容</param>
        public static String SendToClient(Users user, string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                user.bw.Write(message);
                user.bw.Flush();
                return (string.Format("向[{0}]发送信息成功", user.userName));
            }
            catch
            {
                return (string.Format("向[{0}]发送信息失败", user.userName));
            }
        }
        private void RemoveUser(Users user)
        {
            userList.Remove(user);
            user.Close();
        }
        private void EndTcpLinsten()
        {
            isNormalExit = true;
            for (int i = userList.Count - 1; i >= 0; i--)
            {
                RemoveUser(userList[i]);
            }
            myListener.Stop();
        }

    }
}