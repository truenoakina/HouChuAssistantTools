using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    public partial class AttendanceConfig : Form
    {
        //private bool isConfigExist;//当天配置文件是否存在，存在就更新，不存在插入
        private Dictionary<TextBox, string> _textBoxInitialValuesKQ = new Dictionary<TextBox, string>();
        private Dictionary<TextBox, string> _textBoxInitialValuesAll = new Dictionary<TextBox, string>();
        int hasChanged = 0;//通过此参数决定参数修改之后该如何修改数据库，0,没有变动，1，有变动，并且包含考勤参数，2，有变动，不包含考勤参数
        private string flog="";//记录参数修改日志
        
        public AttendanceConfig()
        {
            InitializeComponent();
        }

        private void AttendanceConfig_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Left = Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2;//桌面的宽度的一半减去自身宽的的一半
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2;
            //如果是从功能入口进入参数配置，取当天，否则取myvar中的date
            if ((MyVar.FDDconfigDate- new DateTime(2000, 1, 1)).Days>0)
            {
                dateTimePicker1.Value = MyVar.FDDconfigDate;
            }
            else {
                dateTimePicker1.Value = DateTime.Now.AddDays(-1).Date;
            }
            //以下三个参数，在点击确定后进行判断，是否有修改，有修改需要去批量更新当天的考勤数据
            _textBoxInitialValuesKQ.Add(tb_dayHours, tb_dayHours.Text);
            _textBoxInitialValuesKQ.Add(tb_regularHours, tb_regularHours.Text);
            _textBoxInitialValuesKQ.Add(tb_diningTime, tb_diningTime.Text);
            // 遍历窗体上所有TextBox，记录初始文本，除了以上三个参数
            GetAllTextBoxes(this);
            refresh();
        }

        // 递归遍历控件，找到所有TextBox
        private void GetAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    _textBoxInitialValuesAll[textBox] = textBox.Text;
                }
                else if (control.HasChildren)
                {
                    GetAllTextBoxes(control); // 递归找子控件里的TextBox
                }
            }
        }

        //判断是否有考勤参数修改
        private int cfgChange()
        {
            //0,没有变动，1，有变动，并且包含考勤参数，2，有变动，不包含考勤参数
            hasChanged = 0;
            bool kqHasChange = false;
            bool otherHasChange = false;

            // 遍历对比
            foreach (var item in _textBoxInitialValuesAll)
            {
                TextBox textBox = item.Key;
                string originalText = item.Value;

                if (textBox.Text != originalText)
                {

                    if (_textBoxInitialValuesKQ.ContainsKey(item.Key))
                    { 
                        kqHasChange = true;
                    }
                    else if (!_textBoxInitialValuesKQ.ContainsKey(item.Key))
                    {
                        otherHasChange = true;
                    }

                    flog = flog + textBox.Name + "由" + originalText + "改为" + textBox.Text+";"; 
                }
            }

            if (kqHasChange == true)
            {
                hasChanged = 1;
            }
            else if(kqHasChange == false && otherHasChange == true)
            {
                hasChanged = 2;
            }
            return hasChanged;
        }


        private void refresh()
        {
            string sql = @"select * from FDD_Config where fdate = @fdate";
            DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
            if (ds.Tables[0].Rows.Count > 0)
            {
                tb_dayHours.Text = ds.Tables[0].Rows[0][2].ToString();
                tb_regularHours.Text = ds.Tables[0].Rows[0][3].ToString();
                tb_diningTime.Text = ds.Tables[0].Rows[0][4].ToString();
                cb_WuCanKJ.Checked = ds.Tables[0].Rows[0][5].ToString() == "True" ? true : false;
                cb_WanCanKJ.Checked = ds.Tables[0].Rows[0][6].ToString() == "True" ? true : false;
                tb_regEmpPrice.Text = ds.Tables[0].Rows[0][7].ToString();
                tb_tempEmpPrice.Text = ds.Tables[0].Rows[0][8].ToString();
                tb_manualFixValue_JZJ.Text = ds.Tables[0].Rows[0][9].ToString();
                tb_manualFixValue_GZT.Text = ds.Tables[0].Rows[0][25].ToString();
                tb_waterPrice_JZJ.Text = ds.Tables[0].Rows[0][10].ToString();
                tb_waterPrice_GZT.Text = ds.Tables[0].Rows[0][11].ToString();
                tb_elecPrice.Text = ds.Tables[0].Rows[0][12].ToString();
                tb_ngPrice.Text = ds.Tables[0].Rows[0][13].ToString();
                tb_steamPrice.Text = ds.Tables[0].Rows[0][14].ToString();
                tb_ymEmpNum_JZJ.Text= ds.Tables[0].Rows[0][15].ToString();
                tb_YMwater_JZJ.Text = ds.Tables[0].Rows[0][16].ToString();
                tb_YMelec_JZJ.Text = ds.Tables[0].Rows[0][17].ToString();
                tb_YMng_JZJ.Text = ds.Tables[0].Rows[0][18].ToString();
                tb_YMsteam_JZJ.Text = ds.Tables[0].Rows[0][19].ToString();
                tb_ymEmpNum_GZT.Text = ds.Tables[0].Rows[0][20].ToString();
                tb_YMwater_GZT.Text = ds.Tables[0].Rows[0][21].ToString();
                tb_YMelec_GZT.Text = ds.Tables[0].Rows[0][22].ToString();
                tb_YMng_GZT.Text = ds.Tables[0].Rows[0][23].ToString();
                tb_YMsteam_GZT.Text = ds.Tables[0].Rows[0][24].ToString();
                tb_YMRenGongCost_JZJ.Text = ds.Tables[0].Rows[0][26].ToString();
                tb_YMRenGongCost_GZT.Text = ds.Tables[0].Rows[0][27].ToString();
            }
            else
            {
                tb_dayHours.Text = Properties.Settings.Default.FDD_dayHours.ToString();
                tb_regularHours.Text = Properties.Settings.Default.FDD_regularHours.ToString();
                tb_diningTime.Text = Properties.Settings.Default.FDD_diningTime.ToString();
                cb_WuCanKJ.Checked = Properties.Settings.Default.FDD_isWuCanKJ ? true : false;
                cb_WanCanKJ.Checked = Properties.Settings.Default.FDD_isWanCanKJ ? true : false;
                tb_regEmpPrice.Text = Properties.Settings.Default.FDD_regEmpPrice.ToString();
                tb_tempEmpPrice.Text = Properties.Settings.Default.FDD_tempEmpPrice.ToString();
                tb_manualFixValue_JZJ.Text = Properties.Settings.Default.FDD_manualFixValue_JZJ.ToString();
                tb_manualFixValue_GZT.Text = Properties.Settings.Default.FDD_manualFixValue_GZT.ToString();
                tb_waterPrice_JZJ.Text = Properties.Settings.Default.FDD_waterPrice_JZJ.ToString();
                tb_waterPrice_GZT.Text = Properties.Settings.Default.FDD_waterPrice_GZT.ToString();
                tb_elecPrice.Text = Properties.Settings.Default.FDD_elecPrice.ToString();
                tb_ngPrice.Text = Properties.Settings.Default.FDD_ngPrice.ToString();
                tb_steamPrice.Text = Properties.Settings.Default.FDD_steamPrice.ToString();
                tb_ymEmpNum_JZJ.Text = Properties.Settings.Default.FDD_YMEmpNum_JZJ.ToString();
                tb_YMwater_JZJ.Text = Properties.Settings.Default.FDD_YMwater_JZJ.ToString();
                tb_YMelec_JZJ.Text = Properties.Settings.Default.FDD_YMelec_JZJ.ToString();
                tb_YMng_JZJ.Text = Properties.Settings.Default.FDD_YMng_JZJ.ToString();
                tb_YMsteam_JZJ.Text = Properties.Settings.Default.FDD_YMsteam_JZJ.ToString();
                tb_ymEmpNum_GZT.Text = Properties.Settings.Default.FDD_YMEmpNum_GZT.ToString();
                tb_YMwater_GZT.Text = Properties.Settings.Default.FDD_YMwater_GZT.ToString();
                tb_YMelec_GZT.Text = Properties.Settings.Default.FDD_YMelec_GZT.ToString();
                tb_YMng_GZT.Text = Properties.Settings.Default.FDD_YMng_GZT.ToString();
                tb_YMsteam_GZT.Text = Properties.Settings.Default.FDD_YMsteam_GZT.ToString();
                tb_YMRenGongCost_JZJ.Text = "0";
                tb_YMRenGongCost_GZT.Text = "0";
            }
           

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (tb_dayHours.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_dayHours.Location.X + tb_dayHours.Width + 20, tb_dayHours.Location.Y + tb_dayHours.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_regularHours.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_regularHours.Location.X + tb_regularHours.Width + 20, tb_regularHours.Location.Y + tb_regularHours.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_diningTime.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_diningTime.Location.X + tb_diningTime.Width + 20, tb_diningTime.Location.Y + tb_diningTime.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_waterPrice_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_waterPrice_JZJ.Location.X + tb_waterPrice_JZJ.Width + 20, tb_waterPrice_JZJ.Location.Y + tb_waterPrice_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_waterPrice_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_waterPrice_GZT.Location.X + tb_waterPrice_GZT.Width + 20, tb_waterPrice_GZT.Location.Y + tb_waterPrice_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_elecPrice.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_elecPrice.Location.X + tb_elecPrice.Width + 20, tb_elecPrice.Location.Y + tb_elecPrice.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_ngPrice.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_ngPrice.Location.X + tb_ngPrice.Width + 20, tb_ngPrice.Location.Y + tb_ngPrice.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_steamPrice.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_steamPrice.Location.X + tb_steamPrice.Width + 20, tb_steamPrice.Location.Y + tb_steamPrice.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_regEmpPrice.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_regEmpPrice.Location.X + tb_regEmpPrice.Width + 20, tb_regEmpPrice.Location.Y + tb_regEmpPrice.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_tempEmpPrice.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_tempEmpPrice.Location.X + tb_tempEmpPrice.Width + 20, tb_tempEmpPrice.Location.Y + tb_tempEmpPrice.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_manualFixValue_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_manualFixValue_JZJ.Location.X + tb_manualFixValue_JZJ.Width + 20, tb_manualFixValue_JZJ.Location.Y + tb_manualFixValue_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_manualFixValue_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_manualFixValue_GZT.Location.X + tb_manualFixValue_GZT.Width + 20, tb_manualFixValue_GZT.Location.Y + tb_manualFixValue_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_ymEmpNum_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_ymEmpNum_JZJ.Location.X + tb_ymEmpNum_JZJ.Width + 20, tb_ymEmpNum_JZJ.Location.Y + tb_ymEmpNum_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMwater_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMwater_JZJ.Location.X + tb_YMwater_JZJ.Width + 20, tb_YMwater_JZJ.Location.Y + tb_YMwater_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMelec_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMelec_JZJ.Location.X + tb_YMelec_JZJ.Width + 20, tb_YMelec_JZJ.Location.Y + tb_YMelec_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMng_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMng_JZJ.Location.X + tb_YMng_JZJ.Width + 20, tb_YMng_JZJ.Location.Y + tb_YMng_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMsteam_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMsteam_JZJ.Location.X + tb_YMsteam_JZJ.Width + 20, tb_YMsteam_JZJ.Location.Y + tb_YMsteam_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_ymEmpNum_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_ymEmpNum_GZT.Location.X + tb_ymEmpNum_GZT.Width + 20, tb_ymEmpNum_GZT.Location.Y + tb_ymEmpNum_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMwater_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMwater_GZT.Location.X + tb_YMwater_GZT.Width + 20, tb_YMwater_GZT.Location.Y + tb_YMwater_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMelec_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMelec_GZT.Location.X + tb_YMelec_GZT.Width + 20, tb_YMelec_GZT.Location.Y + tb_YMelec_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMng_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMng_GZT.Location.X + tb_YMng_GZT.Width + 20, tb_YMng_GZT.Location.Y + tb_YMng_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMsteam_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMsteam_GZT.Location.X + tb_YMsteam_GZT.Width + 20, tb_YMsteam_GZT.Location.Y + tb_YMsteam_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMRenGongCost_JZJ.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMRenGongCost_JZJ.Location.X + tb_YMRenGongCost_JZJ.Width + 20, tb_YMRenGongCost_JZJ.Location.Y + tb_YMRenGongCost_JZJ.Height - 10, this.Text, "不能为空！", this);
            }
            else if (tb_YMRenGongCost_GZT.Text.Trim().Equals(""))
            {
                MsgBox.TipMsgShow(tb_YMRenGongCost_GZT.Location.X + tb_YMRenGongCost_GZT.Width + 20, tb_YMRenGongCost_GZT.Location.Y + tb_YMRenGongCost_GZT.Height - 10, this.Text, "不能为空！", this);
            }
            else 
            {
                string sql = @"select * from FDD_Config where fdate = @fdate";
                DataSet ds = SQLHelper.GetDataSet(sql, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cfgChange();
                    //参数有变动并且考勤参数有变动
                    if (hasChanged==1)
                    {
                        DialogResult dr = MessageBox.Show("考勤相关参数有变动，如继续确认会重新计算当天已有的考勤数据，对应日期的工时调整也会无效，确认修改?", "确认修改", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            //修改参数，并重新计算考勤参数
                            sql = @"update FDD_Config set dayHours=@dayHours,regularHours=@regularHours,dinetime=@dinetime,
                                 isWuCanKJ=@isWuCanKJ,isWanCanKJ=@isWanCanKJ,regEmpPrice=@regEmpPrice,tempEmpPrice=@tempEmpPrice,
                                 manualFixValue_JZJ=@manualFixValue_JZJ,manualFixValue_GZT=@manualFixValue_GZT,waterPrice_JZJ=@waterPrice_JZJ,
                                 waterPrice_GZT=@waterPrice_GZT,elecPrice=@elecPrice,ngPrice=@ngPrice,
                                 steamPrice=@steamPrice,YMEmpNum_JZJ =@YMEmpNum_JZJ
                                  ,YMwater_JZJ = @YMwater_JZJ
                                  ,YMelec_JZJ = @YMelec_JZJ
                                  ,YMng_JZJ = @YMng_JZJ
                                  ,YMsteam_JZJ = @YMsteam_JZJ
                                  ,YMEmpNum_GZT = @YMEmpNum_GZT
                                  ,YMwater_GZT = @YMwater_GZT
                                  ,YMelec_GZT = @YMelec_GZT
                                  ,YMng_GZT = @YMng_GZT
                                  ,YMsteam_GZT = @YMsteam_GZT,YMRenGongCost_JZJ=@YMRenGongCost_JZJ
                                  ,YMRenGongCost_GZT=@YMRenGongCost_GZT where fdate=@fdate";
                            //批量修改考勤数据
                            string sql2 = @"update ad set ad.days=iif(ad.endtime-ad.begintime>cfg.dayHours,1,0),
                                        ad.overtimes=iif(ad.endtime-ad.begintime-iif(ad.isKJWuCan=1,cfg.dinetime,0)-iif(ad.isKJWanCan=1,cfg.dinetime,0)-cfg.regularHours>0,ad.endtime-ad.begintime-iif(ad.isKJWuCan=1,cfg.dinetime,0)-iif(ad.isKJWanCan=1,cfg.dinetime,0)-cfg.regularHours,0),
                                        ad.sumhours=ad.endtime-ad.begintime-iif(ad.isKJWuCan=1,cfg.dinetime,0)-iif(ad.isKJWanCan=1,cfg.dinetime,0)
                                        from  FDD_AttendanceDetail ad inner join FDD_Config cfg on ad.fdate=cfg.fdate
                                        where ad.fdate=@fdate and ad.fmark='出勤'";
                            bool success1 = SQLHelper.ExecSql(sql,
                                    SQLHelper.MakeParam("dayHours", tb_dayHours.Text.Trim()),
                                    SQLHelper.MakeParam("regularHours", tb_regularHours.Text.Trim()),
                                    SQLHelper.MakeParam("dinetime", tb_diningTime.Text.Trim()),
                                    SQLHelper.MakeParam("isWuCanKJ", cb_WuCanKJ.Checked ? 1 : 0),
                                    SQLHelper.MakeParam("isWanCanKJ", cb_WanCanKJ.Checked ? 1 : 0),
                                    SQLHelper.MakeParam("regEmpPrice", tb_regEmpPrice.Text.Trim()),
                                    SQLHelper.MakeParam("tempEmpPrice", tb_tempEmpPrice.Text.Trim()),
                                    SQLHelper.MakeParam("manualFixValue_JZJ", tb_manualFixValue_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("manualFixValue_GZT", tb_manualFixValue_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("waterPrice_JZJ", tb_waterPrice_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("waterPrice_GZT", tb_waterPrice_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("elecPrice", tb_elecPrice.Text.Trim()),
                                    SQLHelper.MakeParam("ngPrice", tb_ngPrice.Text.Trim()),
                                    SQLHelper.MakeParam("steamPrice", tb_steamPrice.Text.Trim()),
                                    SQLHelper.MakeParam("YMEmpNum_JZJ", tb_ymEmpNum_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMwater_JZJ", tb_YMwater_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMelec_JZJ", tb_YMelec_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMng_JZJ", tb_YMng_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMsteam_JZJ", tb_YMsteam_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMEmpNum_GZT", tb_ymEmpNum_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMwater_GZT", tb_YMwater_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMelec_GZT", tb_YMelec_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMng_GZT", tb_YMng_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMsteam_GZT", tb_YMsteam_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMRenGongCost_JZJ", tb_YMRenGongCost_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMRenGongCost_GZT", tb_YMRenGongCost_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
                            bool success2 = SQLHelper.ExecSql(sql2, SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date));
                            if (success1 || success2)
                            {
                                MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "保存成功！", this);
                            }
                        }
                    }
                    //考勤参数没变，其他变了
                    else if(hasChanged == 2)
                    {
                        //修改参数，不重新计算考勤数据
                        sql = @"update FDD_Config set dayHours=@dayHours,regularHours=@regularHours,dinetime=@dinetime,
                                 isWuCanKJ=@isWuCanKJ,isWanCanKJ=@isWanCanKJ,regEmpPrice=@regEmpPrice,tempEmpPrice=@tempEmpPrice,
                                 manualFixValue_JZJ=@manualFixValue_JZJ,manualFixValue_GZT=@manualFixValue_GZT,waterPrice_JZJ=@waterPrice_JZJ,
                                 waterPrice_GZT=@waterPrice_GZT,elecPrice=@elecPrice,ngPrice=@ngPrice,
                                 steamPrice=@steamPrice,YMEmpNum_JZJ =@YMEmpNum_JZJ
                                  ,YMwater_JZJ = @YMwater_JZJ
                                  ,YMelec_JZJ = @YMelec_JZJ
                                  ,YMng_JZJ = @YMng_JZJ
                                  ,YMsteam_JZJ = @YMsteam_JZJ
                                  ,YMEmpNum_GZT = @YMEmpNum_GZT
                                  ,YMwater_GZT = @YMwater_GZT
                                  ,YMelec_GZT = @YMelec_GZT
                                  ,YMng_GZT = @YMng_GZT
                                  ,YMsteam_GZT = @YMsteam_GZT,YMRenGongCost_JZJ=@YMRenGongCost_JZJ
                                  ,YMRenGongCost_GZT=@YMRenGongCost_GZT where fdate=@fdate";
                        if (SQLHelper.ExecSql(sql,
                                    SQLHelper.MakeParam("dayHours", tb_dayHours.Text.Trim()),
                                    SQLHelper.MakeParam("regularHours", tb_regularHours.Text.Trim()),
                                    SQLHelper.MakeParam("dinetime", tb_diningTime.Text.Trim()),
                                    SQLHelper.MakeParam("isWuCanKJ", cb_WuCanKJ.Checked ? 1 : 0),
                                    SQLHelper.MakeParam("isWanCanKJ", cb_WanCanKJ.Checked ? 1 : 0),
                                    SQLHelper.MakeParam("regEmpPrice", tb_regEmpPrice.Text.Trim()),
                                    SQLHelper.MakeParam("tempEmpPrice", tb_tempEmpPrice.Text.Trim()),
                                    SQLHelper.MakeParam("manualFixValue_JZJ", tb_manualFixValue_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("manualFixValue_GZT", tb_manualFixValue_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("waterPrice_JZJ", tb_waterPrice_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("waterPrice_GZT", tb_waterPrice_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("elecPrice", tb_elecPrice.Text.Trim()),
                                    SQLHelper.MakeParam("ngPrice", tb_ngPrice.Text.Trim()),
                                    SQLHelper.MakeParam("steamPrice", tb_steamPrice.Text.Trim()),
                                    SQLHelper.MakeParam("YMEmpNum_JZJ", tb_ymEmpNum_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMwater_JZJ", tb_YMwater_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMelec_JZJ", tb_YMelec_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMng_JZJ", tb_YMng_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMsteam_JZJ", tb_YMsteam_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMEmpNum_GZT", tb_ymEmpNum_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMwater_GZT", tb_YMwater_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMelec_GZT", tb_YMelec_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMng_GZT", tb_YMng_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMsteam_GZT", tb_YMsteam_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("YMRenGongCost_JZJ", tb_YMRenGongCost_JZJ.Text.Trim()),
                                    SQLHelper.MakeParam("YMRenGongCost_GZT", tb_YMRenGongCost_GZT.Text.Trim()),
                                    SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date)))
                        {
                            MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "保存成功！", this);
                        }
                    }

                    //记录日志
                    if (!flog.Trim().Equals(""))
                    {
                        string sql3 = @"insert into FDD_log values (@ftype,@operator,@pcname,@opertime,@cfgdate,@flog)";
                        SQLHelper.ExecSql(sql3,
                            SQLHelper.MakeParam("ftype", "参数修改"),
                            SQLHelper.MakeParam("operator", MyVar.LoginName),
                            SQLHelper.MakeParam("pcname", MyVar.LoginComputer),
                            SQLHelper.MakeParam("opertime", DateTime.Now.ToString()),
                            SQLHelper.MakeParam("cfgdate", dateTimePicker1.Value.Date),
                            SQLHelper.MakeParam("flog", flog));
                    }
                }
                //没有当日有配置信息，新增
                else
                {
                    sql = @"insert into FDD_Config values (@fdate,@dayHours,@regularHours,@dinetime,
                                 @isWuCanKJ,@isWanCanKJ,@regEmpPrice,@tempEmpPrice,
                                 @manualFixValue_JZJ,@waterPrice_JZJ,@waterPrice_GZT,@elecPrice,@ngPrice,@steamPrice,@YMEmpNum_JZJ
                                   ,@YMwater_JZJ
                                   ,@YMelec_JZJ
                                   ,@YMng_JZJ
                                   ,@YMsteam_JZJ
                                   ,@YMEmpNum_GZT
                                   ,@YMwater_GZT
                                   ,@YMelec_GZT
                                   ,@YMng_GZT
                                   ,@YMsteam_GZT,@manualFixValue_GZT,@YMRenGongCost_JZJ,@YMRenGongCost_GZT)";
                    if (SQLHelper.ExecSql(sql,
                            SQLHelper.MakeParam("fdate", dateTimePicker1.Value.Date),
                            SQLHelper.MakeParam("dayHours", tb_dayHours.Text.Trim()),
                            SQLHelper.MakeParam("regularHours", tb_regularHours.Text.Trim()),
                            SQLHelper.MakeParam("dinetime", tb_diningTime.Text.Trim()),
                            SQLHelper.MakeParam("isWuCanKJ", cb_WuCanKJ.Checked ? 1 : 0),
                            SQLHelper.MakeParam("isWanCanKJ", cb_WanCanKJ.Checked ? 1 : 0),
                            SQLHelper.MakeParam("regEmpPrice", tb_regEmpPrice.Text.Trim()),
                            SQLHelper.MakeParam("tempEmpPrice", tb_tempEmpPrice.Text.Trim()),
                            SQLHelper.MakeParam("manualFixValue_JZJ", tb_manualFixValue_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("waterPrice_JZJ", tb_waterPrice_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("waterPrice_GZT", tb_waterPrice_GZT.Text.Trim()),
                            SQLHelper.MakeParam("elecPrice", tb_elecPrice.Text.Trim()),
                            SQLHelper.MakeParam("ngPrice", tb_ngPrice.Text.Trim()),
                            SQLHelper.MakeParam("steamPrice", tb_steamPrice.Text.Trim()),
                            SQLHelper.MakeParam("YMEmpNum_JZJ", tb_ymEmpNum_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMwater_JZJ", tb_YMwater_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMelec_JZJ", tb_YMelec_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMng_JZJ", tb_YMng_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMsteam_JZJ", tb_YMsteam_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMEmpNum_GZT", tb_ymEmpNum_GZT.Text.Trim()),
                            SQLHelper.MakeParam("YMwater_GZT", tb_YMwater_GZT.Text.Trim()),
                            SQLHelper.MakeParam("YMelec_GZT", tb_YMelec_GZT.Text.Trim()),
                            SQLHelper.MakeParam("YMng_GZT", tb_YMng_GZT.Text.Trim()),
                            SQLHelper.MakeParam("YMsteam_GZT", tb_YMsteam_GZT.Text.Trim()),
                            SQLHelper.MakeParam("manualFixValue_GZT", tb_manualFixValue_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMRenGongCost_JZJ", tb_YMRenGongCost_JZJ.Text.Trim()),
                            SQLHelper.MakeParam("YMRenGongCost_GZT", tb_YMRenGongCost_GZT.Text.Trim()))
                        )
                    {
                        MsgBox.TipMsgShow(btn_ok.Location.X + btn_ok.Width + 20, btn_ok.Location.Y + btn_ok.Height - 10, this.Text, "保存成功！", this);
                    }
                }
                Properties.Settings.Default.FDD_dayHours = decimal.Parse(tb_dayHours.Text.Trim().ToString());
                Properties.Settings.Default.FDD_regularHours = decimal.Parse(tb_regularHours.Text.Trim().ToString());
                Properties.Settings.Default.FDD_diningTime = decimal.Parse(tb_diningTime.Text.Trim().ToString());
                Properties.Settings.Default.FDD_isWuCanKJ = cb_WuCanKJ.Checked ? true : false;
                Properties.Settings.Default.FDD_isWanCanKJ = cb_WanCanKJ.Checked ? true : false;
                Properties.Settings.Default.FDD_regEmpPrice = int.Parse(tb_regEmpPrice.Text.Trim().ToString());
                Properties.Settings.Default.FDD_tempEmpPrice = int.Parse(tb_tempEmpPrice.Text.Trim().ToString());
                Properties.Settings.Default.FDD_manualFixValue_JZJ = int.Parse(tb_manualFixValue_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_manualFixValue_GZT = int.Parse(tb_manualFixValue_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_waterPrice_JZJ = decimal.Parse(tb_waterPrice_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_waterPrice_GZT = decimal.Parse(tb_waterPrice_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_elecPrice = decimal.Parse(tb_elecPrice.Text.Trim().ToString());
                Properties.Settings.Default.FDD_ngPrice = decimal.Parse(tb_ngPrice.Text.Trim().ToString());
                Properties.Settings.Default.FDD_steamPrice = decimal.Parse(tb_steamPrice.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMEmpNum_JZJ = int.Parse(tb_ymEmpNum_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMwater_JZJ = int.Parse(tb_YMwater_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMelec_JZJ = int.Parse(tb_YMelec_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMng_JZJ = int.Parse(tb_YMng_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMsteam_JZJ = int.Parse(tb_YMsteam_JZJ.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMEmpNum_GZT = int.Parse(tb_ymEmpNum_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMwater_GZT = int.Parse(tb_YMwater_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMelec_GZT = int.Parse(tb_YMelec_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMng_GZT = int.Parse(tb_YMng_GZT.Text.Trim().ToString());
                Properties.Settings.Default.FDD_YMsteam_GZT = int.Parse(tb_YMsteam_GZT.Text.Trim().ToString());
                Properties.Settings.Default.Save();
                MyVar.DayHours = decimal.Parse(Properties.Settings.Default.FDD_dayHours.ToString());
                MyVar.RegularHours = decimal.Parse(Properties.Settings.Default.FDD_regularHours.ToString());
                MyVar.DiningTime = decimal.Parse(Properties.Settings.Default.FDD_diningTime.ToString());
                MyVar.IsWuCanKJ = Properties.Settings.Default.FDD_isWuCanKJ;
                MyVar.IsWanCanKJ = Properties.Settings.Default.FDD_isWanCanKJ;
                MyVar.WaterPriceJZJ = Properties.Settings.Default.FDD_waterPrice_JZJ;
                MyVar.WaterPriceGZT = Properties.Settings.Default.FDD_waterPrice_GZT;
                MyVar.ElecPrice = Properties.Settings.Default.FDD_elecPrice;
                MyVar.NgPrice = Properties.Settings.Default.FDD_ngPrice;
                MyVar.SteamPrice = Properties.Settings.Default.FDD_steamPrice;
                MyVar.RegEmpPrice = Properties.Settings.Default.FDD_regEmpPrice;
                MyVar.TempEmpPrice = Properties.Settings.Default.FDD_tempEmpPrice;
                MyVar.ManualFixValue = Properties.Settings.Default.FDD_manualFixValue_JZJ;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void tb_dayHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_regularHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tb_diningTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_waterPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_elecPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_ngPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_steamPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_regEmpPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            
        }

        private void tb_tempEmpPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            
        }

        private void tb_manualFixValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void tb_ymEmpNum_JZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
        }

        private void YMRenGongCost_JZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }

        private void tb_ymEmpNum_GZT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            
        }

        private void YMRenGongCost_GZT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9')) e.Handled = true;
            if (e.KeyChar == '\b') e.Handled = false;
            if (e.KeyChar == '.') e.Handled = false;
        }
    }
}
