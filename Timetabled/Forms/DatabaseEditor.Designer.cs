﻿
namespace Timetabled.Forms {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseEditor));
            this.AddDataSelect = new System.Windows.Forms.ListBox();
            this.AcceptChangesButton = new System.Windows.Forms.Button();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddDataSelect
            // 
            this.AddDataSelect.FormattingEnabled = true;
            this.AddDataSelect.Items.AddRange(new object[] {
            "Дисциплина",
            "Преподаватель",
            "Аудитория",
            "Группа"});
            this.AddDataSelect.Location = new System.Drawing.Point(9, 10);
            this.AddDataSelect.Margin = new System.Windows.Forms.Padding(2);
            this.AddDataSelect.Name = "AddDataSelect";
            this.AddDataSelect.Size = new System.Drawing.Size(84, 56);
            this.AddDataSelect.TabIndex = 22;
            this.AddDataSelect.SelectedIndexChanged += new System.EventHandler(this.AddDataSelect_SelectedIndexChanged);
            // 
            // AcceptChangesButton
            // 
            this.AcceptChangesButton.Location = new System.Drawing.Point(9, 70);
            this.AcceptChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.AcceptChangesButton.Name = "AcceptChangesButton";
            this.AcceptChangesButton.Size = new System.Drawing.Size(83, 24);
            this.AcceptChangesButton.TabIndex = 23;
            this.AcceptChangesButton.Text = "Применить";
            this.AcceptChangesButton.UseVisualStyleBackColor = true;
            this.AcceptChangesButton.Click += new System.EventHandler(this.AcceptChangesButton_Click);
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.Location = new System.Drawing.Point(9, 98);
            this.CancelChangesButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(83, 24);
            this.CancelChangesButton.TabIndex = 24;
            this.CancelChangesButton.Text = "Отменить";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            this.CancelChangesButton.Click += new System.EventHandler(this.CancelChangesButton_Click);
            // 
            // DatabaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 217);
            this.Controls.Add(this.CancelChangesButton);
            this.Controls.Add(this.AcceptChangesButton);
            this.Controls.Add(this.AddDataSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DatabaseEditor";
            this.Opacity = 0.95D;
            this.Text = "Data Editor";
            this.Load += new System.EventHandler(this.DatabaseManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AddDataSelect;
        private System.Windows.Forms.Button AcceptChangesButton;
        private System.Windows.Forms.Button CancelChangesButton;
    }
}