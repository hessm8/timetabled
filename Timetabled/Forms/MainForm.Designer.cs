
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
            this.SuspendLayout();
            // 
            // DisplayScheduleButton
            // 
            this.DisplayScheduleButton.Location = new System.Drawing.Point(14, 186);
            this.DisplayScheduleButton.Margin = new System.Windows.Forms.Padding(2);
            this.DisplayScheduleButton.Name = "DisplayScheduleButton";
            this.DisplayScheduleButton.Size = new System.Drawing.Size(110, 37);
            this.DisplayScheduleButton.TabIndex = 22;
            this.DisplayScheduleButton.Text = "Открыть расписание";
            this.DisplayScheduleButton.UseVisualStyleBackColor = true;
            this.DisplayScheduleButton.Click += new System.EventHandler(this.DisplayScheduleButton_Click);
            // 
            // SelectDate
            // 
            this.SelectDate.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.SelectDate.Location = new System.Drawing.Point(14, 15);
            this.SelectDate.Margin = new System.Windows.Forms.Padding(7);
            this.SelectDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.SelectDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 186);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 37);
            this.button1.TabIndex = 25;
            this.button1.Text = "Настроить базу данных";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenDatabase);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 652);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SelectDate);
            this.Controls.Add(this.DisplayScheduleButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Timetabled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OnClosed);
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button DisplayScheduleButton;
        private System.Windows.Forms.MonthCalendar SelectDate;
        private System.Windows.Forms.Button button1;
    }
}

