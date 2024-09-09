
//*************************************************************************************************
//** Developer: Kameron Zulfic
//** Date:  03 / 03 / 2024
//** Function: Generic Utilities Class
//*************************************************************************************************
namespace EventMessageTool {
    public class Utils {
        public Utils() {
        }

        /// <summary>
        /// Delay timer
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Task Delay(double milliseconds) {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) => {
                tcs.TrySetResult(true);
            };
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }

        /// <summary>
        /// Validate IP address
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IPValidate(ref string ip) {
            var ipOctets = ip.Split(".");
            foreach (string octet in ipOctets) {
                if (octet != null & octet != "" & ipOctets.Length == 4) {
                    try {
                        int intOctet = int.Parse(octet);
                        if (intOctet < 0 || intOctet > 255) {
                            ip = null; return false;
                        }
                    } catch {
                        return false;
                    }
                } else {
                    return false;
                }
            }
            return true;
        }
    }
}
