using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using MacroAutomation.Forms;
using MacroAutomation.Core;
using System.Threading;

namespace MacroAutomation
{
    public partial class MainWindow : Form
    {
        private RecordingAppBar dock;
        public List<Event> events { get; set; } = new List<Event>();
        private DataTable dataTable = new DataTable();
        private BindingSource bindingSource = new BindingSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// </summary>
        /// Adapted from https://www.codeproject.com/Articles/5264831/How-to-Send-Inputs-using-Csharp
        /// <summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUTUNION
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public INPUTUNION u;
        }
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        public enum MouseEventF
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }

        [DllImport("User32.dll", SetLastError = true)]
        private static extern uint SendInput(uint cInputs, INPUT[] pInputs, int cbSize);


        [DllImport("User32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private void run_Click(object sender, EventArgs e)
        {


            foreach (Event evt in this.events)
            {
                List<INPUT> inputs = new List<INPUT>();
                switch (evt.Name)
                {
                    case EventType.MOUSE_LBUTTONUP:
                        SetCursorPos((int)evt.MouseX, (int)evt.MouseY);
                        inputs.Add(new INPUT
                        {
                            type = (int)InputType.Mouse,
                            u = new INPUTUNION
                            {
                                mi = new MOUSEINPUT
                                {
                                    dx = 0,
                                    dy = 0,
                                    dwFlags = (uint)MouseEventF.LeftUp,
                                    dwExtraInfo = GetMessageExtraInfo()
                                }
                            }
                        });
                        break;
                    case EventType.MOUSE_LBUTTONDOWN:
                        SetCursorPos((int)evt.MouseX, (int)evt.MouseY);
                        inputs.Add(new INPUT
                        {
                            type = (int)InputType.Mouse,
                            u = new INPUTUNION
                            {
                                mi = new MOUSEINPUT
                                {
                                    dx = 0,
                                    dy = 0,
                                    dwFlags = (uint)MouseEventF.LeftDown,
                                    dwExtraInfo = GetMessageExtraInfo()
                                }
                            }
                        });
                        break;
                    default:
                        continue;
                }

                
                SendInput((uint)inputs.ToArray().Length, inputs.ToArray(), Marshal.SizeOf(typeof(INPUT)));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            this.Visible = false;
            dock = new RecordingAppBar(this);
            dock.Show();
        }

        private void export_Click(object sender, EventArgs e)
        {


            string fileContent = "";
            fileContent += "import pyautogui as pg\n";

            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                ["lwin"] = "winleft",
                ["rightshift"] = "shiftright",
                ["d1"] = "1",
                ["d2"] = "2",
                ["d3"] = "3",
                ["d4"] = "4",
                ["d5"] = "5",
                ["d6"] = "6",
                ["d7"] = "7",
                ["d8"] = "8",
                ["d9"] = "9",
                ["d0"] = "0"
            };

            foreach (Event ev in this.events)
            {
                switch (ev.Name)
                {
                    case EventType.MOUSE_LBUTTONDOWN:
                        Console.WriteLine(string.Format("Mouse Left Button Down at {0},{1}", ev.MouseX, ev.MouseY));
                        fileContent += string.Format("pg.click(x={0},y={1},button='left')\n", ev.MouseX, ev.MouseY);
                        break;

                    case EventType.MOUSE_RBUTTONDOWN:
                        Console.WriteLine(string.Format("Mouse Right Button Down at {0},{1}", ev.MouseX, ev.MouseY));
                        fileContent += string.Format("pg.click(x={0},y={1},button='right')\n", ev.MouseX, ev.MouseY);
                        break;

                    case EventType.KEYEVENT_DOWN:

                        string value = null;
                        dict.TryGetValue(ev.Keyname.ToLower(), out value);
                        Console.WriteLine(string.Format("KeyEvent down with '{0}'", ev.Keyname));
                        fileContent += string.Format("pg.press('{0}')\n", value != null ? value : ev.Keyname.ToLower());
                        break;
                }


            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                FileStream myStream;
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Filter = "Python files (*.py)|*.py|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if ((myStream = (FileStream)saveFileDialog.OpenFile()) != null)
                    {

                        var bytes = Encoding.UTF8.GetBytes(fileContent);
                        myStream.Write(bytes, 0, bytes.Length);
                        myStream.Close();
                    }

                }
            }
        }

        public void ShowExportButton()
        {
            this.exportBtn.Enabled = true;
        }


        public static DataTable ConvertToDataTable(List<Event> items)
        {
            DataTable dt = new DataTable();

            //E.g., Mouse, KeyBoard
            dt.Columns.Add("Name");
            //E.g., Left Click, Right Click, KeyDown,
            dt.Columns.Add("Type");
            //X and Y mouse coordinates or key name pressed or text pressed
            dt.Columns.Add(" ");
            dt.Columns.Add("  ");

            foreach (Event item in items)
            {
                DataRow row = dt.NewRow();

                switch (item.Name)
                {
                    case EventType.MOUSE_LBUTTONUP:
                        continue;
                    case EventType.MOUSE_LBUTTONDOWN:
                        row["Type"] = "Left Click";
                        row["Name"] = "Mouse";
                        row[2] = string.Format("X={0}", item.MouseX);
                        row[3] = string.Format("Y={0}", item.MouseY);
                        break;

                    case EventType.MOUSE_RBUTTONUP:
                        continue;
                    case EventType.MOUSE_RBUTTONDOWN:
                        row["Type"] = "Right Click";
                        row["Name"] = "Mouse";
                        row[2] = string.Format("X={0}", item.MouseX);
                        row[3] = string.Format("Y={0}", item.MouseY);
                        break;

                    case EventType.KEYEVENT_UP:
                        continue;
                    case EventType.KEYEVENT_DOWN:
                        row["Type"] = "KeyDown";
                        row["Name"] = "Keyboard";
                        row[2] = item.Keyname;
                        break;
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        public void LoadData()
        {
            dataTable = ConvertToDataTable(this.events);
            this.bindingSource.DataSource = dataTable;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.dataGrid.DataSource = this.bindingSource;
            this.LoadData();
        }

        private void MainWindow_VisibleChanged(object sender, EventArgs e)
        {

            this.LoadData();
            if (this.events != null && this.events.Count > 0)
            {
                this.ShowExportButton();
            }
        }
    }

    class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
