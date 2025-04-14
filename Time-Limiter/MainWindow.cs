using System;
using System.Drawing;
using System.Windows.Forms;
using Time_Limiter;
using Time_Limiter.Properties;

namespace TimeLimiter
{
    public partial class MainWindow : Form
    {
        public double timeLeft = 0;
        bool hasTimeItem = false, switched = false;
        TimerSaver currentSaver = null;

        public MainWindow()
        {
            currentSaver = TimeSaveFiller.LoadFile();
            hasTimeItem = currentSaver != null;

            InitializeComponent();
            ConfigureMenu();
            ConfigureTheme(Settings.Default.systemDefault ? Utils.IsSystemDark() : Settings.Default.theme);
            
            if (hasTimeItem)
            {
                timeLeft = currentSaver.timeLeft;
                numericUpDown1.Value = (decimal)currentSaver.workTime;
                numericUpDown2.Value = (decimal)currentSaver.restTime;
                checkBox1.Checked = currentSaver.runInBackground;
            }
            else
            {
                // Make warning text below invisible
                label5.Visible = false;
            }
        }

        private void ConfigureMenu()
        {
            // Hide system default dark mode item if OS doesn't support dark mode
            if (Utils.GetTheme() == -1)
                systemDefaultToolStripMenuItem.Visible = false;

            //Check menu item of the progress bar type
            ToolStripMenuItem i = (ToolStripMenuItem)runProgressBarToolStripMenuItem.DropDownItems[(int)Settings.Default.progressBar];
            i.Checked = true;

            darkToolStripMenuItem.Checked = Settings.Default.theme;
            systemDefaultToolStripMenuItem.Checked = Settings.Default.systemDefault;
            checkBox2.Checked = Settings.Default.isSchoolTime;
            textBox1.Text = Settings.Default.timeProperties;

            if (Settings.Default.systemDefault)
                darkToolStripMenuItem.Enabled = false;
        }

        public void SetParameters()
        {
            Settings.Default.isSchoolTime = checkBox2.Checked;
            Settings.Default.timeProperties = textBox1.Text;
            Settings.Default.Save();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!hasTimeItem)
            {
                DialogResult r = MessageBox.Show("If you setted it at first time, you won't be able to stop it. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    SetTime();
                }
            }
            else
                SetTime();
        }
        void SetTime(bool showMessage = true)
        {
            TimerSaver s = new TimerSaver()
            {
                timeLeft = currentSaver == null ? 0 : currentSaver.timeLeft,
                hasTimeLeft = false,
                workTime = Convert.ToDouble(numericUpDown1.Value),
                restTime = Convert.ToDouble(numericUpDown2.Value),
                lastLeavedDate = hasTimeItem ? currentSaver.lastLeavedDate : DateTime.Now.ToOADate(),
                runInBackground = checkBox1.Checked
            };

            SetParameters();
            timer1.Stop();
            timer1.Enabled = false;

            TimeSaveFiller.SaveFile(s);
            if(showMessage)
                MessageBox.Show("Time setted successfully!", "Setted!");


            switched = true;
            MainActionWindow f = new MainActionWindow();

            Visible = false;
            f.Visible = !s.runInBackground;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hasTimeItem)
            {
                if ((DateTime.Now - DateTime.FromOADate(currentSaver.lastLeavedDate)).TotalSeconds >= 10 && !switched)
                    SetTime(false);
            }
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.theme = darkToolStripMenuItem.Checked;
            ConfigureTheme(Settings.Default.theme);
            Settings.Default.Save();
        }
        private void ConfigureTheme(bool isDark)
        {
            if (isDark)
                ConfigureDarkTheme();
            else 
                ConfigureLightTheme();
        }

        public void ConfigureDarkTheme()
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
            
            numericUpDown1.BackColor = Color.Black;
            numericUpDown1.ForeColor = Color.White;
            numericUpDown2.BackColor = Color.Black;
            numericUpDown2.ForeColor = Color.White;

            button1.BackColor = Color.FromArgb(64, 64, 64);
            button1.ForeColor = Color.White;

            checkBox1.BackColor = Color.Black;
            checkBox1.ForeColor = Color.White;
            checkBox2.BackColor = Color.Black;
            checkBox2.ForeColor = Color.White;

            textBox1.BackColor = Color.Black;
            textBox1.ForeColor = Color.White;

            OverrideMenu();
        }

        private void TurnOnMenuItem(ToolStripMenuItem menuItem)
        {
            ToolStripItemCollection c = menuItem.GetCurrentParent().Items;
            foreach (ToolStripItem i in c)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)i;
                item.Checked = item == menuItem;
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = (int)ProgressBarType.None;
            TurnOnMenuItem(noneToolStripMenuItem);
            Settings.Default.Save();
        }

        private void runNormallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = (int)ProgressBarType.Normal;
            TurnOnMenuItem(runNormallyToolStripMenuItem);
            Settings.Default.Save();
        }

        private void runBackwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = (int)ProgressBarType.Backwards;
            TurnOnMenuItem(runBackwardToolStripMenuItem);
            Settings.Default.Save();
        }
        private void ChangeMenuItemColor(ToolStripMenuItem menuItem, bool isDark)
        {
            menuItem.BackColor = isDark ? Color.Black : Color.White;
            menuItem.ForeColor = !isDark ? Color.Black : Color.White;
        }
        void OverrideMenu()
        {
            ToolStripItemCollection collection = actionsToolStripMenuItem.DropDownItems;
            bool isDark = Settings.Default.systemDefault ? Utils.GetTheme() == 0 : Settings.Default.theme;
            foreach(ToolStripItem item in collection)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                ChangeMenuItemColor(menuItem, isDark);

                ToolStripItemCollection inItems = menuItem.DropDownItems;
                if(inItems.Count > 0)
                {
                    foreach(ToolStripItem item1 in inItems)
                    {
                        ToolStripMenuItem menuItem1 = (ToolStripMenuItem)item1;
                        ChangeMenuItemColor(menuItem1, isDark);
                    }
                }
            }
        }

        private void systemDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.systemDefault = systemDefaultToolStripMenuItem.Checked;
            darkToolStripMenuItem.Enabled = !Settings.Default.systemDefault;
            ConfigureTheme(Settings.Default.systemDefault ? Utils.IsSystemDark() : Settings.Default.theme);
            Settings.Default.Save();
        }

        private void aboutThisAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutBox = new AboutBox1();
            aboutBox.Activate();
            aboutBox.Show();
        }

        public void ConfigureLightTheme()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;

            numericUpDown1.BackColor = Color.White;
            numericUpDown1.ForeColor = Color.Black;
            numericUpDown2.BackColor = Color.White;
            numericUpDown2.ForeColor = Color.Black;

            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;

            checkBox1.BackColor = Color.Transparent;
            checkBox1.ForeColor = Color.Black;
            checkBox2.BackColor = Color.Transparent;
            checkBox2.ForeColor = Color.Black;

            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;

            OverrideMenu();
        }
    }
}
