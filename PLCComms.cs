using System.Diagnostics;

using libplctag;
using libplctag.DataTypes;

//*************************************************************************************************
//** Developer: Kameron Zulfic
//** Date:  03 / 03 / 2024
//** Function: PLC Connection Class
//*************************************************************************************************
namespace EventMessageTool {

    public class PLCComms {
        private readonly FormMain _form;
        private readonly Utils _utils = new();
        private const int _TIMEOUT = 1000;

        public PLCComms(FormMain form) {
            _form = form;

        }

        public int TagSize(string ip, string path, string baseTag) {
            var tags = new Tag<TagInfoPlcMapper, TagInfo[]>() {
                Gateway = ip,
                Path = path,
                PlcType = PlcType.ControlLogix,
                Protocol = Protocol.ab_eip,
                Name = "@tags",
            };

            //Read tags
            tags.Read();
            int tagSize;
            //Iterate through list to find desired tag
            foreach (var tag in tags.Value) {
                if (tag.Name == baseTag) {
                    tagSize = Convert.ToInt32(tag.Dimensions[0]);
                    return tagSize;
                }
            }
            return 0;
        }

        /// <summary>
        /// Import PLC Tags
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="path"></param>
        /// <param name="baseTag"></param>
        /// <returns></returns>
        public List<Workbook.EventObj> MsgImport(string ip, string path, string baseTag, string filePath) {
            int index = 0;
            int tagLen = 0;
            double elapsedTime;
            Stopwatch timer = new();
            bool ipOk = _utils.IPValidate(ref ip);
            List<Workbook.EventObj> eventsPLC = new();

            if (ipOk == true && baseTag != null && baseTag != "") {
                //Set path to default if slot equals 0 otherwise assign slot position
                path = (path == "0") ? "1,0" : path += ",0";
                //Start write timer
                timer.Start();

                //Create generic tag handle to check base tag status
                var tag = new Tag<DintPlcMapper, int>() {
                    Name = baseTag,
                    Gateway = ip,
                    Path = path,
                    Protocol = Protocol.ab_eip,
                    PlcType = PlcType.ControlLogix,
                    Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                };

                //Initialize base tag
                try {
                    tag.Initialize();
                } catch (Exception err) {
                    _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tag.Name}\" returned status \"{err.Message}\"");
                    return null;
                }

                //Get tag array size
                tagLen = TagSize(ip, path, baseTag);

