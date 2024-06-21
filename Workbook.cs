using System.Diagnostics;

using ClosedXML.Excel;

//*************************************************************************************************
//** Developer: Kameron Zulfic
//** Date:  03 / 03 / 2024
//** Function: Workbook Management Class
//*************************************************************************************************
namespace EventMessageTool {

    public class Workbook {

        public Workbook() {
        }

        public class EventObj : IComparable<EventObj> {
            public int ID { get; set; }
            public int Category { get; set; }
            public string Message1 { get; set; }
            public string Message2 { get; set; }


            public int CompareTo(EventObj eventCompare) {
                if (eventCompare == null) {
                    return 1;
                } else {
                    return this.ID.CompareTo(eventCompare.ID);
                }
            }
        }

        Dictionary<string, int> AlarmCategory = new() {
            {"Safety", 0 },
            {"All Immediate Stop", 1 },
            {"Station Immediate Stop", 2 },
            {"Cycle Stop", 3 },
            {"Spare4", 4},
            {"Special State", 5 },
            {"Warning", 6 },
            {"Message", 7 }
        };

        /// <summary>
        /// Read workbook contents
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="_form"></param>
        /// <returns></returns>
        public List<EventObj> WrkBkRead(string filePath, FormMain _form) {
            var time = Stopwatch.StartNew();
            FileStream fileStream;
            string fileName = filePath; //File path
            string sheetName = "Fault Messages"; //Workbook sheet name
            List<EventObj> Events = new(); //Alarm list object

            if (filePath == null || filePath == "") {
                _form.EventMessage("ReadWrkBk: Invalid file");
                return null;
            }

            //Validate file type .xlsx .xlsm
            var fileExt = filePath.Split(".");
            if (fileExt[1] != "xlsx" && fileExt[1] != "xlsm") {
                _form.EventMessage("Invalid File Extension");
                return null;
            }

            //Attempt to create file stream catch exception
            try {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            } catch (Exception err) {
                _form.EventMessage($"Failed to open file. Exception \"{err.Message}\"");
                return null;
            }

            //Open workbook from file stream
            var wrkBk = new XLWorkbook(fileStream);
            //Specify sheet name
            wrkBk.Worksheet(sheetName);
            //Get row count for iteration
            int rowLen = wrkBk.Worksheet(sheetName).RowsUsed().Count();
            //Extract row strip rows 1-2
            var rows = wrkBk.Worksheet(sheetName).Rows(3, rowLen + 1);
            foreach (var row in rows) {
                //Create event object to append to event list
                var evnt = new EventObj();

                //Read in cell values
                var cellID = row.Cell(1).GetString(); //Event ID
                var cellStn = row.Cell(2).GetString(); //Station
                var cellLang1 = row.Cell(3).GetString(); //Language 1 Message
                var cellLang2 = row.Cell(4).GetString(); //Language 2 Message
                var cellCat = row.Cell(7).GetString(); //Alarm category

                //Event ID
                if (cellID != "") {
                    evnt.ID = Convert.ToInt32(cellID);
                }
                //Lookup alarm category value
                if (cellCat != "") {
                    AlarmCategory.TryGetValue(cellCat, out int category);
                    evnt.Category = category;
                }
                //Concatenate ID station and language 1 message
                if (cellLang1 != "") {
                    evnt.Message1 = cellID + " - " + cellStn + ": " + cellLang1;
                }else if (evnt.Category == 6 || evnt.Category == 7){
                    evnt.Message1 = cellID + " - " + cellStn + ": " + "Unknown Info Message";
                }else{
                    evnt.Message1 = cellID + " - " + cellStn + ": " + "Unknown Fault Message";
                }
                //Concatenate ID station and language 2 message
                if (cellLang2 != "") {
                    evnt.Message2 = cellID + " - " + cellStn + ": " + cellLang2;
                } else if (evnt.Category == 6 || evnt.Category == 7) {
                    evnt.Message2 = cellID + " - " + cellStn + ": " + "Unknown Info Message";
                } else {
                    evnt.Message2 = cellID + " - " + cellStn + ": " + "Unknown Fault Message";
                }
                //Append event object to list
                Events.Add(evnt);
            }

            time.Stop();
            double elapsedMs = time.ElapsedMilliseconds;
            _form.EventMessage($"File read complete");
            _form.EventMessage($"Workbook Read time (ms) {elapsedMs}");
            //Clean up and dispose file stream
            fileStream.Close();
            fileStream.Dispose();
            return Events;
        }

        /// <summary>
        /// Write Workbook
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="events"></param>
        /// <param name="_form"></param>
        public void WrkBkWrite(string filePath, List<Workbook.EventObj> events, FormMain _form) {
            var time = Stopwatch.StartNew();
            FileStream fileStream;
            string fileName = filePath; //File path
            string sheetName = "Fault Messages"; //Workbook sheet name
            List<EventObj> Events = new(); //Alarm list object

            if (filePath == null || filePath == "") {
                _form.EventMessage("ReadWrkBk: Invalid file");
                return;
            }

            //Validate file type .xlsx .xlsm
            var fileExt = Path.GetExtension(filePath);
            if (fileExt != "xlsx" && fileExt != "xlsm") {
                _form.EventMessage("Invalid File Extension");
                return;
            }

            //Attempt to create file stream catch exception
            try {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            } catch (Exception err) {
                _form.EventMessage($"Failed to open file. Exception \"{err.Message}\"");
                return;
            }

            //Open workbook from file stream
            var wrkBk = new XLWorkbook(fileStream);
            //Specify sheet name
            var wrkSheet = wrkBk.Worksheet(sheetName);

            var style = wrkSheet.Column(7).Style;

            for (int i = 0; i < events.Count; i++) {
                //Parse message 1
                var msg1SplitDash = events[i].Message1.Split("-", 2);
                var msg1SplitColon = msg1SplitDash[1].Split(":", 2);
                var msg1station = msg1SplitColon[0].TrimStart();
                var message1 = msg1SplitColon[1].TrimStart();

                //Parse message 1
                var msg2SplitDash = events[i].Message2.Split("-", 2);
                var msg2SplitColon = msg2SplitDash[1].Split(":", 2);
                var station = msg2SplitColon[0].TrimStart();
                var message2 = msg2SplitColon[1].TrimStart();

                wrkSheet.Cell(i + 3, 1).Value = events[i].ID;
                wrkSheet.Cell(i + 3, 2).Value = station;
                wrkSheet.Cell(i + 3, 3).Value = message1;
                wrkSheet.Cell(i + 3, 4).Value = message2;
                foreach (KeyValuePair<string, int> category in AlarmCategory) {
                    if (events[i].Category == category.Value) {
                        wrkSheet.Cell(i + 3, 7).Value = category.Key;
                        break;
                    }
                }
            }

            //Align columns
            wrkSheet.Column(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            wrkSheet.Column(2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            wrkSheet.Column(3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            wrkSheet.Column(4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            wrkSheet.Column(7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            //Save clean up and dispose file
            wrkBk.Save();
            fileStream.Close();
            fileStream.Dispose();

            //Capture write time write to event messages
            time.Stop();
            double elapsedMs = time.ElapsedMilliseconds;
            _form.EventMessage($"File Write complete");
            _form.EventMessage($"Workbook Write time (ms) {elapsedMs}");
        }
    }
}
