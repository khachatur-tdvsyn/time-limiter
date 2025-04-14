using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLimiter
{
    public partial class RestWindow : Form
    {
        private TimerSaver s;
        public RestWindow()
        {
            s = TimeSaveFiller.LoadFile();
            InitializeComponent();
            Location = new Point(0, 0);
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            OverrideNumbers();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OverrideNumbers();
        }
        private void OverrideNumbers()
        {
            // Check if rest time expired and this window can go down
            if (s.restTimeDate <= DateTime.Now.ToOADate() && s.hasTimeLeft)
            {
                s.hasTimeLeft = false;
                s.timeLeft = 0;
                TimeSaveFiller.SaveFile(s);
                Application.Restart();
            }
            double seconds = (DateTime.FromOADate(s.restTimeDate) - DateTime.Now).TotalSeconds;
            label2.Text = Math.Floor(seconds / 60) + " minutes and " + ((int)(Math.Floor(seconds)%60)).ToString("D2")+" seconds left for use device again.";
        }
    }
}
