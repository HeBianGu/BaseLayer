using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.MouseHelp
{
    /// <summary> 模拟鼠标点击 </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public class MyMouseHelper
    {
        /// <summary> 检查鼠标是否已经安装. </summary>
        public static bool MousePresent
        {
            get { return SystemInformation.MousePresent; }
        }

        /// <summary> 检查鼠标是否存在滚轮 </summary>
        public static bool WheelExists
        {
            get
            {
                if (!SystemInformation.MousePresent)
                {
                    throw new InvalidOperationException("没有找到鼠标.");
                }
                return SystemInformation.MouseWheelPresent;
            }
        }

        /// <summary> 获取鼠标滚轮每次滚动的行数 </summary>
        public static int WheelScrollLines
        {
            get
            {
                if (!WheelExists)
                {
                    throw new InvalidOperationException("没有找到鼠标滑轮.");
                }
                return SystemInformation.MouseWheelScrollLines;
            }
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        /// <summary> 连续两次鼠标单击之间会被处理成双击事件的间隔时间。以毫秒表示的双击时间 </summary>
        [DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public static extern int GetDoubleClickTime();


        /// <summary> 检取光标的位置，以屏幕坐标表示。如果成功，返回值非零；如果失败，返回值为零 </summary>
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(Point lpPoint);

        /// <summary> 把光标移到屏幕的指定位置。如果新位置不在由 ClipCursor函数设置的屏幕矩形区域之内，则系统自动调整坐标，使得光标在矩形之内。如果成功，返回非零值；如果失败，返回值是零 </summary>
        public static extern int SetCursorPos(int x, int y);

        [Flags]
        private enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }


        /// <summary> 在当前鼠标的位置左键点击一下 </summary>
        public static void MouseClick()
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary> 移动到坐标位置点击 要点击的坐标位置,屏幕绝对值 </summary>
        public static void MouseClick(Point location)
        {
            MouseMove(location);
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary> 移动到坐标位置点击 要点击的坐标位置,屏幕绝对值 </summary>
        public static void MouseRightClick(Point location)
        {
            MouseMove(location);
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary> 移动到坐标位置 要移动到的坐标位置,屏幕绝对值 </summary>
        public static void MouseMove(Point location)
        {
            SetCursorPos(location.X, location.Y);
        }

    }
}