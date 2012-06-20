namespace TCAPP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtTimeFile = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnProcessFile = new System.Windows.Forms.Button();
            this.btnPrintWk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxIds = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtTimeFile
            // 
            this.txtTimeFile.Location = new System.Drawing.Point(13, 13);
            this.txtTimeFile.Name = "txtTimeFile";
            this.txtTimeFile.Size = new System.Drawing.Size(321, 20);
            this.txtTimeFile.TabIndex = 0;
            this.txtTimeFile.Text = "C:\\Users\\ssadeghi\\Desktop\\ALOG_001.TXT";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(340, 13);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "select file";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(13, 68);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(599, 276);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.WordWrap = false;
            // 
            // btnProcessFile
            // 
            this.btnProcessFile.Location = new System.Drawing.Point(13, 40);
            this.btnProcessFile.Name = "btnProcessFile";
            this.btnProcessFile.Size = new System.Drawing.Size(75, 23);
            this.btnProcessFile.TabIndex = 3;
            this.btnProcessFile.Text = "process file";
            this.btnProcessFile.UseVisualStyleBackColor = true;
            this.btnProcessFile.Click += new System.EventHandler(this.btnProcessFile_Click);
            // 
            // btnPrintWk
            // 
            this.btnPrintWk.Location = new System.Drawing.Point(340, 42);
            this.btnPrintWk.Name = "btnPrintWk";
            this.btnPrintWk.Size = new System.Drawing.Size(75, 23);
            this.btnPrintWk.TabIndex = 4;
            this.btnPrintWk.Text = "print";
            this.btnPrintWk.UseVisualStyleBackColor = true;
            this.btnPrintWk.Click += new System.EventHandler(this.btnPrintWk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Print for Id:";
            // 
            // cbxIds
            // 
            this.cbxIds.FormattingEnabled = true;
            this.cbxIds.Location = new System.Drawing.Point(287, 44);
            this.cbxIds.Name = "cbxIds";
            this.cbxIds.Size = new System.Drawing.Size(47, 21);
            this.cbxIds.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 356);
            this.Controls.Add(this.cbxIds);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrintWk);
            this.Controls.Add(this.btnProcessFile);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtTimeFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtTimeFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnProcessFile;
        private System.Windows.Forms.Button btnPrintWk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxIds;
    }
}

