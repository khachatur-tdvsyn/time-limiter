using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace TimeLimiter
{
    public static class TimeSaveFiller
    {
        public const int ENCRYPTION_TACTS_COUNT = 2;
        const int OS_ANYSERVER = 29;
        private const string FILE_NAME = "timer.set";

        public static bool IsServerOS
        {
            get => IsOS(OS_ANYSERVER);
        }

        [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
        private static extern bool IsOS(int os);
        public static void SaveFile(TimerSaver saver)
        {
            string npath = IsServerOS ? Application.LocalUserAppDataPath : Application.CommonAppDataPath;
            string path = npath + "\\" + FILE_NAME;
            if (!File.Exists(path))
            {
                FileStream stream = File.Create(path);
                stream.Close();
            }
            string val = SerializeTimeSaver(saver);
            val = HCrypter.Encrypt(val, ENCRYPTION_TACTS_COUNT);
            File.WriteAllText(path, val);
        }

        static string SerializeTimeSaver(TimerSaver saver)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream s = new MemoryStream();
            formatter.Serialize(s, saver);

            s.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(s, Encoding.Unicode);
            string value = reader.ReadToEnd();
            return value;
        }

        static string SerializeTimeSaver_Old(TimerSaver saver)
        {
            string val = saver.timeLeft + " " + saver.workTime + " " + saver.restTime + " " + (saver.hasTimeLeft ? "1" : "0") + " " + saver.restTimeDate + " " + saver.lastLeavedDate + " " + (saver.runInBackground ? "1" : "0");
            return val;
        }

        static TimerSaver DeserializeTimeSaver(string value)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream s = new MemoryStream();
                StreamWriter writer = new StreamWriter(s, Encoding.Unicode);

                writer.AutoFlush = true;
                s.Seek(0, SeekOrigin.Begin);
                writer.Write(value);
                s.Seek(0, SeekOrigin.Begin);

                TimerSaver saver = formatter.Deserialize(s) as TimerSaver;
                return saver;
            }
            catch (Exception e)
            {
                try {
                    return DeserializeTimeSaver_Old(value);
                }
                catch(Exception e2)
                {
                    // Temporary logging
                    //MessageBox.Show(e2.ToString());
                    return null;
                }
            }
        }

        static TimerSaver DeserializeTimeSaver_Old(string value)
        {
            string[] args = value.Split(' ');
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
        public static TimerSaver LoadFile()
        {
            string npath = IsServerOS ? Application.LocalUserAppDataPath : Application.CommonAppDataPath;
            string path = npath + "\\" + FILE_NAME;
            if (File.Exists(path))
            {
                string val = File.ReadAllText(path);
                if (val == "")
                    return null;

                val = HCrypter.Decrypt(val, ENCRYPTION_TACTS_COUNT, false);
                TimerSaver s = DeserializeTimeSaver(val);
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
