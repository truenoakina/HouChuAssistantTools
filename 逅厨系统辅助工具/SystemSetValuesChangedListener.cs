using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 逅厨系统辅助工具
{
    /// <summary>
    /// 参数为旧值,新值和是否取消修改.
    /// </summary>
    public class ModifedChangedEventArgs : EventArgs
    {
        public bool OldValue { get; set; } = false;
        public bool NewValue { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
    }

    /// <summary>
    /// 属性发生变化代理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">修改相关的参数。</param>
    public delegate void ControlModificationEventHandler(object sender, ModifedChangedEventArgs e);

    public class SystemSetValuesChangedListener
    {
        /// <summary>
        /// 在Modifed的值变化后触发。
        /// </summary>
        public event ControlModificationEventHandler ModifedChanged;

        bool modified = false;
        /// <summary>
        /// 控件是否已经修改，同时会触发 ModifedChanged 事件。
        /// </summary>
        public bool Modified
        {
            get { return modified; }
            set
            {
                Console.WriteLine("Old value: " + modified + "\tnew value: " + value);
                if (modified != value && ModifedChanged != null)
                {
                    ModifedChangedEventArgs args = new ModifedChangedEventArgs()
                    {
                        OldValue = modified,
                        NewValue = value,
                        IsCanceled = false
                    };
                    ModifedChanged.Invoke(this, args);
                    if (!args.IsCanceled)
                        modified = value;
                }

            }
        }

        /// <summary>
        /// 对指定窗体进行绑定，用于监听编辑控件中的值的变化。
        /// </summary>
        /// <param name="control"></param>
        public SystemSetValuesChangedListener(Control control)
        {
            Modified = false;
            foreach (var item in GetAllControls(control))
            {
                if (item is TextBox tb)
                    tb.TextChanged += (sender, e) => Modified = tb.Focused ? true : modified;
                else if (item is DateTimePicker dp)
                    dp.TextChanged += (sender, e) => Modified = dp.Focused ? true : modified;
                else if (item is CheckBox ckb)
                    ckb.CheckedChanged += (sender, e) => Modified = ckb.Focused ? true : modified;
                // 如果有更多的控件需要监听，可在此加入相应的代码
            }
        }

        /// <summary>
        /// 返回指定控件其子属性。
        /// </summary>
        /// <param name="control">待遍历控件。</param>
        /// <param name="controls">控件列表。</param>
        /// <returns></returns>
        public List<Control> GetAllControls(Control control)
        {
            List<Control> controls = new List<Control>();
            if (control != null)
            {
                foreach (Control subControl in control.Controls)
                {
                    controls.Add(subControl);
                    controls.AddRange(GetAllControls(subControl));
                }
            }
            return controls;
        }
    }
}
