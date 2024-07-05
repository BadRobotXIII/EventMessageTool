using Newtonsoft.Json;

using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

//*************************************************************************************************
//** Developer: Kameron Zulfic
//** Date:  03 / 03 / 2024
//** Function: Main Form Class
//*************************************************************************************************
namespace EventMessageTool {
    public partial class FormMain : Form {
        private TaskManager tasks = new();
        private Workbook wrkBk = new();
        private Utils utils = new();
        private List<Workbook.EventObj> tagValues = new();
        private string filePath;

        //Display console for debug
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


        public FormMain(IContainer components, OpenFileDialog openFileDialog, TabPage tabEvents, NumericUpDown upDnSlot, TextBox tbIPAddress, ProgressBar progressBar, TextBox tbEvents, TextBox tbDBName) {
            this.components = components;
            this.openFileDialog = openFileDialog;
            this.tabEvents = tabEvents;
            this.upDnSlot = upDnSlot;
            this.tbIPAddress = tbIPAddress;
            this.progressBar = progressBar;
            this.tbEvents = tbEvents;
            this.tbDBName = tbDBName;
        }

        public class AppDefaults {
            public string Module { get; set; }
            public string DbName { get; set; }
            public string IP { get; set; }
            public string Tag { get; set; }
            public string Path { get; set; }

        }

        private void SetAppDefaults() {
            string appLocation = AppContext.BaseDirectory;
            string appFileLoc = appLocation + "EventMsgDefaults.json";
            bool defaultFileExists = File.Exists(appFileLoc);
            AppDefaults defaults = new();
            if (defaultFileExists != true) {
                //Set form element defaults
                defaults.Module = "1000";
                defaults.DbName = "MyProject_db";
                defaults.IP = "172.30.110.41";
                defaults.Tag = "gMod2010_uaEventDetails";
                defaults.Path = "C:\\user\\%USERPROFILE%\\documents";

                tbModule.Text = defaults.Module;
                tbDBName.Text = defaults.DbName;
                tbIPAddress.Text = defaults.IP;
                tbBaseTag.Text = defaults.Tag;
                openFileDialog.InitialDirectory = defaults.Path;

                //Format output
                string jsonOut = JsonConvert.SerializeObject(defaults, Formatting.Indented);
                //Write output file
                File.WriteAllText(appLocation + "EventMsgDefaults.json", jsonOut);

                //Print location of JSON file
                EventMessage($"File {"EventMsgDefaults.json"} exported to {appLocation}");
            } else {
                var jsonIn = File.ReadAllText(appFileLoc);
                var defaultsIn = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonIn);
                if (defaultsIn.ContainsKey("Module")) {
                    defaultsIn.TryGetValue("Module", out string value);
                    tbModule.Text = value;
                }
                if (defaultsIn.ContainsKey("DbName")) {
                    defaultsIn.TryGetValue("DbName", out string value);
                    tbDBName.Text = value;
                }
                if (defaultsIn.ContainsKey("IP")) {
                    defaultsIn.TryGetValue("IP", out string value);
                    tbIPAddress.Text = value;
                }
                if (defaultsIn.ContainsKey("Tag")) {
                    defaultsIn.TryGetValue("Tag", out string value);
                    tbBaseTag.Text = value;
                }
                if (defaultsIn.ContainsKey("Path")) {
                    defaultsIn.TryGetValue("Path", out string value);
                    openFileDialog.InitialDirectory = value;
                }
            }
        }

        public FormMain() {
            InitializeComponent();

        }

        public void ProgressUpdate(int val) {
            progressBar.BeginInvoke(new Action(() => progressBar.Value = val));
        }

        public void EventMessage(string msg) {
            tbEvents.BeginInvoke(new Action(() => tbEvents.AppendText($"{msg}\r\n")));
        }


        public void OutputMessage(string msg) {
            tbOutput.BeginInvoke(new Action(() => tbOutput.AppendText($"{msg}\r\n")));
        }

