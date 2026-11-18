using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class Login : Form
    {
        public Login(string[] args)
        {
            //if (args.Length < 1)
            //{
            //    MessageBox.Show("请从自动更新程序[HCToolAutoUpdate.exe]启动！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    System.Environment.Exit(0);
            //}
            //else
            //{
            //    if (args[0] != "AutoUpdateStart")
            //    {
            //        MessageBox.Show("请从自动更新程序[HCToolAutoUpdate.exe]启动！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        System.Environment.Exit(0);
            //    }
            //}

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                GetComputerName();
                GetIP();
            }
            catch (Exception)
            {
            }
        }

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
        /// 计算机名
        /// </summary>
        private string computer = "";
        /// <summary>
        /// IP地址
        /// </summary>
        private string ip = "";

        /// <summary>
        /// 返回计算机名
        /// </summary>
        public void GetComputerName()
        {
            //获得主机名
            computer = Dns.GetHostName();
            MyVar.LoginComputer = computer;
        }

        /// <summary>
        /// 返回IP地址
        /// </summary>
        public void GetIP()
        {
            ip = Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
        }

        private void bt_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_UserName.Text))
            {
                MsgBox.TipMsgShow(groupBox1.Location.X + txt_UserName.Location.X + txt_UserName.Width, groupBox1.Location.Y + txt_UserName.Location.Y + txt_UserName.Height, this.Text, "用户名不能为空！", this);
                txt_UserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txt_PassWord.Text))
            {
                MsgBox.TipMsgShow(groupBox1.Location.X + txt_PassWord.Location.X + txt_PassWord.Width, groupBox1.Location.Y + txt_PassWord.Location.Y + txt_PassWord.Height, this.Text, "密码不能为空！", this);
                txt_PassWord.Focus();
                return;
            }
            //是否外网登录
            if (cb_isOutNet.Checked)
            {
                Properties.Settings.Default.isOutNet = true;
                MyVar.IsOutNet = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.isOutNet = false;
                MyVar.IsOutNet = false;
                Properties.Settings.Default.Save();
            }
            try
            {
                string sql = "select *,getdate() NowDate from HC_User(nolock) where UserName=@username";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("username", txt_UserName.Text));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["UserStatus"].ToString() != "正常")
                    {
                        MsgBox.TipMsgShow(groupBox1.Location.X + txt_UserName.Location.X + txt_UserName.Width, groupBox1.Location.Y + txt_UserName.Location.Y + txt_UserName.Height, this.Text, "当前用户已被禁止登录！", this);
                        txt_UserName.Focus();
                        txt_UserName.SelectAll();
                        return;
                    }
                    string _passWord = StrHelper.PasswordDeEncrypt(ds.Tables[0].Rows[0]["PassWord"].ToString());
                    if (txt_PassWord.Text == _passWord)
                    {
                        LoginInfo.UserName = txt_UserName.Text;
                        LoginInfo.PassWord = _passWord;
                        LoginInfo.LoginComputer = computer;
                        LoginInfo.LoginIP = ip;
                        string sql1 = "update HC_User set LastLoginComputer=@LoginComputer,LastLoginIP=@LoginIP,LastLoginTime=getdate() where UserName=@username";
                        if (!SQLHelper.ExecSql(sql1, SQLHelper.MakeParam("LoginComputer", LoginInfo.LoginComputer), SQLHelper.MakeParam("LoginIP", LoginInfo.LoginIP), SQLHelper.MakeParam("username", LoginInfo.UserName)))
                        {
                            MessageBox.Show("更新登录信息失败！请重新点击登录！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //确认是否记住用户名
                        if (cb_savename.Checked == true)
                        {
                            Properties.Settings.Default.isSaveUserName = true;
                            Properties.Settings.Default.username = txt_UserName.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.isSaveUserName = false;
                            Properties.Settings.Default.username ="";
                            Properties.Settings.Default.Save();
                        }
                        MyVar.LoginName = txt_UserName.Text;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        MsgBox.TipMsgShow(groupBox1.Location.X + txt_PassWord.Location.X + txt_PassWord.Width, groupBox1.Location.Y + txt_PassWord.Location.Y + txt_PassWord.Height, this.Text, "密码不正确！", this);
                        txt_PassWord.Text = "";
                        txt_PassWord.Focus();
                        return;
                    }
                }
                else
                {
                    MsgBox.TipMsgShow(groupBox1.Location.X + txt_UserName.Location.X + txt_UserName.Width, groupBox1.Location.Y + txt_UserName.Location.Y + txt_UserName.Height, this.Text, "用户名不存在！", this);
                    txt_UserName.Text = "";
                    txt_UserName.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PassWord.Focus();
            }
        }

        private void txt_PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt_Login_Click(sender, e);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lbl_progress.BackColor = Color.Transparent;

            if (Properties.Settings.Default.isSaveUserName == true)
            {
                this.txt_UserName.Text = Properties.Settings.Default.username;
                this.cb_savename.Checked = true;
            }
            if (Properties.Settings.Default.isOutNet == true)
            {
                this.cb_isOutNet.Checked = true;
            }
            else
            {
                this.cb_isOutNet.Checked = false;
            }
            //程序版本，需要与数据库中版本一致
            string ver = "5.6";
            lbl_ver.Text = ver;

            string sql = @"select version from Version where 1=@data";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("data", 1));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["version"].ToString() == ver)
                {

                }
                else if (ds.Tables[0].Rows[0]["version"].ToString() != ver)
                {
                    MessageBox.Show("程序存在新版本，请通过自动更新程序[HCToolAutoUpdate.exe]更新！");
                    //this.Close();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Contract contract = new Contract();
            contract.StartPosition = FormStartPosition.CenterParent;
            contract.ShowDialog();
        }

        private void downloadAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ftpusername = "AutoUpdate";
            ftppassword = "HouChu@123";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                Download(fbd.DirectoryPath+"/", "逅厨系统辅助工具.rar", "ftp://192.168.10.238/完整版下载/");
            }
            else if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                
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
                    lbl_progress.Text = "下载进度：" + (int)(startbyte / 1024) + "KB/" + "总大小：" + (int)(allbyte / 1024) + "KB";
                    this.progressBar1.Value = startbyte;
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
                //保证进度条走完。暂时不明为什么startbyte不等于allbyte
                this.progressBar1.Value = allbyte;
                lbl_progress.Text = "下载完成 "+"总大小：" + (int)(allbyte / 1024) + "KB";

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
    }
}
