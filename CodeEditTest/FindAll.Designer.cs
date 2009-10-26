namespace MonMon
{
    partial class FindAll
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
            this.buttonFindAll = new System.Windows.Forms.Button();
            this.comboBoxFind = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.buttonFindAll.Location = new System.Drawing.Point(263, 112);
            this.buttonFindAll.Name = "button1";
            this.buttonFindAll.Size = new System.Drawing.Size(75, 23);
            this.buttonFindAll.TabIndex = 0;
            this.buttonFindAll.Text = "Find All";
            this.buttonFindAll.UseVisualStyleBackColor = true;
            this.buttonFindAll.Click += new System.EventHandler(this.OnClickFindAll);
            // 
            // comboBox1
            // 
            this.comboBoxFind.FormattingEnabled = true;
            this.comboBoxFind.Location = new System.Drawing.Point(24, 58);
            this.comboBoxFind.Name = "comboBox1";
            this.comboBoxFind.Size = new System.Drawing.Size(314, 21);
            this.comboBoxFind.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fi&nd What:";
            // 
            // FindAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 147);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFind);
            this.Controls.Add(this.buttonFindAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FindAll";
            this.Text = "FindAll";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFindAll;
        private System.Windows.Forms.ComboBox comboBoxFind;
        private System.Windows.Forms.Label label1;
    }
}