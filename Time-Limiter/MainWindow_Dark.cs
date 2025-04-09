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
    public partial class MainWindow_Dark : Form
    {
        Timer t;
        public MainWindow_Dark()
        {
            InitializeComponent();
            if (TimeSaveFiller.LoadFile() != null)
            {
                MainActionWindow w = new MainActionWindow();
                t = new Timer();
                t.Interval = 1;
                t.Tick += TimerTick;
                t.Start();
            }
        }
        private void TimerTick(object sender, EventArgs args)
        {
            RestWindow w = new RestWindow();
            Visible = false;
            w.Visible = true;
            w.Activate();
            t.Stop();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
