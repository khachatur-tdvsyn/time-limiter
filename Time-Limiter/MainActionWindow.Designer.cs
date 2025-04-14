using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace TimeLimiter
{
    partial class MainActionWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Point mousePoint;
        private bool mousePressed;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mousePressed = true;
            mousePoint = MousePosition;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mousePressed = false;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (GetClosableData() && rejectQuiting)
            {
                MainActionWindow w = new MainActionWindow();
                w.Show();
                w.Activate();
            }
        }
        private bool GetClosableData()
        {
            if (File.Exists(Application.StartupPath + "./allowTime.cnf"))
            {
                string r = File.ReadAllText(Application.StartupPath + "./allowTime.cnf");
                return HCrypter.Decrypt(r, 512);
            }
            else
            {
                FileStream stream = File.Create(Application.StartupPath + "./allowTime.cnf");
                stream.Close();
                stream = File.Create(Application.StartupPath + "./allowFalseTime.cnf");
                stream.Close();
                File.WriteAllText(Application.StartupPath + "./allowTime.cnf", HCrypter.Encrypt(true, 512));
                File.WriteAllText(Application.StartupPath + "./allowFalseTime.cnf", HCrypter.Encrypt(false, 512));
                return false;
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainActionWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.runInBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restNowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.thePanel = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "0:00 / 15:00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "From 2 minutes your device\'s display will be disabled.";
            this.notifyIcon1.BalloonTipTitle = "Time Limiter";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Time Limiter";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.maximizeToolStripMenuItem,
            this.restNowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 76);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // maximizeToolStripMenuItem
            // 
            this.maximizeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.maximizeToolStripMenuItem.Name = "maximizeToolStripMenuItem";
            this.maximizeToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.maximizeToolStripMenuItem.Text = "Maximize";
            this.maximizeToolStripMenuItem.Click += new System.EventHandler(this.maximizeToolStripMenuItem_Click);
            // 
            // restNowToolStripMenuItem
            // 
            this.restNowToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.restNowToolStripMenuItem.Name = "restNowToolStripMenuItem";
            this.restNowToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.restNowToolStripMenuItem.Text = "Rest now";
            this.restNowToolStripMenuItem.Click += new System.EventHandler(this.restNowToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(182, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem2,
            this.runInBackgroundToolStripMenuItem,
            this.restNowToolStripMenuItem1});
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(72, 24);
            this.editToolStripMenuItem1.Text = "Actions";
            // 
            // editToolStripMenuItem2
            // 
            this.editToolStripMenuItem2.Name = "editToolStripMenuItem2";
            this.editToolStripMenuItem2.Size = new System.Drawing.Size(153, 26);
            this.editToolStripMenuItem2.Text = "Edit";
            this.editToolStripMenuItem2.Click += new System.EventHandler(this.editToolStripMenuItem2_Click);
            // 
            // runInBackgroundToolStripMenuItem
            // 
            this.runInBackgroundToolStripMenuItem.Name = "runInBackgroundToolStripMenuItem";
            this.runInBackgroundToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.runInBackgroundToolStripMenuItem.Text = "Minimize";
            this.runInBackgroundToolStripMenuItem.Click += new System.EventHandler(this.runInBackgroundToolStripMenuItem_Click);
            // 
            // restNowToolStripMenuItem1
            // 
            this.restNowToolStripMenuItem1.Name = "restNowToolStripMenuItem1";
            this.restNowToolStripMenuItem1.Size = new System.Drawing.Size(153, 26);
            this.restNowToolStripMenuItem1.Text = "Rest now";
            this.restNowToolStripMenuItem1.Click += new System.EventHandler(this.restNowToolStripMenuItem1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.thePanel);
            this.panel1.Location = new System.Drawing.Point(16, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 27);
            this.panel1.TabIndex = 2;
            // 
            // thePanel
            // 
            this.thePanel.BackColor = System.Drawing.Color.LimeGreen;
            this.thePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.thePanel.Location = new System.Drawing.Point(0, 0);
            this.thePanel.Name = "thePanel";
            this.thePanel.Size = new System.Drawing.Size(152, 27);
            this.thePanel.TabIndex = 0;
            // 
            // MainActionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(182, 103);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainActionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Timer";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem restNowToolStripMenuItem;
        public NotifyIcon notifyIcon1;
        private ToolStripMenuItem editToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem2;
        private ToolStripMenuItem runInBackgroundToolStripMenuItem;
        private ToolStripMenuItem restNowToolStripMenuItem1;
        private ToolStripMenuItem maximizeToolStripMenuItem;
        private Panel panel1;
        private Panel thePanel;

        public int MonitorStatus { get; private set; }
    }
}