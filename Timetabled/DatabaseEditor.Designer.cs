
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
            this.SuspendLayout();
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
            this.AddDataSelect.Location = new System.Drawing.Point(12, 284);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(111, 68);
            this.AddDataSelect.TabIndex = 22;
            this.AddDataSelect.SelectedIndexChanged += new System.EventHandler(this.AddDataSelect_SelectedIndexChanged);
            // 
            // DatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.AddDataSelect);
            this.Name = "DatabaseEditor";
            this.Text = "DatabaseManager";
            this.Load += new System.EventHandler(this.DatabaseManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AddDataSelect;
    }
}