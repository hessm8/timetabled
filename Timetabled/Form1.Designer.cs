
namespace Timetabled {
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
            this.AddDataButton = new System.Windows.Forms.Button();
            this.AddDataText = new System.Windows.Forms.TextBox();
            this.SelectData = new System.Windows.Forms.ListBox();
            this.DisplayScheduleButton = new System.Windows.Forms.Button();
            this.SelectDate = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // AddDataButton
            // 
            this.AddDataButton.Location = new System.Drawing.Point(12, 142);
            this.AddDataButton.Name = "AddDataButton";
            this.AddDataButton.Size = new System.Drawing.Size(111, 29);
            this.AddDataButton.TabIndex = 0;
            this.AddDataButton.Text = "Добавить";
            this.AddDataButton.UseVisualStyleBackColor = true;
            this.AddDataButton.Click += new System.EventHandler(this.AddDataButton_Click);
            // 
            // AddDataText
            // 
            this.AddDataText.Location = new System.Drawing.Point(12, 101);
            this.AddDataText.Name = "AddDataText";
            this.AddDataText.Size = new System.Drawing.Size(111, 22);
            this.AddDataText.TabIndex = 5;
            // 
            // SelectData
            // 
            this.SelectData.FormattingEnabled = true;
            this.SelectData.ItemHeight = 16;
            this.SelectData.Items.AddRange(new object[] {
            "Дисциплина",
            "Преподаватель",
            "Группа",
            "Аудитория"});
            this.SelectData.Location = new System.Drawing.Point(12, 16);
            this.SelectData.Name = "SelectData";
            this.SelectData.Size = new System.Drawing.Size(111, 68);
            this.SelectData.TabIndex = 19;
            // 
            // DisplayScheduleButton
            // 
            this.DisplayScheduleButton.Location = new System.Drawing.Point(12, 197);
            this.DisplayScheduleButton.Name = "DisplayScheduleButton";
            this.DisplayScheduleButton.Size = new System.Drawing.Size(111, 46);
            this.DisplayScheduleButton.TabIndex = 22;
            this.DisplayScheduleButton.Text = "Открыть расписание";
            this.DisplayScheduleButton.UseVisualStyleBackColor = true;
            this.DisplayScheduleButton.Click += new System.EventHandler(this.DisplayScheduleButton_Click);
            // 
            // SelectDate
            // 
            this.SelectDate.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.SelectDate.Location = new System.Drawing.Point(18, 271);
            this.SelectDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.SelectDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.TabIndex = 24;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 802);
            this.Controls.Add(this.SelectDate);
            this.Controls.Add(this.DisplayScheduleButton);
            this.Controls.Add(this.SelectData);
            this.Controls.Add(this.AddDataText);
            this.Controls.Add(this.AddDataButton);
            this.Name = "MainForm";
            this.Text = "Timetabled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OnClosed);
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddDataButton;
        private System.Windows.Forms.TextBox AddDataText;
        private System.Windows.Forms.ListBox SelectData;
        private System.Windows.Forms.Button DisplayScheduleButton;
        private System.Windows.Forms.MonthCalendar SelectDate;
    }
}

