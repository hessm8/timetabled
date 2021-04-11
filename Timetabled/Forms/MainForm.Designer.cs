
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
            this.ViewScheduleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заполнитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.SelectDate.Location = new System.Drawing.Point(16, 82);
            this.SelectDate.Margin = new System.Windows.Forms.Padding(7);
            this.SelectDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.SelectDate.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.TabIndex = 24;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewScheduleMenuItem,
            this.базаДанныхToolStripMenuItem,
            this.заполнитьToolStripMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(832, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ViewScheduleMenuItem
            // 
            this.ViewScheduleMenuItem.Name = "ViewScheduleMenuItem";
            this.ViewScheduleMenuItem.Size = new System.Drawing.Size(76, 20);
            this.ViewScheduleMenuItem.Text = "Просмотр";
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.базаДанныхToolStripMenuItem.Text = "База данных";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Настроить";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearToolStripMenuItem.Text = "Очистить";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuItem.Text = "Справка";
            this.HelpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
            // 
            // заполнитьToolStripMenuItem
            // 
            this.заполнитьToolStripMenuItem.Name = "заполнитьToolStripMenuItem";
            this.заполнитьToolStripMenuItem.Size = new System.Drawing.Size(148, 20);
            this.заполнитьToolStripMenuItem.Text = "Случайное расписание";
            this.заполнитьToolStripMenuItem.ToolTipText = "Составляет расписание с случайными данными. Если поля пусты, то они были заняты д" +
    "ругими группами";
            this.заполнитьToolStripMenuItem.Click += new System.EventHandler(this.FillRandomData);
            this.заполнитьToolStripMenuItem.DoubleClick += new System.EventHandler(this.заполнитьToolStripMenuItem_DoubleClick);
            this.заполнитьToolStripMenuItem.MouseHover += new System.EventHandler(this.заполнитьToolStripMenuItem_MouseHover);
            // 
            // TestLoc
            // 
            this.TestLoc.Location = new System.Drawing.Point(253, 50);
            this.TestLoc.Name = "TestLoc";
            this.TestLoc.Size = new System.Drawing.Size(229, 466);
            this.TestLoc.TabIndex = 27;
            this.TestLoc.Text = "button2";
            this.TestLoc.UseVisualStyleBackColor = true;
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Location = new System.Drawing.Point(108, 285);
            this.CancelChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(135, 24);
            this.CancelChangesButton.TabIndex = 30;
            this.CancelChangesButton.Text = "Загрузить данные";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            // 
            // AcceptChangesButton
            // 
            this.AcceptChangesButton.Location = new System.Drawing.Point(108, 253);
            this.AcceptChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.AcceptChangesButton.Name = "AcceptChangesButton";
            this.AcceptChangesButton.Size = new System.Drawing.Size(135, 24);
            this.AcceptChangesButton.TabIndex = 29;
            this.AcceptChangesButton.Text = "Применить изменения";
            this.AcceptChangesButton.UseVisualStyleBackColor = true;
            // 
            // AddDataSelect
            // 
            this.AddDataSelect.FormattingEnabled = true;
            this.AddDataSelect.Items.AddRange(new object[] {
            "Дисциплина",
            "Преподаватель",
            "Аудитория",
            "Группа"});
            this.AddDataSelect.Location = new System.Drawing.Point(16, 253);
            this.AddDataSelect.Margin = new System.Windows.Forms.Padding(2);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(88, 56);
            this.AddDataSelect.TabIndex = 28;
            // 
            // testGrid
            // 
            this.testGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testGrid.Location = new System.Drawing.Point(16, 314);
            this.testGrid.Name = "testGrid";
            this.testGrid.RowHeadersWidth = 51;
            this.testGrid.Size = new System.Drawing.Size(227, 201);
            this.testGrid.TabIndex = 31;
            // 
            // placeholderGroup
            // 
            this.placeholderGroup.FormattingEnabled = true;
            this.placeholderGroup.Location = new System.Drawing.Point(16, 51);
            this.placeholderGroup.Name = "placeholderGroup";
            this.placeholderGroup.Size = new System.Drawing.Size(227, 21);
            this.placeholderGroup.TabIndex = 32;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 530);
            this.Controls.Add(this.placeholderGroup);
            this.Controls.Add(this.testGrid);
            this.Controls.Add(this.CancelChangesButton);
            this.Controls.Add(this.AcceptChangesButton);
            this.Controls.Add(this.AddDataSelect);
            this.Controls.Add(this.TestLoc);
            this.Controls.Add(this.SelectDate);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1476, 569);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 569);
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
        private System.Windows.Forms.Button TestLoc;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.Button AcceptChangesButton;
        private System.Windows.Forms.ListBox AddDataSelect;
        private System.Windows.Forms.DataGridView testGrid;
        private System.Windows.Forms.ComboBox placeholderGroup;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заполнитьToolStripMenuItem;
    }
}

