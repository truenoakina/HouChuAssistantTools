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
    public partial class EnergyWrite : Form
    {
        
        private string id;//不是新数据时获取的记录的id，用于更新
        private DateTime originalDate;//记录datetimepicker选择前的数据
        public EnergyWrite()
        {
            InitializeComponent();
        }

        private void EnergyWrite_Load(object sender, EventArgs e)
        {
            cb_factory.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now.AddDays(-1).Date;
            showConfig();
        }

        private void showConfig()
        {
            lbl_config.Text = "当前参数:\n水单价(佳之家):" + MyVar.WaterPriceJZJ + "\n水单价(谷之田):" + MyVar.WaterPriceGZT + "\n电单价:" + MyVar.ElecPrice
                              + "\n天然气单价:" + MyVar.NgPrice + "\n蒸汽单价:" + MyVar.SteamPrice;
                              
        }
        private void tb_water_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_elec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_ng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_steam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void cb_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string sql = @"select * from FDD_Config where fdate=@fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                MyVar.WaterPriceJZJ = decimal.Parse(ds.Tables[0].Rows[0][10].ToString());
                MyVar.WaterPriceGZT = decimal.Parse(ds.Tables[0].Rows[0][11].ToString());
                MyVar.ElecPrice = decimal.Parse(ds.Tables[0].Rows[0][12].ToString());
                MyVar.NgPrice = decimal.Parse(ds.Tables[0].Rows[0][13].ToString());
                MyVar.SteamPrice = decimal.Parse(ds.Tables[0].Rows[0][14].ToString());
                refresh();
                originalDate = dateTimePicker1.Value.Date;
            }
            else
            {
                DialogResult dr = MessageBox.Show("该日期未设置参数，请设置", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    MyVar.FDDconfigDate = dateTimePicker1.Value.Date;
                    AttendanceConfig attendanceConfig = new AttendanceConfig();
                    attendanceConfig.ShowDialog();
                    originalDate = dateTimePicker1.Value.Date;
                    refresh();
                }
                else
                {
                    dateTimePicker1.Value = originalDate;
                    return;
                }

            }
            showConfig();
        }

        private void refresh()
        {
            string sql = @"select * from FDD_EnergyDetail where fdate=@fdate and factory=@factory";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
               SQLHelper.MakeParam("factory", cb_factory.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                id = ds.Tables[0].Rows[0][0].ToString();
                tb_water.Text = ds.Tables[0].Rows[0][3].ToString();
                tb_elec.Text = ds.Tables[0].Rows[0][4].ToString();
                tb_ng.Text = ds.Tables[0].Rows[0][5].ToString();
                tb_steam.Text = ds.Tables[0].Rows[0][6].ToString();

            }
            else
            {
                
                tb_water.Text = "";
                tb_elec.Text = "";
                tb_ng.Text = "";
                tb_steam.Text = "";

            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (tb_water.Text.Trim().Equals("") || tb_elec.Text.Trim().Equals("") || tb_ng.Text.Trim().Equals("") || tb_steam.Text.Trim().Equals(""))
            {
                MessageBox.Show("数据不能为空");
                return;
            }
            else
            {
                string sql = @"select * from FDD_EnergyDetail where fdate=@fdate and factory=@factory";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                SQLHelper.MakeParam("factory", cb_factory.Text));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    sql = @"insert FDD_EnergyDetail values (@fdate,@factory,@water,@elec,@ng,@steam)";
                    if (SQLHelper.ExecSql(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                               SQLHelper.MakeParam("factory", cb_factory.Text),
                               SQLHelper.MakeParam("water", tb_water.Text),
                               SQLHelper.MakeParam("elec", tb_elec.Text),
                               SQLHelper.MakeParam("ng", tb_ng.Text),
                               SQLHelper.MakeParam("steam", tb_steam.Text)))
                    {
                        MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "保存成功！", this);
                    }
                    else
                    {
                        MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "保存失败，请联系管理员！", this);
                    }

                }
                else
                {
                    DialogResult dr = MessageBox.Show("该工厂当日已有能耗数据，是否修改？", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    { 
                        sql = @"update FDD_EnergyDetail set water=@water,elec=@elec,
                                     ng=@ng,steam=@steam where id=@id";
                        if (SQLHelper.ExecSql(sql,
                                   SQLHelper.MakeParam("water", tb_water.Text),
                                   SQLHelper.MakeParam("elec", tb_elec.Text),
                                   SQLHelper.MakeParam("ng", tb_ng.Text),
                                   SQLHelper.MakeParam("steam", tb_steam.Text),
                                   SQLHelper.MakeParam("id", id)))
                        {
                            MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "修改成功！", this);
                        }
                        else
                        {
                            MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "修改失败，请联系管理员！", this);
                        }
                    }
                }
            }
        }

        private void tb_water_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_water.Text.Trim().Equals(""))
                {
                    tb_elec.Focus();
                    tb_elec.SelectAll();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_water.Location.X + tb_water.Width + 20, tb_water.Location.Y + tb_water.Height - 10, this.Text, "请填写用水量！", this);
                }
            }
        }

        private void tb_elec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_elec.Text.Trim().Equals(""))
                {
                    tb_ng.Focus();
                    tb_ng.SelectAll();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_elec.Location.X + tb_elec.Width + 20, tb_elec.Location.Y + tb_elec.Height - 10, this.Text, "请填写用电量！", this);
                }
            }
        }

        private void tb_ng_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_ng.Text.Trim().Equals(""))
                {
                    tb_steam.Focus();
                    tb_steam.SelectAll();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_ng.Location.X + tb_ng.Width + 20, tb_ng.Location.Y + tb_ng.Height - 10, this.Text, "请填写天然气用量！", this);
                }
            }
        }

        private void tb_steam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!tb_steam.Text.Trim().Equals(""))
                {
                    btn_ok.Focus();
                }
                else
                {
                    MsgBox.TipMsgShow(tb_steam.Location.X + tb_steam.Width + 20, tb_steam.Location.Y + tb_steam.Height - 10, this.Text, "请填写蒸汽用量！", this);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
