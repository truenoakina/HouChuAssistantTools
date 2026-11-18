using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace HCToolAutoUpdate
{
    public partial class AutoUpdate : Form
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string varFilePath = null;

        public AutoUpdate()
        {
            InitializeComponent();
            //D:\AutoUpdate\AutoUpdate\bin\Debug\
            varFilePath = AppDomain.CurrentDomain.BaseDirectory;
            //DefaultConnectionLimit是默认的2，而当前的FTP的connection用完了，导致后续的GetResponse或GetRequestStream超时死掉
            //因为该程序有递归算法遍历FTP目录，所以每进入一个目录，会占用一个connection
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            //MessageBox.Show(varFilePath);
        }

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
        private static string ftpurlJZJZLD = null; //佳之家报表目录
        /// <summary>
        /// FTP登录用户名 例：root
        /// </summary>
        private static string ftpusername = null;
        /// <summary>
        /// FTP登录密码 例：root
        /// </summary>
        private static string ftppassword = null;
        /// <summary>
        /// 需要执行的主程序名 例：CCMSmartDevice
        /// </summary>
        private static string exename = null;
        /// <summary>
        /// 更新方式  更新目录所有文件更新至本地（all） 更新目录指定部分文件更新至本地 （part）
        /// </summary>
        private static string upDateType = null;
        /// <summary>
        /// part模式下，存放需要更新的文件信息
        /// </summary>
        private static string[] upDateFiles;
        /// <summary>
        /// FTP下载缓存大小，默认2048，可在FTP端的配置文件调整
        /// </summary>
        private static int ftpBufferSize = 2048;
        /// <summary>
        /// 配置文件名 
        /// </summary>
        private static string varfilename = "Update.ini";
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

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="filename">配置文件路径</param>
        /// <returns></returns>
        private void GetLocalVar(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                varno = Tool.GetIniInfo("AutoUpdate", "version", filename);
                ftpurl = @"ftp://" + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename)) + @"/"
                            + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "MainDir", filename));   //主目录
                //ftpurlJZJZLD = @"ftp://" + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename)) + @"/"
                //            + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "JzjzldDir", filename));  //佳之家指令单
                ftpusername = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpusername", filename));
                ftppassword = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftppassword", filename));
                exename = Tool.GetIniInfo("AutoUpdate", "exename", filename);
                ftpip = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename));
            }
            else
            {
                //没有配置文件自动生成
                string path = varFilePath + varfilename;
                Tool.SetIniFile("AutoUpdate", "version", "0", path);
                Tool.SetIniFile("FTP", "ftpip", @"ssusesssstsptYtnsvts7ansamstsuuat", path);
                Tool.SetIniFile("FTP", "MainDir", @"逅厨系统辅助工具/", path);
                //Tool.SetIniFile("FTP", "JzjzldDir", @"逅厨系统辅助工具/", path);
                Tool.SetIniFile("FTP", "ftpusername", @"orqodoPplppvhYtofqroY`r[Aq;pqruul", path);
                Tool.SetIniFile("FTP", "ftppassword", @"ornpis:rlrsqNN<cusqomsvay`=sptnul", path);
                Tool.SetIniFile("AutoUpdate", "exename", "逅厨系统辅助工具", path);
                varno = "0";
                ftpurl = @"ftp://192.168.10.238/逅厨系统辅助工具";
                //ftpurlJZJZLD= @"ftp://192.168.10.12/逅厨系统辅助工具/佳之家生产指令单";
                ftpusername = "AutoUpdate";
                ftppassword = "HouChu@123";
                exename = "逅厨系统辅助工具";
                ftpip = "192.168.10.238";
            }
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
        /// 自动更新动作
        /// </summary>
        public void autoUpdate()
        {
            try
            {
                //获取本地配置文件信息
                GetLocalVar(varFilePath + varfilename);
                if (!InternetCheck.PingIpOrDomainName(ftpip))
                {
                    label1.Text = "更新服务器连接失败！";
                    label1.ForeColor = Color.Red;
                    return;
                    throw new Exception("无法建立与服务器的连接！\r\n\r\n请检查网络环境！");
                    
                }
                //获取FTP端的配置文件信息
                GetFtpVar(varFilePath, varfilename);

                if (varno != ftpvar)
                //{
                //    MessageBox.Show("版本号一致，不需要更新！");
                //}
                //else
                {
                    Process[] proc = System.Diagnostics.Process.GetProcessesByName(exename);
                    foreach (Process pro in proc)
                    {
                        pro.Kill();
                    }
                    //释放list元素，加载新的集合
                    updateFileInfo.Clear();
                    //获取需要更新的所有文件的名称和对应目录
                    GetFilesDetailList(ftpurl);
                    //当前已下载数量
                    int count = 0;
                    //获取下载总量
                    int sumCount = 0;
                    if (upDateType == "part" && varno != "0")
                    {
                        foreach (string file in updateFileInfo.ToArray())
                        {
                            string upFileName = file.Substring(file.IndexOf(",") + 1);
                            foreach (string upDateFile in upDateFiles)
                            {
                                if (upFileName == upDateFile)
                                {
                                    sumCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        sumCount = updateFileInfo.Count;
                    }
                    label2.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                    //按个下载更新
                    foreach (string file in updateFileInfo.ToArray())
                    {
                        //解析文件信息
                        string locolSubDirPath = file.Substring(0, file.IndexOf(","));
                        string ftpSubDirPath = locolSubDirPath.Replace(@"\", @"/");
                        string upFileName = file.Substring(file.IndexOf(",") + 1);

                        //FTP上的配置文件不更新到本地目录
                        //if (upFileName == varfilename)
                        //{
                        //    continue;
                        //}
                        //判断更新方式
                        if (upDateType == "part" && varno != "0")
                        {
                            foreach (string upDateFile in upDateFiles)
                            {
                                if (upFileName != upDateFile)
                                {
                                    continue;
                                }
                                else
                                {
                                    label3.Text = "正在下载：" + upFileName;
                                    Download(varFilePath + locolSubDirPath, upFileName, ftpurl + ftpSubDirPath);
                                    count++;
                                }
                            }
                        }
                        else if (upDateType == "all" || varno == "0")
                        {
                            label3.Text = "正在下载：" + upFileName;
                            Download(varFilePath + locolSubDirPath, upFileName, ftpurl + ftpSubDirPath);
                            count++;
                        }
                        else
                        {
                            throw new Exception("更新模式配置错误！");
                        }
                        label2.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                    }
                    //MessageBox.Show("更新成功！");
                    //更新成功，更新本地配置文件版本号
                    Tool.SetIniFile("AutoUpdate", "version", ftpvar, varFilePath + varfilename);
                    Thread.Sleep(10);
                }
                if (p == null)
                {
                    p = new Process();
                    p.StartInfo.FileName = exename;
                    //启动参数
                    p.StartInfo.Arguments = "AutoUpdateStart";
                    p.Start();
                }
                else
                {
                    if (p.HasExited)
                    {
                        p.StartInfo.Arguments = "AutoUpdateStart";
                        p.Start();
                    }
                }
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
                Download(ftpvarfilepath, fileName, ftpurl);
                ftpvar = Tool.GetIniInfo("AutoUpdate", "version", ftpvarfilepath + fileName);
                upDateType = Tool.GetIniInfo("AutoUpdate", "upDateType", ftpvarfilepath + fileName);
                upDateFiles = Tool.GetIniInfo("AutoUpdate", "upDateFiles", ftpvarfilepath + fileName).Split(',');
                ftpBufferSize = Convert.ToInt32(Tool.GetIniInfo("FTP", "bufferSize", ftpvarfilepath + fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            autoUpdate();
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
                            result.Append(url.Replace(ftpurl, "") + "," + fileName);
                            result.Append("\n");
                        }
                    }
                    line = reader.ReadLine();
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


        private void AutoUpdate_Shown(object sender, EventArgs e)
        {
            //button1_Click(sender, e);
            timer1.Enabled = true;
            
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}

