namespace MonMon
{
    partial class FunctionList
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
            this._listBoxFunctions = new System.Windows.Forms.ListBox();
            this._buttonRefreshFunctions = new System.Windows.Forms.Button();
            this._filterTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._listBoxFunctions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._buttonRefreshFunctions, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._filterTextBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _listBoxFunctions
            // 
            this._listBoxFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBoxFunctions.FormattingEnabled = true;
            this._listBoxFunctions.Location = new System.Drawing.Point(3, 3);
            this._listBoxFunctions.Name = "_listBoxFunctions";
            this._listBoxFunctions.Size = new System.Drawing.Size(144, 82);
            this._listBoxFunctions.TabIndex = 4;
            this._listBoxFunctions.DoubleClick += new System.EventHandler(this.OnDoubleClickFunction);
            // 
            // _buttonRefreshFunctions
            // 
            this._buttonRefreshFunctions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonRefreshFunctions.Location = new System.Drawing.Point(3, 124);
            this._buttonRefreshFunctions.Name = "_buttonRefreshFunctions";
            this._buttonRefreshFunctions.Size = new System.Drawing.Size(144, 23);
            this._buttonRefreshFunctions.TabIndex = 5;
            this._buttonRefreshFunctions.Text = "Refresh";
            this._buttonRefreshFunctions.UseVisualStyleBackColor = true;
            this._buttonRefreshFunctions.Click += new System.EventHandler(this.OnClickRefresh);
            // 
            // _filterTextBox
            // 
            this._filterTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._filterTextBox.Location = new System.Drawing.Point(3, 103);
            this._filterTextBox.Name = "_filterTextBox";
            this._filterTextBox.Size = new System.Drawing.Size(144, 20);
            this._filterTextBox.TabIndex = 6;
            this._filterTextBox.TextChanged += new System.EventHandler(this.OnFilterTyping);
            // 
            // FunctionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FunctionList";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox _listBoxFunctions;
        private System.Windows.Forms.Button _buttonRefreshFunctions;
        private System.Windows.Forms.TextBox _filterTextBox;

    }
}
