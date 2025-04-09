using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace TimeLimiter
{
    public static class TimeSaveFiller
    {
        public const int ENCRYPT_LENGTH = 4;
        const int OS_ANYSERVER = 29;
        public static bool IsServerOS
        {
            get => IsOS(OS_ANYSERVER);
        }

        [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
        private static extern bool IsOS(int os);
        public static void SaveFile(TimerSaver saver)
        {
            string npath = IsServerOS ? Application.LocalUserAppDataPath : Application.CommonAppDataPath;
            string path = npath + "/timer.set";
            if (!File.Exists(path))
            {
                FileStream stream = File.Create(path);
                stream.Close();
            }
            string val = saver.timeLeft + " " + saver.workTime + " " + saver.restTime + " " + (saver.hasTimeLeft ? "1" : "0") + " " + saver.restTimeDate + " " + saver.lastLeavedDate + " " + (saver.runInBackground ? "1" : "0");
            val = HCrypter.Encrypt(val, ENCRYPT_LENGTH);
            File.WriteAllText(path, val);
        }
        public static TimerSaver LoadFile()
        {
            string npath = IsServerOS ? Application.LocalUserAppDataPath : Application.CommonAppDataPath;
            string path = npath + "/timer.set";
            if (File.Exists(path))
            {
                string val = File.ReadAllText(path);
                val = HCrypter.Decrypt(val, ENCRYPT_LENGTH, false);
                string[] args = val.Split(' ');
                TimerSaver s = new TimerSaver()
                {
                    timeLeft = double.Parse(args[0]),
                    workTime = double.Parse(args[1]),
                    restTime = double.Parse(args[2]),
                    hasTimeLeft = args[3] == "1",
                    restTimeDate = double.Parse(args[4]),
                    lastLeavedDate = double.Parse(args[5]),
                    runInBackground = args[6] == "1"
                };
                return s;
            }
            else
            {
                Debug.WriteLine("The file is not existed!");
                return null;
            }
        }
    }
}