        public void LockUnlockCtrls(bool lockCtrls) {

            if (lockCtrls == true) {
                //Disable button controls
                btnImportTags.BeginInvoke(new Action(() => btnImportTags.Enabled = false));
                btnExportMsg.BeginInvoke(new Action(() => btnExportMsg.Enabled = false));
                btnDBExport.BeginInvoke(new Action(() => btnDBExport.Enabled = false));
                return;
            }

            if (lockCtrls == false) {
                //Disable button controls
                btnImportTags.BeginInvoke(new Action(() => btnImportTags.Enabled = true));
                btnExportMsg.BeginInvoke(new Action(() => btnExportMsg.Enabled = true));
                btnDBExport.BeginInvoke(new Action(() => btnDBExport.Enabled = true));
                return;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e) {
            SetAppDefaults();
            //AllocConsole(); //Display console for debug
            //Initialize defaults on form object load
            //tbDBName.Text = "MyProject_db";
            //tbModule.Text = "1000";
            //tbBaseTag.Text = "gMod2010_uaEventDetails";//Base tag
            //tbIPAddress.Text = "172.30.110.41";//Default IP Address
            //openFileDialog.InitialDirectory = "C:\\user\\%USERPROFILE%\\documents";//Default path
        }

        private void btnFileSel_Click(object sender, EventArgs e) {
            //Clear dialog text if content remains and not equal to default path
            if (openFileDialog.FileName != "" & openFileDialog.FileName != openFileDialog.InitialDirectory) {
                openFileDialog.FileName = "";
            }

            //Display file dialog 
            openFileDialog.ShowDialog();
            tbFileSelect.Text = openFileDialog.FileName;
            Thread.Sleep(250);

            if (openFileDialog.FileName != "") {
                //Acquire path from file dialog
                //Apply environment variables to path to display current user's directory
                filePath = openFileDialog.FileName;
                filePath = Environment.ExpandEnvironmentVariables(filePath);
            } else {
                EventMessage("Message: Invalid File\r\n");
            }
        }

        private void btnImportTags_Click(object sender, EventArgs e) {
            //Prompt user and confirm import
            var promptResult = MessageBox.Show("Worksheet \"Fault Messages\" tab will be overwritten with PLC message contents.", "Proceed with upload?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (promptResult == DialogResult.Yes) {
                //Start read thread
                PLCComms plc = new(this);
                var filePath = openFileDialog.FileName;
                var threadImport = new Thread(() => tasks.ImportMessages(filePath, this));
                threadImport.Start();
            } else {
                EventMessage("Message import aborted by user.");
            }
        }

        private void btnExportMsg_Click(object sender, EventArgs e) {
            //Prompt user and confirm export
            var promptResult = MessageBox.Show("PLC messages will be overwritten with Worksheet contents. ", "Proceed with download?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (promptResult == DialogResult.Yes) {
                //Create message export thread with anonymous function to pass arguments
                var threadExportMsgs = new Thread(() => tasks.ExportMessages(filePath, this));
                //Start write thread
                if (threadExportMsgs.ThreadState != ThreadState.Running) {
                    threadExportMsgs.Start();
                }
            } else {
                EventMessage("Message export aborted by user.");
            }
        }

        private void tbIPAddress_TextChanged(object sender, EventArgs e) {
            string ipAddress = tbIPAddress.Text;
            if (utils.IPValidate(ref ipAddress) != true) {
                tbIPAddress.ForeColor = Color.Red; //If IP is invalid set text to red
            } else {
                tbIPAddress.ForeColor = Color.Black; //If IP is valid return to black
            }
        }

        private void btnPing_Click(object sender, EventArgs e) {
            Ping plcPing = new();
            var plcResponse = plcPing.Send(tbIPAddress.Text, 1000);

            if (plcResponse.Status == IPStatus.Success) {
                EventMessage($"Message: PLC Ping Succeeded IP Address: {plcResponse.Address} Response Time(ms) {plcResponse.RoundtripTime}\n");
            } else {
                EventMessage($"Message: PLC Ping Failed IP Address: {plcResponse.Address}\n");
            }
        }

        private void btnDBExport_Click(object sender, EventArgs e) {
            string filePath = openFileDialog.FileName;
            string module = tbModule.Text;
            string dbName = tbDBName.Text;
            var threadExportDb = new Thread(() => tasks.ExportDatabase(filePath, dbName, module, this));
            if (threadExportDb.ThreadState != ThreadState.Running) {
                threadExportDb.Start();
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e) {

        }
    }
}
