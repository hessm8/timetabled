
namespace Timetabled.Forms {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DisplayScheduleButton = new System.Windows.Forms.Button();
            this.SelectDate = new System.Windows.Forms.MonthCalendar();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.EditDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScheduleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestLoc = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayScheduleButton
            // 
            this.DisplayScheduleButton.Location = new System.Drawing.Point(16, 222);
            this.DisplayScheduleButton.Margin = new System.Windows.Forms.Padding(2);
            this.DisplayScheduleButton.Name = "DisplayScheduleButton";
            this.DisplayScheduleButton.Size = new System.Drawing.Size(110, 37);
            this.DisplayScheduleButton.TabIndex = 22;
            this.DisplayScheduleButton.Text = "Открыть расписание";
            this.DisplayScheduleButton.UseVisualStyleBackColor = true;
            this.DisplayScheduleButton.Click += new System.EventHandler(this.ViewSchedule);
            // 
            // SelectDate
            // 
            this.SelectDate.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.SelectDate.Location = new System.Drawing.Point(16, 51);
            this.SelectDate.Margin = new System.Windows.Forms.Padding(7);
            this.SelectDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.SelectDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 222);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 37);
            this.button1.TabIndex = 25;
            this.button1.Text = "Настроить базу данных";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenDatabase);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditDataMenuItem,
            this.ViewScheduleMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(778, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // EditDataMenuItem
            // 
            this.EditDataMenuItem.Name = "EditDataMenuItem";
            this.EditDataMenuItem.Size = new System.Drawing.Size(77, 20);
            this.EditDataMenuItem.Text = "Настроить";
            // 
            // ViewScheduleMenuItem
            // 
            this.ViewScheduleMenuItem.Name = "ViewScheduleMenuItem";
            this.ViewScheduleMenuItem.Size = new System.Drawing.Size(134, 20);
            this.ViewScheduleMenuItem.Text = "Открыть расписание";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuItem.Text = "Справка";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // TestLoc
            // 
            this.TestLoc.Location = new System.Drawing.Point(253, 51);
            this.TestLoc.Name = "TestLoc";
            this.TestLoc.Size = new System.Drawing.Size(75, 23);
            this.TestLoc.TabIndex = 27;
            this.TestLoc.Text = "button2";
            this.TestLoc.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 652);
            this.Controls.Add(this.TestLoc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SelectDate);
            this.Controls.Add(this.DisplayScheduleButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Timetabled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OnClosed);
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DisplayScheduleButton;
        private System.Windows.Forms.MonthCalendar SelectDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewScheduleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditDataMenuItem;
        private System.Windows.Forms.Button TestLoc;
    }
}

