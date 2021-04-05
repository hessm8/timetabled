
namespace Timetabled {
    partial class DatabaseEditor {
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
            this.AddDataSelect = new System.Windows.Forms.ListBox();
            this.AddDataText = new System.Windows.Forms.TextBox();
            this.AddDataButton = new System.Windows.Forms.Button();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Teacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // AddDataSelect
            // 
            this.AddDataSelect.FormattingEnabled = true;
            this.AddDataSelect.ItemHeight = 16;
            this.AddDataSelect.Items.AddRange(new object[] {
            "Дисциплина",
            "Преподаватель",
            "Группа",
            "Аудитория"});
            this.AddDataSelect.Location = new System.Drawing.Point(12, 12);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(111, 68);
            this.AddDataSelect.TabIndex = 22;
            // 
            // AddDataText
            // 
            this.AddDataText.Location = new System.Drawing.Point(495, 535);
            this.AddDataText.Name = "AddDataText";
            this.AddDataText.Size = new System.Drawing.Size(267, 22);
            this.AddDataText.TabIndex = 21;
            // 
            // AddDataButton
            // 
            this.AddDataButton.Location = new System.Drawing.Point(495, 576);
            this.AddDataButton.Name = "AddDataButton";
            this.AddDataButton.Size = new System.Drawing.Size(111, 29);
            this.AddDataButton.TabIndex = 20;
            this.AddDataButton.Text = "Добавить";
            this.AddDataButton.UseVisualStyleBackColor = true;
            // 
            // DataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.Subject,
            this.Teacher,
            this.Classroom});
            this.DataGrid.Location = new System.Drawing.Point(339, 499);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.RowHeadersWidth = 51;
            this.DataGrid.RowTemplate.Height = 24;
            this.DataGrid.Size = new System.Drawing.Size(119, 106);
            this.DataGrid.TabIndex = 23;
            this.DataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Group
            // 
            this.Group.HeaderText = "Группа";
            this.Group.MinimumWidth = 110;
            this.Group.Name = "Group";
            this.Group.Width = 110;
            // 
            // Subject
            // 
            this.Subject.HeaderText = "Предмет";
            this.Subject.MinimumWidth = 150;
            this.Subject.Name = "Subject";
            this.Subject.Width = 150;
            // 
            // Teacher
            // 
            this.Teacher.HeaderText = "Преподаватель";
            this.Teacher.MinimumWidth = 140;
            this.Teacher.Name = "Teacher";
            this.Teacher.Width = 140;
            // 
            // Classroom
            // 
            this.Classroom.HeaderText = "Аудитория";
            this.Classroom.MinimumWidth = 100;
            this.Classroom.Name = "Classroom";
            this.Classroom.Width = 125;
            // 
            // DatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.AddDataSelect);
            this.Controls.Add(this.AddDataText);
            this.Controls.Add(this.AddDataButton);
            this.Name = "DatabaseEditor";
            this.Text = "DatabaseManager";
            this.Load += new System.EventHandler(this.DatabaseManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AddDataSelect;
        private System.Windows.Forms.TextBox AddDataText;
        private System.Windows.Forms.Button AddDataButton;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Teacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classroom;
    }
}