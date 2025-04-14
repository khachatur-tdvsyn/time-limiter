using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace TimeLimiter
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if(TimeSaveFiller.LoadFile() != null)
            {
                Application.Restart();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runProgressBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runNormallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runBackwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutThisAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.CausesValidation = false;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(49, 327);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Set";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(682, 61);
            this.label1.TabIndex = 2;
            this.label1.Text = "Time Limiter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(598, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Set the time after what it reminds you leave the device and take rest some time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Work Time (min)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Rest Time (min)";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(200, 158);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(437, 27);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.Location = new System.Drawing.Point(200, 191);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(437, 27);
            this.numericUpDown2.TabIndex = 8;
            this.numericUpDown2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 392);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(450, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Please set your parameters quickly because timer starts within 10 seconds.";
            this.label5.UseCompatibleTextRendering = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(200, 343);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(137, 20);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Run in background";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Red;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(682, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runProgressBarToolStripMenuItem,
            this.switchThemeToolStripMenuItem,
            this.aboutThisAppToolStripMenuItem});
            this.actionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // runProgressBarToolStripMenuItem
            // 
            this.runProgressBarToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.runProgressBarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.runNormallyToolStripMenuItem,
            this.runBackwardToolStripMenuItem});
            this.runProgressBarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.runProgressBarToolStripMenuItem.Name = "runProgressBarToolStripMenuItem";
            this.runProgressBarToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.runProgressBarToolStripMenuItem.Text = "Run progress bar";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.noneToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // runNormallyToolStripMenuItem
            // 
            this.runNormallyToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.runNormallyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.runNormallyToolStripMenuItem.Name = "runNormallyToolStripMenuItem";
            this.runNormallyToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.runNormallyToolStripMenuItem.Text = "Run normally";
            this.runNormallyToolStripMenuItem.Click += new System.EventHandler(this.runNormallyToolStripMenuItem_Click);
            // 
            // runBackwardToolStripMenuItem
            // 
            this.runBackwardToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.runBackwardToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.runBackwardToolStripMenuItem.Name = "runBackwardToolStripMenuItem";
            this.runBackwardToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.runBackwardToolStripMenuItem.Text = "Run backward";
            this.runBackwardToolStripMenuItem.Click += new System.EventHandler(this.runBackwardToolStripMenuItem_Click);
            // 
            // switchThemeToolStripMenuItem
            // 
            this.switchThemeToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.switchThemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkToolStripMenuItem,
            this.systemDefaultToolStripMenuItem});
            this.switchThemeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.switchThemeToolStripMenuItem.Name = "switchThemeToolStripMenuItem";
            this.switchThemeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.switchThemeToolStripMenuItem.Text = "Switch theme";
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.darkToolStripMenuItem.CheckOnClick = true;
            this.darkToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.darkToolStripMenuItem.Text = "Is Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // systemDefaultToolStripMenuItem
            // 
            this.systemDefaultToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.systemDefaultToolStripMenuItem.CheckOnClick = true;
            this.systemDefaultToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.systemDefaultToolStripMenuItem.Name = "systemDefaultToolStripMenuItem";
            this.systemDefaultToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.systemDefaultToolStripMenuItem.Text = "System default";
            this.systemDefaultToolStripMenuItem.Click += new System.EventHandler(this.systemDefaultToolStripMenuItem_Click);
            // 
            // aboutThisAppToolStripMenuItem
            // 
            this.aboutThisAppToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.aboutThisAppToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutThisAppToolStripMenuItem.Name = "aboutThisAppToolStripMenuItem";
            this.aboutThisAppToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutThisAppToolStripMenuItem.Text = "About this app";
            this.aboutThisAppToolStripMenuItem.Click += new System.EventHandler(this.aboutThisAppToolStripMenuItem_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(43, 225);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(138, 24);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "Edutaion mode";
            this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(45, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Education times";
            // 
            // textBox1
            // 
            this.textBox1.AccessibleDescription = "";
            this.textBox1.AccessibleName = "";
            this.textBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(200, 261);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(437, 76);
            this.textBox1.TabIndex = 17;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(682, 453);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindow";
            this.Text = "Time Limiter";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label5;
        private CheckBox checkBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem actionsToolStripMenuItem;
        private ToolStripMenuItem aboutThisAppToolStripMenuItem;
        private ToolStripMenuItem switchThemeToolStripMenuItem;
        private ToolStripMenuItem darkToolStripMenuItem;
        private ToolStripMenuItem systemDefaultToolStripMenuItem;
        private ToolStripMenuItem noneToolStripMenuItem;
        private ToolStripMenuItem runNormallyToolStripMenuItem;
        private ToolStripMenuItem runBackwardToolStripMenuItem;
        private ToolStripMenuItem runProgressBarToolStripMenuItem;
        private CheckBox checkBox2;
        private Label label6;
        private TextBox textBox1;
    }
}

