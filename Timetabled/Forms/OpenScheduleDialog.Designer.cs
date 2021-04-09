
namespace Timetabled.Forms {
    partial class OpenScheduleDialog {
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
            this.SelectDayOfWeek = new System.Windows.Forms.ComboBox();
            this.SendDate = new System.Windows.Forms.Button();
            this.LabelSelect = new System.Windows.Forms.Label();
            this.shouldOpenBrowser = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SelectDayOfWeek
            // 
            this.SelectDayOfWeek.FormattingEnabled = true;
            this.SelectDayOfWeek.Items.AddRange(new object[] {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"});
            this.SelectDayOfWeek.Location = new System.Drawing.Point(12, 64);
            this.SelectDayOfWeek.Name = "SelectDayOfWeek";
            this.SelectDayOfWeek.Size = new System.Drawing.Size(160, 21);
            this.SelectDayOfWeek.TabIndex = 0;
            // 
            // SendDate
            // 
            this.SendDate.Location = new System.Drawing.Point(12, 127);
            this.SendDate.Name = "SendDate";
            this.SendDate.Size = new System.Drawing.Size(160, 32);
            this.SendDate.TabIndex = 1;
            this.SendDate.Text = "Принять";
            this.SendDate.UseVisualStyleBackColor = true;
            this.SendDate.Click += new System.EventHandler(this.SendOpenRequest);
            // 
            // LabelSelect
            // 
            this.LabelSelect.AutoSize = true;
            this.LabelSelect.Location = new System.Drawing.Point(27, 32);
            this.LabelSelect.Name = "LabelSelect";
            this.LabelSelect.Size = new System.Drawing.Size(123, 13);
            this.LabelSelect.TabIndex = 2;
            this.LabelSelect.Text = "Выберите день недели";
            // 
            // shouldOpenBrowser
            // 
            this.shouldOpenBrowser.AutoSize = true;
            this.shouldOpenBrowser.Checked = true;
            this.shouldOpenBrowser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shouldOpenBrowser.Location = new System.Drawing.Point(12, 104);
            this.shouldOpenBrowser.Name = "shouldOpenBrowser";
            this.shouldOpenBrowser.Size = new System.Drawing.Size(129, 17);
            this.shouldOpenBrowser.TabIndex = 3;
            this.shouldOpenBrowser.Text = "Открыть в браузере";
            this.shouldOpenBrowser.UseVisualStyleBackColor = true;
            // 
            // OpenScheduleDialog
            // 
            this.AcceptButton = this.SendDate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 186);
            this.Controls.Add(this.shouldOpenBrowser);
            this.Controls.Add(this.LabelSelect);
            this.Controls.Add(this.SendDate);
            this.Controls.Add(this.SelectDayOfWeek);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenScheduleDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OpenScheduleDialog_FormClosed);
            this.Load += new System.EventHandler(this.OpenScheduleDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectDayOfWeek;
        private System.Windows.Forms.Button SendDate;
        private System.Windows.Forms.Label LabelSelect;
        private System.Windows.Forms.CheckBox shouldOpenBrowser;
    }
}