using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

using Newtonsoft.Json;

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

        [JsonObject(Description = "Application default values", Id = "App Defaults", Title = "App Defaults")]
        public class AppDefaults {
            public string Module { get; set; }
            public string DbName { get; set; }
            public string IP { get; set; }
            public string Tag { get; set; }
            public string Path { get; set; }

        }

        public struct DEFAULTS {
            public const string MODULE = "Module";
            public const string DBNAME = "DbName";
            public const string IP = "IP";
            public const string TAG = "Tag";
            public const string PATH = "Path";
        }

        private void SetAppDefaults() {
            string appLocation = AppContext.BaseDirectory;
            string appFileLoc = appLocation + "EventMsgDefaults.json";
            bool defaultFileExists = File.Exists(appFileLoc);
            Dictionary<string, string> defaultsIn = new();
            AppDefaults appDefaults = new();

            if (defaultFileExists != true) {
                //Set default values
                appDefaults.Module = "1000";
                appDefaults.DbName = "MyProject_db";
                appDefaults.IP = "192.168.1.100";
                appDefaults.Tag = "gMod1000_uaEventDetails";
                appDefaults.Path = "C:\\user\\%USERPROFILE%\\documents";

                //Set UI elements to defaults
                tbModule.Text = appDefaults.Module;
                tbDBName.Text = appDefaults.DbName;
                tbIPAddress.Text = appDefaults.IP;
                tbBaseTag.Text = appDefaults.Tag;
                openFileDialog.InitialDirectory = appDefaults.Path;

                //Format output
                string jsonOut = JsonConvert.SerializeObject(appDefaults, Formatting.Indented);
                //Write output file
                File.WriteAllText(appLocation + "EventMsgDefaults.json", jsonOut);

                //Print location of JSON file
                EventMessage($"File {"EventMsgDefaults.json"} exported to {appLocation}");
            } else {
                //Read in JSON file contents
                var jsonIn = File.ReadAllText(appFileLoc);
                //Deserialize JSON file
                try {
                    defaultsIn = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonIn);
                } catch (Exception err) {
                    EventMessage($"SetAppDefaults Error: Failed to deserialize JSON file. Deserialization returned status \"{err.Message}\"");
                }
                if (defaultsIn.ContainsKey(DEFAULTS.MODULE)) {
                    defaultsIn.TryGetValue(DEFAULTS.MODULE, out string value);
                    tbModule.Text = value;
                }else{
                    tbModule.Text = "1000";
                }
                if (defaultsIn.ContainsKey(DEFAULTS.DBNAME)) {
                    defaultsIn.TryGetValue(DEFAULTS.DBNAME, out string value);
                    tbDBName.Text = value;
                }else{
                    tbDBName.Text = "MyProject_db";
                }
                if (defaultsIn.ContainsKey(DEFAULTS.IP)) {
                    defaultsIn.TryGetValue(DEFAULTS.IP, out string value);
                    tbIPAddress.Text = value;
                }else{
                    tbIPAddress.Text = "192.168.1.100";
                }
                if (defaultsIn.ContainsKey(DEFAULTS.TAG)) {
                    defaultsIn.TryGetValue(DEFAULTS.TAG, out string value);
                    tbBaseTag.Text = value;
                }else{
                    tbBaseTag.Text = "gMod1000_uaEventDetails";
                }
                if (defaultsIn.ContainsKey(DEFAULTS.PATH)) {
                    defaultsIn.TryGetValue(DEFAULTS.PATH, out string value);
                    openFileDialog.InitialDirectory = value;
                }else{
                    openFileDialog.InitialDirectory = "C:\\user\\%USERPROFILE%\\documents";
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
            //AllocConsole(); //Display console for debug
            //Initialize defaults on form object load
            SetAppDefaults();
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
