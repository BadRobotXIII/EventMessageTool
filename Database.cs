using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//INSERT INTO BBS_db.m1000_detail_state_description (`DetailState`,`Description`) VALUES (0,'None');
//INSERT INTO BBS_db.mMMMM_detail_state_description (`DetailState`,`Description`) VALUES (XXX,'ddd ddd');
//MMMM = module number
//XXX = Message ID
//ddd ddd = Description

namespace EventMessageTool {
    internal class Database {
        public Database() {
        }
        public void DatabaseExport(string path, string dataBase, string module, List<Workbook.EventObj> events, FormMain form){
            using (StreamWriter outFile = new StreamWriter(Path.Combine(path, dataBase + ".sql")))
                for(int i = 0; i < events.Count; i++){
                    if (i == 0) {
                        string lineDetailStateZero = "INSERT INTO " + dataBase + "." + "m" + module + "_detail_state_description (`DetailState`,`Description`) VALUES (" + "0" + ",'" + "" + "')";
                        string lineDetailStateZeroOnDup = "ON DUPLICATE KEY UPDATE `Description`= '" + "" + "';";
                        outFile.WriteLine(lineDetailStateZero);
                        outFile.WriteLine(lineDetailStateZeroOnDup);
                    }
                    string lineInsert = "INSERT INTO " + dataBase + "." + "m" + module + "_detail_state_description (`DetailState`,`Description`) VALUES (" + events[i].ID + ",'" + events[i].Message1 + "')";
                    string lineOnDup = "ON DUPLICATE KEY UPDATE `Description`= '" + events[i].Message1 + "';";
                    outFile.WriteLine(lineInsert);
                    outFile.WriteLine(lineOnDup);
                }
            form.EventMessage("Database file export complete");
            form.EventMessage($"File {dataBase + ".sql"} exported to {path}");
        }
    }
}
