using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace 逅厨系统辅助工具
{
    public class MsgBox
    {
        /// <summary>
        /// 提示消息显示
        /// </summary>
        /// <param name="X">消息位置，相对于左上角，X坐标</param>
        /// <param name="Y">消息位置，相对于左上角，Y坐标</param>
        /// <param name="title">消息标题</param>
        /// <param name="msg">消息内容</param>
        /// <param name="windows">窗口</param>
        public static void TipMsgShow(int X, int Y, string title, string msg, IWin32Window windows)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = title;
            Point showLocation = new Point(X, Y);
            toolTip.Show(msg, windows, showLocation, 2000);
        }

        



    }
}
