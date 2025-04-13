using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Time_Limiter.Properties;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using Time_Limiter;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Collections.Generic;

namespace TimeLimiter
{
    public partial class MainWindow : Form
    {
        public double timeLeft = 0;
        bool hasTimeItem = false, switched = false;
        TimerSaver s = null;
        public MainWindow()
        {
            hasTimeItem = TimeSaveFiller.LoadFile() != null;
            s = TimeSaveFiller.LoadFile();
            InitializeComponent();
            if (Utils.GetTheme() == -1)
                systemDefaultToolStripMenuItem.Visible = false;
            ToolStripMenuItem i = (ToolStripMenuItem)runProgressBarToolStripMenuItem.DropDownItems[(int)Settings.Default.progressBar];
            i.Checked = true;
            darkToolStripMenuItem.Checked = Settings.Default.theme;
            systemDefaultToolStripMenuItem.Checked = Settings.Default.systemDefault;
            checkBox2.Checked = Settings.Default.isSchoolTime;
            textBox1.Text = Settings.Default.timeProperties;
            if (Settings.Default.systemDefault)
            {
                darkToolStripMenuItem.Enabled = false;
                if (Utils.GetTheme() == 0)
                    ConfigureDarkTheme();
                else
                    ConfigureLightTheme();
            }
            else
            {
                if (Settings.Default.theme)
                    ConfigureDarkTheme();
                else
                    ConfigureLightTheme();
            }
            
            if (hasTimeItem)
            {
                timeLeft = s.timeLeft;
                numericUpDown1.Value = (decimal)s.workTime;
                numericUpDown2.Value = (decimal)s.restTime;
                checkBox1.Checked = s.runInBackground;
            }
            else
            {
                label5.Visible = false;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void SetParameters()
        {
            Settings.Default.isSchoolTime = checkBox2.Checked;
            Settings.Default.timeProperties = textBox1.Text;
            Settings.Default.Save();
            
        }
        private async void button1_Click(object sender, EventArgs e)
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
                timeLeft = TimeSaveFiller.LoadFile() == null ? 0 : TimeSaveFiller.LoadFile().timeLeft,
                hasTimeLeft = false,
                workTime = Convert.ToDouble(numericUpDown1.Value),
                restTime = Convert.ToDouble(numericUpDown2.Value),
                lastLeavedDate = hasTimeItem ? TimeSaveFiller.LoadFile().lastLeavedDate : DateTime.Now.ToOADate(),
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
                if ((DateTime.Now - DateTime.FromOADate(s.lastLeavedDate)).TotalSeconds >= 10 && !switched)
                    SetTime(false);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.theme = darkToolStripMenuItem.Checked;
            if (Settings.Default.theme)
                ConfigureDarkTheme();
            else
                ConfigureLightTheme();
            Settings.Default.Save();
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

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = 0;
            ToolStripItemCollection c = noneToolStripMenuItem.GetCurrentParent().Items;
            foreach(ToolStripItem i in c)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)i;
                item.Checked = item == noneToolStripMenuItem;
            }
            Settings.Default.Save();
        }

        private void runNormallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = 1;
            ToolStripItemCollection c = noneToolStripMenuItem.GetCurrentParent().Items;
            foreach (ToolStripItem i in c)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)i;
                item.Checked = item == runNormallyToolStripMenuItem;
            }
            Settings.Default.Save();
        }

        private void runBackwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.progressBar = 2;
            ToolStripItemCollection c = noneToolStripMenuItem.GetCurrentParent().Items;
            foreach (ToolStripItem i in c)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)i;
                item.Checked = item == runBackwardToolStripMenuItem;
            }
            Settings.Default.Save();
        }
        void OverrideMenu()
        {
            ToolStripItemCollection collection = actionsToolStripMenuItem.DropDownItems;
            bool isDark = Settings.Default.systemDefault ? Utils.GetTheme() == 0 : Settings.Default.theme;
            foreach(ToolStripItem item in collection)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                menuItem.BackColor = isDark ? Color.Black : Color.White;
                menuItem.ForeColor = !isDark ? Color.Black : Color.White;
                ToolStripItemCollection inItems = menuItem.DropDownItems;
                if(inItems.Count > 0)
                {
                    foreach(ToolStripItem item1 in inItems)
                    {
                        ToolStripMenuItem menuItem1 = (ToolStripMenuItem)item1;
                        menuItem1.BackColor = isDark ? Color.Black : Color.White;
                        menuItem1.ForeColor = !isDark ? Color.Black : Color.White;
                    }
                }
            }
        }

        private void systemDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.systemDefault = systemDefaultToolStripMenuItem.Checked;
            darkToolStripMenuItem.Enabled = !Settings.Default.systemDefault;
            if (systemDefaultToolStripMenuItem.Checked)
            {
                if (Utils.GetTheme() == 0)
                    ConfigureDarkTheme();
                else
                    ConfigureLightTheme();
            }
            else
            {
                if (Settings.Default.theme)
                    ConfigureDarkTheme();
                else
                    ConfigureLightTheme();
            }
            Settings.Default.Save();
        }

        private void helpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            byte[] file = Resources.helptext;
            string txt = "";
            for (int i = 0; i < file.Length; i++)
            {
                txt += (char)file[i];
            }
            txt = HCrypter.Decrypt(txt, 2, false);
            MessageBox.Show(txt, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
