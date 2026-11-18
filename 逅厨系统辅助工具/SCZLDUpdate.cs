using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class SCZLDUpdate : Form
    {



        /// <summary>
        /// app程序路径和指令单路径，
        /// </summary>
        private static string varFilePath = null;
        private static string appPath = null;

        private static Process p;

        /// <summary>
        /// 本地版本号 例： 0
        /// </summary>
        private static string varno = null;
        /// <summary>
        /// FTP上的版本号
        /// </summary>
        private static string ftpvar = null;
        /// <summary>
        /// FTP地址 例：ftp://172.17.70.252/PCUpdateTest/
        /// </summary>
        private static string ftpurl = null;    ///主目录文件
        private static string ftpurlZLD = null; //报表目录路径
        private static string zldDirName = null; //指令单所在文件夹名称

        /// <summary>
        /// FTP登录用户名 例：root
        /// </summary>
        private static string ftpusername = null;
        /// <summary>
        /// FTP登录密码 例：root
        /// </summary>
        private static string ftppassword = null;
        /// <summary>
        /// FTP下载缓存大小，默认2048，可在FTP端的配置文件调整
        /// </summary>
        private static int ftpBufferSize = 2048;
        /// <summary>
        /// 配置文件名 
        /// </summary>
        private static string varfilename = "zldUpdate.ini";
        /// <summary>
        /// 用于临时存放FTP的文件信息
        /// </summary>
        private string[] fileInfo;
        /// <summary>
        /// 存放全部的FTP文件详细信息
        /// </summary>
        private List<string> updateFileInfo = new List<string>();
        /// <summary>
        /// 存放根据配置文件得到的FTP地址，用于检查网络是否连通用
        /// </summary>
        private string ftpip = null;

        public SCZLDUpdate()
        {
            InitializeComponent();
            //应用程序所在的目录
            appPath = AppDomain.CurrentDomain.BaseDirectory;
            //DefaultConnectionLimit是默认的2，而当前的FTP的connection用完了，导致后续的GetResponse或GetRequestStream超时死掉
            //因为该程序有递归算法遍历FTP目录，所以每进入一个目录，会占用一个connection
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
        }


        /// <summary>
        /// 获取本地配置文件信息
        /// </summary>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        private void GetLocalVar(string filename)
        {


            if (System.IO.File.Exists(filename))
            {
                varno = Tool.GetIniInfo("AutoUpdate", "version", filename);
                ftpurlZLD = @"ftp://" + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename)) + @"/"
                            + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "zldDir", filename));   //报表目录
                ftpusername = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpusername", filename));
                ftppassword = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftppassword", filename));
                ftpip = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename));
            }
            else
            {
                //没有配置文件自动生成
                string path = varFilePath + varfilename;

                if (varFilePath.Contains("佳之家"))
                {
                    zldDirName = @"佳之家生产指令单/";
                }
                else if (varFilePath.Contains("谷之田"))
                {
                    zldDirName = @"谷之田生产指令单/";
                }
                else if (varFilePath.Contains("好利森"))
                {
                    zldDirName = @"好利森生产指令单/";
                }

                Tool.SetIniFile("AutoUpdate", "version", "0", path);
                Tool.SetIniFile("FTP", "ftpip", @"ssus*sfs,tspu^t0tvtsVansamstsuuar", path);
                Tool.SetIniFile("FTP", "zldDir", @"逅厨系统辅助工具/SCZLD/" + zldDirName, path);
                Tool.SetIniFile("FTP", "ftpusername", @"orqoNoOp)ppv2:9?pqroj`rjAqbpqruul", path);
                Tool.SetIniFile("FTP", "ftppassword", @"opqo_o?pAppv_lfblqro<`r(^qMporuul", path);
                varno = "0";
                ftpurlZLD = @"ftp://192.168.10.12/逅厨系统辅助工具/SCZLD/" + zldDirName;
                ftpusername = "AutoUpdate";
                ftppassword = "autoupdate";
                ftpip = "192.168.10.12";
            }
        }

        /// <summary>
        /// 获取FTP服务器上指定文件的大小
        /// </summary>
        /// <param name="ftpurl">FTP路径</param>
        /// <param name="fileName">FTP服务器上的文件名</param>
        /// <returns></returns>
        private int GetFtpFileSize(string ftpurl, string fileName)
        {
            FtpWebRequest reqFTP;
            int fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpurl + fileName));
                reqFTP.Credentials = new NetworkCredential(ftpusername, ftppassword);
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = (int)response.ContentLength;
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fileSize;
        }


        /// <summary>
        /// 实现ftp下载操作
        /// </summary>
        /// <param name="filePath">保存到本地的文件夹</param>
        /// <param name="fileName">FTP目标文件名</param>
        /// <param name="uri">FTP的目标目录</param>
        public void Download(string filePath, string fileName, string uri)
        {
            System.Net.FtpWebRequest reqFTP;
            try
            {
                //不存在就创建目录 
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }

                //filePath = <<The full path where the file is to be created.>>,
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                //打开一个文件流 (System.IO.FileStream) 去读要下载的文件
                FileStream outputStream = new FileStream(filePath + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri + fileName));
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                // 指定数据传输类型
                reqFTP.UseBinary = true;
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(ftpusername, ftppassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                //long cl = response.ContentLength;
                int bufferSize = ftpBufferSize;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                //FTP上文件的大小
                int allbyte = this.GetFtpFileSize(uri, fileName);
                int startbyte = 0;
                this.progressBar1.Maximum = allbyte;
                this.progressBar1.Minimum = startbyte;
                // 流内容没有结束
                while (readCount > 0)
                {
                    // 把内容从file stream 写入 DownLoadStream
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    //控制进度条
                    startbyte += readCount;
                    label1.Text = "下载进度：" + (int)(startbyte / 1024) + "KB/" + "总长度：" + (int)(allbyte / 1024) + "KB";
                    this.progressBar1.Value = startbyte;
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
                //保证进度条走完。暂时不明为什么startbyte不等于allbyte
                this.progressBar1.Value = allbyte;
                label1.Text = "下载进度：" + (int)(allbyte / 1024) + "KB/" + "总长度：" + (int)(allbyte / 1024) + "KB";

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取FTP端的配置文件信息，决定是否更新，更新哪些内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void GetFtpVar(string filePath, string fileName)
        {
            try
            {
                string ftpvarfilepath = filePath + @"temp\";
                Download(ftpvarfilepath, fileName, ftpurlZLD);
                ftpvar = Tool.GetIniInfo("AutoUpdate", "version", ftpvarfilepath + fileName);
                ftpBufferSize = Convert.ToInt32(Tool.GetIniInfo("FTP", "bufferSize", ftpvarfilepath + fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /*
        *  -rwxr-xr-x 1 ftp ftp           6656 Dec 14 16:47 HLSystem.exe
        *  drwxr-xr-x 1 ftp ftp              0 Dec 16 09:19 xxx

        *  第一位表示文件类型。d是目录文件，l是链接文件，-是普通文件，p是管道
        *  第2-4位表示这个文件的属主拥有的权限，r是读，w是写，x是执行。
        *  第5-7位表示和这个文件属主所在同一个组的用户所具有的权限。
        *  第8-10位表示其他用户所具有的权限。
        */
        /// <summary>
        /// 递归遍历FTP文件信息（文件名及FTP对应目录）,使用之前先清空updateFileInfo中的元素 
        /// </summary>
        /// <param name="ftpurl"></param>
        /// <returns></returns>
        public void GetFilesDetailList(string url)
        {

            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                //垃圾回收
                System.GC.Collect();
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                //MessageBox.Show(line);
                //FTP工作目录（主目录）没文件或者文件夹则跳出方法
                if (line == null || line.Length <= 0)
                {
                    return;
                }
                

                while (line != null)
                {
                    //如果是目录，递归
                    if (line.Substring(0, 1) == "d")
                    {
                        string directoryName = line.Substring(line.LastIndexOf(" ") + 1);
                        GetFilesDetailList(url + directoryName + @"/");
                    }
                    else
                    {
                        //是文件，则记录信息（文件名+路径）
                        string fileName = line.Substring(line.LastIndexOf(" ") + 1);
                        //如果是配置文件，则不纳入文件列表
                        if (fileName != varfilename)
                        {
                            result.Append(url.Replace(ftpurlZLD, "") + "," + fileName);
                            result.Append("\n");
                        }
                    }
                    line = reader.ReadLine();
                }
                if (result.Length<=0)
                {
                    MessageBox.Show("服务端没有可更新的报表，请联系管理员！");
                    return;
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                //将获取到的文件信息存放到List中
                fileInfo = result.ToString().Split('\n');
                foreach (string fileinfo in fileInfo)
                {
                    updateFileInfo.Add(fileinfo.Replace(@"/", @"\"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("文件信息获取异常！" + ex.Message);
            }
        }

        /// <summary>
        /// 自动更新动作
        /// </summary>
        public void zldUpdate()
        {
            string sczldFilePath = "";
            varFilePath = appPath; //程序主目录路径

            if (SCZLDMAIN.nodeName == "JZJ")
            {
                sczldFilePath = @"佳之家生产指令单\";
            }
            else if (SCZLDMAIN.nodeName == "GZT")
            {
                sczldFilePath = @"谷之田生产指令单\";
            }
            else if (SCZLDMAIN.nodeName == "HLS")
            {
                sczldFilePath = @"好利森生产指令单\";
            }
            //指令单报表配置文件所在文件夹目录
            varFilePath = varFilePath + @"SCZLD\" + sczldFilePath;
            try
            {
                GetLocalVar(varFilePath + varfilename);
                if (!InternetCheck.PingIpOrDomainName(ftpip))
                {
                    MessageBox.Show("更新服务器连接失败！");
                    return;
                    throw new Exception("无法建立与服务器的连接！\r\n\r\n请检查网络环境！");
                }
                //获取FTP端的配置文件信息
                GetFtpVar(varFilePath, varfilename);
               //不管本地版本和服务器版本是否一致，都下载
                updateFileInfo.Clear();
                //获取需要更新的所有文件的名称和对应目录
                GetFilesDetailList(ftpurlZLD);
                //当前已下载数量
                int count = 0;
                //获取下载总量
                int sumCount = 0;
                sumCount = updateFileInfo.Count;
                if (sumCount == 0)
                {
                    return;
                }
                label1.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                //按个下载更新
                foreach (string file in updateFileInfo.ToArray())
                {
                    //解析文件信息
                    string locolSubDirPath = file.Substring(0, file.IndexOf(","));
                    string ftpSubDirPath = locolSubDirPath.Replace(@"\", @"/");
                    string upFileName = file.Substring(file.IndexOf(",") + 1);
                    label2.Text = "正在下载：" + upFileName;
                    Download(varFilePath + locolSubDirPath, upFileName, ftpurlZLD + ftpSubDirPath);
                    count++;
                    label1.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                }
                label2.Text = "更新完成！";
                MessageBox.Show("更新成功！");
                //更新成功，更新本地配置文件版本号
                Tool.SetIniFile("AutoUpdate", "version", ftpvar, varFilePath + varfilename);
                Thread.Sleep(10);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新异常！" + ex.Message);
                FileInfo fi = new FileInfo(varFilePath + @"\" + varfilename);
                fi.Delete();
                System.Environment.Exit(0);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            zldUpdate();
        }

        private void SCZLDUpdate_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void SCZLDUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
