
namespace Timetabled.Forms {
    partial class AddGroupForm {
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
            this.FormDesc = new System.Windows.Forms.Label();
            this.AcceptGroup = new System.Windows.Forms.Button();
            this.GroupInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FormDesc
            // 
            this.FormDesc.AutoSize = true;
            this.FormDesc.Location = new System.Drawing.Point(12, 22);
            this.FormDesc.Name = "FormDesc";
            this.FormDesc.Size = new System.Drawing.Size(225, 13);
            this.FormDesc.TabIndex = 0;
            this.FormDesc.Text = "Для продолжения работы добавьте группу";
            // 
            // AcceptGroup
            // 
            this.AcceptGroup.Location = new System.Drawing.Point(66, 87);
            this.AcceptGroup.Name = "AcceptGroup";
            this.AcceptGroup.Size = new System.Drawing.Size(118, 33);
            this.AcceptGroup.TabIndex = 1;
            this.AcceptGroup.Text = "Добавить";
            this.AcceptGroup.UseVisualStyleBackColor = true;
            this.AcceptGroup.Click += new System.EventHandler(this.AcceptGroup_Click);
            // 
            // GroupInput
            // 
            this.GroupInput.Location = new System.Drawing.Point(15, 51);
            this.GroupInput.Name = "GroupInput";
            this.GroupInput.Size = new System.Drawing.Size(222, 20);
            this.GroupInput.TabIndex = 2;
            this.GroupInput.Leave += new System.EventHandler(this.GroupInput_Leave);
            // 
            // AddGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 136);
            this.Controls.Add(this.GroupInput);
            this.Controls.Add(this.AcceptGroup);
            this.Controls.Add(this.FormDesc);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(270, 175);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(270, 175);
            this.Name = "AddGroupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавьте данные";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddGroupForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FormDesc;
        private System.Windows.Forms.Button AcceptGroup;
        private System.Windows.Forms.TextBox GroupInput;
    }
}