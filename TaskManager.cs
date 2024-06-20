
//*************************************************************************************************
//** Developer: Kameron Zulfic
//** Date:  03 / 03 / 2024
//** Function: Task Manager
//*************************************************************************************************
namespace EventMessageTool {
    internal class TaskManager {

        private readonly Workbook wrkBk = new();
        private readonly Database database = new();
        private List<Workbook.EventObj> eventMsgs = new();
        private List<Workbook.EventObj> eventsPLC = new();
        private List<Workbook.EventObj> eventsDatabase = new();

        public void ExportMessages(string filePath, FormMain form) {
            //Lock form controls
            form.LockUnlockCtrls(true);

            //Create PLC instance
            PLCComms plc = new(form);

            //Open workbook read event messages and store into list object
            eventMsgs = wrkBk.WrkBkRead(filePath, form);

            //Export workbook messages to PLC
            plc.MsgExport(form.tbIPAddress.Text, Convert.ToString(form.upDnSlot.Value), form.tbBaseTag.Text, eventMsgs);

            //Unlock form controls
            form.LockUnlockCtrls(false);
        }

        public void ImportMessages(string filePath, FormMain form) {
            //Lock form controls
            form.LockUnlockCtrls(true);

            if (filePath != null && filePath != "") {
                //Create PLC instance
                PLCComms plc = new(form);

                //Import messages form PLC
                eventsPLC = plc.MsgImport(form.tbIPAddress.Text, Convert.ToString(form.upDnSlot.Value), form.tbBaseTag.Text, filePath);

                //Write messages to workbook
                if (eventsPLC != null) {
                    wrkBk.WrkBkWrite(filePath, eventsPLC, form);
                }
            } else {
                form.EventMessage("ImportMessages: Invalid File");
            }

            //Unlock form controls
            form.LockUnlockCtrls(false);
        }

        public void ExportDatabase(string filePath, string dbName, string module, FormMain form) {
            string path = Path.GetDirectoryName(filePath);
            //Lock form controls
            form.LockUnlockCtrls(true);

            if (filePath != null && filePath != "") {
                //Read event from workbook
                eventsDatabase = wrkBk.WrkBkRead(filePath, form);

                if (dbName != null && dbName != "" && module != null && module != "") {
                    //Export messages to database file
                    database.DatabaseExport(path, dbName, module, eventsDatabase, form);
                } else {
                    form.EventMessage("ExportDatabase: Database and/or module cannot be empty");
                }
            } else {
                form.EventMessage("ExportDatabase: Invalid File");
            }

            //Unlock form controls
            form.LockUnlockCtrls(false);
        }
    }
}
