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
            //Initialize defaults on form object load
            //AllocConsole(); //Display console for debug
            tbBaseTag.Text = "gMod2010_uaEventDetails";//Base tag
            tbIPAddress.Text = "172.30.110.41";//Default IP Address
            openFileDialog.InitialDirectory = "C:\\user\\%USERPROFILE%\\documents";//Default path
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
                tbEvents.AppendText("Message: Invalid File\r\n");
            }
        }

        private void btnImportTags_Click(object sender, EventArgs e) {
            //Start read thread
            PLCComms plc = new(this);
            var filePath = openFileDialog.FileName;
            var threadImport = new Thread(() => tasks.ImportMessages(filePath, this));
            threadImport.Start();

        }

        private void btnExportMsg_Click(object sender, EventArgs e) {

            //Create message export thread with anonymous function to pass arguments
            var threadExportMsgs = new Thread(() => tasks.ExportMessages(filePath, this));
            //Start write thread
            if (threadExportMsgs.ThreadState != ThreadState.Running) {
                threadExportMsgs.Start();
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
            if(threadExportDb.ThreadState != ThreadState.Running){
                threadExportDb.Start();
            }
        }
    }
}
