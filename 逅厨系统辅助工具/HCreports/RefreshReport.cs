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
    public partial class RefreshReport : Form
    {
        private static string reportPath = null;
        private static string updateFilePath = null;

        public RefreshReport()
        {
            InitializeComponent();
            updateFilePath= AppDomain.CurrentDomain.BaseDirectory;
            reportPath = AppDomain.CurrentDomain.BaseDirectory+"report/";
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            //MessageBox.Show(reportPath);
        }

        private static Process p;
        // <summary>
        /// FTP地址 例：ftp://172.17.70.252/PCUpdateTest/
        /// </summary>
        private static string ftpurl = null;   
        ///主目录文件
        /// <summary>
        /// 配置文件名 
        /// </summary>
        private static string varfilename = "Update.ini";
        /// <summary>
        /// 用于临时存放FTP的文件信息
        /// </summary>
        private string[] fileInfo;
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
        /// 存放全部的FTP文件详细信息
        /// </summary>
        private List<string> updateReportInfo = new List<string>();
        /// <summary>
        /// 存放根据配置文件得到的FTP地址，用于检查网络是否连通用
        /// </summary>
        private string ftpip = null;

        private void RefreshReport_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2 ;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2 ;
            btn_close.Enabled = false;
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


        private void GetLocalVar(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
               
                ftpurl = @"ftp://" + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename)) + @"/"
                            + Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ReportDir", filename));   //主目录
                ftpusername = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpusername", filename));
                ftppassword = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftppassword", filename));
                ftpip = Tool.PasswordDeEncrypt(Tool.GetIniInfo("FTP", "ftpip", filename));
            }
            else
            {
                
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
                //FTP工作目录（主目录）没文件或者文件夹则跳出方法
                if (line == null || line.Length <= 0)
                {
                    return;
                }

                while (line != null)
                {
                    //如果是目录，递归
                    if (line.Contains("DIR"))
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
                    updateReportInfo.Add(fileinfo.Replace(@"/", @"\"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("文件信息获取异常！" + ex.Message);
            }
        }


        /// <summary>
        /// 自动下载
        /// </summary>
        public void autoDownload()
        {
            try
            {
                //获取本地配置文件信息
                GetLocalVar(updateFilePath + varfilename);
                if (!InternetCheck.PingIpOrDomainName(ftpip))
                {
                    label1.Text = "更新服务器连接失败！";
                    label1.ForeColor = Color.Red;
                    return;
                    throw new Exception("无法建立与服务器的连接！\r\n\r\n请检查网络环境！");

                }

                //释放list元素，加载新的集合
                updateReportInfo.Clear();
                //获取需要更新的所有文件的名称和对应目录
                GetFilesDetailList(ftpurl);
                //当前已下载数量
                int count = 0;
                //获取下载总量
                int sumCount = 0;
                sumCount = updateReportInfo.Count;
                label2.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                //按个下载更新
                foreach (string file in updateReportInfo.ToArray())
                {
                    //解析文件信息
                    string locolSubDirPath = file.Substring(0, file.IndexOf(","));
                    string ftpSubDirPath = locolSubDirPath.Replace(@"\", @"/");
                    string upFileName = file.Substring(file.IndexOf(",") + 1);


                    label3.Text = "正在下载：" + upFileName;
                    Download(reportPath + locolSubDirPath, upFileName, ftpurl + ftpSubDirPath);
                    count++;

                    label2.Text = "已下载：" + count.ToString() + " / " + sumCount.ToString();
                }
                Thread.Sleep(10);
                label3.Text = "更新完毕！";
                btn_close.Enabled = true;
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新异常！" + ex.Message);
                
            }
        }

        private void RefreshReport_Shown(object sender, EventArgs e)
        {
            autoDownload();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
