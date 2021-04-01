
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Label1 = new System.Windows.Forms.Label();
            this.DisplayScheduleButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddDataButton
            // 
            this.AddDataButton.Location = new System.Drawing.Point(657, 586);
            this.AddDataButton.Name = "AddDataButton";
            this.AddDataButton.Size = new System.Drawing.Size(111, 29);
            this.AddDataButton.TabIndex = 0;
            this.AddDataButton.Text = "Добавить";
            this.AddDataButton.UseVisualStyleBackColor = true;
            this.AddDataButton.Click += new System.EventHandler(this.AddDataButton_Click);
            // 
            // AddDataText
            // 
            this.AddDataText.Location = new System.Drawing.Point(657, 545);
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
            this.SelectData.Location = new System.Drawing.Point(657, 460);
            this.SelectData.Name = "SelectData";
            this.SelectData.Size = new System.Drawing.Size(111, 68);
            this.SelectData.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(64, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 33);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(189, 24);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(189, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBox1);
            this.flowLayoutPanel1.Controls.Add(this.comboBox2);
            this.flowLayoutPanel1.Controls.Add(this.textBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(793, 512);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(195, 93);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(790, 489);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(36, 17);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "DAY";
            // 
            // DisplayScheduleButton
            // 
            this.DisplayScheduleButton.Location = new System.Drawing.Point(760, 683);
            this.DisplayScheduleButton.Name = "DisplayScheduleButton";
            this.DisplayScheduleButton.Size = new System.Drawing.Size(127, 46);
            this.DisplayScheduleButton.TabIndex = 22;
            this.DisplayScheduleButton.Text = "button1";
            this.DisplayScheduleButton.UseVisualStyleBackColor = true;
            this.DisplayScheduleButton.Click += new System.EventHandler(this.DisplayScheduleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 802);
            this.Controls.Add(this.DisplayScheduleButton);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.SelectData);
            this.Controls.Add(this.AddDataText);
            this.Controls.Add(this.AddDataButton);
            this.Name = "MainForm";
            this.Text = "Timetabled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_OnClosed);
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddDataButton;
        private System.Windows.Forms.TextBox AddDataText;
        private System.Windows.Forms.ListBox SelectData;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button DisplayScheduleButton;
    }
}