                //Iterate through events list create tag handles and write to PLC tags
                //Parallel processing loop
                Parallel.For(1, tagLen, new ParallelOptions(), (i, state) => {
                    //for (int i = 0; i < tagDims; i++) {
                    //Create alarm object instance
                    //C# Sharp strings are immutable thus requiring the object to be instantiated each iteration of the loop
                    Workbook.EventObj eventObj = new();
                    //Create PLC tag instances
                    //Event .ID tag handle (DINT)
                    var tagID = new Tag<DintPlcMapper, int>() {
                        Name = baseTag + $"[{i}]" + ".ID",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Category tag handle (DINT)
                    var tagCategory = new Tag<DintPlcMapper, int>() {
                        Name = baseTag + $"[{i}]" + ".Category",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Message[0] tag handle (STRING)
                    var tagMessage1 = new Tag<StringPlcMapper, string>() {
                        Name = baseTag + $"[{i}]" + ".Message[0]",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Message[1] tag handle (STRING)
                    var tagMessage2 = new Tag<StringPlcMapper, string>() {
                        Name = baseTag + $"[{i}]" + ".Message[1]",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Initialize tags
                    try { //Initialize ID tag catch exception if thrown
                        tagID.Initialize();
                        try { //Initialize category tag catch exception if thrown
                            tagCategory.Initialize();
                            try { //Initialize message 1 tag catch exception if thrown
                                tagMessage1.Initialize();
                                try { //Initialize message 2 tag catch exception if thrown
                                    tagMessage2.Initialize();
                                } catch (Exception err) {
                                    _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagMessage2.Name}\" returned status \"{err.Message}\"");
                                    return;
                                }
                            } catch (Exception err) {
                                _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagMessage1.Name}\" returned status \"{err.Message}\"");
                                return;
                            }
                        } catch (Exception err) {
                            _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagCategory.Name}\" returned status \"{err.Message}\"");
                            return;
                        }
                    } catch (Exception err) {
                        _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagID.Name}\" returned status \"{err.Message}\"");
                        state.Stop();
                        return;
                    }

                    //Read values to PLC tags
                    try { //Read ID tag catch exception if thrown
                        tagID.Read();
                        try { //Read category tag catch exception if thrown
                            tagCategory.Read();
                            try { //Read tag message 1 catch exception if thrown
                                tagMessage1.Read();
                                try { //Read tag message 2 catch exception if thrown
                                    tagMessage2.Read();
                                } catch (Exception err) {
                                    _form.EventMessage($"MsgExport Error: Failed to Read tag. Tag \"{tagMessage2.Name}\" returned status \"{err.Message}\"");
                                    return;
                                }
                            } catch (Exception err) {
                                _form.EventMessage($"MsgExport Error: Failed to Read tag. Tag \"{tagMessage1.Name}\" returned status \"{err.Message}\"");
                                return;
                            }
                        } catch (Exception err) {
                            _form.EventMessage($"MsgExport Error: Failed to Read tag. Tag \"{tagCategory.Name}\" returned status \"{err.Message}\"");
                            return;
                        }
                    } catch (Exception err) {
                        _form.EventMessage($"MsgExport Error: Failed to Read tag. Tag \"{tagID.Name}\" returned status \"{err.Message}\"");
                        return;
                    }

                    ////Set tag values
                    eventObj.ID = tagID.Value; eventObj.Category = tagCategory.Value; ; eventObj.Message1 = tagMessage1.Value; eventObj.Message2 = tagMessage2.Value;

                    index++;
                    //Calculate progress bar values
                    int progValue = Convert.ToInt32(Convert.ToDouble(index) * ((Convert.ToDouble(_form.progressBar.Maximum) - Convert.ToDouble(_form.progressBar.Minimum)) / tagLen));
                    _form.ProgressUpdate(progValue);
                    _form.OutputMessage($"{eventObj.Message1}");
                    if (eventObj.ID != 0 & eventObj.Message1 != "") {
                        eventsPLC.Add(eventObj);
                    }else{
                        eventObj.ID = i; eventObj.Category = 0;
                        eventObj.Message1 = i + " - " + 0 + ": " + "Unknown Event Message";
                        eventObj.Message2 = i + " - " + 0 + ": " + "Unknown Event Message";
                        eventsPLC.Add(eventObj);
                    }
                });

                //Sort events acquired from PLC
                eventsPLC.Sort();

                //Total read time
                elapsedTime = timer.ElapsedMilliseconds;
                _form.EventMessage($"Read execution time(ms) {elapsedTime}");

                //Stop write timer
                timer.Stop();

            } else {
                if (tagLen == 0) { //Error event empty list. File was not imported
                    _form.EventMessage($"MsgImport Error: Cannot Export Empty List");
                } else if (ipOk == false) { //Error event invalid IP address.
                    _form.EventMessage($"MsgExport Error: Invalid IP Address");
                } else if (baseTag == "") { //Base tag was empty
                    _form.EventMessage($"MsgExport Error: Base Tag Cannot be Empty");
                }
            }
            return eventsPLC;

        }

        /// <summary>
        /// Export Event Messages 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="path"></param>
        /// <param name="baseTag"></param>
        /// <param name="events"></param>
        public void MsgExport(string ip, string path, string baseTag, List<Workbook.EventObj> events) {
            int index = 0;
            double elapsedTime;
            Stopwatch timer = new();
            bool ipOk = _utils.IPValidate(ref ip);//Validate IP address

            if (events != null && events.Count != 0 && baseTag != "" && ipOk) {
                int listLen = events.Count() + 1;
                //Set path to default if slot equals 0 otherwise assign slot position
                path = (path == "0") ? "1,0" : path += ",0";
                //Start write timer
                timer.Start();

                //Create generic tag handle to check base tag status
                var tag = new Tag<DintPlcMapper, int>() {
                    Name = baseTag,
                    Gateway = ip,
                    Path = path,
                    Protocol = Protocol.ab_eip,
                    PlcType = PlcType.ControlLogix,
                    Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                };

                //Initialize base tag
                try {
                    tag.Initialize();
                } catch (Exception err) {
                    _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tag.Name}\" returned status \"{err.Message}\"");
                    return;
                }

                //Iterate through events list create tag handles and write to PLC tags
                //Parallel processing loop
                Parallel.For(1, listLen, new ParallelOptions(), (i, state) => {
                    //Create alarm object instance
                    //C# Sharp strings are immutable thus requiring the object to be instantiated each iteration of the loop
                    Workbook.EventObj eventObj = events[i - 1];
                    //Create PLC tag instances
                    //Event .ID tag handle (DINT)
                    var tagID = new Tag<DintPlcMapper, int>() {
                        Name = baseTag + $"[{i}]" + ".ID",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Category tag handle (DINT)
                    var tagCategory = new Tag<DintPlcMapper, int>() {
                        Name = baseTag + $"[{i}]" + ".Category",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Message[0] tag handle (STRING)
                    var tagMessage1 = new Tag<StringPlcMapper, string>() {
                        Name = baseTag + $"[{i}]" + ".Message[0]",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Event .Message[1] tag handle (STRING)
                    var tagMessage2 = new Tag<StringPlcMapper, string>() {
                        Name = baseTag + $"[{i}]" + ".Message[1]",
                        Gateway = ip,
                        Path = path,
                        Protocol = Protocol.ab_eip,
                        PlcType = PlcType.ControlLogix,
                        Timeout = TimeSpan.FromMilliseconds(_TIMEOUT)
                    };
                    //Initialize tags
                    try { //Initialize ID tag catch exception if thrown
                        tagID.Initialize();
                        try { //Initialize category tag catch exception if thrown
                            tagCategory.Initialize();
                            try { //Initialize message 1 tag catch exception if thrown
                                tagMessage1.Initialize();
                                try { //Initialize message 2 tag catch exception if thrown
                                    tagMessage2.Initialize();
                                } catch (Exception err) {
                                    _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagMessage2.Name}\" returned status \"{err.Message}\"");
                                    return;
                                }
                            } catch (Exception err) {
                                _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagMessage1.Name}\" returned status \"{err.Message}\"");
                                return;
                            }
                        } catch (Exception err) {
                            _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagCategory.Name}\" returned status \"{err.Message}\"");
                            return;
                        }
                    } catch (Exception err) {
                        _form.EventMessage($"MsgExport Error: Failed to create tag handle. Tag \"{tagID.Name}\" returned status \"{err.Message}\"");
                        //state.Stop();
                        return;
                    }

                    ////Set tag values
                    tagID.Value = eventObj.ID; tagCategory.Value = eventObj.Category; tagMessage1.Value = eventObj.Message1; tagMessage2.Value = eventObj.Message2;

                    //Write values to PLC tags
                    try { //Write ID tag catch exception if thrown
                        tagID.Write();
                        try { //Write category tag catch exception if thrown
                            tagCategory.Write();
                            try { //Write tag message 1 catch exception if thrown
                                tagMessage1.Write();
                                try { //Write tag message 2 catch exception if thrown
                                    tagMessage2.Write();
                                } catch (Exception err) {
                                    _form.EventMessage($"MsgExport Error: Failed to write tag. Tag \"{tagMessage2.Name}\" returned status \"{err.Message}\"");
                                    return;
                                }
                            } catch (Exception err) {
                                _form.EventMessage($"MsgExport Error: Failed to write tag. Tag \"{tagMessage1.Name}\" returned status \"{err.Message}\"");
                                return;
                            }
                        } catch (Exception err) {
                            _form.EventMessage($"MsgExport Error: Failed to write tag. Tag \"{tagCategory.Name}\" returned status \"{err.Message}\"");
                            return;
                        }
                    } catch (Exception err) {
                        _form.EventMessage($"MsgExport Error: Failed to write tag. Tag \"{tagID.Name}\" returned status \"{err.Message}\"");
                        return;
                    }
                    index++;
                    //Calculate progress bar values
                    int progValue = Convert.ToInt32(Convert.ToDouble(index) * ((Convert.ToDouble(_form.progressBar.Maximum) - Convert.ToDouble(_form.progressBar.Minimum)) / listLen));
                    _form.ProgressUpdate(progValue);
                    _form.OutputMessage($"{eventObj.Message1}");
                });

                //Total write time
                elapsedTime = timer.ElapsedMilliseconds;
                _form.EventMessage($"Write execution time(ms) {elapsedTime}");

                //Stop write timer
                timer.Stop();

            } else {
                if (events == null || events.Count == 0) { //Error event empty list. File was not imported
                    _form.EventMessage($"MsgExport Error: Cannot Export Empty List");
                } else if (ipOk == false) { //Error event invalid IP address.
                    _form.EventMessage($"MsgExport Error: Invalid IP Address");
                } else if (baseTag == "") { //Base tag was empty
                    _form.EventMessage($"MsgExport Error: Base Tag Cannot be Empty");
                }
            }
        }
    }
}