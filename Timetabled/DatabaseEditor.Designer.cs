
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
            this.AcceptChangesButton = new System.Windows.Forms.Button();
            this.CancelChangesButton = new System.Windows.Forms.Button();
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
            this.AddDataSelect.Location = new System.Drawing.Point(12, 12);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(111, 68);
            this.AddDataSelect.TabIndex = 22;
            this.AddDataSelect.SelectedIndexChanged += new System.EventHandler(this.AddDataSelect_SelectedIndexChanged);
            // 
            // AcceptChangesButton
            // 
            this.AcceptChangesButton.Location = new System.Drawing.Point(12, 86);
            this.AcceptChangesButton.Name = "AcceptChangesButton";
            this.AcceptChangesButton.Size = new System.Drawing.Size(111, 29);
            this.AcceptChangesButton.TabIndex = 23;
            this.AcceptChangesButton.Text = "Применить";
            this.AcceptChangesButton.UseVisualStyleBackColor = true;
            this.AcceptChangesButton.Click += new System.EventHandler(this.AcceptChangesButton_Click);
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Location = new System.Drawing.Point(12, 121);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(111, 29);
            this.CancelChangesButton.TabIndex = 24;
            this.CancelChangesButton.Text = "Отменить";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            this.CancelChangesButton.Click += new System.EventHandler(this.CancelChangesButton_Click);
            // 
            // DatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.CancelChangesButton);
            this.Controls.Add(this.AcceptChangesButton);
            this.Controls.Add(this.AddDataSelect);
            this.Name = "DatabaseEditor";
            this.Text = "DatabaseManager";
            this.Load += new System.EventHandler(this.DatabaseManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AddDataSelect;
        private System.Windows.Forms.Button AcceptChangesButton;
        private System.Windows.Forms.Button CancelChangesButton;
    }
}