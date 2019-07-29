using System;
using System.IO;
using System.Net;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Blog.App_Start
{
    public class GetIPAndMac
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        //获取本机的IP
        public string getLocalIP()
        {
            string strHostName = Dns.GetHostName(); //得到本机的主机名

            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP

            string strAddr = ipEntry.AddressList[0].ToString();
            return (strAddr);
        }

        /**获取ip地址*/
        public static string ipTrue()
        {
            string ip = "0.0.0.0";
            System.Net.IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;//IP获取一个LIST里面有一个是IP
            for (int i = 0; i < addressList.Length; i++)
            {
                //判断是否为IP的格式
                if (System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(addressList[i]), @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)") == true)
                {
                    ip = addressList[i].ToString();

                }
            }
            return ip;

        }

        //获取MAC地址
        public static string GetMacAddressByDos()
        {
            string macAddress = "";
            Process p = null;
            StreamReader reader = null;
            try
            {
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                start.FileName = "ipconfig";
                start.Arguments = "/all";

                start.CreateNoWindow = true;

                start.RedirectStandardOutput = true;

                start.RedirectStandardInput = true;

                start.UseShellExecute = false;

                p = Process.Start(start);

                reader = p.StandardOutput;
                //读取当前行
                string line = reader.ReadLine();
                //循环到出现物理地址为止
                while (!reader.EndOfStream)
                {
                    if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                    {
                        int index = line.IndexOf(":");
                        index += 2;
                        macAddress = line.Substring(index);
                        macAddress = macAddress.Replace('-', ':');
                        break;
                    }
                    //不断一个个读取
                    line = reader.ReadLine();
                }
            }
            catch
            {
                //写到错误日志里面去，具体自己写

            }
            finally
            {
                if (p != null)
                {

                    p.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return macAddress;
        }

        //获取远程主机IP
        public string[] getRemoteIP(string RemoteHostName)
        {
            IPHostEntry ipEntry = Dns.GetHostByName(RemoteHostName);
            IPAddress[] IpAddr = ipEntry.AddressList;
            string[] strAddr = new string[IpAddr.Length];
            for (int i = 0; i < IpAddr.Length; i++)
            {
                strAddr[i] = IpAddr[i].ToString();
            }
            return (strAddr);
        }

        //获取远程主机MAC
        public string getRemoteMac(string localIP, string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的ip 

            Int32 lhost = inet_addr(localIP); //本地ip


            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                return Convert.ToString(macinfo, 16);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return 0.ToString();
        }

        //oo-xxand1-9随机字符串
        public string getRandomStr(string ranStr, int ranCount)
        {
            string RandomStr = "";
            int ranStrCount = Encoding.Default.GetByteCount(ranStr);            
            Random ran = new Random();
            for (int i = 0; i < ranCount; i++) {
                int number = ran.Next(ranStrCount);
                string nowranStr = ranStr;
                nowranStr = nowranStr.Remove(0, number);
                if (Encoding.Default.GetByteCount(nowranStr) > 1) {
                    RandomStr += nowranStr.Remove(1);
                }                                
            }
            return RandomStr;
        }
    }
}