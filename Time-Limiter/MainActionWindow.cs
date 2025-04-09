using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Time_Limiter.Properties;

namespace TimeLimiter
{
    public partial class MainActionWindow : Form
    {
        TimerSaver s;
        bool showedNotification = false, settedSize = false;
        int size = 146;
        private bool rejectQuiting = true;
        public bool isLight;
        public MainActionWindow()
        {
            s = TimeSaveFiller.LoadFile();
            InitializeComponent();
            OverrideNumbers();
            GetClosableData();
            restNowToolStripMenuItem.Enabled = false;
            restNowToolStripMenuItem1.Enabled = false;
            maximizeToolStripMenuItem.Text = s.runInBackground ? "Maximize" : "Minimize";
            isLight = Settings.Default.systemDefault ? MainWindow.GetTheme() != 0 : !Settings.Default.theme;
            ConfigureTheme();
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Size.Width, Screen.PrimaryScreen.WorkingArea.Height - Size.Height);
            if (Settings.Default.progressBar == 0)
            {
                panel1.Visible = false;
                thePanel.Visible = false;
            }
            timer1.Enabled = true;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!CheckIfInSchool())
                OverrideNumbers();
            else
            {
                label1.Text = "During education time the timer stopped";
            }
            if (Settings.Default.systemDefault)
                ConfigureTheme();
        }
        void OverrideNumbers()
        {
            if (s.timeLeft >= s.workTime && !s.hasTimeLeft)
            {
                s.hasTimeLeft = true;
                s.timeLeft = 0;
                s.restTimeDate = DateTime.Now.AddMinutes(s.workTime).ToOADate();
                timer1.Stop();
                timer1.Enabled = false;
                
                TimeSaveFiller.SaveFile(s);
                Application.Restart();
            }
            if (s.workTime - s.timeLeft <= 2 && !showedNotification)
            {
                showedNotification = true;
                notifyIcon1.ShowBalloonTip(1500);
                restNowToolStripMenuItem.Enabled = true;
                restNowToolStripMenuItem1.Enabled = true;

                thePanel.BackColor = Color.OrangeRed;
                if (s.runInBackground)
                    MaximizeMinize();
                Activate();

            }
            if(s.timeLeft > 3)
            {
                editToolStripMenuItem.Enabled = false;
                editToolStripMenuItem2.Enabled = false;
            }
            if (!settedSize)
            {
                size = thePanel.Size.Width;
                settedSize = true;
            }
            string txt = ((int)Math.Floor(s.timeLeft)).ToString("D2") + ":" + ((int)(Math.Floor(s.timeLeft * 60) % 60)).ToString("D2") + " / " + Math.Floor(s.workTime) + ":00";
            if(Settings.Default.progressBar != 0)
            {
                thePanel.Size = new Size(Settings.Default.progressBar == 1 ? (int)Math.Floor(s.timeLeft/s.workTime * size) : (int)Math.Floor((s.workTime - s.timeLeft)/s.workTime * size), thePanel.Size.Height);
            }
            if (!s.runInBackground)
                label1.Text = txt;
            notifyIcon1.Text = "Time Limiter: " + txt;
            s.timeLeft += timer1.Interval / 60000.0;
            if ((DateTime.Now - DateTime.FromOADate(s.lastLeavedDate)).TotalMinutes >= 1)
            {
                s.timeLeft -= (DateTime.Now - DateTime.FromOADate(s.lastLeavedDate)).TotalMinutes;
                s.timeLeft = s.timeLeft < 0 ? 0 : s.timeLeft;
                if(s.workTime - s.timeLeft > 2 && showedNotification)
                {
                    showedNotification = false;
                    restNowToolStripMenuItem.Enabled = false;
                    restNowToolStripMenuItem1.Enabled = false;
                    thePanel.BackColor = Color.LimeGreen;
                }
                s.timeLeft = (DateTime.Now - DateTime.FromOADate(s.lastLeavedDate)).TotalMinutes >= s.restTime ? 0 : s.timeLeft;
                if (s.timeLeft <= 3)
                {
                    editToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem2.Enabled = true;
                }
            }
            s.lastLeavedDate = DateTime.Now.ToOADate();
            TimeSaveFiller.SaveFile(s);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            mousePressed = true;
        }
        public bool CheckIfInSchool()
        {
            if (!Settings.Default.isSchoolTime)
                return false;
            else
            {
                string[] arguments = Settings.Default.timeProperties.Split(' ');
                List<string> values = new List<string>(new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
                for (int i = 0; i < arguments.Length; i++)
                {
                    int index = values.IndexOf(arguments[i]);
                    if (index >= 0)
                    {
                        if ((int)DateTime.Now.DayOfWeek == index)
                        {
                            string[] hourMinute1 = arguments[i + 1].Split(':'), hourMinute2 = arguments[i+2].Split(':');
                            int time1 = int.Parse(hourMinute1[0]) * 60 + int.Parse(hourMinute1[1]), time2 = int.Parse(hourMinute2[0]) * 60 + int.Parse(hourMinute2[1]);
                            if (time1 > time2)
                            {
                                time2 += 24 * 60;
                            }
                            if(time1 < (DateTime.Now.Hour * 60 + DateTime.Now.Minute) && (DateTime.Now.Hour * 60 + DateTime.Now.Minute) < time2)
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        private void restNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.hasTimeLeft = true;
            s.timeLeft = 0;
            s.restTimeDate = DateTime.Now.AddMinutes(s.restTime).ToOADate();
            timer1.Stop();
            timer1.Enabled = false;
            TimeSaveFiller.SaveFile(s);
            Application.Restart();
        }
        
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rejectQuiting = false;
            MainWindow window = new MainWindow();
            window.Visible = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            s.hasTimeLeft = true;
            s.timeLeft = 0;
            s.restTimeDate = DateTime.Now.AddMinutes(s.restTime).ToOADate();
            timer1.Stop();
            timer1.Enabled = false;
            TimeSaveFiller.SaveFile(s);
            Application.Restart();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rejectQuiting = false;
            MainWindow window = new MainWindow();
            window.Visible = true;
            this.Close();
        }

        private void runInBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaximizeMinize();
        }

        private void restNowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            s.hasTimeLeft = true;
            s.timeLeft = 0;
            s.restTimeDate = DateTime.Now.AddMinutes(s.restTime).ToOADate();
            timer1.Stop();
            timer1.Enabled = false;
            TimeSaveFiller.SaveFile(s);
            Application.Restart();
        }
        void MaximizeMinize()
        {
            s.runInBackground = !s.runInBackground;
            string txt = ((int)Math.Floor(s.timeLeft)).ToString("D2") + ":" + ((int)(Math.Floor(s.timeLeft * 60) % 60)).ToString("D2") + " / " + Math.Floor(s.workTime) + ":00";
            label1.Text = txt;
            maximizeToolStripMenuItem.Text = s.runInBackground ? "Maximize" : "Minimize";
            Visible = !s.runInBackground;
            TimeSaveFiller.SaveFile(s);
        }
        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaximizeMinize();
        }
        private void ConfigureTheme()
        {
            bool isDark = Settings.Default.systemDefault ? MainWindow.GetTheme() == 0 : Settings.Default.theme;
            if (isDark && !isLight)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                ToolStripItemCollection items = editToolStripMenuItem1.DropDownItems, contextItems = contextMenuStrip1.Items;
                menuStrip1.BackColor = Color.Black;
                menuStrip1.ForeColor = Color.White;
                foreach(ToolStripItem item in items)
                {
                    ToolStripMenuItem nitem = (ToolStripMenuItem)item;
                    nitem.BackColor = Color.Black;
                    nitem.ForeColor = Color.White;
                }
                foreach (ToolStripItem item in contextItems)
                {
                    ToolStripMenuItem nitem = (ToolStripMenuItem)item;
                    nitem.BackColor = Color.Black;
                    nitem.ForeColor = Color.White;
                }
                isLight = !isLight;
                panel1.BackColor = Color.FromArgb(64, 64, 64);
            }
            else if(!isDark && isLight)
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
                ToolStripItemCollection items = editToolStripMenuItem1.DropDownItems, contextItems = contextMenuStrip1.Items;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                foreach (ToolStripItem item in items)
                {
                    ToolStripMenuItem nitem = (ToolStripMenuItem)item;
                    nitem.BackColor = Color.White;
                    nitem.ForeColor = Color.Black;
                }
                foreach (ToolStripItem item in contextItems)
                {
                    ToolStripMenuItem nitem = (ToolStripMenuItem)item;
                    nitem.BackColor = Color.White;
                    nitem.ForeColor = Color.Black;
                }
                isLight = !isLight;
                panel1.BackColor = Color.FromArgb(128, 128, 128);
            }
        }
    }
}
