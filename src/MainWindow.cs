using System;
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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


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

            var mouseEvents = new EventType[] { EventType.MOUSE_LBUTTONDOWN, EventType.MOUSE_RBUTTONDOWN };
            var keyBoardEvents = new EventType[] { EventType.KEYEVENT_DOWN, EventType.KEYEVENT_UP };

            foreach (Event item in items)
            {
                DataRow row = dt.NewRow();
                if (mouseEvents.Contains(item.Name))
                {
                    row["Name"] = "Mouse";
                    row[2] = string.Format("X={0}", item.MouseX);
                    row[3] = string.Format("Y={0}", item.MouseY);
                }
                else if (keyBoardEvents.Contains(item.Name))
                {
                    row["Name"] = "Keyboard";
                    row[2] = item.Keyname;
                }


                switch (item.Name)
                {
                    case EventType.MOUSE_LBUTTONDOWN:
                        row["Type"] = "Left Click";
                        break;

                    case EventType.MOUSE_RBUTTONDOWN:
                        row["Type"] = "Right Click";
                        break;

                    case EventType.KEYEVENT_DOWN:
                        row["Type"] = "KeyDown";
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
