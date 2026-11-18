using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    #region Editor
    /// <summary>
    /// FolderBrowser 的设计器基类
    /// </summary>
    public class FolderNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if (value != null)
            {
                browser.DirectoryPath = string.Format("{0}", value);
            }
            if (browser.ShowDialog(null) == DialogResult.OK)
                return browser.DirectoryPath;
            return value;
        }
    }
    #endregion
}

