using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class ChangePassWord : Form
    {
        public ChangePassWord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_OldPassWord.Text))
            {
                MsgBox.TipMsgShow(txt_OldPassWord.Location.X + txt_OldPassWord.Width + 20, txt_OldPassWord.Location.Y + txt_OldPassWord.Height + 20, this.Text, "请填写旧密码！", this);
                txt_OldPassWord.Focus();
                txt_OldPassWord.SelectAll();
                return;
            }
            if (string.IsNullOrEmpty(txt_NewPassWord.Text))
            {
                MsgBox.TipMsgShow(txt_NewPassWord.Location.X + txt_NewPassWord.Width + 20, txt_NewPassWord.Location.Y + txt_NewPassWord.Height + 20, this.Text, "请填写新密码！", this);
                txt_NewPassWord.Focus();
                txt_NewPassWord.SelectAll();
                return;
            }
            if (string.IsNullOrEmpty(txt_NewPassWord1.Text))
            {
                MsgBox.TipMsgShow(txt_NewPassWord1.Location.X + txt_NewPassWord1.Width + 20, txt_NewPassWord1.Location.Y + txt_NewPassWord1.Height + 20, this.Text, "请填写确认新密码，不能为空！", this);
                txt_NewPassWord1.Focus();
                txt_NewPassWord1.SelectAll();
                return;
            }
            if (txt_OldPassWord.Text != LoginInfo.PassWord)
            {
                MsgBox.TipMsgShow(txt_OldPassWord.Location.X + txt_OldPassWord.Width + 20, txt_OldPassWord.Location.Y + txt_OldPassWord.Height + 20, this.Text, "旧密码错误，请检查！", this);
                txt_OldPassWord.Focus();
                txt_OldPassWord.SelectAll();
                return;
            }
            if (txt_NewPassWord.Text != txt_NewPassWord1.Text)
            {
                MsgBox.TipMsgShow(txt_NewPassWord1.Location.X + txt_NewPassWord1.Width + 20, txt_NewPassWord1.Location.Y + txt_NewPassWord1.Height + 20, this.Text, "两次新密码不一致，请确认！", this);
                txt_NewPassWord1.Focus();
                txt_NewPassWord1.SelectAll();
                return;
            }
            string sql = "update HC_User set PassWord=@PassWord where UserName=@UserName";
            try
            {
                if (SQLHelper.ExecSql(sql, SQLHelper.MakeParam("PassWord", StrHelper.PasswordEncrypt(txt_NewPassWord.Text)), SQLHelper.MakeParam("UserName", LoginInfo.UserName)))
                {
                    LoginInfo.PassWord = txt_NewPassWord.Text;
                    MessageBox.Show("密码修改成功, 请牢记！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_OldPassWord.Text = "";
                    txt_NewPassWord.Text = "";
                    txt_NewPassWord1.Text = "";
                    return;
                }
                else
                {
                    MessageBox.Show("密码修改失败！请再次点击确认按钮尝试修改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void txt_OldPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_NewPassWord.Focus();
            }
        }

        private void txt_NewPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_NewPassWord1.Focus();
            }
        }

        private void txt_NewPassWord1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
