using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLimiter
{
    [Serializable]
    public class TimerSaver
    {
        public double timeLeft;
        public bool hasTimeLeft;
        public double restTimeDate, lastLeavedDate;
        public double workTime, restTime;
        public bool runInBackground;
    }
}
