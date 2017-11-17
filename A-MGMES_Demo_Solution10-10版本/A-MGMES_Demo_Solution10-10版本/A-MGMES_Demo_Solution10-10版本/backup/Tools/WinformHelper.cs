using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tools
{
    public class WinformHelper
    {
        public static Point GetCentrePoint()
        {
            int xWidth = SystemInformation.PrimaryMonitorSize.Width;//获取显示器屏幕宽度
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;//高度
            return new Point(xWidth / 2 - 208, yHeight / 2 - 123);//这里需要再减去窗体本身的宽度和高度的一半
        }
    }
}
