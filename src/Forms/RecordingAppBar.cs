using EventHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MacroAutomation.Core;

namespace MacroAutomation.Forms
{

    public partial class RecordingAppBar : Form
    {

        private FlowLayoutPanel flowLayoutPanel1;
        internal Button button1;
        internal Button button2;


        internal Button playPauseBtn;
        internal Button stopBtn;
        private MainWindow parentWindow;
        private bool isRecording = false;
        EventHookRecorder recorder = new EventHookRecorder();
        List<Event> events = new List<Event>();

        public RecordingAppBar(MainWindow parentWindow)
        {
            this.parentWindow = parentWindow;
            this.InitializeComponent();
            this.recorder.AddMouseListener(MouseListener);
            this.recorder.AddKeyBoardListener(KeyboardListener);
        }


        public List<Event> GetEvents()
        {
            return this.events;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingAppBar));
            this.stopBtn = new System.Windows.Forms.Button();
            this.playPauseBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stopBtn
            // 
            this.stopBtn.BackColor = System.Drawing.Color.Transparent;
            this.stopBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.stopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.stopBtn.Image = ((System.Drawing.Image)(resources.GetObject("stopBtn.Image")));
            this.stopBtn.Location = new System.Drawing.Point(68, 0);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(0);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(68, 60);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "Stop";
            this.stopBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stopBtn.UseVisualStyleBackColor = false;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // playPauseBtn
            // 
            this.playPauseBtn.BackColor = System.Drawing.Color.Transparent;
            this.playPauseBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.playPauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playPauseBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.playPauseBtn.Image = ((System.Drawing.Image)(resources.GetObject("playPauseBtn.Image")));
            this.playPauseBtn.Location = new System.Drawing.Point(0, 0);
            this.playPauseBtn.Margin = new System.Windows.Forms.Padding(0);
            this.playPauseBtn.Name = "playPauseBtn";
            this.playPauseBtn.Size = new System.Drawing.Size(68, 60);
            this.playPauseBtn.TabIndex = 0;
            this.playPauseBtn.Text = "Play";
            this.playPauseBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.playPauseBtn.UseVisualStyleBackColor = false;
            this.playPauseBtn.Click += new System.EventHandler(this.PlayPauseBtn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.playPauseBtn);
            this.flowLayoutPanel1.Controls.Add(this.stopBtn);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(204, 60);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Image = global::MacroAutomation.Properties.Resources.delete_32px;
            this.button1.Location = new System.Drawing.Point(136, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 60);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.CancelBtn_click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(0, 60);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 60);
            this.button2.TabIndex = 3;
            this.button2.Text = "Stop";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // RecordingAppBar
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(204, 60);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordingAppBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void Cancel()
        {

            this.events.Clear();
        }

        private void BackToMain()
        {
            this.Close();
            this.parentWindow.Visible = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            this.parentWindow.events.AddRange(this.GetEvents());
            this.BackToMain();
        }

        private void PlayPauseBtn_Click(object sender, EventArgs e)
        {
            if (this.isRecording)
            {
                this.playPauseBtn.Image = global::MacroAutomation.Properties.Resources.play_icon;
                this.isRecording = false;
                this.recorder.Stop();
                this.stopBtn.Enabled = true;
            }
            else
            {
                this.playPauseBtn.Image = global::MacroAutomation.Properties.Resources.pause_icon;
                this.isRecording = true;
                this.recorder.Start();
                this.stopBtn.Enabled = false;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            RegisterBar();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RegisterBar();
        }

        public void MouseListener(object sender, EventHook.MouseEventArgs e)
        {
            if (!this.isRecording)
            {
                return;
            }

            var eventMessage = e.Message.ToString();
            if (this.Bounds.Contains(new Point(e.Point.x, e.Point.y)))
            {
                Console.WriteLine(string.Format("Rejected Mouse event {0} at point {1},{2}", eventMessage, e.Point.x, e.Point.y));
                return;
            }

            Console.WriteLine(string.Format("Mouse event {0} at point {1},{2}", eventMessage, e.Point.x, e.Point.y));
            Event ev = new Event();

            switch (eventMessage)
            {

                case "WM_LBUTTONDOWN":
                    ev.Name = EventType.MOUSE_LBUTTONDOWN;
                    break;

                case "WM_RBUTTONDOWN":
                    ev.Name = EventType.MOUSE_RBUTTONDOWN;
                    break;

                default:
                    return;
            }

            ev.MouseX = e.Point.x;
            ev.MouseY = e.Point.y;
            ev.TimeInMillis = DateTime.Now.Millisecond;
            this.events.Add(ev);
        }

        public void KeyboardListener(object sender, KeyInputEventArgs e)
        {
            Event ev = new Event();
            Console.WriteLine(string.Format("KeyInput event {0} at point {1},{2}", e.KeyData.EventType, e.KeyData.Keyname, e.KeyData.UnicodeCharacter));

            switch (e.KeyData.EventType)
            {
                case KeyEvent.up:
                    ev.Name = EventType.KEYEVENT_UP;
                    break;

                case KeyEvent.down:
                    ev.Name = EventType.KEYEVENT_DOWN;
                    break;
            }

            ev.Keyname = e.KeyData.Keyname;
            ev.UnicodeCharacter = e.KeyData.UnicodeCharacter;
            ev.TimeInMillis = DateTime.Now.Millisecond;
            this.events.Add(ev);
        }

        private void CancelBtn_click(object sender, EventArgs e)
        {
            this.Cancel();
            this.BackToMain();
        }

        /// <summary>
        /// Adapted from https://www.codeproject.com/Articles/6741/AppBar-using-C
        /// </summary>
        #region Appbar
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        enum ABMsg : int
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }

        enum ABNotify : int
        {
            ABN_STATECHANGE = 0,
            ABN_POSCHANGED,
            ABN_FULLSCREENAPP,
            ABN_WINDOWARRANGE
        }

        enum ABEdge : int
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        enum ABWindowPos : int
        {
            HWND_NOTOPMOST = -2,
            HWND_TOPMOST,
            HWND_TOP,
            HWND_BOTTOM,
        };

        enum ABS : int
        {
            ABS_AUTOHIDE = 0x0000001,
            ABS_ALWAYSONTOP = 0x0000002
        }

        enum SWP : int
        {
            SWP_NOMOVE = 0x0002,
            SWP_NOSIZE = 0x0001,
            SWP_NOACTIVATE = 0x0010,
        }

        private bool fBarRegistered = false;

        [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
        static extern uint SHAppBarMessage(int dwMessage,  ref APPBARDATA pData);
        [DllImport("USER32")]
        static extern int GetSystemMetrics(int Index);
        [DllImport("User32.dll", ExactSpelling = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool MoveWindow
            (IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern int RegisterWindowMessage(string msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetWindowPos
            (IntPtr hWnd,  IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private int uCallBack;


        private void RegisterBar()
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = this.Handle;

            if (!fBarRegistered)
            {
                uCallBack = RegisterWindowMessage("AppBarMessage");
                abd.uCallbackMessage = uCallBack;

                uint ret = SHAppBarMessage((int)ABMsg.ABM_SETAUTOHIDEBAR, ref abd);
                fBarRegistered = true;

                ABSetPos();
            }
            else
            {
                SHAppBarMessage((int)ABMsg.ABM_SETAUTOHIDEBAR, ref abd);
                fBarRegistered = false;
            }

            //SetWindowPos(this.Handle, (IntPtr)ABWindowPos.HWND_TOP, 0, 0, 0, 0, (int)SWP.SWP_NOMOVE | (int)SWP.SWP_NOSIZE);

        }

        private void ABSetPos()
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = this.Handle;
            abd.uEdge = (int)ABEdge.ABE_TOP;

            if (abd.uEdge == (int)ABEdge.ABE_LEFT || abd.uEdge == (int)ABEdge.ABE_RIGHT)
            {
                abd.rc.top = 0;
                abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
                if (abd.uEdge == (int)ABEdge.ABE_LEFT)
                {
                    abd.rc.left = 0;
                    abd.rc.right = Size.Width;
                }
                else
                {
                    abd.rc.right = SystemInformation.PrimaryMonitorSize.Width;
                    abd.rc.left = abd.rc.right - Size.Width;
                }

            }
            else
            {
                abd.rc.left = SystemInformation.PrimaryMonitorSize.Width / 2 - this.Size.Width / 2;
                abd.rc.right = SystemInformation.PrimaryMonitorSize.Width / 2 + this.Size.Width / 2;
                if (abd.uEdge == (int)ABEdge.ABE_TOP)
                {
                    abd.rc.top = 0;
                    abd.rc.bottom = this.Size.Height;
                }
                else
                {
                    abd.rc.bottom = SystemInformation.PrimaryMonitorSize.Height;
                    abd.rc.top = abd.rc.bottom - Size.Height;
                }
            }

            // Query the system for an approved size and position. 
            SHAppBarMessage((int)ABMsg.ABM_QUERYPOS, ref abd);

            // Adjust the rectangle, depending on the edge to which the 
            // appbar is anchored. 
            switch (abd.uEdge)
            {
                case (int)ABEdge.ABE_LEFT:
                    abd.rc.right = abd.rc.left + Size.Width;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    abd.rc.left = abd.rc.right - Size.Width;
                    break;
                case (int)ABEdge.ABE_TOP:
                    abd.rc.bottom = abd.rc.top + Size.Height;
                    break;
                case (int)ABEdge.ABE_BOTTOM:
                    abd.rc.top = abd.rc.bottom - Size.Height;
                    break;
            }

            // Pass the final bounding rectangle to the system. 
            SHAppBarMessage((int)ABMsg.ABM_SETPOS, ref abd);

            // Move and size the appbar so that it conforms to the 
            // bounding rectangle passed to the system. 
            MoveWindow(abd.hWnd, abd.rc.left, abd.rc.top,
                abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, true);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == uCallBack)
            {
                switch (m.WParam.ToInt32())
                {
                    case (int)ABNotify.ABN_POSCHANGED:
                        ABSetPos();
                        break;
                }
            }

            base.WndProc(ref m);
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= (~0x00C00000); // WS_CAPTION
                cp.Style &= (~0x00800000); // WS_BORDER
                cp.ExStyle = 0x00000080 | 0x00000008; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST
                return cp;
            }
        }

        #endregion 
    }
}
