namespace MonMon
{
    partial class OpenFilesList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._textBoxFilter = new System.Windows.Forms.TextBox();
            this._fileListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._textBoxFilter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._fileListBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(110, 376);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _textBoxFilter
            // 
            this._textBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textBoxFilter.Location = new System.Drawing.Point(3, 349);
            this._textBoxFilter.Name = "_textBoxFilter";
            this._textBoxFilter.Size = new System.Drawing.Size(104, 20);
            this._textBoxFilter.TabIndex = 0;
            this._textBoxFilter.TextChanged += new System.EventHandler(this.OnFilterTextChanged);
            // 
            // _fileListBox
            // 
            this._fileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fileListBox.FormattingEnabled = true;
            this._fileListBox.Location = new System.Drawing.Point(3, 3);
            this._fileListBox.Name = "_fileListBox";
            this._fileListBox.Size = new System.Drawing.Size(104, 329);
            this._fileListBox.TabIndex = 1;
            this._fileListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnDoubleClickFileList);
            // 
            // OpenFilesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OpenFilesList";
            this.Size = new System.Drawing.Size(110, 376);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox _textBoxFilter;
        private System.Windows.Forms.ListBox _fileListBox;

    }
}
