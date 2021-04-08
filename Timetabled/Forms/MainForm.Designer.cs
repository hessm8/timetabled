
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
            this.SelectDate = new System.Windows.Forms.MonthCalendar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.EditDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScheduleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestLoc = new System.Windows.Forms.Button();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.AcceptChangesButton = new System.Windows.Forms.Button();
            this.AddDataSelect = new System.Windows.Forms.ListBox();
            this.testGrid = new System.Windows.Forms.DataGridView();
            this.placeholderGroup = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectDate
            // 
            this.SelectDate.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.SelectDate.Location = new System.Drawing.Point(21, 101);
            this.SelectDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.SelectDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.TabIndex = 24;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditDataMenuItem,
            this.ViewScheduleMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(657, 28);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // EditDataMenuItem
            // 
            this.EditDataMenuItem.Name = "EditDataMenuItem";
            this.EditDataMenuItem.Size = new System.Drawing.Size(187, 24);
            this.EditDataMenuItem.Text = "Настроить базу данных";
            // 
            // ViewScheduleMenuItem
            // 
            this.ViewScheduleMenuItem.Name = "ViewScheduleMenuItem";
            this.ViewScheduleMenuItem.Size = new System.Drawing.Size(168, 24);
            this.ViewScheduleMenuItem.Text = "Открыть расписание";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(81, 24);
            this.HelpMenuItem.Text = "Справка";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // TestLoc
            // 
            this.TestLoc.Location = new System.Drawing.Point(337, 62);
            this.TestLoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestLoc.Name = "TestLoc";
            this.TestLoc.Size = new System.Drawing.Size(305, 574);
            this.TestLoc.TabIndex = 27;
            this.TestLoc.Text = "button2";
            this.TestLoc.UseVisualStyleBackColor = true;
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Location = new System.Drawing.Point(144, 351);
            this.CancelChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(180, 30);
            this.CancelChangesButton.TabIndex = 30;
            this.CancelChangesButton.Text = "Отменить";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            // 
            // AcceptChangesButton
            // 
            this.AcceptChangesButton.Location = new System.Drawing.Point(144, 311);
            this.AcceptChangesButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AcceptChangesButton.Name = "AcceptChangesButton";
            this.AcceptChangesButton.Size = new System.Drawing.Size(180, 30);
            this.AcceptChangesButton.TabIndex = 29;
            this.AcceptChangesButton.Text = "Применить";
            this.AcceptChangesButton.UseVisualStyleBackColor = true;
            // 
            // AddDataSelect
            // 
            this.AddDataSelect.FormattingEnabled = true;
            this.AddDataSelect.ItemHeight = 16;
            this.AddDataSelect.Items.AddRange(new object[] {
            "Дисциплина",
            "Преподаватель",
            "Аудитория",
            "Группа"});
            this.AddDataSelect.Location = new System.Drawing.Point(21, 311);
            this.AddDataSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(116, 68);
            this.AddDataSelect.TabIndex = 28;
            // 
            // testGrid
            // 
            this.testGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testGrid.Location = new System.Drawing.Point(21, 386);
            this.testGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.testGrid.Name = "testGrid";
            this.testGrid.RowHeadersWidth = 51;
            this.testGrid.Size = new System.Drawing.Size(303, 247);
            this.testGrid.TabIndex = 31;
            // 
            // placeholderGroup
            // 
            this.placeholderGroup.FormattingEnabled = true;
            this.placeholderGroup.Location = new System.Drawing.Point(21, 63);
            this.placeholderGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.placeholderGroup.Name = "placeholderGroup";
            this.placeholderGroup.Size = new System.Drawing.Size(301, 24);
            this.placeholderGroup.TabIndex = 32;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 644);
            this.Controls.Add(this.placeholderGroup);
            this.Controls.Add(this.testGrid);
            this.Controls.Add(this.CancelChangesButton);
            this.Controls.Add(this.AcceptChangesButton);
            this.Controls.Add(this.AddDataSelect);
            this.Controls.Add(this.TestLoc);
            this.Controls.Add(this.SelectDate);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1963, 691);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(621, 691);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timetabled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OnClosed);
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MonthCalendar SelectDate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewScheduleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditDataMenuItem;
        private System.Windows.Forms.Button TestLoc;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.Button AcceptChangesButton;
        private System.Windows.Forms.ListBox AddDataSelect;
        private System.Windows.Forms.DataGridView testGrid;
        private System.Windows.Forms.ComboBox placeholderGroup;
    }
}

